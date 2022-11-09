using UnitGenerator;

namespace AdventureWorks.HumanResources
{

    /// <summary>
    /// ID of Employee
    /// </summary>
    [UnitOf(typeof(int), UnitGenerateOptions.DapperTypeHandler)]
    public partial struct EmployeeId
    {
    }

}
