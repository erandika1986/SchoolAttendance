using BCrypt.Net;
using MediatR;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Pipelines.User.Commands.DeleteUser;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Constants;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.User.Commands.SaveUser
{
    public class SaveUserCommand: IRequest<ResponseViewModel>
    {
        public UserViewModel User { get; set; }
    }

    public class SaveUserCommandHandler : IRequestHandler<SaveUserCommand, ResponseViewModel>
    {
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IUserRoleCommandRepository _userUserRoleCommandRepository;
        private readonly ISubjectTeacherCommandRepository _subjectTeacherCommandRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<SaveUserCommandHandler> _logger;

        public SaveUserCommandHandler(
            IUserCommandRepository userCommandRepository, 
            IUserQueryRepository userQueryRepository,
            IUserRoleCommandRepository userUserRoleCommandRepository,
            ICurrentUserService currentUserService,
            ISubjectTeacherCommandRepository subjectTeacherCommandRepository,
            ILogger<SaveUserCommandHandler> logger)
        {
            this._userCommandRepository = userCommandRepository;
            this._userQueryRepository = userQueryRepository;
            this._userUserRoleCommandRepository = userUserRoleCommandRepository;
            this._currentUserService = currentUserService;
            this._subjectTeacherCommandRepository = subjectTeacherCommandRepository;
            this._logger = logger;

        }

        public async Task<ResponseViewModel> Handle(SaveUserCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel();

            try
            {
                var user = await _userQueryRepository
                    .GetById(request.User.Id, cancellationToken);

                if (user == null)
                {
                    var existingUserNames = await _userQueryRepository
                        .Query(x => x.Username.Trim().ToLower() == request.User.Username.ToLower());

                    if (existingUserNames.Count() > 0)
                    {
                        response.Message = "Provided username already taken. Please try with new username.";
                        response.IsSuccess = false;

                        return response;
                    }

                    user = new SchoolAttendance.Domain.Entities.User()
                    {
                        FullName = request.User.FullName,
                        IsActive = true,
                        Gender = request.User.Gender,
                        Password = BCrypt.Net.BCrypt.HashPassword(request.User.Password),
                        TimeZoneId = Constants.SRI_LANKA_TIME_ZONE_ID,
                        Username = request.User.Username,
                        ContactNo = request.User.ContactNo,
                        Email = request.User.Email
                    };

                    foreach (var role in request.User.AssignedRoles)
                    {
                        user.UserRoles.Add(new UserRole()
                        {
                            RoleId = role,
                            AssignedOn = DateTime.UtcNow
                        });
                    }

                    foreach (var item in request.User.AssignedSubjects)
                    {
                        user.SubjectTeachers.Add(new SubjectTeacher()
                        {
                            AssignedDate = DateTime.UtcNow,
                            SubjectId = item
                        });
                    }

                    await _userCommandRepository.AddAsync(user, cancellationToken);

                    response.Message = "New teacher record has been created successfully.";
                    response.IsSuccess = true;
                }
                else
                {
                    var existingUserNames = await _userQueryRepository
                        .Query(x =>
                                x.Username.Trim().ToLower() == request.User.Username.ToLower() &&
                                x.Id != request.User.Id);

                    if (existingUserNames.Count() > 0)
                    {
                        response.Message = "Provided username already taken. Please try with new username.";
                        response.IsSuccess = false;

                        return response;
                    }
                    else
                    {
                        user.Username = request.User.Username;
                        user.FullName = request.User.FullName;
                        user.Gender = request.User.Gender;
                        user.Email = request.User.Email;
                        user.ContactNo = request.User.ContactNo;

                        var savedRoles = user.UserRoles.ToList();

                        var assignedRoles = request.User.AssignedRoles;

                        var newRoles = (
                            from nr in assignedRoles
                            where !savedRoles.Any(x => x.RoleId == nr)
                            select nr).ToList();

                        foreach (var role in newRoles)
                        {
                            user.UserRoles.Add(new UserRole()
                            {
                                RoleId = role,
                                AssignedOn = DateTime.UtcNow
                            });
                        }

                        var deletedRoles = (
                                from dr in savedRoles
                                where !assignedRoles.Any(x => x == dr.RoleId)
                                select dr
                            ).ToList();

                        foreach (var item in deletedRoles)
                        {
                            await _userUserRoleCommandRepository
                                .DeleteAsync(item, cancellationToken);
                        }

                        var savedSubjects = user
                            .SubjectTeachers
                            .Where(x => !x.DeAllocatedDate.HasValue)
                            .ToList();

                        var deletedSubjects = (
                            from s in savedSubjects
                            where !request.User.AssignedSubjects.Any(x => x == s.SubjectId)
                            select s).ToList();

                        foreach (var item in deletedSubjects)
                        {
                            item.DeAllocatedDate = DateTime.UtcNow;

                            await _subjectTeacherCommandRepository
                                .UpdateAsync(item, cancellationToken);
                        }

                        var newlyAddedSubjects = (
                            from n in request.User.AssignedSubjects
                            where !savedSubjects.Any(x => x.SubjectId == n)
                            select n).ToList();

                        foreach (var item in newlyAddedSubjects)
                        {
                            user.SubjectTeachers.Add(new SubjectTeacher()
                            {
                                AssignedDate = DateTime.UtcNow,
                                SubjectId = item
                            });

                        }

                        await _userCommandRepository.UpdateAsync(user, cancellationToken);

                        response.Message = "Teacher record has been updated successfully.";
                        response.IsSuccess = true;
                    }
                }

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
