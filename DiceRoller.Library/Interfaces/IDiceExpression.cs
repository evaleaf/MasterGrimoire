using DiceRoller.Library.Implementations;
using DiceRoller.Library.Models;

namespace DiceRoller.Library.Interfaces;

public interface IDiceExpression
{
    void Add(IDiceExpression exprToAdd);
    void Add(int modifier);
    DiceExpressionResult Roll();
    string ToString();
}
