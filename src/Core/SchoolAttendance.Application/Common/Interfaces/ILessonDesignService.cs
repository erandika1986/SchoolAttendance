using Microsoft.AspNetCore.Http;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Common.Interfaces
{
    public interface ILessonDesignService
    {
        void HandleLessonClasses(Lesson lesson, LessonDetailViewModel vm);
        void HandleLessonPrerequisites(Lesson lesson, LessonPrerequisiteForm vm);
        void HandleLessonLearningOutcome(Lesson lesson, LessonOutcomeForm vm);
        void AddNewLessonUnitTestTopics(List<LessonUnitTestTopicViewModel> topics, LessonUnitTest lessonUnitTest, User currentUser);
        void UpdatedLessonUnitTest(List<LessonUnitTestTopicViewModel> topics, LessonUnitTest lessonUnitTest, User currentUser);
        void DeleteLessonUnitTestTopics(List<LessonUnitTestTopic> deletedTopics);
        void AddNewLessonTopicQuestions(List<LessonUnitTestTopicQuestionViewModel> questions, LessonUnitTestTopic lessonUnitTestTopic, User currentUser);
        void UpdateLessonTopicQuestions(List<LessonUnitTestTopicQuestionViewModel> questions, LessonUnitTestTopic lessonUnitTestTopic, User currentUser);
        void DeleteLessonTopicQuestions(List<LessonUnitTestTopicQuestion> topicQuestions);

    }
}
