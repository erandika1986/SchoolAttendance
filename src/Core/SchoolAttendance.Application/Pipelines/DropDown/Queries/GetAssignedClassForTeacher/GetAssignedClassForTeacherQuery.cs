using MediatR;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAssignedClassForTeacher
{
    public record GetAssignedClassForTeacherQuery(int academicYear, int gradeId, int teacherId) : IRequest<List<DropDownViewModel>>
    {
    }

    public class GetAssignedClassForTeacherQueryHandler : IRequestHandler<GetAssignedClassForTeacherQuery, List<DropDownViewModel>>
    {
        private readonly IGradeQueryRepository _gradeQueryRepository;

        public GetAssignedClassForTeacherQueryHandler(IGradeQueryRepository gradeQueryRepository)
        {
            this._gradeQueryRepository = gradeQueryRepository;
        }


        public async Task<List<DropDownViewModel>> Handle(GetAssignedClassForTeacherQuery request, CancellationToken cancellationToken)
        {
            var classes = (await _gradeQueryRepository
                            .Query(g => g.IsActive == true && g.Id == request.gradeId))
                            .SelectMany(cl => cl.Classes)
                            .Where(t => 
                                t.ClassTeacherId == request.teacherId && 
                                t.AcademicYear == request.academicYear
                             )
                            .Distinct()
                            .Select(d => new DropDownViewModel() 
                            { 
                                Id = d.Id, 
                                Name = d.Name 
                            })
                            .OrderBy(x => x.Name)
                            .ToList();

            return classes;
        }
    }
}
