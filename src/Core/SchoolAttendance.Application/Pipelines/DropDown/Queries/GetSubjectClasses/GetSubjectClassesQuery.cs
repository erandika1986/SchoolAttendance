using MediatR;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.DropDown.Queries.GetSubjectClasses
{
    public record GetSubjectClassesQuery(int gradeId, int subjectId, int lessonId) : IRequest<List<DropDownViewModel>>
    {
    }

    public class GetSubjectClassesQueryHandler : IRequestHandler<GetSubjectClassesQuery, List<DropDownViewModel>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly ILessonQueryRepository _lessonQueryRepository;
        private readonly IAcademicYearQueryRepository _academicYearQueryRepository;
        private readonly IClassSubjectQueryRepository _classSubjectQueryRepository;

        public GetSubjectClassesQueryHandler(
            ICurrentUserService currentUserService,
            IUserQueryRepository userQueryRepository,
            ILessonQueryRepository lessonQueryRepository, 
            IAcademicYearQueryRepository academicYearQueryRepository, 
            IClassSubjectQueryRepository classSubjectQueryRepository)
        {
            this._currentUserService = currentUserService;
            this._userQueryRepository = userQueryRepository;
            this._lessonQueryRepository = lessonQueryRepository;
            this._academicYearQueryRepository = academicYearQueryRepository;
            this._classSubjectQueryRepository = classSubjectQueryRepository;

        }

        public async Task<List<DropDownViewModel>> Handle(GetSubjectClassesQuery request, CancellationToken cancellationToken)
        {
            var loggedInUser = await _userQueryRepository
                .GetById(_currentUserService.UserId.Value, cancellationToken);

            var roles = loggedInUser
                .UserRoles
                .Select(x => x.RoleId)
            .ToList();

            var lesson = await _lessonQueryRepository.GetById(request.lessonId, cancellationToken);

            var currentAcademicYear = await _academicYearQueryRepository.GetCurrentAcademicYear(cancellationToken);
           
            var classes = (
                            await _classSubjectQueryRepository
                            .Query(x =>
                                        x.SubjectId == request.subjectId &&
                                        x.Class.GradeId == request.gradeId &&
                                        x.Class.AcademicYear == currentAcademicYear.Id &&
                                        x.SubjectTeacherId == lesson.LessonOwnerId)
                            )
                            .Select(cl => new DropDownViewModel()
                            {
                                Id = cl.ClassId,
                                Name = cl.Class.Name
                            })
                            .ToList();

            return classes;
        }
    }
}
