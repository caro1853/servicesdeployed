using System;
using AutoMapper;
using MediatR;
using Moq;
using Scheduling.Application.Contracts.Persistence;
using Scheduling.Application.Features.AppoitmentFeatures.Commands.CreateAppoitment;
using Scheduling.Domain.Entities;

namespace Scheduling.Application.Tests
{
	public class AppointmentTest
	{
        [Fact]
		public async Task Create_Appoitment_Return_Error_Patient_No_Exists()
		{
			var mockMapper = new Mock<IMapper>();
			var mockIAppoitmentRepository = new Mock<IAppointmentRepository>();
            var mockIDoctorRepository = new Mock<IDoctorRepository>();
            var mockIPatientRepository = new Mock<IPatientRepository>();
            var mockIOperationalHourRepository = new Mock<IOperationalHourRepository>();

            mockIDoctorRepository.Setup(x => x.GetByIdAsync(1)).Returns(Task.FromResult(new Doctor() { Id = 1 }));
            mockIPatientRepository.Setup(x => x.GetByIdAsync(1)).Returns(Task.FromResult<Patient>(null));

            CreateAppointmentCommandHandler handler =
                new CreateAppointmentCommandHandler(
                    mockIAppoitmentRepository.Object,
                    mockMapper.Object,
                    mockIDoctorRepository.Object,
                    mockIPatientRepository.Object,
                    mockIOperationalHourRepository.Object
                    );

            CreateAppointmentCommand request = new CreateAppointmentCommand();
            request.DoctorId = 1;
            try
            {
                var response = await handler.Handle(request, new CancellationToken());
            }
            catch(ApplicationException ex)
            {
                Assert.True(ex.Message == "Paciente Inválido"); 
            }
        }

        [Fact]
        public async Task Create_Appoitment_Return_OK()
        {
            var mockMapper = new Mock<IMapper>();
            var mockIAppoitmentRepository = new Mock<IAppointmentRepository>();
            var mockIDoctorRepository = new Mock<IDoctorRepository>();
            var mockIPatientRepository = new Mock<IPatientRepository>();
            var mockIOperationalHourRepository = new Mock<IOperationalHourRepository>();

            CreateAppointmentCommand request = new CreateAppointmentCommand();
            request.DoctorId = 1;
            request.PatientId = 1;
            request.ScheduledDate = new DateTime(2024, 2, 12);
            request.ScheduleTime = 10;

            Appointment appointment = new Appointment()
            {
                PatientId = request.PatientId,
                DoctorId = request.DoctorId,
                ScheduledDate = request.ScheduledDate,
                ScheduleTime = request.ScheduleTime
            };

            //Mock Setup
            mockIDoctorRepository.Setup(x => x.GetByIdAsync(1)).Returns(Task.FromResult(new Doctor() { Id = 1 }));
            mockIPatientRepository.Setup(x => x.GetByIdAsync(1)).Returns(Task.FromResult<Patient>(new Patient()));
            mockMapper.Setup(x => x.Map<Appointment>(request)).Returns(appointment);
            //Mock OperationalHour
            IEnumerable<OperationalHour> opHours = new List<OperationalHour>() {
                new OperationalHour()
                {
                    Available = true, Day = (int)request.ScheduledDate.DayOfWeek,
                    Hours = new List<Hour>()
                    {
                        new Hour(){ Available = true, Schedule = request.ScheduleTime }
                    }
                }
            };
            mockIOperationalHourRepository.Setup(x => x.GetOperationalHoursByDoctor(1)).Returns(Task.FromResult(opHours));

            //Mock Setup response
            mockIAppoitmentRepository.Setup(x => x.AddAsync(appointment)).Returns(Task.FromResult(new Appointment() { Id = 1 }));

            CreateAppointmentCommandHandler handler =
                new CreateAppointmentCommandHandler(
                    mockIAppoitmentRepository.Object,
                    mockMapper.Object,
                    mockIDoctorRepository.Object,
                    mockIPatientRepository.Object,
                    mockIOperationalHourRepository.Object
                    );

            var response = await handler.Handle(request, new CancellationToken());
            Assert.IsType<int>(response);
            Assert.True(response == 1);
        }
    }
}

