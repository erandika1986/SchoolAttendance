using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Helpers
{
	public static class DateTimeHelper
	{
    public static DateTime ConvertToLocalTime(this DateTime utcTime,string timeZoneId)
    {

      TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
      var hongKongTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZoneInfo);

      return hongKongTime;
    }

    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
    {
      int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
      return dt.AddDays(-1 * diff).Date;
    }
  }
}
