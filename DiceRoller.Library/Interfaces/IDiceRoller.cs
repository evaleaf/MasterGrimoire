using DiceRoller.Library.Models;

namespace DiceRoller.Library.Interfaces;

public interface IDiceRoller {
    DiceExpressionResult Roll(int faces);
    DiceExpressionResult Roll(int rollNumbers, int faces);

    DiceExpressionResult Roll(IDiceExpression expr);
    DiceExpressionResult Roll(string writtenExpr);
}