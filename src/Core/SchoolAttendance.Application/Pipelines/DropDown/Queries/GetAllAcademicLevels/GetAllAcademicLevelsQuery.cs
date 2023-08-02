using MediatR;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllAcademicLevels
{
    public record GetAllAcademicLevelsQuery() : IRequest<List<DropDownViewModel>>
    {
    }

    public class GetAllAcademicLevelsQueryHandler : IRequestHandler<GetAllAcademicLevelsQuery, List<DropDownViewModel>>
    {
        private readonly IGradeQueryRepository _gradeQueryRepository;

        public GetAllAcademicLevelsQueryHandler(IGradeQueryRepository gradeQueryRepository)
        {
            this._gradeQueryRepository = gradeQueryRepository;
        }

        public async Task<List<DropDownViewModel>> Handle(GetAllAcademicLevelsQuery request, CancellationToken cancellationToken)
        {
            var grades = await _gradeQueryRepository.GetAll(cancellationToken);

            return grades.Select(x => new DropDownViewModel() { Id = x.Id, Name = x.Name })
              .OrderBy(x => x.Id)
              .ToList();
        }
    }
}
