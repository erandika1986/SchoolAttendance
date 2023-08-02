using MediatR;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllAcademicYears
{
    public record GetAllAcademicYearsQuery : IRequest<List<DropDownViewModel>>
    {
    }

    public class GetAllAcademicYearsQueryHandler : IRequestHandler<GetAllAcademicYearsQuery, List<DropDownViewModel>>
    {
        private readonly IAcademicYearQueryRepository _academicYearQueryRepository;

        public GetAllAcademicYearsQueryHandler(IAcademicYearQueryRepository academicYearQueryRepository)
        {
            this._academicYearQueryRepository = academicYearQueryRepository;
        }

        public async Task<List<DropDownViewModel>> Handle(GetAllAcademicYearsQuery request, CancellationToken cancellationToken)
        {
            var academicYears = await _academicYearQueryRepository.GetAll(cancellationToken);

            return academicYears.Select(x => new DropDownViewModel() { Id = x.Id, Name = x.Name })
              .OrderBy(x => x.Name)
              .ToList();
        }
    }
}
