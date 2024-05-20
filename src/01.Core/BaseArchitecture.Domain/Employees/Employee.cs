using BaseArchitecture.Domain.Employees.ValueObjects;
using Framework.Domain.Entities;

namespace BaseArchitecture.Domain.Employees;

public class Employee : AggregateRoot
{
    public EmployeeId Id { get; }
    public FullName FullName { get; private set; }
    public NationalCode NationalCode { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }

    public Employee(
        EmployeeId employeeId,
        FullName fullName,
        NationalCode nationalCode,
        PhoneNumber phoneNumber)
    {
        Id = employeeId;
        FullName = fullName;
        NationalCode = nationalCode;
        PhoneNumber = phoneNumber;
    }

    private Employee()
    {
    }
}