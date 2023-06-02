using MediatR;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.Grade.Queries.GetGradeList
{
    public record GetGradeListQuery : IRequest<List<GradeViewModel>>
    {
    }

    public class GetGradeListQueryHandler : IRequestHandler<GetGradeListQuery, List<GradeViewModel>>
    {
        private readonly IGradeQueryRepository _gradeQueryRepository;

        public GetGradeListQueryHandler(IGradeQueryRepository gradeQueryRepository)
        {
            this._gradeQueryRepository = gradeQueryRepository;
        }

        public async Task<List<GradeViewModel>> Handle(GetGradeListQuery request, CancellationToken cancellationToken)
        {
            var response = new List<GradeViewModel>();

            var grades = (await _gradeQueryRepository.Query(x => x.IsActive == true))
                .ToList();

            foreach (var item in grades)
            {
                response.Add(new GradeViewModel()
                {
                    GradeSubjectsText = string.Join(",", item.GradeSubjects.Select(x => x.Subject.Name).ToList()),
                    GradeSubjects = item.GradeSubjects.Select(x => x.SubjectId).ToList(),
                    Id = item.Id,
                    LevelHeadId = item.LevelHeadId.HasValue ? item.LevelHeadId.Value : 0,
                    LevelHeadName = item.LevelHeadId.HasValue ? item.LevelHead.FullName : string.Empty,
                    Name = item.Name
                });
            }

            return response;
        }
    }
}
