using Framework.Domain.Entities;
using Framework.Domain.Exceptions;

namespace BaseArchitecture.Domain.Employees.ValueObjects;

public class EmployeeId : ValueObject
{
    public string Value { get; }

    public EmployeeId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("employee id should be value");
        }
        
        Value = value;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}