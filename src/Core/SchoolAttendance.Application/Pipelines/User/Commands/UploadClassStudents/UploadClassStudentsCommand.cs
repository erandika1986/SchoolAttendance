using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Constants;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.User.Commands.UploadClassStudents
{
    public class UploadClassStudentsCommand : IRequest<MasterDataUploadResponse>
    {
        public FileContainerViewModel Container { get; set; }
    }

    public class UploadClassStudentsCommandHandler : IRequestHandler<UploadClassStudentsCommand, MasterDataUploadResponse>
    {
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IStudentClassCommandRepository _studentClassCommandRepository;
        private readonly IAcademicYearQueryRepository _academicYearQueryRepository;
        private readonly IClassQueryRepository _classQueryRepository;
        private readonly IGradeQueryRepository _gradeQueryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UploadClassStudentsCommandHandler> _logger;
        private StudentExcelContainer studentExcelContainer;

        public UploadClassStudentsCommandHandler(
            IUserCommandRepository userCommandRepository,
            IUserQueryRepository userQueryRepository,
            IStudentClassCommandRepository studentClassCommandRepository,
            IAcademicYearQueryRepository academicYearQueryRepository,
            IClassQueryRepository classQueryRepository,
            IGradeQueryRepository gradeQueryRepository,
            ICurrentUserService currentUserService, 
            IConfiguration configuration, 
            ILogger<UploadClassStudentsCommandHandler> logger)
        {
            this._userCommandRepository = userCommandRepository;
            this._userQueryRepository = userQueryRepository;
            this._studentClassCommandRepository = studentClassCommandRepository;
            this._academicYearQueryRepository = academicYearQueryRepository;
            this._gradeQueryRepository = gradeQueryRepository;
            this._classQueryRepository = classQueryRepository;
            this._currentUserService = currentUserService;
            this._configuration = configuration;
            this._logger = logger;
            this.studentExcelContainer = new StudentExcelContainer();
        }
        public async Task<MasterDataUploadResponse> Handle(UploadClassStudentsCommand request, CancellationToken cancellationToken)
        {
            var response = new MasterDataUploadResponse();

            response.IsSucccess = true;

            try
            {
                //var curentUser = coreDataService.GetLoggedInUserByUserName(userName);
                var folderPath = _configuration.GetSection("FileUploadPath").Value;

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var upLoadedFiles = new Dictionary<string, string>();

                foreach (var item in request.Container.Files)
                {
                    var filePath = string.Format(@"{0}\{1}", folderPath, item.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }

                    upLoadedFiles.Add(filePath, item.FileName);
                }

                foreach (var item in upLoadedFiles)
                {
                    studentExcelContainer = new StudentExcelContainer();
                    try
                    {
                        var validateResult = await ValidateExcelFileContents(item.Key, cancellationToken);

                        response.Results.AddRange(validateResult);

                        var validationErrorCount = response.Results.Where(x => x.IsSuccess == false).Count();

                        if (response.IsSucccess == true && validationErrorCount > 0)
                        {
                            response.IsSucccess = false;
                        }
                        else
                        {
                            foreach (var student in studentExcelContainer.Students)
                            {
                                var studentRecord = (await _userQueryRepository.Query(x => x.Username.Trim().ToLower() == student.Username.Trim().ToLower()))
                                    .FirstOrDefault();

                                if (studentRecord == null)
                                {
                                    studentRecord = new Domain.Entities.User()
                                    {
                                        Gender = student.Gender,
                                        FullName = student.FullName,
                                        IsActive = true,
                                        TimeZoneId = Constants.SRI_LANKA_TIME_ZONE_ID,
                                        Password = BCrypt.Net.BCrypt.HashPassword(student.Username),
                                        Username = student.Username,
                                        ContactNo = string.Empty,
                                        Email = string.Empty,

                                    };

                                    studentRecord.UserRoles.Add(new UserRole()
                                    {
                                        RoleId = (int)SystemRole.Student,
                                        AssignedOn = DateTime.UtcNow

                                    });

                                    studentRecord.StudentClasses.Add(new StudentClass()
                                    {
                                        ClassId = studentExcelContainer.ClassId,
                                        AssignedDate = DateTime.UtcNow,
                                        IsActive = true
                                    });

                                    await _userCommandRepository.AddAsync(studentRecord, cancellationToken);
                                }
                                else
                                {
                                    studentRecord.Gender = student.Gender;
                                    studentRecord.FullName = student.FullName;

                                    var studentClass = studentRecord.StudentClasses.FirstOrDefault(x => x.IsActive == true);
                                    if (studentClass.ClassId != studentExcelContainer.ClassId)
                                    {

                                        studentClass.IsActive = false;
                                        studentClass.RemovedDate = DateTime.UtcNow;

                                        await _studentClassCommandRepository.UpdateAsync(studentClass, cancellationToken);

                                        studentRecord.StudentClasses.Add(new StudentClass()
                                        {
                                            ClassId = studentExcelContainer.ClassId,
                                            AssignedDate = DateTime.UtcNow,
                                            IsActive = true
                                        });
                                    }

                                    await _userCommandRepository.UpdateAsync(studentRecord, cancellationToken);

                                }
                            }

                            response.Results.Add(new MasterDataFileValidateResult()
                            {
                                IsSuccess = true,
                                ValidateMessage = $"Class student list has been uploaded successfully for file name {item.Value}."
                            });
                        }

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.ToString());
                        response.IsSucccess = false;
                        response.Results.Add(new MasterDataFileValidateResult() { IsSuccess = false, ValidateMessage = $"Exception has been occured while processing the class student import using file name {item.Value}." });
                    }
                }

                var errorCount = response.Results.Where(x => x.IsSuccess == false).Count();

                if (response.IsSucccess == true && errorCount == 0)
                {
                    response.Results.Add(new MasterDataFileValidateResult() { IsSuccess = true, ValidateMessage = "All file has been uploaded and process successfully." });
                }
            }
            catch (Exception ex1)
            {
                _logger.LogError(ex1.ToString());
                response.IsSucccess = false;
                response.Results.Add(
                    new MasterDataFileValidateResult()
                    {
                        IsSuccess = false,
                        ValidateMessage = "An exception has been occured while processing the excel file."
                    });

            }
            return response;
        }

        private async Task<List<MasterDataFileValidateResult>> ValidateExcelFileContents(string fileSavePath, CancellationToken cancellationToken)
        {
            var response = new List<MasterDataFileValidateResult>();

            try
            {
                var academicYears = await _academicYearQueryRepository.GetAll(cancellationToken);
                var grades = await _gradeQueryRepository.GetAll(cancellationToken);

                FileInfo fileInfo = new FileInfo(fileSavePath);
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    //get the first worksheet in the workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["ClassStudents"];

                    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    int rowCount = worksheet.Dimension.End.Row - 5;

                    var yearValue = worksheet.Cells[1, 2].Value.ToString().Trim();

                    if (!string.IsNullOrEmpty(yearValue))
                    {
                        int enteredYear;

                        if (int.TryParse(yearValue, out enteredYear))
                        {
                            var currentYear = DateTime.Now.Year;
                            if (!(enteredYear == currentYear || enteredYear == currentYear + 1))
                            {
                                response.Add(new MasterDataFileValidateResult() 
                                { 
                                    ValidateMessage = "Invalid user excel template. Year can be current year or next year only.", 
                                    IsSuccess = false 
                                });
                            }
                            else
                            {
                                studentExcelContainer.Year = academicYears.FirstOrDefault(x => x.Name == yearValue).Id;
                            }
                        }
                        else
                        {
                            response.Add(new MasterDataFileValidateResult() 
                            { 
                                ValidateMessage = "Invalid user excel template. Invalid Year format has been entered.", 
                                IsSuccess = false 
                            });
                        }
                    }
                    else
                    {
                        response.Add(new MasterDataFileValidateResult() 
                        { 
                            ValidateMessage = "Invalid user excel template. Academic year is not entered.", 
                            IsSuccess = false 
                        });
                    }

                    var gradeValue = worksheet.Cells[2, 2].Value.ToString().Trim().ToLower();

                    var enteredGrade = grades.FirstOrDefault(x => x.Name.Trim().ToLower() == gradeValue);
                    if (enteredGrade != null)
                    {
                        studentExcelContainer.GradeId = enteredGrade.Id;
                    }
                    else
                    {
                        response.Add(new MasterDataFileValidateResult() { ValidateMessage = "Invalid user excel template. Grade value does not exists or invalid grade value has been entered.", IsSuccess = false });
                    }

                    if (studentExcelContainer.Year > 0 && studentExcelContainer.GradeId > 0)
                    {
                        var classValue = worksheet.Cells[3, 2].Value.ToString().Trim().ToLower();

                        var enteredClass = (
                            await _classQueryRepository
                                .Query(x => x.Name.Trim().ToLower() == classValue && x.GradeId == studentExcelContainer.GradeId && x.AcademicYear == studentExcelContainer.Year))
                                .FirstOrDefault(); 

                        if (enteredClass != null)
                        {
                            studentExcelContainer.ClassId = enteredClass.Id;
                        }
                        else
                        {
                            response.Add(new MasterDataFileValidateResult() { ValidateMessage = "Invalid user excel template. Class name value does not exists or invalid class name value has been entered.", IsSuccess = false });
                        }
                    }

                    var teacherValue = worksheet.Cells[4, 2].Value.ToString().Trim().ToLower();

                    var enterTeacher = await _userQueryRepository.GetUserByUsername(teacherValue, cancellationToken);

                    if (enterTeacher != null)
                    {
                        studentExcelContainer.ClassTeacherId = enterTeacher.Id;
                    }
                    else
                    {
                        response.Add(new MasterDataFileValidateResult() 
                        { 
                            ValidateMessage = "Invalid user excel template. Teacher username does not exists or invalid teacher username has been entered.", 
                            IsSuccess = false 
                        });
                    }

                    if (colCount != 3)
                    {
                        response.Add(new MasterDataFileValidateResult() 
                        { 
                            ValidateMessage = "Invalid user excel template. Column count does not match.", 
                            IsSuccess = false 
                        });
                    }
                    else
                    {
                        response.Add(new MasterDataFileValidateResult() 
                        { 
                            ValidateMessage = "Uploaded user excel template valid.", 
                            IsSuccess = true 
                        });
                    }

                    for (int row = 6; row <= rowCount; row++)
                    {
                        var user = new UserViewModel();

                        for (int col = 1; col <= colCount; col++)
                        {
                            if (row == 6)
                            {
                                if (col == 1)
                                {
                                    if (worksheet.Cells[row, col].Value.ToString().Trim() != "IndexNo")
                                    {
                                        response.Add(new MasterDataFileValidateResult() { ValidateMessage = "Invalid excel file. IndexNo column (Column index 0) is missing.", IsSuccess = false });

                                    }

                                }
                                else if (col == 2)
                                {
                                    if (worksheet.Cells[row, col].Value.ToString().Trim() != "Full Name")
                                    {
                                        response.Add(new MasterDataFileValidateResult() { ValidateMessage = "Invalid excel file. Full Name column (Column index 1) is missing.", IsSuccess = false });

                                    }
                                }
                                else if (col == 3)
                                {
                                    if (worksheet.Cells[row, col].Value.ToString().Trim() != "Gender")
                                    {
                                        response.Add(new MasterDataFileValidateResult() { ValidateMessage = "Invalid excel file. Gender column (Column index 2) is missing.", IsSuccess = false });

                                    }
                                }

                            }
                            else
                            {


                                if (col == 1)
                                {
                                    var indexNo = worksheet.Cells[row, col].Value == null ? string.Empty : worksheet.Cells[row, col].Value.ToString().Trim();
                                    if (!string.IsNullOrEmpty(indexNo))
                                    {
                                        user.Username = indexNo;
                                    }
                                    else
                                    {
                                        response.Add(new MasterDataFileValidateResult() { ValidateMessage = "Empty index number found in row " + row.ToString() + ".", IsSuccess = false });
                                    }
                                }
                                else if (col == 2)
                                {
                                    var fullName = worksheet.Cells[row, col].Value == null ? string.Empty : worksheet.Cells[row, col].Value.ToString().Trim();
                                    if (!string.IsNullOrEmpty(fullName))
                                    {
                                        user.FullName = fullName;
                                    }
                                    else
                                    {
                                        response.Add(new MasterDataFileValidateResult() { ValidateMessage = "Full name can not be empty in row " + row.ToString() + ".", IsSuccess = false });
                                    }

                                }
                                else if (col == 3)
                                {
                                    var gender = worksheet.Cells[row, col].Value == null ? string.Empty : worksheet.Cells[row, col].Value.ToString().Trim();
                                    if (!string.IsNullOrEmpty(gender))
                                    {

                                        user.Gender = gender;
                                    }
                                    else
                                    {
                                        response.Add(new MasterDataFileValidateResult() { ValidateMessage = "Gender can not be empty in row " + row.ToString() + ".", IsSuccess = false });
                                    }
                                }

                            }

                        }

                        if (row > 6)
                            studentExcelContainer.Students.Add(user);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }


    }
}
