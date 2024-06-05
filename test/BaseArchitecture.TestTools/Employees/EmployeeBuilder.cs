using BaseArchitecture.Domain.Employees;
using BaseArchitecture.Domain.Employees.ValueObjects;

namespace BaseArchitecture.TestTools.Employees
{
    public class EmployeeBuilder : EntityBuilder<Employee>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string PhoneNumber { get; set; }

        public EmployeeBuilder WithId(string id)
        {
            Id = id;
            return this;
        }

        public EmployeeBuilder WithFirstName(string firstName)
        {
            FirstName = firstName; return this;
        }

        public EmployeeBuilder WithLastName(string lastName)
        {
            LastName = lastName; return this;
        }

        public EmployeeBuilder WithNationalCode(string nationalCode)
        {
            NationalCode = nationalCode;
            return this;
        }

        public EmployeeBuilder WithPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            return this;
        }

        public Employee Build()
        {
            return new Employee(
                new EmployeeId(Id),
                new FullName(FirstName, LastName),
                new NationalCode(NationalCode),
                new PhoneNumber(PhoneNumber)
                );
        }
    }
}
