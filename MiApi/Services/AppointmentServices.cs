using Microsoft.EntityFrameworkCore;
using MiApi.Repositories;
using MiApi.Data;
using MiApi.Models;

namespace MiApi.Services
{
    public class AppointmentServices(ApplicationDbContex dbContext) : IAppointmentRepositories
    {
        private readonly ApplicationDbContex _dbContext = dbContext;

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Appointments.ToListAsync(); 
            }
            catch (Exception ex)
            {
                
                throw new Exception($"¡Ups! Ocurrio un error {ex.Message}");
            }
            
        }

        public async Task<Appointment> FindById(int id)
        {
            try
            {
                return await _dbContext.Appointments.FindAsync(id);
            }
            catch (Exception ex)
            {
                
                throw new Exception($"¡Ups! Ocurrio un error {ex.Message}");
            }
        }

        public async Task Add(Appointment appointment)
        {
            if (appointment == null)
            {
                throw new ArgumentNullException(nameof(appointment), "La cita no puede ser nula.");
            }
            try
            {
                var conflictingAppointment = await _dbContext.Appointments
                        .FirstOrDefaultAsync(a => a.DoctorId == appointment.DoctorId && a.Date == appointment.Date && a.Time == appointment.Time);

                if (conflictingAppointment != null)
                {
                    throw new Exception("Ya existe una cita agendada para este médico en este horario.");
                }


                var doctorExists = await _dbContext.Doctors.AnyAsync(d => d.Id == appointment.DoctorId);
                if (!doctorExists)
                {
                    throw new ArgumentException($"El doctor con ID {appointment.DoctorId} no existe.");
                }

                await _dbContext.Appointments.AddAsync(appointment);
                await _dbContext.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                
                throw new Exception($"¡Ups! Ocurrio un error{ex.Message}");
            }
        }

        public async Task<Appointment> Update(Appointment appointment)
        {
            if (appointment == null)
            {
                throw new ArgumentNullException(nameof(appointment), "La cita no puede ser nula.");
            }
            try
            {
                var idAppointment = appointment.Id;
                //llamamos metodo ya existente
                var appointmentDb = await FindById(idAppointment);

                if (appointmentDb == null)
                {
                    throw new Exception($"La cita con id {idAppointment} no existe.");
                }

                appointmentDb.Date = appointment.Date;
                appointmentDb.Time = appointment.Time;
                appointmentDb.Reason = appointment.Reason;
                appointmentDb.Status = appointment.Status;

                // _dbContex.Entry(appointmentDb).CurrentValues.SetValues(appointment);

                //va a la db accede con try a la entidad user indica a entity que debe actualizar la bd
                _dbContext.Entry(appointmentDb).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                
                return appointmentDb;
            }
            catch (Exception ex)
            {
               throw new Exception($"¡Ups! Ocurrio un error {ex.Message}");
            }

        }

        public async Task<bool> DeleteById(int id)
        {
            try
            {
                var appointmentFind = await FindById(id);

                if (appointmentFind == null)
                {
                    return false;
                }

                _dbContext.Appointments.Remove(appointmentFind);
                await _dbContext.SaveChangesAsync();

                return true;
                
            }
            catch (Exception ex)
            {
                throw new Exception($"¡Ups! Ocurrio un error {ex.Message}");
            }
            
        }


    }
}