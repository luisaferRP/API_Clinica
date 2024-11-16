
using Microsoft.EntityFrameworkCore;
using MiApi.Repositories;
using MiApi.Data;
using MiApi.Models;
using MiApi.DTOS;

namespace MiApi.Services
{
    public class DoctorServices(ApplicationDbContex dbContex) : IDoctorRepositories
    {
        private readonly ApplicationDbContex _dbContex = dbContex;

        public async Task<IEnumerable<Doctor>> GetAll()
        {
            try
            {
                return await _dbContex.Doctors.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"¡Ups! Ocurrio un error {ex.Message}");
            }
        }


        //bring doctor's data by user
         public async Task<object> FindByIdUser(int id)
        {
            try
            {
                var doctorSearch = await _dbContex.Doctors.FindAsync(id);

                if (doctorSearch == null)
                {
                    throw new Exception($"El doctor con ese id no exite");
                }
                
               var doctorFound =  await _dbContex.Users.FirstOrDefaultAsync(d => d.Id == doctorSearch.UserId);

                if (doctorFound == null)
                {
                    throw new Exception($"El usuario asociado al doctor con ese id no existe");
                }

                var dataDoctors = new {

                    doctorFound.Name,
                    doctorFound.Email,
                    doctorFound.Rol,
                    doctorSearch.Date,
                    doctorSearch.Time,
                    doctorSearch.Specialization,
                };

                return dataDoctors;

            }
            catch (Exception ex)
            {
                
                throw new Exception($"¡Ups! Ocurrio un error {ex.Message}");
            }
        }

        //doctor id
        public async Task<Doctor> FindById(int id)
        {
            try
            {
                return await _dbContex.Doctors.FindAsync(id);
            }
            catch (Exception ex)
            {
                
                throw new Exception($"¡Ups! Ocurrio un error {ex.Message}");
            }
        }


        public async Task<Doctor> Update(Doctor doctor)
        {
            if (doctor == null)
            {
                throw new ArgumentNullException(nameof(doctor), "Los datos no pueden ser nulos.");
                
            }
            try
            {
                var doctorId = doctor.Id;
                var doctorDb = await FindById(doctorId);

                if (doctorDb == null)
                {
                    throw new Exception($"El doctor con id {doctorId} no existe.");
                }

                doctorDb.Date = doctor.Date;
                doctorDb.Time = doctor.Time;
                doctorDb.Specialization = doctor.Specialization;
                doctorDb.user.Name = doctor.user.Name;
                doctorDb.user.Email = doctor.user.Email;
                doctorDb.user.Password = doctor.user.Password;

                _dbContex.Entry(doctorDb).State = EntityState.Modified;
                
               // _dbContex.Entry(doctorDb).CurrentValues.SetValues(doctor);
                await _dbContex.SaveChangesAsync();

                return doctorDb;
                
            }
            catch (Exception ex)
            {
               throw new Exception($"¡Ups! Ocurrio un error {ex.Message}");
            }
        }

        public async Task Add(Doctor doctor)
        {
            if (doctor == null)
            {
                throw new ArgumentNullException(nameof(doctor), "El doctor no puede ser nulo.");
            }
            try
            {

                 var existingDoctor = await _dbContex.Users
                        .FirstOrDefaultAsync(u => u.Email == doctor.user.Email);

                User user;

                if (existingDoctor != null)
                {
                    user = existingDoctor;

                }else
                {
                    user = new User
                    {
                        Name = doctor.user.Name,
                        Email = doctor.user.Email,
                        Password = doctor.user.Password, 
                        Rol = "Doctor" 
                    };
    
                    await _dbContex.Users.AddAsync(user);
                    await _dbContex.SaveChangesAsync();
    
                }
    
                Doctor newDoctor = new Doctor
                {
                    UserId = user.Id, 
                    Specialization = doctor.Specialization,
                    Date = doctor.Date,
                    Time = doctor.Time 
                };
    
                await _dbContex.Doctors.AddAsync(newDoctor);
                await _dbContex.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                
                throw new Exception($"¡Ups! Ocurrio un error{ex.Message}");
            }

        }

        public async Task<Doctor> FindSpecialty(string FindSpecialty)
        {
            try
            {

                var doctor = await _dbContex.Doctors
                        .Include(d => d.user) 
                        .FirstOrDefaultAsync(d => d.Specialization == FindSpecialty);
                

                if (doctor == null){
                    throw new Exception("No hay doctores registrados con esa especialidad");
                }
                        
                return doctor;
            }
            catch (Exception ex)
            {
                
                throw new Exception($"¡Ups! Ocurrio un error {ex.Message}");
            }
        }
    }
}