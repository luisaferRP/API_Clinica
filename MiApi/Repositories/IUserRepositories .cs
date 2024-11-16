using MiApi.Models;

//aca van todos lo metodos que quiero que sean obligatorios 

namespace MiApi.Repositories
{
    public interface IUserRepositories 
    {
        Task<IEnumerable<User>> GetAll();

        Task<User> FindById(int id);

        Task Add(User user);

        Task<User> Update(User user);

        Task <bool> DeleteById(int id);

        Task<IEnumerable<User>> GetPatientAll();


    }
}