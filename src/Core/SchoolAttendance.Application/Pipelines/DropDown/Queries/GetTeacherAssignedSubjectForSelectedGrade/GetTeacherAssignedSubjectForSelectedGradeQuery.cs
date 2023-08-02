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

namespace SchoolAttendance.Application.Pipelines.DropDown.Queries.GetTeacherAssignedSubjectForSelectedGrade
{
    public record GetTeacherAssignedSubjectForSelectedGradeQuery(int gradeId): IRequest<List<DropDownViewModel>>
    {
    }

    public class GetTeacherAssignedSubjectForSelectedGradeQueryHandler : IRequestHandler<GetTeacherAssignedSubjectForSelectedGradeQuery, List<DropDownViewModel>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IClassSubjectQueryRepository _classSubjectQueryRepository;
        
        public GetTeacherAssignedSubjectForSelectedGradeQueryHandler(
            ICurrentUserService currentUserService, 
            IClassSubjectQueryRepository classSubjectQueryRepository)
        {
            this._currentUserService = currentUserService;
            this._classSubjectQueryRepository = classSubjectQueryRepository;
        }

        public async Task<List<DropDownViewModel>> Handle(GetTeacherAssignedSubjectForSelectedGradeQuery request, CancellationToken cancellationToken)
        {
            var response = (await 
                _classSubjectQueryRepository
                .Query(x => 
                        x.SubjectTeacherId == _currentUserService.UserId.Value && 
                        x.Class.GradeId == request.gradeId))
              .Select(x => x.Subject)
              .Distinct()
              .Select(s => 
                    new DropDownViewModel() 
                    { 
                        Id = s.Id, 
                        Name = s.Name 
                    }).ToList();

            return response;
        }
    }
}
