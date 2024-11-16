using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using MiApi.Repositories;
using MiApi.Data;
using MiApi.Models;


namespace MiApi.Services
{
    public class UserServices(ApplicationDbContex dbContext) : IUserRepositories
    {
        private readonly ApplicationDbContex _dbContex = dbContext;

        //implementamos los metodos que necesitamos en IUserRepositories
        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return await _dbContex.Users.ToListAsync(); 
            }
            catch (Exception ex)
            {
                
                throw new Exception($"¡Ups! Ocurrio un error {ex.Message}");
            }
           
        }

//----------------------------------------------------------------
        public async Task<User> FindById(int id)
        {
            try
            {
                return await _dbContex.Users.FindAsync(id);
            }
            catch (Exception ex)
            {
                
                throw new Exception($"¡Ups! Ocurrio un error {ex.Message}");
            }
        }

//----------------------------------------------------------------
        public async Task Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "El Usuario no puede ser nulo.");
            }
            try
            {
                await _dbContex.Users.AddAsync(user);
                await _dbContex.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                
                throw new Exception($"¡Ups! Ocurrio un error{ex.Message}");
            }

        }

//----------------------------------------------------------------
        public async Task<User> Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "El Usuario no puede ser nulo.");
            }
            try
            {
                var idUser = user.Id;
                //llamamos metodo ya existente
                var userDb = await FindById(idUser);

                if (userDb == null)
                {
                    throw new Exception($"El usuario con id {idUser} no existe.");
                }

                userDb.Name = user.Name;
                userDb.Email = user.Email;
                userDb.Password = user.Password;

                //va a la db accede con try a la entidad user indica a entity que debe actualizar la bd
                _dbContex.Entry(userDb).State = EntityState.Modified;
                await _dbContex.SaveChangesAsync();
                
                return userDb;
            }
            catch (Exception ex)
            {
               throw new Exception($"¡Ups! Ocurrio un error {ex.Message}");
            }
        }

//----------------------------------------------------------------
        public async Task<bool> DeleteById(int id)
        {
            try
            {
                var userFind = await FindById(id);

                if (userFind == null)
                {
                    return false;
                }

                _dbContex.Users.Remove(userFind);
                await _dbContex.SaveChangesAsync();

                return true;
                
            }
            catch (Exception ex)
            {
                throw new Exception($"¡Ups! Ocurrio un error {ex.Message}");
            }
        }

        public async Task<IEnumerable<User>> GetPatientAll()
        {
            try
            {
                var PatientAll = await _dbContex.Users.Where(u => u.Rol.Equals("Patient",StringComparison.OrdinalIgnoreCase)).ToListAsync(); 
                if(PatientAll == null){
                    throw new Exception("No hay pacientes registrados");
                }
                return PatientAll;
            }
            catch (Exception ex)
            {
                
                throw new Exception($"¡Ups! Ocurrio un error {ex.Message}");
            }
        }
    }
}