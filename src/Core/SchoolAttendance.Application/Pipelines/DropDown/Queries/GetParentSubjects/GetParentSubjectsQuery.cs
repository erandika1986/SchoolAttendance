using MediatR;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.DropDown.Queries.GetParentSubjects
{
    public class GetParentSubjectsQuery : IRequest<List<DropDownViewModel>>
    {
    }

    public class GetParentSubjectsQueryHandler : IRequestHandler<GetParentSubjectsQuery, List<DropDownViewModel>>
    {
        private readonly ISubjectQueryRepository _subjectQueryRepository;

        public GetParentSubjectsQueryHandler(ISubjectQueryRepository subjectQueryRepository)
        {
            this._subjectQueryRepository = subjectQueryRepository;
        }
        public async Task<List<DropDownViewModel>> Handle(GetParentSubjectsQuery request, CancellationToken cancellationToken)
        {
            var response = new List<DropDownViewModel>();

            var parentSubjects = (await _subjectQueryRepository
                .Query(x => 
                    x.IsParentSubject == true && 
                    x.IsActive == true)).Select(x => new DropDownViewModel() { Id = x.Id, Name = x.Name });

            response.AddRange(parentSubjects);

            return response;
        }
    }
}
