using Microsoft.Extensions.Configuration;
using SchoolAttendance.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Infrastructure.Reports
{
  public class ReportFactory
  {
    public BaseReportGenerator GetReportGenerator(Dictionary<string, string> reportParams, ISchoolAttendanceContext db, IConfiguration config)
    {
      switch (reportParams["ReportType"])
      {
        case "WeeklyAttedance":
          {
            return new WeeklyAttendanceReportGenerator(reportParams, db, config);
          }
      }

      throw new Exception("Unable to find the matching Report Generation class.");
    }
  }
}
