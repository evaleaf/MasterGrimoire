using MasterGrimoire.DiceLibrary.Models;

namespace MasterGrimoire.DiceLibrary.Interfaces;

public interface IDiceRoller<T> where T : IDiceExpression
{
	DiceExpressionResult Roll(int faces);
	DiceExpressionResult Roll(int quantity, int faces);
	DiceExpressionResult Roll(int quantity, int faces, int modifier);

	DiceExpressionResult Roll(T expr);
	DiceExpressionResult Roll(string writtenExpr);
}