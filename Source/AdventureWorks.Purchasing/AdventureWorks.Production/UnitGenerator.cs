using UnitGenerator;

namespace AdventureWorks.Production
{

    /// <summary>
    /// ID of Unit
    /// </summary>
    [UnitOf(typeof(int), UnitGenerateOptions.Validate, "{0:###,###,###}")]
    public partial struct UserId
    {
        private partial void Validate()
        {
            if (value < 20 is false) throw new Exception($"Invalid value range: {value}");
        }
    }

}
