
using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Common.Interfaces
{
    public interface ICoreDataService
    {
        User GetLoggedInUserByUserName(string userName);
        User GetLoggedInUserByUserId(int id);
        AcademicYear GetCurrentAcademicYear();
    }
}
