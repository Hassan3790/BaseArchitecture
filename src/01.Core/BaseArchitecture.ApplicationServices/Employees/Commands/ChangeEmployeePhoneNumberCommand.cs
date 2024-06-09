using System.ComponentModel.DataAnnotations;

namespace BaseArchitecture.ApplicationServices.Employees.Commands
{
    public class ChangeEmployeePhoneNumberCommand
    {
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
