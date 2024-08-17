namespace BaseArchitecture.ApplicationServices.Employees.Commands;

public record RegisterEmployeeCommand(
    string FirstName,
    string LastName,
    string NationalCode,
    string PhoneNumber); 
