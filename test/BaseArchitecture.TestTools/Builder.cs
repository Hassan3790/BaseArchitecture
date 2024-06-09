using BaseArchitecture.TestTools.Employees;

namespace BaseArchitecture.TestTools
{
    public class Builder
    {
        public static EmployeeBuilder Employee => new EmployeeBuilder();
    }
}
