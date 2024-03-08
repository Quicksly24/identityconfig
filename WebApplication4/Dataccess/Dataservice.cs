using WebApplication4.models;

namespace WebApplication4.Dataccess
{
    public class Dataservice : Idata
    {
        List<User> users = new List<User>() {
            new User {Id=1,Name="jeff",Email="123" },
            new User {Id=2,Name="evan",Email="1234@gmail.com" },
            new User {Id=3,Name="nice",Email="12"},
            new User {Id=4,Name="best",Email="4321"}

        };
        public List<User> usersall()
        {
            return users;
        }

        public string Login(string username, string password)
        {
            if(!users.Where(x=>x.Name==username && x.Email == password).Any()) {

                return "failure";

            };

            return "sucess";

        }
    }
}
