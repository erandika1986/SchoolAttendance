using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses.Common
{
    public class ResultViewModel
    {
        internal ResultViewModel(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }

        public static ResultViewModel Success()
        {
            return new ResultViewModel(true, Array.Empty<string>());
        }

        public static ResultViewModel Failure(IEnumerable<string> errors)
        {
            return new ResultViewModel(false, errors);
        }
    }
}
