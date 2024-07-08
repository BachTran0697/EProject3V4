using eProject3.Interface;
using eProject3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace eProject3.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly DatabaseContext db;

        public UserRepo(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<User> CreateUser(User user)
        {
            try
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> UpdateUser(User user)
        {
            var oldUser = await db.Users.SingleOrDefaultAsync(u => u.LOGIN_ID == user.LOGIN_ID);
            if (oldUser != null)
            {
                var userType = typeof(User);
                foreach (var property in userType.GetProperties())
                {
                    var newValue = property.GetValue(user);
                    if (newValue != null && !string.IsNullOrWhiteSpace(newValue.ToString()))
                    {
                        property.SetValue(oldUser, newValue);
                    }
                }
                db.Users.Update(oldUser);
                await db.SaveChangesAsync();
                return oldUser;
            }
            else
            {
                throw new ArgumentException("No ID found");
            }
        }


        public async Task<IEnumerable<User>> GetUsers()
        {
            return await db.Users.ToListAsync();
        }

        public async Task<User> GetUserByNameOrEmail(string name, string email)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Name or Email must be filled");
            }
            try
            {
                return await db.Users.SingleOrDefaultAsync(u => u.LOGIN_NAME == name || u.Email == email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<User> DeleteUser(int id)
        {
            try
            {
                var oldUser = await db.Users.SingleOrDefaultAsync(u => u.LOGIN_ID == id);
                if (oldUser != null)
                {
                    db.Users.Remove(oldUser);
                    await db.SaveChangesAsync();
                    return oldUser;
                }
                else
                {
                    throw new ArgumentException("No ID found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUser(string identifier)
        {
            if (string.IsNullOrEmpty(identifier))
            {
                throw new ArgumentException("Identifier must be filled");
            }
            try
            {
                return await db.Users.SingleOrDefaultAsync(u => u.LOGIN_NAME == identifier || u.Email == identifier);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the user.", ex);
            }
        }
    }
}
