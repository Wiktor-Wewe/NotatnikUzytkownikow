using Newtonsoft.Json;
using NotatnikVeloce.Models;
using NotatnikVeloce.Services.Interfaces;
using OfficeOpenXml;

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

        public byte[] GetRaportInExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var users = GetUsers();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Raport");

                worksheet.Cells[1, 1].Value = "Name";
                worksheet.Cells[1, 2].Value = "Surname";
                worksheet.Cells[1, 3].Value = "Email";
                worksheet.Cells[1, 4].Value = "Birth Date";
                worksheet.Cells[1, 5].Value = "Age";
                worksheet.Cells[1, 6].Value = "Sex";
                worksheet.Cells[1, 7].Value = "Phone Number";
                worksheet.Cells[1, 8].Value = "Shoe Size";
                worksheet.Cells[1, 9].Value = "Workstation Id";

                int row = 2;
                foreach (var user in users)
                {
                    worksheet.Cells[row, 1].Value = user.Name;
                    worksheet.Cells[row, 2].Value = user.Surname;
                    worksheet.Cells[row, 3].Value = user.Email;
                    worksheet.Cells[row, 4].Value = user.BirthDate.ToString("yyyy-MM-dd");
                    worksheet.Cells[row, 5].Value = user.GetAge();
                    worksheet.Cells[row, 6].Value = user.GetGender();
                    worksheet.Cells[row, 7].Value = user.PhoneNumber;
                    worksheet.Cells[row, 8].Value = user.ShoeSize;
                    worksheet.Cells[row, 9].Value = user.WorkstationId;

                    row++;
                }

                worksheet.Cells["A1:I1"].Style.Font.Bold = true;
                worksheet.Cells.AutoFitColumns();

                byte[] raportBytes = package.GetAsByteArray();
                return raportBytes;
            }
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
                WorkstationId = userDto.WorkstationId
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
            user.WorkstationId = userDto.WorkstationId;

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
