using WebApplication4.models;

namespace WebApplication4.Dataccess
{
    public interface Idata
    {
        public string Login(string username, string password);

        public List<User> usersall();

    }
}
