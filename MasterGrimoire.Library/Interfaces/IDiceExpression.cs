using MasterGrimoire.Library.Implementations;
using MasterGrimoire.Library.Models;
using System.Runtime.CompilerServices;

namespace MasterGrimoire.Library.Interfaces;

public interface IDiceExpression
{
    void Add(IDiceExpression exprToAdd);
    void Add(int modifier);
    DiceExpressionResult Roll();
    string ToString();
    IDiceExpression ToIDiceExpression();
}
