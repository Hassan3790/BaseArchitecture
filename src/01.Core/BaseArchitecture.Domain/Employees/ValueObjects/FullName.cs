using Framework.Domain.Entities;
using Framework.Domain.Exceptions;

namespace BaseArchitecture.Domain.Employees.ValueObjects;

public class FullName : ValueObject
{
    public string FirstName { get; }
    public string LastName { get; }

    public FullName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) ||
            string.IsNullOrWhiteSpace(lastName))
        {
            throw new DomainException(
                "first name and last name should be value");
        }

        FirstName = firstName;
        LastName = lastName;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName + LastName;
    }
}