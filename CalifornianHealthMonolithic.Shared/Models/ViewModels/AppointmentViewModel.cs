using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalifornianHealthMonolithic.Shared.Models.ViewModels
{
    public partial class AppointmentViewModel
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string ConsultantName { get; set; }
        public string PatientName { get; set; }
    }
}