using MediatR;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllSubjects
{
    public class GetAllSubjectsQuery : IRequest<List<CheckBoxViewModel>>
    {
    }

    public class GetAllSubjectsQueryHandler : IRequestHandler<GetAllSubjectsQuery, List<CheckBoxViewModel>>
    {
        private readonly ISubjectQueryRepository _subjectQueryRepository;

        public GetAllSubjectsQueryHandler(ISubjectQueryRepository subjectQueryRepository)
        {
            this._subjectQueryRepository = subjectQueryRepository;
        }


        public async Task<List<CheckBoxViewModel>> Handle(GetAllSubjectsQuery request, CancellationToken cancellationToken)
        {
            var allSubjects = (await _subjectQueryRepository.GetAll(cancellationToken))
                .Select(x => 
                        new CheckBoxViewModel() 
                        { 
                            Id = x.Id, 
                            Name = x.Name 
                        })
                .OrderBy(s => s.Name).ToList();

            return allSubjects;
        }
    }
}
