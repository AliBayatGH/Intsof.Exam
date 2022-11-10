using Intsoft.Exam.Application.Validators.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Intsoft.Exam.Application.Models
{
    public class UpdateUserModel
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "FirstName must not exceed 50 character.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        [MaxLength(50, ErrorMessage = "LastName must not exceed 50 character.")]
        public string LastName { get; set; }

        [NationalCodeValidation(ErrorMessage = "NationalCode is not valid.")]
        public string NationalCode { get; set; }

        [MobileNumberValidation(ErrorMessage = "PhoneNumber is not valid")]
        public string PhoneNumber { get; set; }
    }
}
