using Microsoft.Extensions.Configuration;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Infrastructure.Reports
{
  public class BaseReportGenerator
  {
    protected readonly Dictionary<string, string> reportParams;
    protected readonly ISchoolAttendanceContext db;
    protected readonly IConfiguration config;


    public BaseReportGenerator(Dictionary<string, string> reportParams, ISchoolAttendanceContext db, IConfiguration config)
    {
      this.reportParams = reportParams;
      this.db = db;
      this.config = config;
    }

    public virtual DownloadFileModel GeneratePDFReport()
    {
      throw new NotImplementedException("Method has not implemented.");
    }

    public virtual DownloadFileModel GenerateExcelReport()
    {
      throw new NotImplementedException("Method has not implemented.");
    }
  }
}
