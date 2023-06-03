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
        LessonListFilterMasterData GetLessonDesignDropdownMasterData(string userName);
        Task<LessonViewModel> CreateNewLesson(string userName);
        LessonViewModel GetLessonById(int id);
        Task<ResponseViewModel> SaveLesson(LessonViewModel vm, string userName);
        Task<LessonTopicViewModel> SaveLessonTopic(LessonTopicViewModel vm, string userName);
        Task<LessonLectureViewModel> SaveLessonLecture(LessonLectureViewModel vm, string userName);
        Task<LessonUnitTestViewModel> SaveLessonUnitTest(LessonUnitTestViewModel vm, string userName);
        Task<LessonUnitTestViewModel> SaveUnitTestDetail(LessonUnitTestViewModel vm, string userName);
        Task<LessonUnitTestTopicViewModel> SaveLessonUnitTestTopic(LessonUnitTestTopicViewModel vm, string userName);
        Task<ResponseViewModel> CopyLesson(int id, string userName);
        Task<ResponseViewModel> DeleteLesson(int id, string userName);
        Task<ResponseViewModel> DeleteTopic(int id, string userName);
        Task<ResponseViewModel> DeleteLecture(int id, string userName);
        Task<ResponseViewModel> DeleteLectureContent(int id, string userName);
        PaginatedItemsViewModel<BasicLessonViewModel> GetNotPublishedLesson(LessonFilter filter, string userName);
        Task<ResponseViewModel> PublishLesson(int id, string userName);
        Task<ResponseViewModel> SaveLessonDetail(LessonDetailViewModel vm, string userName);
        Task<ResponseViewModel> SaveLessonPrerequisite(LessonPrerequisiteForm vm, string userName);
        Task<ResponseViewModel> SaveLessonLearningOutcome(LessonOutcomeForm vm, string userName);
        Task<LessonTopicViewModel> CreateNewLessonTopic(int lessonId, string userName);
        Task<LessonLectureViewModel> CreateNewLecture(int topicId, string userName);
        Task<ResponseViewModel> SaveLessonTopicName(LessonTopicViewModel vm);
        Task<LessonLectureViewModel> SaveLessonLectureContent(LessonLectureViewModel vm);
        Task<ResponseViewModel> SaveLessonLectureName(LessonLectureViewModel vm);
        Task<LessonLectureViewModel> UploadTopicContentFile(LessonLectureViewModel vm, IFormFile file, string userName);

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
