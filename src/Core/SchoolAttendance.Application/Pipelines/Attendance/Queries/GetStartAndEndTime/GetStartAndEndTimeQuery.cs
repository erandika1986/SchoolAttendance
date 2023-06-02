using MediatR;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.Attendance.Queries.GetStartAndEndTime
{
    public record GetStartAndEndTimeQuery(AttendanceFilterViewModel filter): IRequest<AttendanceFilterViewModel>
    {
    }

    public class GetStartAndEndTimeQueryHandler : IRequestHandler<GetStartAndEndTimeQuery, AttendanceFilterViewModel>
    {
        private readonly IClassSubjectTimeTableQueryRepository _classSubjectTimeTableQueryRepository;

        public GetStartAndEndTimeQueryHandler(IClassSubjectTimeTableQueryRepository classSubjectTimeTableQueryRepository)
        {
            this._classSubjectTimeTableQueryRepository = classSubjectTimeTableQueryRepository;
        }

        public async Task<AttendanceFilterViewModel> Handle(GetStartAndEndTimeQuery request, CancellationToken cancellationToken)
        {
            var date = new DateTime(request.filter.Year, request.filter.Month, request.filter.Day, 0, 0, 0);

            var timeTableRecord = ( 
                await _classSubjectTimeTableQueryRepository
                .Query(x => 
                            x.ClassId == request.filter.ClassId && 
                            x.SubjectId == request.filter.SubjectId && 
                            x.DayId == (int)date.DayOfWeek)
                ).FirstOrDefault();

            if (timeTableRecord != null)
            {
                request.filter.StartHour = timeTableRecord.StartTime.Hour;
                request.filter.StartMin = timeTableRecord.StartTime.Minute;
                request.filter.EndHour = timeTableRecord.EndTime.Hour;
                request.filter.EndMin = timeTableRecord.EndTime.Minute;
            }

            return request.filter;
        }
    }
}
