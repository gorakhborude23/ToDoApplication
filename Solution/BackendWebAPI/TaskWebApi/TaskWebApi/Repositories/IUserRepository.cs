using TaskWebApi.Classes;

namespace TaskWebApi.Repositories
{
    public interface IUserRepository
    {
            Task<User?> GetByIdAsync(int userId);
    }
}
