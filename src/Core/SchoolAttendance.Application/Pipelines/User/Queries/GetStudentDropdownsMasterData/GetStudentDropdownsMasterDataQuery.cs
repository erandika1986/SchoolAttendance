using MediatR;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllAcademicLevels;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllAcademicYears;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
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
        private readonly IAcademicYearQueryRepository _academicYearQueryRepository;
        private readonly IMediator _mediator;

        public GetStudentDropdownsMasterDataQueryHandler(
            IAcademicYearQueryRepository academicYearQueryRepository,
            IMediator mediator)
        {
            this._academicYearQueryRepository = academicYearQueryRepository;
            this._mediator = mediator;

        }


        public async Task<StudentListDropDownMasterData> Handle(GetStudentDropdownsMasterDataQuery request, CancellationToken cancellationToken)
        {
            var response = new StudentListDropDownMasterData();

            var academicYears = await _mediator.Send(new GetAllAcademicYearsQuery());
            var academicLevels = await _mediator.Send(new GetAllAcademicLevelsQuery());

            response.AcademicYears.AddRange(academicYears);
            response.Grades.AddRange(academicLevels);
            response.CurrentAcademicYear = (await _academicYearQueryRepository.Query(x => x.IsCurrentYear == true)).FirstOrDefault().Id;
            response.SelectedClassId = 0;
            response.SelectedGradeId = 0;

            return response;
        }
    }
}
