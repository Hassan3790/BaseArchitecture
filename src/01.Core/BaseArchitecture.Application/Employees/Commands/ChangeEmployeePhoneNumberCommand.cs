using System.ComponentModel.DataAnnotations;

namespace BaseArchitecture.ApplicationServices.Employees.Commands
{
    public record ChangeEmployeePhoneNumberCommand(
        string EmployeeId,
        string PhoneNumber);
}