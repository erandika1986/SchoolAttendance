using MediatR;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Constants;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.User.Commands.SaveStudent
{
    public record SaveStudentCommand(StudentViewModel vm) : IRequest<ResponseViewModel>
    {
    }

    public class SaveStudentCommandHandler : IRequestHandler<SaveStudentCommand, ResponseViewModel>
    {
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IStudentClassCommandRepository _studentClassCommandRepository;
        private readonly ILogger<SaveStudentCommandHandler> _logger;

        public SaveStudentCommandHandler(
            IUserCommandRepository userCommandRepository, 
            IUserQueryRepository userQueryRepository,
            IStudentClassCommandRepository studentClassCommandRepository,
            ILogger<SaveStudentCommandHandler> logger)
        {
            this._userCommandRepository = userCommandRepository;
            this._userQueryRepository = userQueryRepository;
            this._studentClassCommandRepository = studentClassCommandRepository; 
            this._logger = logger;
        }

        public async Task<ResponseViewModel> Handle(SaveStudentCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel();

            try
            {
                var student = await _userQueryRepository.GetById(request.vm.Id, cancellationToken);

                if (student == null)
                {
                    var anotherStudentsWithSameUserName = await _userQueryRepository
                        .Query(
                                    x => x.Username.Trim().ToLower() == request.vm.Username.Trim().ToLower());

                    if (anotherStudentsWithSameUserName.Count() > 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Registration failed. Student index number already assigned for another student.";

                        return response;
                    }

                    student = new Domain.Entities.User()
                    {
                        FullName = request.vm.FullName,
                        IsActive = true,
                        Gender = request.vm.Gender,
                        Password = BCrypt.Net.BCrypt.HashPassword(request.vm.Password),
                        TimeZoneId = Constants.SRI_LANKA_TIME_ZONE_ID,
                        Username =  request.vm.Username
                    };

                    student.UserRoles.Add(new UserRole()
                    {
                        RoleId = (int)SystemRole.Student,
                        AssignedOn = DateTime.UtcNow
                    });

                    student.StudentClasses.Add(new StudentClass()
                    {
                        AssignedDate = DateTime.UtcNow,
                        ClassId = request.vm.ClassId,
                        IsActive = true,
                    });

                    await _userCommandRepository.AddAsync(student, cancellationToken);

                    response.Message = "New student record has been created successfully.";
                }
                else
                {
                    var anotherStudentsWithSameUserName = await _userQueryRepository
                        .Query(x => x.Username.Trim().ToLower() == request.vm.Username.Trim().ToLower() && x.Id != student.Id);

                    if (anotherStudentsWithSameUserName.Count() > 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Updation failed. Provided new index number already assigned for another student.";

                        return response;
                    }

                    student.Username = request.vm.Username;
                    student.FullName = request.vm.FullName;
                    student.Gender = request.vm.Gender;

                    var studentClass = student.StudentClasses.FirstOrDefault(x => x.IsActive == true);
                    studentClass.IsActive = false;
                    studentClass.RemovedDate = DateTime.UtcNow;

                    await _studentClassCommandRepository.UpdateAsync(studentClass, cancellationToken);

                    student.StudentClasses.Add(new StudentClass()
                    {
                        AssignedDate = DateTime.UtcNow,
                        ClassId = request.vm.ClassId,
                        IsActive = true,
                    });

                    await _userCommandRepository.UpdateAsync(student, cancellationToken);

                    response.Message = "Teacher record has been updated successfully.";
                }

                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while saving the teacher record. Please try again.";
            }

            return response;
        }
    }
}
