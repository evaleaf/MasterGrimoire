using DiceRoller.Library.Models;

namespace DiceRoller.Library.Interfaces;

public interface IDiceRoller<T> where T : IDiceExpression {
    DiceExpressionResult Roll(int faces);
    DiceExpressionResult Roll(int rollNumbers, int faces);

    DiceExpressionResult Roll(T expr);
    DiceExpressionResult Roll(string writtenExpr);
}