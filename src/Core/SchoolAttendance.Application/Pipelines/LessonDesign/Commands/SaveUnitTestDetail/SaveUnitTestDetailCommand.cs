using MediatR;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonUnitTest;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveUnitTestDetail
{
    public class SaveUnitTestDetailCommand : IRequest<LessonUnitTestViewModel>
    {
        public LessonUnitTestViewModel Vm { get; set; }
    }

    public class SaveUnitTestDetailCommandHandler : IRequestHandler<SaveUnitTestDetailCommand, LessonUnitTestViewModel>
    {
        private readonly ILessonUnitTestQueryRepository _lessonUnitTestQueryRepository;
        private readonly ILessonUnitTestCommandRepository _lessonUnitTestCommandRepository;
        private readonly ILogger<SaveUnitTestDetailCommandHandler> _logger;

        public SaveUnitTestDetailCommandHandler(
            ILessonUnitTestQueryRepository lessonUnitTestQueryRepository, 
            ILessonUnitTestCommandRepository lessonUnitTestCommandRepository, 
            ILogger<SaveUnitTestDetailCommandHandler> logger)
        {
            this._lessonUnitTestQueryRepository = lessonUnitTestQueryRepository;
            this._lessonUnitTestCommandRepository = lessonUnitTestCommandRepository;    
            this._logger = logger;
        }

        public async Task<LessonUnitTestViewModel> Handle(SaveUnitTestDetailCommand request, CancellationToken cancellationToken)
        {
            var response = new LessonUnitTestViewModel();

            try
            {
                var lessonUnitTest = await _lessonUnitTestQueryRepository
                    .GetById(request.Vm.Id, cancellationToken);

                if (lessonUnitTest == null)
                {
                    lessonUnitTest = new LessonUnitTest()
                    {
                        LessonId = request.Vm.LessonId,
                        Name = request.Vm.Name,
                        StudentGuide = request.Vm.StudentGuide,
                        LessonUnitTestTopics = new HashSet<LessonUnitTestTopic>()
                    };

                    await _lessonUnitTestCommandRepository.AddAsync(lessonUnitTest, cancellationToken);
                }
                else
                {
                    lessonUnitTest.Name = request.Vm.Name;
                    lessonUnitTest.StudentGuide = request.Vm.StudentGuide;

                    await _lessonUnitTestCommandRepository.UpdateAsync(lessonUnitTest, cancellationToken);
                }

                response = lessonUnitTest.ToVM();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return response;
        }
    }
}
