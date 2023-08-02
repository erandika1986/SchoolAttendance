using MediatR;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonUnitTestTopic
{
    public class SaveLessonUnitTestTopicCommand : IRequest<LessonUnitTestTopicViewModel>
    {
        public LessonUnitTestTopicViewModel Vm { get; set; }
    }

    public class SaveLessonUnitTestTopicCommandHandler : IRequestHandler<SaveLessonUnitTestTopicCommand, LessonUnitTestTopicViewModel>
    {
        private readonly ILessonUnitTestTopicQueryRepository _lessonUnitTestTopicQueryRepository;
        private readonly ILessonUnitTestTopicCommandRepository _lessonUnitTestTopicCommandRepository;
        private readonly ILessonUnitTestQueryRepository _lessonUnitTestQueryRepository;
        private readonly ILogger<SaveLessonUnitTestTopicCommandHandler> _logger;
        public SaveLessonUnitTestTopicCommandHandler(
            ILessonUnitTestTopicQueryRepository lessonUnitTestTopicQueryRepository, 
            ILessonUnitTestTopicCommandRepository lessonUnitTestTopicCommandRepository,
            ILessonUnitTestQueryRepository lessonUnitTestQueryRepository,
            ILogger<SaveLessonUnitTestTopicCommandHandler> logger)
        {
            this._lessonUnitTestTopicQueryRepository = lessonUnitTestTopicQueryRepository;
            this._lessonUnitTestTopicCommandRepository = lessonUnitTestTopicCommandRepository;
            this._lessonUnitTestQueryRepository = lessonUnitTestQueryRepository;
            this._logger = logger;
        }


        public async Task<LessonUnitTestTopicViewModel> Handle(SaveLessonUnitTestTopicCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var unitTestTopic = await _lessonUnitTestTopicQueryRepository
                    .GetById(request.Vm.Id, cancellationToken);

                var unitTest = await _lessonUnitTestQueryRepository
                    .GetById(request.Vm.LessonUnitTestId, cancellationToken);

                if (unitTestTopic == null)
                {
                    unitTestTopic = new LessonUnitTestTopic()
                    {
                        LessonUnitTestId = request.Vm.LessonUnitTestId,
                        Name = $"Section {unitTest.LessonUnitTestTopics.Count + 1}",
                        Instruction = string.Empty,
                        QuestionTypeId = request.Vm.QuestionTypeId
                    };

                    await _lessonUnitTestTopicCommandRepository.AddAsync(unitTestTopic, cancellationToken);
                }
                else
                {
                    unitTestTopic.Name = request.Vm.Name;
                    unitTestTopic.Instruction = request.Vm.Instruction;
                    unitTestTopic.QuestionTypeId = request.Vm.QuestionTypeId;

                    await _lessonUnitTestTopicCommandRepository.UpdateAsync(unitTestTopic, cancellationToken);
                }
                request.Vm.Id = unitTestTopic.Id;
                request.Vm.Name = unitTestTopic.Name;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }



            return request.Vm;
        }
    }
}
