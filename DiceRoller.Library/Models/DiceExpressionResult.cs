namespace DiceRoller.Library.Models;

public class DiceExpressionResult
{
    public IEnumerable<int> Values { get; set; } = Enumerable.Empty<int>();

    public int Total { get; set; }
    public DiceExpressionResult(IEnumerable<int> values)
    {
        Values = values;
        Total = values.Sum();
    }

    public DiceExpressionResult(int value){
        Values = [value];
        Total = value;
    }
}