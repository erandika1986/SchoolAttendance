using MediatR;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllAcademicLevels;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllAcademicYears;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.Attendance.Queries.GetAttendanceListTeacherDropdownMasterData
{
    public record GetAttendanceListTeacherDropdownMasterDataQuery() : IRequest<AttendanceListFilterMasterData>
    {
    }

    public class GetAttendanceListTeacherDropdownMasterDataQueryHandler : IRequestHandler<GetAttendanceListTeacherDropdownMasterDataQuery, AttendanceListFilterMasterData>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IAcademicYearQueryRepository _academicYearQueryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;
        public GetAttendanceListTeacherDropdownMasterDataQueryHandler(
            IUserQueryRepository userQueryRepository, 
            IAcademicYearQueryRepository academicYearQueryRepository,
            ICurrentUserService currentUserService,
            IMediator mediator)
        {
            this._userQueryRepository = userQueryRepository;
            this._academicYearQueryRepository = academicYearQueryRepository;
            this._currentUserService = currentUserService;
            this._mediator = mediator;

        }

        public async Task<AttendanceListFilterMasterData> Handle(GetAttendanceListTeacherDropdownMasterDataQuery request, CancellationToken cancellationToken)
        {
            var respones = new AttendanceListFilterMasterData();

            var loggedInUser = await _userQueryRepository.GetById(_currentUserService.UserId.Value, cancellationToken);

            var roles = loggedInUser.UserRoles.Select(x => x.RoleId).ToList();

            if (roles.Contains((int)SystemRole.Admin) || roles.Contains((int)SystemRole.Principle) || roles.Contains((int)SystemRole.VicePrinciple) || roles.Contains((int)SystemRole.DepartmentHead))
            {
                var academicLevels = await _mediator.Send(new GetAllAcademicLevelsQuery());

                respones.Grades.AddRange(academicLevels);

            }
            else if (roles.Contains((int)SystemRole.LevelHead) || roles.Contains((int)SystemRole.Teacher))
            {
                var levelHeads = new List<DropDownViewModel>();
                if (roles.Contains((int)SystemRole.LevelHead))
                {
                    var userLevelHeads = loggedInUser.Grades.ToList();
                    if (userLevelHeads.Count > 0)
                    {
                        levelHeads = userLevelHeads.Select(x => new DropDownViewModel() { Id = x.Id, Name = x.Name }).ToList();
                    }
                }

                var subjectClasses = loggedInUser.ClassSubjects.Select(x => x.Class.Grade).Where(g => !levelHeads.Any(y => y.Id == g.Id)).Distinct().
                      Select(x => new DropDownViewModel() { Id = x.Id, Name = x.Name }).OrderBy(x => x.Id).ToList().Union(levelHeads).ToList();

                respones.Grades.AddRange(subjectClasses);


            }

            if (respones.Grades.Count > 0)
            {
                respones.SelectedGradeId = (int)respones.Grades.FirstOrDefault().Id;
            }

            var academicYears = await _mediator.Send(new GetAllAcademicYearsQuery());
            respones.AcademicYears.AddRange(academicYears);

            respones.CurrentAcademicYear = (await _academicYearQueryRepository.GetCurrentAcademicYear(cancellationToken)).Id ;

            return respones;
        }
    }
}
