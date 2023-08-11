using CalifornianHealthMonolithic.Shared.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace CalifornianHealthMonolithic.Tests
{
    public class TestBase
    {
        public TestBase() {}
        public static List<Consultant> GetSeedConsultants(int quantity)
        {
            List<Consultant> consultants = new();
            for (int i = 1; i <= quantity; i++)
            {
                Consultant consultant = new() 
                {
                    Id = i,
                    FName = $"ConsultantFName{i}",
                    LName = $"ConsultantLName{i}",
                    Speciality = $"Speciality{i}",
                };
                consultants.Add(consultant);
            }
            return consultants;
        }
        public static List<Patient> GetSeedPatients(int quantity)
        {
            List<Patient> patients = new();
            for (int i = 1; i <= quantity; i++)
            {
                Patient patient = new() 
                {
                    Id = i,
                    FName = $"PatientFName{i}",
                    LName = $"PatientLName{i}",
                    UserName = $"username{i}",
                    NormalizedUserName = $"USERNAME{i}",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Address1 = "",
                    Address2 = "",
                    City = "",
                    Postcode = ""
                };
                var hasher = new PasswordHasher<Patient>();
                patient.PasswordHash = hasher.HashPassword(patient, $"Password.{i}");
                patients.Add(patient);
            }
            return patients;
        }
        public static List<ConsultantCalendar> GetSeedConsultantCalendars(int quantity, List<Consultant> consultants)
        {
            List<ConsultantCalendar> consultantCalendars = new();

            if (consultants.Count < 1)
                return consultantCalendars;

            Random r = new();
            for (int i = 1; i <= quantity; i++)
            {
                ConsultantCalendar consultantCalendar = new()
                {
                    Id = i,
                    ConsultantId = consultants[r.Next(0, consultants.Count - 1)].Id ?? default,
                    Date = DateTime.Now.AddDays(i),
                    Available = true
                };
                consultantCalendars.Add(consultantCalendar);
            }
            return consultantCalendars;
        }
        public static List<Appointment> GetSeedAppointments(int quantity, List<ConsultantCalendar> consultantCalendars, List<Patient> patients)
        {
            List<Appointment> appointments = new();

            if (consultantCalendars.Count < 1 || patients.Count < 1)
                return appointments;

            Random r = new();
            ConsultantCalendar consultantCalendar = consultantCalendars[r.Next(0, consultantCalendars.Count - 1)];

            for (int i = 1; i <= quantity; i++)
            {
                Appointment appointment = new()
                {
                    Id = i,
                    ConsultantId = consultantCalendar.Id ?? default,
                    PatientId = patients[r.Next(0, patients.Count - 1)].Id,
                    StartDateTime = consultantCalendar.Date,
                    EndDateTime = consultantCalendar.Date.AddHours(1),
                };
                appointments.Add(appointment);
            }
            return appointments;
        }
    }
}