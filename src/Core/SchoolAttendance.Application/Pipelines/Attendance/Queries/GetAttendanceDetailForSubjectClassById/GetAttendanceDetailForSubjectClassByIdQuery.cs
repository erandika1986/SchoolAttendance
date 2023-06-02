using MediatR;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.Attendance.Queries.GetAttendanceDetailForSubjectClassById
{
    public record GetAttendanceDetailForSubjectClassByIdQuery(int id) : IRequest<SubjectAttendaceViewModel>
    {
    }

    public class GetAttendanceDetailForSubjectClassByIdQueryHandler : IRequestHandler<GetAttendanceDetailForSubjectClassByIdQuery, SubjectAttendaceViewModel>
    {
        private readonly ISubjectAttendanceQueryRepository _subjectAttendanceQueryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAttendanceService _attendanceService;


        public GetAttendanceDetailForSubjectClassByIdQueryHandler(
            ISubjectAttendanceQueryRepository subjectAttendanceQueryRepository, 
            ICurrentUserService currentUserService,
            IAttendanceService attendanceService)
        {
            this._subjectAttendanceQueryRepository = subjectAttendanceQueryRepository;
            this._currentUserService = currentUserService;
            this._attendanceService = attendanceService;
        }


        public async Task<SubjectAttendaceViewModel> Handle(GetAttendanceDetailForSubjectClassByIdQuery request, CancellationToken cancellationToken)
        {
            var subjectAttendance = await _subjectAttendanceQueryRepository
                .GetById(request.id, cancellationToken);

            var response = _attendanceService
                .GetSubjectAttendaceViewModel(subjectAttendance);

            response.StudentsAttendance = response
                .StudentsAttendance
                .OrderBy(x => x.StudentName)
                .ToList();

            return response;
        }
    }
}
