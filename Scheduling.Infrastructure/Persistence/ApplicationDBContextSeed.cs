using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Microsoft.Extensions.Logging;
using Scheduling.Domain.Entities;

namespace Scheduling.Infrastructure.Persistence
{
	public class ApplicationDBContextSeed
	{
		public static async Task SeedAsync(ApplicationDbContext context, ILogger<ApplicationDBContextSeed> logger)
		{
			context.Doctors.AddRange(GetPreconfiguredDoctors(context));
            context.Patients.AddRange(GetPreconfiguredPatients(context));
            await context.SaveChangesAsync();

            var doctorsCreated = context.Doctors.ToList();
            foreach (var item in doctorsCreated)
            {
                if(!context.OperationalHours.Any(p=>p.DoctorId == item.Id))
                {
                    context.OperationalHours.AddRange(AssignNewConfiguration(item.Id));
                }
            }
            await context.SaveChangesAsync();

        }

		private static IEnumerable<Doctor> GetPreconfiguredDoctors(ApplicationDbContext context)
		{
			var doctors = new List<Doctor>();
			var names = new List<string>() {
                "Dr. Juan Perez", "Dr. Maria Lopez", "Dr. Pedro Ramirez", "Dr. Carlos Sanchez", "Dr. Luisa Martinez", "Dr. Ana Rodriguez" };

            var especiality = new List<string>()
            {
                "Odontólogo", "Cardiólogo", "Neurólogo", "Pediatra", "Ginecólogo", "Oftalmólogo", "Dermatólogo", "Psiquiatra", "Ortopedista", "Urólogo", "Endocrinólogo", "Gastroenterólogo", "Nefrólogo", "Pulmonólogo", "Reumatólogo", "Oncólogo", "Hematólogo", "Especialista en enfermedades infecciosas", "Alergista", "Anestesiólogo", "Radiólogo", "Oncólogo", "Hematólogo", "Especialista en enfermedades infecciosas", "Alergista", "Anestesiólogo", "Radiólogo"
            };

            var description = new List<string>()
            {
                "Odontólogo especializado en higiena oral",
                "Cardiólogo especializado en enfermedades del corazón",
                "Neurólogo especializado en enfermedades del sistema nervioso",
                "Pediatra especializado en enfermedades de niños",
                "Ginecólogo especializado en enfermedades de mujeres", "Oftalmólogo especializado en enfermedades de los ojos", "Dermatólogo especializado en enfermedades de la piel", "Psiquiatra especializado en enfermedades mentales", "Ortopedista especializado en enfermedades de los huesos", "Urólogo especializado en enfermedades del sistema urinario", "Endocrinólogo especializado en enfermedades de las glandulas endocrinas", "Gastroenterólogo especializado en enfermedades del sistema digestivo", "Nefrólogo especializado en enfermedades de los riñones", "Pulmonólogo especializado en enfermedades del sistema respiratorio", "Reumatólogo especializado en enfermedades de las articulaciones", "Oncólogo especializado en enfermedades del cáncer", "Hematólogo especializado en enfermedades de la sangre", "Especialista en enfermedades infecciosas", "Alergista especializado en enfermedades alérgicas", "Anestesiólogo especializado en anestesia", "Radiólogo especializado en radiografías", "Oncólogo especializado en enfermedades del cáncer", "Hematólogo especializado en enfermedades de la sangre", "Especialista en enfermedades infecciosas", "Alergista especializado en enfermedades alérgicas", "Anestesiólogo especializado en anestesia", "Radiólogo especializado en radiografías"
            };


            for (int i = 1; i <= 5; i++)
			{
				string email = $"doctor{i}@gmail.com";
				if(!context.Doctors.Any(p=>p.Email == email))
				{
					doctors.Add(new Doctor
					{
						Email = email,
						Name = names[i],
                        Especiality = especiality[i],
                        Description = description[i]
					});
                }
			}
			return doctors;
		}

        private static IEnumerable<Patient> GetPreconfiguredPatients(ApplicationDbContext context)
        {
            var patients = new List<Patient>();
            var names = new List<string>() {
                "Alan Smith", "Sara Connor", "John Doe", "Jane Doe", "John Smith", "Sara Smith", "Alan Doe", "Jane Connor" };

            for (int i = 1; i <= 5; i++)
            {
                string email = $"patient{i}@gmail.com";
                if (!context.Patients.Any(p => p.Email == email))
                {
                    patients.Add(new Patient
                    {
                        Email = email,
                        Name = names[i]
                    });
                }
            }
            return patients;
        }

        private static List<OperationalHour> AssignNewConfiguration(int doctorId)
        {
            var listOperationalHours = new List<OperationalHour>();
            var dayOff = new List<int>() { 0, 6 };
            var hourOff = new List<int>() { 12, 13 };
            int start = 8;
            int end = 17;
            for (int day = 0; day < 7; day++)
            {
                var op = new OperationalHour()
                {
                    DoctorId = doctorId,
                    Day = day,
                    Available = !dayOff.Contains(day)
                };
                op.Hours = new List<Hour>();
                bool available = !dayOff.Contains(day);
                for (int hour = start; hour <= end; hour++)
                {
                    op.Hours.Add(
                        new Hour
                        {
                            Schedule = hour,
                            Available = !hourOff.Contains(hour) && available
                        });
                }
                listOperationalHours.Add(op);
            }
            return listOperationalHours;
        }
    }
}

