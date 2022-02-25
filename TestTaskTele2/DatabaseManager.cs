using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TestTaskTele2
{
    public class DatabaseManager
    {
        public void fillDatabase()
        {
            DataManager dataManager = new DataManager();

            using(UsersContext db = new UsersContext())
            {
                if (!db.Users.Any())
                {
                    foreach (var d in dataManager.Users)
                    {
                        db.Users.Add(d);
                    }
                    db.SaveChanges();
                }
            }
        }

        public IQueryable<User> getPartialDataSex(string sex)
        {
            IQueryable<User> userQuery = null;
            UsersContext db = new UsersContext();
            if (sex == "male" || sex == "female")
            {
                userQuery = from user in db.Users where user.Sex == sex select user;
            }
            return userQuery;
        }

        public IQueryable<User> getPartialDataAge(int ageStart, int ageStop)
        {
            IQueryable<User> userQuery = null;
            UsersContext db = new UsersContext();
            if (ageStart >= 0 && ageStart < 90 && ageStop >= 0 && ageStop < 90)
            {
                userQuery = from user in db.Users 
                            where user.Age >= ageStart && user.Age <= ageStop
                            select user;
            }
            return userQuery;
        }

        public IQueryable<User> getAllData()
        {
            IQueryable<User> userQuery = null;
            UsersContext db = new UsersContext();
            userQuery = from user in db.Users select user;
            return userQuery;
        }

        public User getCurrentUser(string id)
        {
            User user = null;
            UsersContext db = new UsersContext();
            user = db.Users.Find(id);
            return user;
        }
    }

    class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=User;Trusted_Connection=true;");
        }
    }
}
