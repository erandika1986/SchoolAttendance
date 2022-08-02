using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Entities
{
    public class UserRole 
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime AssignedOn { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
