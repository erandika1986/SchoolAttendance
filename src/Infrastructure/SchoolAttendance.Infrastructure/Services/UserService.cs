using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Constants;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Infrastructure.Services
{
  public class UserService : IUserService
  {
    //private readonly ISchoolAttendanceContext db;
    //private readonly ILogger<IDropDownService> logger;
    //private readonly IConfiguration config;
    //private readonly ICoreDataService coreDataService;
    //private readonly IDropDownService dropDownService;

    //private StudentExcelContainer studentExcelContainer;
    //public UserService(ISchoolAttendanceContext db, ILogger<DropDownService> logger, ICoreDataService coreDataService, IConfiguration config,IDropDownService dropDownService)
    //{
    //  this.db = db;
    //  this.logger = logger;
    //  this.coreDataService = coreDataService;
    //  this.config = config;
    //  this.dropDownService = dropDownService;

    //  studentExcelContainer = new StudentExcelContainer();
    //}


    //public PaginatedItemsViewModel<UserViewModel> GetUsersList(string searchText, int currentPage, int pageSize)
    //{
    //  int totalRecordCount = 0;
    //  double totalPages = 0;
    //  int totalPageCount = 0;
    //  var vms = new List<UserViewModel>();
    //  int userRoleId = 0;
    //  var studentRoleId = (int)SystemRole.Student;

    //  var users = db.Users
    //    .Where(x => !x.UserRoles.Any(r=>r.RoleId == studentRoleId) && x.UserRoles.Count()>0 && x.IsActive == true)
    //    .OrderBy(u => u.FullName);

    //  if(userRoleId>0)
    //  {
    //    users = users
    //    .Where(x => x.UserRoles.Any(r => r.RoleId == userRoleId) && x.IsActive == true)
    //    .OrderBy(u => u.FullName);
    //  }

    //  if(!string.IsNullOrEmpty(searchText))
    //  {
    //    users = users.Where(x => x.FullName.Contains(searchText)).OrderBy(u => u.FullName);
    //  }

    //  totalRecordCount = users.Count();
    //  totalPages = (double)totalRecordCount / pageSize;
    //  totalPageCount = (int)Math.Ceiling(totalPages);

    //  var userList = users.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

    //  userList.ForEach(u =>
    //  {
    //    vms.Add(u.ToVm());

    //  });

    //  var container = new PaginatedItemsViewModel<UserViewModel>(currentPage, pageSize, totalPageCount, totalRecordCount, vms);

    //  return container;
    //}

    //public UserViewModel GetUserById(int id)
    //{
    //  var response = new UserViewModel();

    //  var user = db.Users.FirstOrDefault(x => x.Id == id);

    //  var allSubject = db.Subjects.OrderBy(x => x.Name).ToList();

    //  response = user.ToVm();

    //  return response;

      
    //}

    //public async Task<ResponseViewModel> SaveUser(UserViewModel vm,string userName)
    //{
    //  var response = new ResponseViewModel();

    //  try
    //  {
    //    var user = db.Users.FirstOrDefault(x => x.Id == vm.Id);

    //    if(user==null)
    //    {
    //      var exisitngUserName = db.Users.FirstOrDefault(x => x.Username.Trim().ToLower() == vm.Username.Trim().ToLower());

    //      if(exisitngUserName!=null)
    //      {
    //        response.Message = "Provided username already taken. Please try with new username.";
    //        response.IsSuccess = false;

    //        return response;
    //      }

    //      user = new User()
    //      {
    //        FullName = vm.FullName,
    //        IsActive = true,
    //        Gender = vm.Gender,
    //        Password = BCrypt.Net.BCrypt.HashPassword(vm.Password),
    //        //Role = SystemRole.Teacher.ToString(),
    //        TimeZoneId = Constants.SRI_LANKA_TIME_ZONE_ID,
    //        Username = vm.Username,
    //        ContactNo = "+94702605650",
    //        Email = ""
    //      };

    //      user.UserRoles = new HashSet<UserRole>();

    //      foreach (var role in vm.AssignedRoles)
    //      {
    //        user.UserRoles.Add(new UserRole() { RoleId = role, AssignedOn = DateTime.UtcNow });
    //      }

    //      user.SubjectTeachers = new HashSet<SubjectTeacher>();

    //      foreach (var item in vm.AssignedSubjects)
    //      {
    //        user.SubjectTeachers.Add(new SubjectTeacher()
    //        {
    //          AssignedDate=DateTime.UtcNow,
    //          SubjectId=item
    //        });
    //      }
         

    //      db.Users.Add(user);

    //      response.Message = "New teacher record has been created successfully.";
    //    }
    //    else
    //    {
    //      var exisitngUserName = db.Users.FirstOrDefault(x => x.Username.Trim().ToLower() == vm.Username.Trim().ToLower() && x.Id!=user.Id);

    //      if (exisitngUserName != null)
    //      {
    //        response.Message = "Provided username already taken. Please try with new username.";
    //        response.IsSuccess = false;

    //        return response;
    //      }

    //      user.Username = vm.Username;
    //      user.FullName = vm.FullName;
    //      user.Gender = vm.Gender;

    //      var savedRoles = user.UserRoles.ToList();

    //      var assignedRoles = vm.AssignedRoles;

    //      var newRoles = (from nr in assignedRoles where !savedRoles.Any(x => x.RoleId == nr) select nr).ToList();

    //      foreach (var role in newRoles)
    //      {
    //        user.UserRoles.Add(new UserRole() { RoleId = role, AssignedOn = DateTime.UtcNow });
    //      }

    //      var deletedRoles = (from dr in savedRoles where !assignedRoles.Any(x => x == dr.RoleId) select dr).ToList();

    //      foreach (var item in deletedRoles)
    //      {
    //        db.UserRoles.Remove(item);
    //      }

    //      var savedSubjects = user.SubjectTeachers.Where(x => !x.DeAllocatedDate.HasValue).ToList();

    //      var deletedSubjects = (from s in savedSubjects where !vm.AssignedSubjects.Any(x => x == s.SubjectId) select s).ToList();

    //      foreach (var item in deletedSubjects)
    //      {
    //        item.DeAllocatedDate = DateTime.UtcNow;

    //        db.SubjectTeachers.Update(item);
    //      }

    //      var newlyAddedSubjects = (from n in vm.AssignedSubjects where !savedSubjects.Any(x => x.SubjectId == n) select n).ToList();

    //      foreach (var item in newlyAddedSubjects)
    //      {
    //        user.SubjectTeachers.Add(new SubjectTeacher()
    //        {
    //          AssignedDate = DateTime.UtcNow,
    //          SubjectId = item
    //        });

    //      }

    //      response.Message = "Teacher record has been updated successfully.";
    //    }

    //    response.IsSuccess = true;
    //    await db.SaveChangesAsync();
    //  }
    //  catch(Exception ex)
    //  {
    //    response.IsSuccess = false;
    //    response.Message = "Error has been occured while saving the teacher record. Please try again.";
    //  }

    //  return response;
    //}

    //public async Task<ResponseViewModel> DeleteUser(int id)
    //{
    //  var response = new ResponseViewModel();

    //  try
    //  {
    //    var user = db.Users.FirstOrDefault(x => x.Id == id);

    //    user.IsActive = true;

    //    db.Users.Update(user);

    //    await db.SaveChangesAsync();

    //    response.IsSuccess = true;
    //    response.Message = "User record has been deleted successfully.";
    //  }
    //  catch (Exception ex)
    //  {
    //    logger.LogError(ex.ToString());
    //    response.IsSuccess = false;
    //    response.Message = "Error has been occured while deleting the user record.";
    //  }

    //  return response;
    //}

    //public PaginatedItemsViewModel<BasicStudentViewModel> GetStudentList(string searchText, int currentPage, int pageSize, int academicYearId, int gradeId, int classId)
    //{
    //  int totalRecordCount = 0;
    //  double totalPages = 0;
    //  int totalPageCount = 0;
    //  var vms = new List<BasicStudentViewModel>();
    //  var studentId = (int)SystemRole.Student;
    //  var users = db.Users
    //    .Where(x => x.UserRoles.Any(x=>x.RoleId==studentId) && x.IsActive == true)
    //    .OrderBy(u => u.FullName);

    //  if (academicYearId > 0)
    //  {
    //    users = users.Where(x => x.StudentClasses.FirstOrDefault(x => x.IsActive == true).Class.AcademicYear == academicYearId).OrderBy(u => u.FullName);
    //  }

    //  if ( gradeId> 0)
    //  {
    //    users = users.Where(x => x.StudentClasses.FirstOrDefault(x => x.IsActive == true).Class.GradeId == gradeId).OrderBy(u => u.FullName);
    //  }

    //  if (classId > 0)
    //  {
    //    users = users.Where(x => x.StudentClasses.FirstOrDefault(x => x.IsActive == true).ClassId == classId).OrderBy(u => u.FullName);
    //  }

    //  if (!string.IsNullOrEmpty(searchText))
    //  {
    //    users = users.Where(x => x.FullName.Contains(searchText)).OrderBy(u => u.FullName);
    //  }



    //  totalRecordCount = users.Count();
    //  totalPages = (double)totalRecordCount / pageSize;
    //  totalPageCount = (int)Math.Ceiling(totalPages);

    //  var userList = users.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

    //  userList.ForEach(u =>
    //  {
    //    var studentClass = u.StudentClasses.FirstOrDefault(x => x.IsActive == true);

    //    vms.Add(new BasicStudentViewModel()
    //    {
    //      Id = u.Id,
    //      FullName = u.FullName,
    //      IndexNo = u.Username,
    //      TimeZoneId = u.TimeZoneId,
    //      Gender = u.Gender == "F" ? "Female" : "Male",
    //      Year = studentClass==null?"Not Assigned":studentClass.Class.AcademicYearNavigation.Name,
    //      ClassName=studentClass==null?"Not Assigned":studentClass.Class.Name,
    //      Grade= studentClass==null?"Not Assigned":studentClass.Class.Grade.Name
    //    });

    //  });

    //  var container = new PaginatedItemsViewModel<BasicStudentViewModel>(currentPage, pageSize, totalPageCount, totalRecordCount, vms);

    //  return container;
    //}

    //public async Task<ResponseViewModel> SaveStudent(StudentViewModel vm,string userName)
    //{
    //  var response = new ResponseViewModel();

    //  try
    //  {
    //    var student = db.Users.FirstOrDefault(x => x.Id == vm.Id);

    //    if (student == null)
    //    {
    //      student = db.Users.FirstOrDefault(x => x.Username.Trim().ToLower() == vm.Username.Trim().ToLower());
    //      if(student!=null)
    //      {
    //        response.IsSuccess = false;
    //        response.Message = "Registration failed. Student index number already assigned for another student.";

    //        return response;
    //      }

    //      student = new User()
    //      {
    //        FullName = vm.FullName,
    //        IsActive = true,
    //        Gender = vm.Gender,
    //        Password = BCrypt.Net.BCrypt.HashPassword(vm.Password),
    //        TimeZoneId = Constants.SRI_LANKA_TIME_ZONE_ID,
    //        Username = vm.Username
    //      };

    //      student.UserRoles = new HashSet<UserRole>();
    //      student.UserRoles.Add(new UserRole()
    //      {
    //        RoleId= (int)SystemRole.Student,
    //        AssignedOn = DateTime.UtcNow
    //      });

    //      student.StudentClasses = new HashSet<StudentClass>();

    //      student.StudentClasses.Add(new StudentClass()
    //      {
    //        AssignedDate=DateTime.UtcNow,
    //        ClassId = vm.ClassId,
    //        IsActive=true,
    //      });

    //      db.Users.Add(student);

    //      response.Message = "New student record has been created successfully.";
    //    }
    //    else
    //    {
    //      var anotherStudent = db.Users.FirstOrDefault(x => x.Username.Trim().ToLower() == vm.Username.Trim().ToLower() && x.Id != student.Id);
    //      if (anotherStudent != null)
    //      {
    //        response.IsSuccess = false;
    //        response.Message = "Updation failed. Provided new index number already assigned for another student.";

    //        return response;
    //      }
    //      student.Username = vm.Username;
    //      student.FullName = vm.FullName;
    //      student.Gender = vm.Gender;

    //      var studentClass = student.StudentClasses.FirstOrDefault(x => x.IsActive == true);
    //      studentClass.IsActive = false;
    //      studentClass.RemovedDate = DateTime.UtcNow;

    //      db.StudentClasses.Update(studentClass);

    //      student.StudentClasses.Add(new StudentClass()
    //      {
    //        AssignedDate = DateTime.UtcNow,
    //        ClassId = vm.ClassId,
    //        IsActive = true,
    //      });

    //      db.Users.Update(student);

    //      response.Message = "Teacher record has been updated successfully.";
    //    }

    //    response.IsSuccess = true;
    //    await db.SaveChangesAsync();
    //  }
    //  catch (Exception ex)
    //  {
    //    logger.LogError(ex.ToString());
    //    response.IsSuccess = false;
    //    response.Message = "Error has been occured while saving the teacher record. Please try again.";
    //  }

    //  return response;
    //}

    //public async Task<ResponseViewModel> DeleteStudent(int id)
    //{
    //  var response = new ResponseViewModel();

    //  try
    //  {
    //    var student = db.Users.FirstOrDefault(x => x.Id == id);

    //    student.IsActive = true;

    //    var studentClass = student.StudentClasses.FirstOrDefault(x => x.IsActive == true);

    //    if (studentClass != null)
    //    {
    //      studentClass.IsActive = false;
    //      studentClass.RemovedDate = DateTime.UtcNow;

    //      db.StudentClasses.Update(studentClass);
    //    }

    //    db.Users.Update(student);

    //    await db.SaveChangesAsync();

    //    response.IsSuccess = true;
    //    response.Message = "Student record has been deleted successfully.";
    //  }
    //  catch(Exception ex)
    //  {
    //    logger.LogError(ex.ToString());
    //    response.IsSuccess = false;
    //    response.Message = "Error has been occured while deleting the student record.";
    //  }

    //  return response;  
    //}

    //public StudentViewModel GetStudentById(int id)
    //{
    //  var response = new StudentViewModel();

    //  var student = db.Users.FirstOrDefault(x => x.Id == id);

    //  if(student!=null)
    //  {
    //    response.Id = student.Id;
    //    response.AcademicYearId = student.StudentClasses.FirstOrDefault(x => x.IsActive == true).Class.AcademicYear;
    //    response.FullName = student.FullName;
    //    response.ClassId = student.StudentClasses.FirstOrDefault(x => x.IsActive == true).ClassId;
    //    response.Gender = student.Gender;
    //    response.GradeId = student.StudentClasses.FirstOrDefault(x => x.IsActive == true).Class.GradeId;
    //    response.Username = student.Username;
    //  }
    //  else
    //  {

    //  }

    //  return response;
    //}

    //public async Task<ResponseViewModel> UpdateUserPassword(PasswordUpdateViewModel vm)
    //{
    //  var response = new ResponseViewModel();

    //  if(!vm.NewPassword.Equals(vm.ConfirmPassword))
    //  {
    //    response.IsSuccess = false;
    //    response.Message = "Unable to update the password. Provided new password and confrim password does not match.";

    //    return response;
    //  }

    //  var user = db.Users.FirstOrDefault(x => x.Id == vm.Id);

    //  if(user != null)
    //  {
    //    user.Password = BCrypt.Net.BCrypt.HashPassword(vm.NewPassword);

    //    db.Users.Update(user);

    //    await db.SaveChangesAsync();

    //    response.IsSuccess = true;
    //    response.Message = "User password has been updated successfully.";
    //  }
    //  else
    //  {
    //    response.IsSuccess = false;
    //    response.Message = "Operation failed. User does not exists.";
    //  }


    //  return response;
    //}

    //public StudentListDropDownMasterData GetStudentDropdownsMasterData()
    //{
    //  var response = new StudentListDropDownMasterData();

    //  response.AcademicYears.AddRange(dropDownService.GetAllAcademicYears());
    //  response.Grades.AddRange(dropDownService.GetAllAcademicLevels());
    //  response.CurrentAcademicYear = db.AcademicYears.FirstOrDefault(x => x.IsCurrentYear == true).Id;
    //  response.SelectedClassId = 0;
    //  response.SelectedGradeId = 0;

    //  return response;
    //}

    //public async Task<MasterDataUploadResponse> UploadClassStudents(FileContainerViewModel container, string userName)
    //{
    //  var response = new MasterDataUploadResponse();
    //  response.IsSucccess = true;

    //  try
    //  {
    //    var curentUser = coreDataService.GetLoggedInUserByUserName(userName);
    //    var folderPath = config.GetSection("FileUploadPath").Value;

    //    if (!Directory.Exists(folderPath))
    //    {
    //      Directory.CreateDirectory(folderPath);
    //    }
    //    var upLoadedFiles = new Dictionary<string, string>();

    //    foreach (var item in container.Files)
    //    {
    //      var filePath = string.Format(@"{0}\{1}", folderPath, item.FileName);
    //      using (var stream = new FileStream(filePath, FileMode.Create))
    //      {
    //        await item.CopyToAsync(stream);
    //      }

    //      upLoadedFiles.Add(filePath, item.FileName);
    //    }

    //    foreach (var item in upLoadedFiles)
    //    {
    //      studentExcelContainer = new StudentExcelContainer();
    //      try
    //      {
    //        var validateResult = ValidateExcelFileContents(item.Key);

    //        response.Results.AddRange(validateResult);

    //        var validationErrorCount = response.Results.Where(x => x.IsSuccess == false).Count();

    //        if (response.IsSucccess == true && validationErrorCount > 0)
    //        {
    //          response.IsSucccess = false;
    //        }
    //        else
    //        {
    //          foreach (var student in studentExcelContainer.Students)
    //          {
    //            var studentRecord = db.Users.FirstOrDefault(x => x.Username.Trim().ToLower() == student.Username.Trim().ToLower());
    //            if (studentRecord == null)
    //            {
    //              studentRecord = new User()
    //              {
    //                Gender = student.Gender,
    //                FullName = student.FullName,
    //                IsActive = true,
    //                TimeZoneId= Constants.SRI_LANKA_TIME_ZONE_ID,
    //                Password = BCrypt.Net.BCrypt.HashPassword(student.Username),
    //                Username = student.Username,
    //                ContactNo = string.Empty,
    //                Email = string.Empty,

    //              };

    //              studentRecord.UserRoles = new HashSet<UserRole>();

    //              studentRecord.UserRoles.Add(new UserRole()
    //              {
    //                RoleId = (int)SystemRole.Student,
    //                AssignedOn = DateTime.UtcNow

    //              });

    //              studentRecord.StudentClasses = new HashSet<StudentClass>();

    //              studentRecord.StudentClasses.Add(new StudentClass()
    //              {
    //                ClassId = studentExcelContainer.ClassId,
    //                AssignedDate = DateTime.UtcNow,
    //                IsActive = true
    //              });

    //              db.Users.Add(studentRecord);
    //            }
    //            else
    //            {
    //              studentRecord.Gender = student.Gender;
    //              studentRecord.FullName = student.FullName;

    //              var studentClass = studentRecord.StudentClasses.FirstOrDefault(x => x.IsActive == true);
    //              if(studentClass.ClassId!=studentExcelContainer.ClassId)
    //              {

    //                studentClass.IsActive = false;
    //                studentClass.RemovedDate = DateTime.UtcNow;

    //                db.StudentClasses.Update(studentClass);

    //                studentRecord.StudentClasses.Add(new StudentClass()
    //                {
    //                  ClassId = studentExcelContainer.ClassId,
    //                  AssignedDate = DateTime.UtcNow,
    //                  IsActive = true
    //                });
    //              }

    //              db.Users.Update(studentRecord);

    //            }
    //          }

    //          await db.SaveChangesAsync();

    //          response.Results.Add(new MasterDataFileValidateResult() { IsSuccess = true, ValidateMessage = $"Class student list has been uploaded successfully for file name {item.Value}." });
    //        }

    //      }
    //      catch (Exception ex)
    //      {
    //        response.IsSucccess = false;
    //        response.Results.Add(new MasterDataFileValidateResult() { IsSuccess = false, ValidateMessage = $"Exception has been occured while processing the class student import using file name {item.Value}." });
    //      }
    //    }

    //    var errorCount = response.Results.Where(x => x.IsSuccess == false).Count();

    //    if (response.IsSucccess == true && errorCount == 0)
    //    {
    //      response.Results.Add(new MasterDataFileValidateResult() { IsSuccess = true, ValidateMessage = "All file has been uploaded and process successfully." });
    //    }
    //  }
    //  catch (Exception)
    //  {
    //  }

    //  return response;
    //}

    //private List<MasterDataFileValidateResult> ValidateExcelFileContents(string fileSavePath)
    //{
    //  var response = new List<MasterDataFileValidateResult>();

    //  try
    //  {
    //    var academicYears = db.AcademicYears.ToList();
    //    var grades = db.Grades.ToList();

    //    FileInfo fileInfo = new FileInfo(fileSavePath);
    //    using (ExcelPackage package = new ExcelPackage(fileInfo))
    //    {
    //      //get the first worksheet in the workbook
    //      ExcelWorksheet worksheet = package.Workbook.Worksheets["ClassStudents"];

    //      int colCount = worksheet.Dimension.End.Column;  //get Column Count
    //      int rowCount = worksheet.Dimension.End.Row - 5;

    //      var yearValue = worksheet.Cells[1, 2].Value.ToString().Trim();

    //      if (!string.IsNullOrEmpty(yearValue))
    //      {
    //        int enteredYear;

    //        if (int.TryParse(yearValue, out enteredYear))
    //        {
    //          var currentYear = DateTime.Now.Year;
    //          if (!(enteredYear == currentYear || enteredYear == currentYear + 1))
    //          {
    //            response.Add(new MasterDataFileValidateResult() { ValidateMessage = "Invalid user excel template. Year can be current year or next year only.", IsSuccess = false });
    //          }
    //          else
    //          {
    //            studentExcelContainer.Year = db.AcademicYears.FirstOrDefault(x=>x.Name== yearValue).Id;
    //          }
    //        }
    //        else
    //        {
    //          response.Add(new MasterDataFileValidateResult() { ValidateMessage = "Invalid user excel template. Invalid Year format has been entered.", IsSuccess = false });
    //        }
    //      }
    //      else
    //      {
    //        response.Add(new MasterDataFileValidateResult() { ValidateMessage = "Invalid user excel template. Academic year is not entered.", IsSuccess = false });
    //      }

    //      var gradeValue = worksheet.Cells[2, 2].Value.ToString().Trim().ToLower();

    //      var enteredGrade = db.Grades.FirstOrDefault(x => x.Name.Trim().ToLower() == gradeValue);
    //      if (enteredGrade != null)
    //      {
    //        studentExcelContainer.GradeId = enteredGrade.Id;
    //      }
    //      else
    //      {
    //        response.Add(new MasterDataFileValidateResult() { ValidateMessage = "Invalid user excel template. Grade value does not exists or invalid grade value has been entered.", IsSuccess = false });
    //      }

    //      if (studentExcelContainer.Year > 0 && studentExcelContainer.GradeId > 0)
    //      {
    //        var classValue = worksheet.Cells[3, 2].Value.ToString().Trim().ToLower();

    //        var enteredClass = db.Classes
    //          .FirstOrDefault(x => x.Name.Trim().ToLower() == classValue && x.GradeId == studentExcelContainer.GradeId && x.AcademicYear == studentExcelContainer.Year);
    //        if (enteredClass != null)
    //        {
    //          studentExcelContainer.ClassId = enteredClass.Id;
    //        }
    //        else
    //        {
    //          response.Add(new MasterDataFileValidateResult() { ValidateMessage = "Invalid user excel template. Class name value does not exists or invalid class name value has been entered.", IsSuccess = false });
    //        }
    //      }

    //      var teacherValue = worksheet.Cells[4, 2].Value.ToString().Trim().ToLower();

    //      var enterTeacher = db.Users
    //          .FirstOrDefault(x => x.Username.Trim().ToLower() == teacherValue);
    //      if (enterTeacher != null)
    //      {
    //        studentExcelContainer.ClassTeacherId = enterTeacher.Id;
    //      }
    //      else
    //      {
    //        response.Add(new MasterDataFileValidateResult() { ValidateMessage = "Invalid user excel template. Teacher username does not exists or invalid teacher username has been entered.", IsSuccess = false });
    //      }

    //      if (colCount != 3)
    //      {
    //        response.Add(new MasterDataFileValidateResult() { ValidateMessage = "Invalid user excel template. Column count does not match.", IsSuccess = false });
    //      }
    //      else
    //      {
    //        response.Add(new MasterDataFileValidateResult() { ValidateMessage = "Uploaded user excel template valid.", IsSuccess = true });
    //      }

    //      for (int row = 6; row <= rowCount; row++)
    //      {
    //        var user = new UserViewModel();

    //        for (int col = 1; col <= colCount; col++)
    //        {
    //          if (row == 6)
    //          {
    //            if (col == 1)
    //            {
    //              if (worksheet.Cells[row, col].Value.ToString().Trim() != "IndexNo")
    //              {
    //                response.Add(new MasterDataFileValidateResult() { ValidateMessage = "Invalid excel file. IndexNo column (Column index 0) is missing.", IsSuccess = false });

    //              }

    //            }
    //            else if (col == 2)
    //            {
    //              if (worksheet.Cells[row, col].Value.ToString().Trim() != "Full Name")
    //              {
    //                response.Add(new MasterDataFileValidateResult() { ValidateMessage = "Invalid excel file. Full Name column (Column index 1) is missing.", IsSuccess = false });

    //              }
    //            }
    //            else if (col == 3)
    //            {
    //              if (worksheet.Cells[row, col].Value.ToString().Trim() != "Gender")
    //              {
    //                response.Add(new MasterDataFileValidateResult() { ValidateMessage = "Invalid excel file. Gender column (Column index 2) is missing.", IsSuccess = false });

    //              }
    //            }

    //          }
    //          else
    //          {


    //            if (col == 1)
    //            {
    //              var indexNo = worksheet.Cells[row, col].Value == null ? string.Empty : worksheet.Cells[row, col].Value.ToString().Trim();
    //              if (!string.IsNullOrEmpty(indexNo))
    //              {
    //                user.Username = indexNo;
    //              }
    //              else
    //              {
    //                response.Add(new MasterDataFileValidateResult() { ValidateMessage = "Empty index number found in row " + row.ToString() + ".", IsSuccess = false });
    //              }
    //            }
    //            else if (col == 2)
    //            {
    //              var fullName = worksheet.Cells[row, col].Value == null ? string.Empty : worksheet.Cells[row, col].Value.ToString().Trim();
    //              if (!string.IsNullOrEmpty(fullName))
    //              {
    //                user.FullName = fullName;
    //              }
    //              else
    //              {
    //                response.Add(new MasterDataFileValidateResult() { ValidateMessage = "Full name can not be empty in row " + row.ToString() + ".", IsSuccess = false });
    //              }

    //            }
    //            else if (col == 3)
    //            {
    //              var gender = worksheet.Cells[row, col].Value == null ? string.Empty : worksheet.Cells[row, col].Value.ToString().Trim();
    //              if (!string.IsNullOrEmpty(gender))
    //              {

    //                user.Gender = gender;
    //              }
    //              else
    //              {
    //                response.Add(new MasterDataFileValidateResult() { ValidateMessage = "Gender can not be empty in row " + row.ToString() + ".", IsSuccess = false });
    //              }
    //            }

    //          }

    //        }

    //        if(row>6)
    //          studentExcelContainer.Students.Add(user);

    //      }
    //    }
    //  }
    //  catch (Exception ex)
    //  {
    //    throw ex;
    //  }

    //  return response;
    //}


  }


}
