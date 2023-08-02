using MediatR;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAssignedClassSubjectForTeacher
{
    public record GetAssignedClassSubjectForTeacherQuery(int classId, int teacherId) : IRequest<List<DropDownViewModel>>
    {
    }

    public class GetAssignedClassSubjectForTeacherQueryHandler : IRequestHandler<GetAssignedClassSubjectForTeacherQuery, List<DropDownViewModel>>
    {
        private readonly IClassSubjectQueryRepository _classSubjectQueryRepository;

        public GetAssignedClassSubjectForTeacherQueryHandler(IClassSubjectQueryRepository classSubjectQueryRepository)
        {
            this._classSubjectQueryRepository = classSubjectQueryRepository;
        }

        public async Task<List<DropDownViewModel>> Handle(GetAssignedClassSubjectForTeacherQuery request, CancellationToken cancellationToken)
        {
            var assignedSubject = (await _classSubjectQueryRepository
                .Query(x => 
                            x.ClassId == request.classId && 
                            x.SubjectTeacherId == request.teacherId
                       ))
              .Select(s => new 
                            DropDownViewModel() 
                            { 
                                Id = s.Subject.Id, 
                                Name = s.Subject.Name 
                            })
              .OrderBy(x => x.Name)
              .ToList();

            return assignedSubject;
        }
    }
}
