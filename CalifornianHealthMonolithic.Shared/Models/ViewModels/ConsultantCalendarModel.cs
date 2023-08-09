using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalifornianHealthMonolithic.Shared.Models.ViewModels
{
    public class ConsultantCalendarModel
    {
        public int id { get; set; }
        public string? consultantName { get; set; }
        public List<DateTime>? availableDates { get; set; }
    }
}