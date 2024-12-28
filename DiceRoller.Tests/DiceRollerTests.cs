
using System.Net.Quic;
using DiceRoller.Library.Implementations;
using DiceRoller.Library.Interfaces;
using DiceRoller.Library.Models;
using Xunit;

namespace DiceRoller.Tests;

public class DiceRollerTests
{
    private readonly IDiceRoller<IDiceExpression> _diceRoller;

    public DiceRollerTests()
    {
        _diceRoller = new DiceRoller.Library.Implementations.DiceRoller();
    }

    [Fact]
    public void Roll_ShouldReturnValueWithinRange()
    {
        // Arrange
        int sides = 6;
        int quantity = 10;
        Random randNum = new Random();
        var niterations = Enumerable.Range(0,100);

        Action<DiceExpressionResult> testAction = new Action<DiceExpressionResult>(res => {
            Assert.Equal(quantity, res.Values.Count()); // Che lanci n (quantity) dadi
            Assert.InRange(res.Total, quantity, sides * quantity); // Che il dado lanciato sia fra i possibili valori del dado
        });

        // Act & Assert: per Roll(quantity,sides)
        DiceExpressionResult[] rolledWithRollSidesAndQty = niterations.Select(el => _diceRoller.Roll(quantity,sides)).ToArray();
        Assert.All(rolledWithRollSidesAndQty, testAction);

        // Act & Assert: per Roll(expression)
        DiceExpressionResult[] rolledWithRollExpress = niterations.Select(el => _diceRoller.Roll($"{quantity}d{sides}")).ToArray();
        Assert.All(rolledWithRollExpress, testAction);

        // Act & Assert: per un solo dado con Roll(sides)
        quantity = 1; 
        DiceExpressionResult[] rolledWithRollSides = niterations.Select(el => _diceRoller.Roll(sides)).ToArray();
        Assert.All(rolledWithRollSides, testAction);

    }

    [Fact]
    public void Expression_CheckToStringConsistency(){
        // Arrange
        int sides = 6;
        int quantity = 10;
        string valueToCheck = $"{quantity}d{sides}";
        Assert.Equal(valueToCheck, new DiceExpression(valueToCheck).ToString());
        Assert.Equal(valueToCheck, new DiceExpression(quantity, sides).ToString());
    }

}
