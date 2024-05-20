using Framework.Domain.Entities;
using Framework.Domain.Exceptions;

namespace BaseArchitecture.Domain.Employees.ValueObjects;

public class NationalCode : ValueObject
{
    public string Value { get; }

    public NationalCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("national code should be value");
        }

        Value = value;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}