using MediatR;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Helpers;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Queries.GetLessonDesignDropdownMasterData
{
    public record GetLessonDesignDropdownMasterDataQuery() : IRequest<LessonListFilterMasterData>
    {
    }

    public class GetLessonDesignDropdownMasterDataQueryHandler : IRequestHandler<GetLessonDesignDropdownMasterDataQuery, LessonListFilterMasterData>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IAcademicYearQueryRepository _academicYearQueryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDropDownService _dropDownService;
        public GetLessonDesignDropdownMasterDataQueryHandler(
            IUserQueryRepository userQueryRepository,
            IAcademicYearQueryRepository academicYearQueryRepository,
            ICurrentUserService currentUserService,
            IDropDownService dropDownService)
        {
            this._userQueryRepository = userQueryRepository;
            this._academicYearQueryRepository = academicYearQueryRepository;
            this._currentUserService = currentUserService;
            this._dropDownService = dropDownService;

        }

        public async Task<LessonListFilterMasterData> Handle(GetLessonDesignDropdownMasterDataQuery request, CancellationToken cancellationToken)
        {
            var respones = new LessonListFilterMasterData();

            var loggedInUser = await _userQueryRepository.GetById(_currentUserService.UserId.Value, cancellationToken);

            var roles = loggedInUser.UserRoles.Select(x => x.RoleId).ToList();

            if (roles.Contains((int)SystemRole.Admin) || roles.Contains((int)SystemRole.Principle) || roles.Contains((int)SystemRole.VicePrinciple) || roles.Contains((int)SystemRole.DepartmentHead))
            {
                respones.Grades.AddRange(_dropDownService.GetAllAcademicLevels());

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

            respones.AcademicYears.AddRange(_dropDownService.GetAllAcademicYears());

            foreach (LessonStatus status in (LessonStatus[])Enum.GetValues(typeof(LessonStatus)))
            {
                respones.LessonStatuses.Add(new DropDownViewModel() { Id = (int)status, Name = EnumHelper.GetEnumDescription(status) });
            }

            foreach (TeachingAids aid in (TeachingAids[])Enum.GetValues(typeof(TeachingAids)))
            {
                respones.TeacherAids.Add(new DropDownViewModel() { Id = (int)aid, Name = EnumHelper.GetEnumDescription(aid) });
            }

            respones.CurrentAcademicYear = ( await _academicYearQueryRepository.GetCurrentAcademicYear(cancellationToken)).Id;

            return respones;
        }
    }
}
