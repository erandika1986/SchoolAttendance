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

namespace SchoolAttendance.Application.Pipelines.Grade.Commands.SaveGradeDetail
{
    public record SaveGradeDetailCommand(GradeViewModel vm) : IRequest<ResponseViewModel>
    {
    }

    public class SaveGradeDetailCommandHandler : IRequestHandler<SaveGradeDetailCommand, ResponseViewModel>
    {
        private readonly IGradeQueryRepository _gradeQueryRepository;
        private readonly IGradeCommandRepository _gradeCommandRepository;
        private readonly IAcademicYearQueryRepository _academicYearQueryRepository;
        private readonly IGradeSubjectCommandRepository _gradeSubjectCommandRepository;
        private readonly IClassSubjectCommandRepository _classSubjectCommandRepository;
        private readonly ILogger<SaveGradeDetailCommandHandler> _logger;

        public SaveGradeDetailCommandHandler(
            IGradeQueryRepository gradeQueryRepository,
            IGradeCommandRepository gradeCommandRepository,
            IAcademicYearQueryRepository academicYearQueryRepository,
            IGradeSubjectCommandRepository gradeSubjectCommandRepository,
            IClassSubjectCommandRepository classSubjectCommandRepository,
            ILogger<SaveGradeDetailCommandHandler> logger)
        {
            this._gradeQueryRepository = gradeQueryRepository;
            this._gradeCommandRepository = gradeCommandRepository;
            this._academicYearQueryRepository = academicYearQueryRepository;
            this._gradeSubjectCommandRepository = gradeSubjectCommandRepository;
            this._classSubjectCommandRepository = classSubjectCommandRepository;
            this._logger = logger;
        }

        public async Task<ResponseViewModel> Handle(SaveGradeDetailCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel();
            try
            {
                var grade = await _gradeQueryRepository
                    .GetById(request.vm.Id, cancellationToken);

                grade.LevelHeadId = request.vm.LevelHeadId;

                var currentYear = (await _academicYearQueryRepository
                    .Query(x => x.IsCurrentYear))
                    .FirstOrDefault();

                var gradeAllClasses = grade
                    .Classes
                    .Where(x => x.AcademicYear == currentYear.Id)
                    .ToList();

                var savedAssignedSubject = grade
                    .GradeSubjects
                    .Select(x => x.Subject)
                    .ToList();

                var deletedSubjects =
                    (
                        from d in savedAssignedSubject
                        where !request.vm.GradeSubjects.Any(x => x == d.Id)
                        select d
                    ).ToList();

                var assignedSubjects = gradeAllClasses
                    .SelectMany(x => x.ClassSubjects)
                    .Select(x => x.SubjectId)
                    .Distinct()
                    .ToList();

                var alreadyAssignedDeletedSubject = assignedSubjects
                    .Intersect(deletedSubjects.Select(x => x.Id).ToList())
                    .ToList();

                if (alreadyAssignedDeletedSubject.Count > 0)
                {
                    response.IsSuccess = false;
                    response.Message = "You are not allow to delete the subjects after they have assigned to the classes. Please remove";

                    return response;
                }
                else
                {
                    foreach (var item in deletedSubjects)
                    {
                        var gradeSubject = grade
                            .GradeSubjects
                            .FirstOrDefault(x => x.SubjectId == item.Id);

                        await _gradeSubjectCommandRepository
                            .DeleteAsync(gradeSubject, cancellationToken);
                    }
                }

                var newlyAddedSubjects =
                    (
                        from ins in request.vm.GradeSubjects
                        where !savedAssignedSubject.Any(x => x.Id == ins)
                        select ins
                    ).ToList();


                foreach (var item in newlyAddedSubjects)
                {
                    await _gradeSubjectCommandRepository
                        .AddAsync(new GradeSubject()
                        {
                            SubjectId = item
                        }, cancellationToken);

                    foreach (var cl in gradeAllClasses)
                    {
                        await _classSubjectCommandRepository
                            .AddAsync(new ClassSubject()
                            {
                                IsActive = true,
                                SubjectId = item
                            }, cancellationToken);
                    }
                }



                response.IsSuccess = true;
                response.Message = "Grade subject has been updated successfully.";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while updating the grade subject.";
            }

            return response;
        }
    }

}
