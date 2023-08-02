using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Helpers;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Infrastructure.Services
{
    public class LessonDesignService : ILessonDesignService
    {



        private readonly ISchoolAttendanceContext db;
        private readonly ILogger<ILessonDesignService> logger;
        private readonly IConfiguration config;
        private readonly ICoreDataService coreDataService;
        private readonly IAzureBlobService azureBlobService;

        private readonly IUserQueryRepository _userQueryRepository;
        private readonly ICurrentUserService _currentUserService;



        public LessonDesignService(
            ISchoolAttendanceContext db,
            ILogger<ILessonDesignService> logger,
            IConfiguration config,
            ICoreDataService coreDataService,
            IAzureBlobService azureBlobService,
            IUserQueryRepository userQueryRepository,
            ICurrentUserService currentUserService
            )
        {
            this.db = db;
            this.logger = logger;
            this.config = config;
            this.coreDataService = coreDataService;
            this.azureBlobService = azureBlobService;
            this._userQueryRepository = userQueryRepository;
            this._currentUserService = currentUserService;
        }



        public void HandleLessonClasses(Lesson lesson, LessonDetailViewModel vm)
        {
            var savedAssinedClasses = lesson.LessonAssignedClasses.ToList();

            var newAssignedClasses = vm.AssignedClasses.Where(x => !savedAssinedClasses.Any(s => s.ClassId == x)).ToList();

            newAssignedClasses.ForEach(cl =>
            {

                lesson.LessonAssignedClasses.Add(new LessonAssignedClass() { ClassId = cl });
            });

            var deletedClasses = savedAssinedClasses.Where(x => !vm.AssignedClasses.Any(a => a == x.ClassId)).ToList();

            foreach (var item in deletedClasses)
            {
                db.LessonAssignedClasses.Remove(item);
            }
        }

        public void HandleLessonLearningOutcome(Lesson lesson, LessonOutcomeForm vm)
        {
            var savedLearningOutcomes = lesson.LessonLearningOutcomes.ToList();

            var newLearningOutcomes = vm.LessonOutcomes.Where(x => !savedLearningOutcomes.Any(s => s.Id == x.Id)).ToList();

            newLearningOutcomes.ForEach(lo =>
            {

                lesson.LessonLearningOutcomes.Add(new LessonLearningOutcome()
                {
                    LearningOutcome = lo.LessonOutcome,
                });
            });

            var deletedLearningOutcome = savedLearningOutcomes.Where(x => !vm.LessonOutcomes.Any(a => a.Id == x.Id)).ToList();

            foreach (var item in deletedLearningOutcome)
            {
                db.LessonLearningOutcomes.Remove(item);
            }
        }

        public void HandleLessonPrerequisites(Lesson lesson, LessonPrerequisiteForm vm)
        {
            var savedLessonPrerequisites = lesson.LessonPrerequisites.ToList();

            var newLessonPrerequisites = vm.LessonPrerequisites.Where(x => !savedLessonPrerequisites.Any(s => s.Id == x.Id)).ToList();

            newLessonPrerequisites.ForEach(lo =>
            {

                lesson.LessonPrerequisites.Add(new LessonPrerequisite()
                {
                    Prerequisite = lo.Prerequisite,
                });
            });

            var deletedLessonPrerequisites = savedLessonPrerequisites.Where(x => !vm.LessonPrerequisites.Any(a => a.Id == x.Id)).ToList();

            foreach (var item in deletedLessonPrerequisites)
            {
                db.LessonPrerequisites.Remove(item);
            }
        }

        public void AddNewLessonUnitTestTopics(List<LessonUnitTestTopicViewModel> topics, LessonUnitTest lessonUnitTest, User currentUser)
        {
            foreach (var topic in topics)
            {
                var unitTestTopic = new LessonUnitTestTopic()
                {
                    Instruction = topic.Instruction,
                    Name = topic.Name,
                    QuestionTypeId = topic.QuestionTypeId,
                    LessonUnitTestTopicQuestions = new HashSet<LessonUnitTestTopicQuestion>()
                };

                AddNewLessonTopicQuestions(topic.Questions, unitTestTopic, currentUser);

                lessonUnitTest.LessonUnitTestTopics.Add(unitTestTopic);
            }
        }

        public void UpdatedLessonUnitTest(List<LessonUnitTestTopicViewModel> topics, LessonUnitTest lessonUnitTest, User currentUser)
        {
            foreach (var topic in topics)
            {
                var unitTestTopic = lessonUnitTest.LessonUnitTestTopics.FirstOrDefault(x => x.Id == topic.Id);
                unitTestTopic.Instruction = topic.Instruction;
                unitTestTopic.Name = topic.Name;
                unitTestTopic.QuestionTypeId = topic.QuestionTypeId;

                foreach (var question in topic.Questions)
                {
                    var savedTopicQuestion = unitTestTopic.LessonUnitTestTopicQuestions.ToList();

                    //Add Newly Added Questions
                    var newlyAddedTopicQuestions = topic.Questions.Where(x => !savedTopicQuestion.Any(tq => tq.Id == x.Id)).ToList();
                    AddNewLessonTopicQuestions(newlyAddedTopicQuestions, unitTestTopic, currentUser);

                    //Update existing questions
                    var updatedTopicQuestions = topic.Questions.Where(x => savedTopicQuestion.Any(tq => tq.Id == x.Id)).ToList();
                    UpdateLessonTopicQuestions(updatedTopicQuestions, unitTestTopic, currentUser);

                    //Delete deleted LessonUnitTest Questions
                    var deletedTopicQuestions = savedTopicQuestion.Where(x => !topic.Questions.Any(q => q.Id == x.Id)).ToList();
                    DeleteLessonTopicQuestions(deletedTopicQuestions);
                }


                db.LessonUnitTestTopics.Update(unitTestTopic);
            }
        }

        public void DeleteLessonUnitTestTopics(List<LessonUnitTestTopic> deletedTopics)
        {
            foreach (var deletedTopic in deletedTopics)
            {
                DeleteLessonTopicQuestions(deletedTopic.LessonUnitTestTopicQuestions.ToList());

                db.LessonUnitTestTopics.Remove(deletedTopic);
            }
        }

        public void AddNewLessonTopicQuestions(List<LessonUnitTestTopicQuestionViewModel> questions, LessonUnitTestTopic lessonUnitTestTopic, User currentUser)
        {
            foreach (var q in questions)
            {
                var topicQuestion = new LessonUnitTestTopicQuestion()
                {
                    SequenceNo = q.SequenceNo,
                    Score = q.Score
                };

                if (lessonUnitTestTopic.QuestionTypeId == (int)SchoolAttendance.Domain.Enums.QuestionType.MCQ)
                {
                    topicQuestion.Question = new Question()
                    {
                        AcademicYearId = q.AcademicYearId,
                        GradeId = q.GradeId,
                        SubjectId = q.SubjectId,
                        OwnerId = currentUser.Id,
                        Question1 = q.MCQQuestion.Question,
                        QuestionRt = q.MCQQuestion.QuestionRT,
                        QuestionTypeId = lessonUnitTestTopic.QuestionTypeId
                    };

                    topicQuestion.Question.QuestionMcqteacherAnswers = new HashSet<QuestionMcqteacherAnswer>();

                    foreach (var item in q.MCQQuestion.TeacherAnswers)
                    {
                        var teacherMCQAnswer = new QuestionMcqteacherAnswer()
                        {
                            AnswerText = item.AnswerText,
                            AnswerTextRt = item.AnswerTextRT,
                            SequenceNo = item.SequenceNo,
                            IsCorrectAnswer = item.IsCorrectAnswer
                        };

                        topicQuestion.Question.QuestionMcqteacherAnswers.Add(teacherMCQAnswer);
                    }
                }
                else if (lessonUnitTestTopic.QuestionTypeId == (int)SchoolAttendance.Domain.Enums.QuestionType.OpenEnded)
                {
                    topicQuestion.Question = new Question()
                    {
                        AcademicYearId = q.AcademicYearId,
                        GradeId = q.GradeId,
                        SubjectId = q.SubjectId,
                        OwnerId = currentUser.Id,
                        Question1 = q.OpenEndedQuestion.Question,
                        QuestionRt = q.OpenEndedQuestion.QuestionRT,
                        QuestionTypeId = lessonUnitTestTopic.QuestionTypeId
                    };

                    topicQuestion.Question.QuestionOpenEndedTeacherAnswers = new HashSet<QuestionOpenEndedTeacherAnswer>();

                    foreach (var item in q.OpenEndedQuestion.TeacherAnswers)
                    {
                        var teacherOpenEndedAnswer = new QuestionOpenEndedTeacherAnswer()
                        {
                            AnswerText = item.AnswerText,
                            AnswerTextRt = item.AnswerTextRT
                        };

                        topicQuestion.Question.QuestionOpenEndedTeacherAnswers.Add(teacherOpenEndedAnswer);
                    }
                }

                lessonUnitTestTopic.LessonUnitTestTopicQuestions.Add(topicQuestion);

            }
        }

        public void UpdateLessonTopicQuestions(List<LessonUnitTestTopicQuestionViewModel> questions, LessonUnitTestTopic lessonUnitTestTopic, User currentUser)
        {
            var savedQuestions = lessonUnitTestTopic.LessonUnitTestTopicQuestions.ToList();

            //Add new questions to the topic
            var newQuestions = questions.Where(q => !savedQuestions.Any(sq => sq.Id == q.Id)).ToList();
            AddNewLessonTopicQuestions(newQuestions, lessonUnitTestTopic, currentUser);

            //Update existingQuestions
            var updatedQuestions = questions.Where(q => savedQuestions.Any(sq => sq.Id == q.Id)).ToList();

            foreach (var item in updatedQuestions)
            {
                var lessonUnitTestTopicQuestion = lessonUnitTestTopic.LessonUnitTestTopicQuestions.FirstOrDefault(x => x.Id == item.Id);

                lessonUnitTestTopicQuestion.SequenceNo = item.SequenceNo;
                lessonUnitTestTopicQuestion.Score = item.Score;



                if (lessonUnitTestTopic.QuestionTypeId == (int)SchoolAttendance.Domain.Enums.QuestionType.MCQ)
                {
                    lessonUnitTestTopicQuestion.Question.Question1 = item.MCQQuestion.Question;
                    lessonUnitTestTopicQuestion.Question.QuestionRt = item.MCQQuestion.QuestionRT;

                    var savedAnswers = lessonUnitTestTopicQuestion.Question.QuestionMcqteacherAnswers.ToList();

                    //Add newly added answers
                    var newAnswers = item.MCQQuestion.TeacherAnswers.Where(a => !savedAnswers.Any(sa => sa.Id == a.Id)).ToList();
                    foreach (var newAnswer in newAnswers)
                    {
                        var teacherMCQAnswer = new QuestionMcqteacherAnswer()
                        {
                            AnswerText = newAnswer.AnswerText,
                            AnswerTextRt = newAnswer.AnswerTextRT,
                            SequenceNo = newAnswer.SequenceNo,
                            IsCorrectAnswer = newAnswer.IsCorrectAnswer
                        };

                        lessonUnitTestTopicQuestion.Question.QuestionMcqteacherAnswers.Add(teacherMCQAnswer);
                    }

                    //Update modifed answers
                    var updatedAnswers = item.MCQQuestion.TeacherAnswers.Where(a => savedAnswers.Any(sa => sa.Id == a.Id)).ToList();
                    foreach (var updatedAnswer in updatedAnswers)
                    {
                        var teacherMCQAnswer = lessonUnitTestTopicQuestion.Question.QuestionMcqteacherAnswers.FirstOrDefault(x => x.Id == updatedAnswer.Id);

                        teacherMCQAnswer.AnswerText = updatedAnswer.AnswerText;
                        teacherMCQAnswer.AnswerTextRt = updatedAnswer.AnswerTextRT;
                        teacherMCQAnswer.SequenceNo = updatedAnswer.SequenceNo;
                        teacherMCQAnswer.IsCorrectAnswer = updatedAnswer.IsCorrectAnswer;

                        db.QuestionMcqteacherAnswers.Update(teacherMCQAnswer);
                    }

                    //Delete deleted answers
                    var deletedAnswers = savedAnswers.Where(da => !item.MCQQuestion.TeacherAnswers.Any(ta => ta.Id == da.Id)).ToList();

                    foreach (var deleteAnswer in deletedAnswers)
                    {
                        db.QuestionMcqteacherAnswers.Remove(deleteAnswer);
                    }

                }
                else if (lessonUnitTestTopic.QuestionTypeId == (int)SchoolAttendance.Domain.Enums.QuestionType.OpenEnded)
                {
                    lessonUnitTestTopicQuestion.Question.Question1 = item.OpenEndedQuestion.Question;
                    lessonUnitTestTopicQuestion.Question.QuestionRt = item.OpenEndedQuestion.QuestionRT;

                    var savedAnswers = lessonUnitTestTopicQuestion.Question.QuestionOpenEndedTeacherAnswers.ToList();

                    //Add newly added answers
                    var newAnswers = item.OpenEndedQuestion.TeacherAnswers.Where(a => !savedAnswers.Any(sa => sa.Id == a.Id)).ToList();
                    foreach (var newAnswer in newAnswers)
                    {
                        var teacherOpenEndedAnswer = new QuestionOpenEndedTeacherAnswer()
                        {
                            AnswerText = newAnswer.AnswerText,
                            AnswerTextRt = newAnswer.AnswerTextRT,
                        };

                        lessonUnitTestTopicQuestion.Question.QuestionOpenEndedTeacherAnswers.Add(teacherOpenEndedAnswer);
                    }

                    //Update modifed answers
                    var updatedAnswers = item.OpenEndedQuestion.TeacherAnswers.Where(a => savedAnswers.Any(sa => sa.Id == a.Id)).ToList();
                    foreach (var updatedAnswer in updatedAnswers)
                    {
                        var teacherOpenEndedAnswer = lessonUnitTestTopicQuestion.Question.QuestionOpenEndedTeacherAnswers.FirstOrDefault(x => x.Id == updatedAnswer.Id);

                        teacherOpenEndedAnswer.AnswerText = updatedAnswer.AnswerText;
                        teacherOpenEndedAnswer.AnswerTextRt = updatedAnswer.AnswerTextRT;

                        db.QuestionOpenEndedTeacherAnswers.Update(teacherOpenEndedAnswer);
                    }

                    //Delete deleted answers
                    var deletedAnswers = savedAnswers.Where(da => !item.OpenEndedQuestion.TeacherAnswers.Any(ta => ta.Id == da.Id)).ToList();

                    foreach (var deleteAnswer in deletedAnswers)
                    {
                        db.QuestionOpenEndedTeacherAnswers.Remove(deleteAnswer);
                    }
                }

                db.LessonUnitTestTopicQuestions.Update(lessonUnitTestTopicQuestion);

            }


            //Delete deleted Question from to topic
            var deletedQuestions = savedQuestions.Where(sq => !questions.Any(q => q.Id == sq.Id)).ToList();
            DeleteLessonTopicQuestions(deletedQuestions);
        }

        public void DeleteLessonTopicQuestions(List<LessonUnitTestTopicQuestion> topicQuestions)
        {
            foreach (var topicQuestion in topicQuestions)
            {
                if (topicQuestion.Question.QuestionTypeId == (int)SchoolAttendance.Domain.Enums.QuestionType.MCQ)
                {
                    foreach (var teacherAnswer in topicQuestion.Question.QuestionMcqteacherAnswers)
                    {
                        db.QuestionMcqteacherAnswers.Remove(teacherAnswer);
                    }
                }
                else if (topicQuestion.Question.QuestionTypeId == (int)SchoolAttendance.Domain.Enums.QuestionType.OpenEnded)
                {
                    foreach (var teacherAnswer in topicQuestion.Question.QuestionOpenEndedTeacherAnswers)
                    {
                        db.QuestionOpenEndedTeacherAnswers.Remove(teacherAnswer);
                    }
                }
                db.Questions.Remove(topicQuestion.Question);
                db.LessonUnitTestTopicQuestions.Remove(topicQuestion);
            }
        }

    }
}
