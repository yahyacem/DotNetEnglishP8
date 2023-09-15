using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalifornianHealthMonolithic.Shared.Models
{
    public partial class AppointmentModel
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string ConsultantName { get; set; }
        public string PatientName { get; set; }
    }
}