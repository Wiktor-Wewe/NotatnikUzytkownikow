using System.ComponentModel.DataAnnotations;
using System.Numerics;
using static NotatnikVeloce.Models.User;

namespace NotatnikVeloce.Models
{
    public class User
    {
        public enum Gender
        {
            Male,
            Female,
            Other
        }

        public Guid Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Imię może mieć maksymalnie 50 znaków.")]

        public string Name { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "Nazwisko może mieć maksymalnie 150 znaków.")]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required(ErrorMessage = "Data urodzenia jest wymagana.")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Płeć jest wymagana.")]
        public Gender Sex { get; set; }
        public string? PhoneNumber { get; set; }
        public double? ShoeSize { get; set; }
        public int? WorkstationId { get; set; }

        public string GetGender()
        {
            return Sex.ToString();
        }

        public int GetAge()
        {
            var today = DateTime.Today;
            int age = today.Year - BirthDate.Year;
            if (today.Month < BirthDate.Month || (today.Month == BirthDate.Month && today.Day < BirthDate.Day))
                age--;

            return age;
        }
    }

    public class UserDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Sex { get; set; }
        public string? PhoneNumber { get; set; }
        public double? ShoeSize { get; set; }
        public int? WorkstationId { get; set; }
    }
}
