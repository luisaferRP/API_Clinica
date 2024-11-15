using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiApi.Models;

//usar
// using BlazorCrud.Share;


namespace BlazorServerApp.Services
{
    public interface IUserServices
    {
        Task<List<User>> Lista();

        Task<bool> Eliminar(int id);
        Task<bool> Editar(User user);
        Task<User> Buscar(int id);
        
    }
}