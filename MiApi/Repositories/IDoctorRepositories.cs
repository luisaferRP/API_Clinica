using MiApi.Models;


namespace MiApi.Repositories
{
    public interface IDoctorRepositories
    {
        Task<IEnumerable<Doctor>> GetAll();
        Task<Doctor> Update(Doctor doctor);

        Task<Doctor> FindById(int id);
        Task<object> FindByIdUser(int id);   
        Task<Doctor> FindSpecialty(string FindSpecialty);
        Task Add(Doctor doctor);
    }
}