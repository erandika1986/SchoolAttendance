using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Events
{
    public class TodoItemCompletedEvent : BaseEvent
    {
        public TodoItemCompletedEvent(User item)
        {
            Item = item;
        }

        public User Item { get; }
    }
}
