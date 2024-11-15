using MiApi.Models;

namespace BlazorServerApp.Services
{
    public interface IAppoinmentServices
    {
         Task<List<Appointment>> Lista();
        Task<Appointment> Buscar(int id);
        Task<Appointment> Guardar(Appointment appointment);
        Task<int> Editar(Appointment Appointment);
        Task<bool> Eliminar(int id);
        
    }
}