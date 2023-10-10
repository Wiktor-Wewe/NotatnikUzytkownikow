using NotatnikVeloce.Models;
using NotatnikVeloce.Services.Interfaces;

namespace NotatnikVeloce.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<User> GetUsers()
        {
            return _dbContext.Users.ToList();
        }

        public User? GetUser(Guid id)
        {
            var user = _dbContext.Users.FirstOrDefault(user => user.Id == id);
            if(user == null)
            {
                return null;
            }
            return user; 
        }

        public User? AddUser(UserDto userDto)
        {
            if (userDto.Name == null || userDto.Surname == null || userDto.Email == null)
            {
                return null;
            }

            if(userDto.BirthDate > DateTime.Now)
            {
                return null;
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = userDto.Name,
                Surname = userDto.Surname,
                Email = userDto.Email,
                BirthDate = userDto.BirthDate,
                Sex = userDto.Sex,
                PhoneNumber = userDto.PhoneNumber,
                ShoeSize = userDto.ShoeSize,
                SorkstationId = userDto.SorkstationId
            };

            _dbContext.Users.Add(user);
            var result = _dbContext.SaveChanges();

            if(result < 1) 
            {
                return null;
            }

            return user;
        }

        public User? UpdateUser(Guid id, UserDto userDto)
        {
            var user = _dbContext.Users.FirstOrDefault(user => user.Id == id);
            if(user == null)
            {
                return null;
            }
            
            if(userDto.Name != null)
            {
                user.Name = userDto.Name;
            }

            if(userDto.Surname != null)
            {
                user.Surname = userDto.Surname;
            }

            if(userDto.Email != null)
            {
                user.Email = userDto.Email;
            }

            if(userDto.BirthDate < DateTime.Now)
            {
                user.BirthDate = userDto.BirthDate;
            }

            user.PhoneNumber = userDto.PhoneNumber;
            user.ShoeSize = userDto.ShoeSize;
            user.SorkstationId = userDto.SorkstationId;

            _dbContext.Users.Update(user);
            var result = _dbContext.SaveChanges();

            if(result < 1)
            {
                return null;
            }

            return user;
        }

        public bool DeleteUser(Guid id)
        {
            var user = _dbContext.Users.FirstOrDefault(user => user.Id == id);
            if(user == null)
            {
                return false;
            }

            _dbContext.Users.Remove(user);
            var result = _dbContext.SaveChanges();

            if(result < 1)
            {
                return false;
            }

            return true;
        }
    }
}
