namespace BaseArchitecture.ApplicationServices.Employees.Commands;

public class RegisterEmployeeCommand
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalCode { get; set; }
    public string PhoneNumber { get; set; }
}