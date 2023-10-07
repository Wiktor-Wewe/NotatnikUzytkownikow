using System.ComponentModel.DataAnnotations;

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
        [StringLength(50, ErrorMessage = "Imię może mieć maksymalnie 50 znaków.")]

        public string Name { get; set; }
        [StringLength(150, ErrorMessage = "Nazwisko może mieć maksymalnie 150 znaków.")]
        public string Surname { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Data urodzenia jest wymagana.")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Płeć jest wymagana.")]
        public Gender Sex { get; set; }
        public string? PhoneNumber { get; set; }
        public double? ShoeSize { get; set; }
        public int? SorkstationId { get; set; }
    }
}
