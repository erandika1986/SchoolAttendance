using MediatR;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.User.Queries.GetStudentDropdownsMasterData
{
    public record GetStudentDropdownsMasterDataQuery:IRequest<StudentListDropDownMasterData>
    {
    }

    public class GetStudentDropdownsMasterDataQueryHandler : IRequestHandler<GetStudentDropdownsMasterDataQuery, StudentListDropDownMasterData>
    {
        private readonly IDropDownService _dropDownService;
        private readonly IAcademicYearQueryRepository _academicYearQueryRepository;

        public GetStudentDropdownsMasterDataQueryHandler(
            IDropDownService dropDownService,
            IAcademicYearQueryRepository academicYearQueryRepository)
        {
            this._dropDownService = dropDownService;
            this._academicYearQueryRepository = academicYearQueryRepository;
        }


        public async Task<StudentListDropDownMasterData> Handle(GetStudentDropdownsMasterDataQuery request, CancellationToken cancellationToken)
        {
            var response = new StudentListDropDownMasterData();

            response.AcademicYears.AddRange(_dropDownService.GetAllAcademicYears());
            response.Grades.AddRange(_dropDownService.GetAllAcademicLevels());
            response.CurrentAcademicYear = (await _academicYearQueryRepository.Query(x => x.IsCurrentYear == true)).FirstOrDefault().Id;
            response.SelectedClassId = 0;
            response.SelectedGradeId = 0;

            return response;
        }
    }
}
