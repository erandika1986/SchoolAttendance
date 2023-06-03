using MediatR;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonLecture
{
    public class SaveLessonLectureCommand : IRequest<LessonLectureViewModel>
    {
        public LessonLectureViewModel Vm { get; set; }
    }

    public class SaveLessonLectureCommandHandler : IRequestHandler<SaveLessonLectureCommand, LessonLectureViewModel>
    {
        private readonly ILessonLectureQueryRepository _lessonLectureQueryRepository;
        private readonly ILessonLectureCommandRepository _lessonLectureCommandRepository;

        public SaveLessonLectureCommandHandler(
            ILessonLectureQueryRepository lessonLectureQueryRepository,
            ILessonLectureCommandRepository lessonLectureCommandRepository)
        {
            this._lessonLectureQueryRepository = lessonLectureQueryRepository;
            this._lessonLectureCommandRepository = lessonLectureCommandRepository;
        }

        public async Task<LessonLectureViewModel> Handle(SaveLessonLectureCommand request, CancellationToken cancellationToken)
        {

            var lecture = await _lessonLectureQueryRepository
                .GetById(request.Vm.Id, cancellationToken);

            lecture.LectureContentTypeId = request.Vm.ContentType;

            if (request.Vm.ContentType == (int)LectureContentType.Youtube)
            {
                lecture.LectureContent = request.Vm.Content.Replace("watch?v=", "embed/");
            }
            else
            {
                lecture.LectureContent = request.Vm.Content;
            }

            await _lessonLectureCommandRepository.UpdateAsync(lecture, cancellationToken);

            var response = lecture.ToVM();

            return response;
        }
    }
}
