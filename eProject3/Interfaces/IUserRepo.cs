using eProject3.Models;

namespace eProject3.Interface
{
    public interface IUserRepo
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(string identifier);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(int id);

    }
}
