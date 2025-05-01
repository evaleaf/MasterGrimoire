using MasterGrimoire.DiceLibrary.Implementations;
using MasterGrimoire.DiceLibrary.Models;
using System.Runtime.CompilerServices;

namespace MasterGrimoire.DiceLibrary.Interfaces;

public interface IDiceExpression
{
    void Add(IDiceExpression exprToAdd);
    void Add(int modifier);
    DiceExpressionResult Roll();
    string ToString();
    IDiceExpression ToIDiceExpression();
}
