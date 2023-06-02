using MediatR;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.Subject.Queries.GetSubjectList
{
    public record GetSubjectListQuery(string searchText, int currentPage, int pageSize, bool status) : IRequest<PaginatedItemsViewModel<SubjectViewModel>>
    {
    }

    public class GetSubjectListQueryHandler : IRequestHandler<GetSubjectListQuery, PaginatedItemsViewModel<SubjectViewModel>>
    {
        private readonly ISubjectQueryRepository _subjectQueryRepository;

        public GetSubjectListQueryHandler(ISubjectQueryRepository subjectQueryRepository)
        {
            this._subjectQueryRepository = subjectQueryRepository;
        }
        public async Task<PaginatedItemsViewModel<SubjectViewModel>> Handle(GetSubjectListQuery request, CancellationToken cancellationToken)
        {
            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;
            var vms = new List<SubjectViewModel>();

            var subjects = (await _subjectQueryRepository
              .Query(x => x.IsActive == request.status))
              .OrderBy(s => s.Name);

            if (!string.IsNullOrEmpty(request.searchText))
            {
                subjects = subjects.Where(x => x.Name.Contains(request.searchText))
                  .OrderBy(s => s.Name);
            }

            totalRecordCount = subjects.Count();
            totalPages = (double)totalRecordCount / request.pageSize;
            totalPageCount = (int)Math.Ceiling(totalPages);

            var subjectList = subjects
                .Skip((request.currentPage - 1) * request.pageSize)
                .Take(request.pageSize)
                .ToList();

            subjectList.ForEach(s =>
            {
                vms.Add(s.ToVm());
            });

            var container = new PaginatedItemsViewModel<SubjectViewModel>(request.currentPage, request.pageSize, totalPageCount, totalRecordCount, vms);

            return container;
        }
    }
}
