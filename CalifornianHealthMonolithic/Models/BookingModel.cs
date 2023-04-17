using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CalifornianHealthMonolithic.Models
{
    public class BookingModel
    {
        public PatientDetails patient { get; set; }
        public ConsultantDetails consultant { get; set; }
        public FacilityDetails facility { get; set; }
        public PaymentDetails payment { get; set; }
        public AppointmentDetails appointment { get; set; }
    }

    public class AppointmentDetails
    {
        public Guid appointmentId { get; set; }
        public DateTime appointmentDate { get; set; }
        public DateTime appointmentTime { get; set; }
    }

    public class ConsultantDetails
    {
        public int consultantId { get; set; }
        public string consultantName { get; set; }
        public string consultantSpeciality { get; set; }
        public int facilityId { get; set; }
    }

    public class FacilityDetails
    {
        public int facilityId { get; set; }
        public string facilityName { get; set; }
        public string facilityAddressLine1 { get; set; }
        public string facilityAddressLine2 { get; set; }
        public string facilityRegion { get; set; }
        public string facilityCity { get; set; }
        public string facilityPostcode { get; set; }
        public string facilityContactNumber { get; set; }
    }

    public class PatientDetails
    {
        public int patientId { get; set; }
        public string patientName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string contactNumber { get; set; }
    }

    public class PaymentDetails
    {
        public int paymentId { get; set; }
        public double payment { get; set; }
    }    
}