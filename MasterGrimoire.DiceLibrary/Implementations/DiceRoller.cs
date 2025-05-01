using MasterGrimoire.DiceLibrary.Interfaces;
using MasterGrimoire.DiceLibrary.Models;
using static MasterGrimoire.DiceLibrary.Models.DiceExpressionResult;

namespace MasterGrimoire.DiceLibrary.Implementations;

public class DiceRoller : IDiceRoller<IDiceExpression>
{
	Random randomizer;
	public DiceRoller()
	{
		randomizer = new Random();
	}

	public DiceRoller(int seed)
	{
		randomizer = new Random(seed);
	}

	public DiceExpressionResult Roll(int faces)
	{
		return Roll(new DiceExpression(1, faces));
	}

	public DiceExpressionResult Roll(int rollNumbers, int faces)
	{
		var credatedExpr = new DiceExpression(rollNumbers, faces);
		return Roll(credatedExpr);
	}

	public DiceExpressionResult Roll(int rollNumbers, int faces, int modifier)
	{
		var credatedExpr = new DiceExpression(rollNumbers, faces, modifier);
		return Roll(credatedExpr);
	}

	public DiceExpressionResult Roll(string expressionString)
	{
		// Default parse
		var parsedExpression = DiceExpression.Parse(expressionString);
		return Roll(parsedExpression);
	}

	public DiceExpressionResult Roll(DiceExpression expression)
	{
		List<SingleDiceResult> listOfResult = new List<SingleDiceResult>();
		foreach (var dice in expression.Dices)
		{
			int correctSign = dice.DiceFaces * dice.Quantity > 0 ? 1 : -1;
			if (dice.DiceFaces * dice.Quantity == 0)
			{
				listOfResult.Add(new SingleDiceResult(0, RolledInfo.NotRolled)); 
			}
			else if (dice.DiceFaces == 1)
			{
				listOfResult.Add(new SingleDiceResult(Math.Abs(dice.Quantity) * correctSign, RolledInfo.NotRolled));
			}
			else
			{
				for (var indexRollForDice = 0; indexRollForDice < Math.Abs(dice.Quantity); indexRollForDice++)
				{
					var resOfThisRoll = (randomizer.Next(Math.Abs(dice.DiceFaces) - 1) + 1);
					listOfResult.Add(new SingleDiceResult(resOfThisRoll * correctSign, RolledInfo.Rolled));
				}

			}
		}
		return new DiceExpressionResult(listOfResult, expression);
	}




	public DiceExpressionResult Roll(IDiceExpression expr)
	{
		return new DiceExpression(expr.ToString()).Roll();
	}
}