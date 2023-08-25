using System.Security.Claims;
using System.Security.Principal;
using AutoMapper;
using CalifornianHealthMonolithic.APIBooking.Controllers;
using CalifornianHealthMonolithic.APIBooking.Models;
using CalifornianHealthMonolithic.APIBooking.Repositories;
using CalifornianHealthMonolithic.APIBooking.Services;
using CalifornianHealthMonolithic.APIConsultant.Repositories;
using CalifornianHealthMonolithic.APIConsultant.Services;
using CalifornianHealthMonolithic.Shared.DBContext;
using CalifornianHealthMonolithic.Shared.Mappers;
using CalifornianHealthMonolithic.Shared.Models;
using CalifornianHealthMonolithic.Shared.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CalifornianHealthMonolithic.Tests.Integration.APIBooking.Controllers
{
    [Collection("Sequential")]
    public class ConsultantControllerTests : IntegrationBase
    {
        public ConsultantControllerTests() : base() {}
        [Fact]
        public async Task GetConsultantCalendarByConsultantIdAsync_Should_Return_Ok()
        {
            // Arrange

            // Get in memory database context
            using var context = new CHDBContext(GetOptions());

            // Get seed data for database
            List<Patient> patients = GetSeedPatients(1);
            List<Consultant> consultants = GetSeedConsultants(1);
            List<ConsultantCalendar> consultantCalendars = GetSeedConsultantCalendars(1, consultants);
            List<Appointment> appointments = GetSeedAppointments(1, consultantCalendars, patients);

            // Add data to database
            await context.Patients.AddRangeAsync(patients);
            await context.Consultants.AddRangeAsync(consultants);
            await context.ConsultantCalendars.AddRangeAsync(consultantCalendars);
            await context.Appointments.AddRangeAsync(appointments);
            await context.SaveChangesAsync();

            // Set up parameters and instantiate service
            IConsultantRepository consultantRepository = new ConsultantRepository(context);
            IConsultantCalendarRepository consultantCalendarRepository = new ConsultantCalendarRepository(context);
            IAppointmentRepository appointmentRepository = new AppointmentRepository(context);
            IPatientRepository patientRepository = new PatientRepository(context);
            IMapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            }));
            IConsultantService consultantService = new ConsultantService(consultantRepository,
                                                                         consultantCalendarRepository,
                                                                         appointmentRepository,
                                                                         patientRepository,
                                                                         mapper);

            // Instantiate controller
            ConsultantController consultantController = new(consultantService);

            // Act

            // Call method in controller
            var response = await consultantController.GetConsultantCalendarByConsultantIdAsync(1);

            // Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.NotNull(((ObjectResult)response).Value);
        }
        [Fact]
        public async Task GetConsultantsNamesAsync_Should_Return_Ok()
        {
            // Arrange

            // Get in memory database context
            using var context = new CHDBContext(GetOptions());

            // Get seed data for database
            List<Patient> patients = GetSeedPatients(1);
            List<Consultant> consultants = GetSeedConsultants(1);
            List<ConsultantCalendar> consultantCalendars = GetSeedConsultantCalendars(1, consultants);
            List<Appointment> appointments = GetSeedAppointments(1, consultantCalendars, patients);

            // Add data to database
            await context.Patients.AddRangeAsync(patients);
            await context.Consultants.AddRangeAsync(consultants);
            await context.ConsultantCalendars.AddRangeAsync(consultantCalendars);
            await context.Appointments.AddRangeAsync(appointments);
            await context.SaveChangesAsync();

            // Set up parameters and instantiate service
            IConsultantRepository consultantRepository = new ConsultantRepository(context);
            IConsultantCalendarRepository consultantCalendarRepository = new ConsultantCalendarRepository(context);
            IAppointmentRepository appointmentRepository = new AppointmentRepository(context);
            IPatientRepository patientRepository = new PatientRepository(context);
            IMapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            }));
            IConsultantService consultantService = new ConsultantService(consultantRepository,
                                                                         consultantCalendarRepository,
                                                                         appointmentRepository,
                                                                         patientRepository,
                                                                         mapper);

            // Instantiate controller
            ConsultantController consultantController = new(consultantService);

            // Act

            // Call method in controller
            var response = await consultantController.GetConsultantsNamesAsync();

            // Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.NotNull(((ObjectResult)response).Value);
            List<ConsultantNameModel>? listConsultantNames = (List<ConsultantNameModel>?) ((ObjectResult)response).Value ?? null;
            Assert.NotNull(listConsultantNames);
            Assert.NotEmpty(listConsultantNames);
        }
        [Fact]
        public async Task GetConsultantsAsync_Should_Return_Ok()
        {
            // Arrange

            // Get in memory database context
            using var context = new CHDBContext(GetOptions());

            // Get seed data for database
            List<Patient> patients = GetSeedPatients(1);
            List<Consultant> consultants = GetSeedConsultants(1);
            List<ConsultantCalendar> consultantCalendars = GetSeedConsultantCalendars(1, consultants);
            List<Appointment> appointments = GetSeedAppointments(1, consultantCalendars, patients);

            // Add data to database
            await context.Patients.AddRangeAsync(patients);
            await context.Consultants.AddRangeAsync(consultants);
            await context.ConsultantCalendars.AddRangeAsync(consultantCalendars);
            await context.Appointments.AddRangeAsync(appointments);
            await context.SaveChangesAsync();

            // Set up parameters and instantiate service
            IConsultantRepository consultantRepository = new ConsultantRepository(context);
            IConsultantCalendarRepository consultantCalendarRepository = new ConsultantCalendarRepository(context);
            IAppointmentRepository appointmentRepository = new AppointmentRepository(context);
            IPatientRepository patientRepository = new PatientRepository(context);
            IMapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            }));
            IConsultantService consultantService = new ConsultantService(consultantRepository,
                                                                         consultantCalendarRepository,
                                                                         appointmentRepository,
                                                                         patientRepository,
                                                                         mapper);

            // Instantiate controller
            ConsultantController consultantController = new(consultantService);

            // Act

            // Call method in controller
            var response = await consultantController.GetConsultantsAsync();

            // Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.NotNull(((ObjectResult)response).Value);
            List<Consultant>? listConsultant = (List<Consultant>?) ((ObjectResult)response).Value ?? null;
            Assert.NotNull(listConsultant);
            Assert.NotEmpty(listConsultant);
        }
        [Fact]
        public async Task GetConsultantCalendarByIdAsync_Should_Return_Ok()
        {
            // Arrange

            // Get in memory database context
            using var context = new CHDBContext(GetOptions());

            // Get seed data for database
            List<Patient> patients = GetSeedPatients(1);
            List<Consultant> consultants = GetSeedConsultants(1);
            List<ConsultantCalendar> consultantCalendars = GetSeedConsultantCalendars(1, consultants);
            List<Appointment> appointments = GetSeedAppointments(1, consultantCalendars, patients);

            // Add data to database
            await context.Patients.AddRangeAsync(patients);
            await context.Consultants.AddRangeAsync(consultants);
            await context.ConsultantCalendars.AddRangeAsync(consultantCalendars);
            await context.Appointments.AddRangeAsync(appointments);
            await context.SaveChangesAsync();

            // Set up parameters and instantiate service
            IConsultantRepository consultantRepository = new ConsultantRepository(context);
            IConsultantCalendarRepository consultantCalendarRepository = new ConsultantCalendarRepository(context);
            IAppointmentRepository appointmentRepository = new AppointmentRepository(context);
            IPatientRepository patientRepository = new PatientRepository(context);
            IMapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            }));
            IConsultantService consultantService = new ConsultantService(consultantRepository,
                                                                         consultantCalendarRepository,
                                                                         appointmentRepository,
                                                                         patientRepository,
                                                                         mapper);

            // Instantiate controller
            ConsultantController consultantController = new(consultantService);

            // Act

            // Call method in controller
            var response = await consultantController.GetConsultantCalendarByIdAsync(1);

            // Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.NotNull(((ObjectResult)response).Value);
        }
    }
}