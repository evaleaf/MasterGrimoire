using MasterGrimoire.DiceLibrary.Implementations;
using System.Linq.Expressions;

namespace MasterGrimoire.DiceLibrary.Models;

public class DiceExpressionResult
{
	public record SingleDiceResult
	{
		public int Result { get; set; }
		public RolledInfo WereRolled { get; set; }

		public SingleDiceResult(int result, RolledInfo wereRolled)
		{
			Result = result;
			WereRolled = wereRolled;
		}
	}


	public IEnumerable<SingleDiceResult> Values { get; set; } = Enumerable.Empty<SingleDiceResult>();

	public int Total { get; set; }
	public DiceExpression Expression { get; set; }
	public DiceExpressionResult(IEnumerable<SingleDiceResult> values, DiceExpression expression)
	{
		Values = values;
		Total = values.Select(e => e.Result).Sum();
		Expression = expression;
	}

	public DiceExpressionResult(SingleDiceResult result, DiceExpression expression)
	{
		Values = [result];
		Total = result.Result;
		Expression = expression;
	}
}