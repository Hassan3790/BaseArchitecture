using BaseArchitecture.Domain.Employees;
using BaseArchitecture.Domain.Employees.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseArchitecture.Persistence.EF.Employees;

public class EmployeeEntityMap : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");
        
        builder.Property(c => c.Id)
            .HasConversion(w => w.Value, r => new EmployeeId(r));
        builder.Property(c => c.PhoneNumber)
            .HasConversion(w => w.Value, r => new PhoneNumber(r));
        builder.Property(c => c.NationalCode)
            .HasConversion(w => w.Value, r => new NationalCode(r));
        builder.OwnsOne(c => c.FullName, v =>
        {
            v.Property(q => q.FirstName).HasColumnName("FirstName");
            v.Property(q => q.LastName).HasColumnName("LastName");
        });
    }
}