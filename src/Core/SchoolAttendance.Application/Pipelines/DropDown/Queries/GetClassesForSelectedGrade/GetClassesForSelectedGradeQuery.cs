using MediatR;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.DropDown.Queries.GetClassesForSelectedGrade
{
    public record GetClassesForSelectedGradeQuery(int academicYearId, int gradeId) : IRequest<List<DropDownViewModel>>
    {
    }

    public class GetClassesForSelectedGradeQueryHandler : IRequestHandler<GetClassesForSelectedGradeQuery, List<DropDownViewModel>>
    {
        private readonly IClassQueryRepository _classQueryRepository;

        public GetClassesForSelectedGradeQueryHandler(IClassQueryRepository classQueryRepository)
        {
            this._classQueryRepository = classQueryRepository;
        }

        public async Task<List<DropDownViewModel>> Handle(GetClassesForSelectedGradeQuery request, CancellationToken cancellationToken)
        {
            var classes = await _classQueryRepository.GetClassesForSelectedGrade(request.academicYearId, request.gradeId, cancellationToken);

            var Vms = classes
                .Distinct()
                .Select(d => new DropDownViewModel() { Id = d.Id, Name = d.Name })
                .OrderBy(x => x.Name)
                .ToList();

            return Vms;
        }
    }
}
