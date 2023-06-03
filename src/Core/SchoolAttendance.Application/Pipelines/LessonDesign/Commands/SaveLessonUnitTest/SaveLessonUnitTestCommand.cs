using MediatR;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonUnitTest
{
    public class SaveLessonUnitTestCommand : IRequest<LessonUnitTestViewModel>
    {
        public LessonUnitTestViewModel Vm { get; set; }
    }

    public class SaveLessonUnitTestCommandHandler : IRequestHandler<SaveLessonUnitTestCommand, LessonUnitTestViewModel>
    {
        private readonly ILessonUnitTestQueryRepository _lessonUnitTestQueryRepository;
        private readonly ILessonUnitTestCommandRepository _lessonUnitTestCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILessonDesignService _lessonDesignService;
        private readonly ILogger<SaveLessonUnitTestCommandHandler> _logger;

        public SaveLessonUnitTestCommandHandler(
            ILessonUnitTestQueryRepository lessonUnitTestQueryRepository, 
            ILessonUnitTestCommandRepository lessonUnitTestCommandRepository, 
            IUserQueryRepository userQueryRepository,
            ICurrentUserService currentUserService,
            ILessonDesignService lessonDesignService,
            ILogger<SaveLessonUnitTestCommandHandler> logger)
        {
            this._lessonUnitTestCommandRepository = lessonUnitTestCommandRepository;
            this._lessonUnitTestQueryRepository = lessonUnitTestQueryRepository;
            this._userQueryRepository = userQueryRepository;
            this._currentUserService = currentUserService;
            this._lessonDesignService = lessonDesignService;
            this._logger = logger;
        }


        public async Task<LessonUnitTestViewModel> Handle(SaveLessonUnitTestCommand request, CancellationToken cancellationToken)
        {
            var response = new LessonUnitTestViewModel();

            try
            {
                var currentUser = await _userQueryRepository.GetById(_currentUserService.UserId.Value, cancellationToken);

                var lessonUnitTest = await _lessonUnitTestQueryRepository.GetById(request.Vm.Id, cancellationToken);

                if (lessonUnitTest == null)
                {
                    lessonUnitTest = new LessonUnitTest()
                    {
                        LessonId = request.Vm.LessonId,
                        Name = request.Vm.Name,
                        StudentGuide = request.Vm.StudentGuide,
                        LessonUnitTestTopics = new HashSet<LessonUnitTestTopic>()
                    };

                    _lessonDesignService.AddNewLessonUnitTestTopics(request.Vm.Topics, lessonUnitTest, currentUser);
                }
                else
                {
                    lessonUnitTest.Name = request.Vm.Name;
                    lessonUnitTest.StudentGuide = request.Vm.StudentGuide;

                    var savedTopics = lessonUnitTest.LessonUnitTestTopics.ToList();

                    //Add Newly Added Topics
                    var newlyAddedTopics = request.Vm.Topics.Where(x => !savedTopics.Any(t => t.Id == x.Id)).ToList();

                    _lessonDesignService.AddNewLessonUnitTestTopics(newlyAddedTopics, lessonUnitTest, currentUser);

                    //Updated ExistingTopics
                    var updatedTopics = request.Vm.Topics.Where(x => savedTopics.Any(t => t.Id == x.Id)).ToList();

                    _lessonDesignService.UpdatedLessonUnitTest(updatedTopics, lessonUnitTest, currentUser);

                    //Delete deleted topics
                    var deletedTopics = savedTopics.Where(x => !request.Vm.Topics.Any(t => t.Id == x.Id)).ToList();

                    _lessonDesignService.DeleteLessonUnitTestTopics(deletedTopics);
                }

                await _lessonUnitTestCommandRepository.UpdateAsync(lessonUnitTest, cancellationToken);

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
