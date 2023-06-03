using MediatR;
using SchoolAttendance.Application.Pipelines.Attendance.Queries.GetAttendanceDetailForSubjectClassById;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Queries.GetLessonById
{
    public record GetLessonByIdQuery(int id) : IRequest<LessonViewModel>
    {
    }

    public class GetLessonByIdQueryHandler : IRequestHandler<GetLessonByIdQuery, LessonViewModel>
    {
        private readonly ILessonQueryRepository _lessonQueryRepository;

        public GetLessonByIdQueryHandler(ILessonQueryRepository lessonQueryRepository)
        {
            this._lessonQueryRepository = lessonQueryRepository;
        }

        public async Task<LessonViewModel> Handle(GetLessonByIdQuery request, CancellationToken cancellationToken)
        {
            var lesson = await _lessonQueryRepository.GetById(request.id, cancellationToken);

            return lesson.ToVM();
        }
    }
}
