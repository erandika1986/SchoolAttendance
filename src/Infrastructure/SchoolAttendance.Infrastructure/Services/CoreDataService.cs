using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Infrastructure.Services
{
  public class CoreDataService : ICoreDataService
  {
    private readonly ISchoolAttendanceContext db;
    private readonly ILogger<ICoreDataService> logger;


    public CoreDataService(ISchoolAttendanceContext db, ILogger<ICoreDataService> logger)
    {
      this.db = db;
      this.logger = logger;
    }

    public AcademicYear GetCurrentAcademicYear()
    {
      return db.AcademicYears.FirstOrDefault(x => x.IsCurrentYear == true);
    }

    public User GetLoggedInUserByUserName(string userName)
    {
      var user = db.Users.FirstOrDefault(t => t.Username.ToLower() == userName.ToLower());

      return user;
    }
  }
}
