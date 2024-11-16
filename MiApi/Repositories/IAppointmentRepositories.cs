using MiApi.Models;

namespace MiApi.Repositories
{
    public interface IAppointmentRepositories
    {
        Task<IEnumerable<Appointment>> GetAllAsync();

        Task Add(Appointment appointment);
        
        Task<Appointment> FindById(int id);
        Task<Appointment> Update(Appointment appointment);

        Task <bool> DeleteById(int id);
        
    }
}