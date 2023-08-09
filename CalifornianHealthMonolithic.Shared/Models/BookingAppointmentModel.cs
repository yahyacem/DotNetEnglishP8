namespace CalifornianHealthMonolithic.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class BookingAppointmentModel
    {
        public int PatientId { get; set; }
        public int ConsultantCalendarId { get; set; }
    }
}
