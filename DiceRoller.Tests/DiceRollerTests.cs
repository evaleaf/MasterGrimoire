
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

    [Theory]
    [InlineData(6,10)]
    public void Expression_CheckPositiveToString(int sides, int quantity){
        string expcted = $"{Math.Abs(quantity)}d{Math.Abs(sides)}";       
        Assert.Equal(expcted, new DiceExpression(expcted).ToString());
        Assert.Equal(expcted, new DiceExpression(quantity, sides).ToString());
    }

    [Fact]
    public void ActuallyRollingAllTheDices(){
        int seed = 85;
        var _diceRoller = new DiceRoller.Library.Implementations.DiceRoller(seed);
        var _diceRollerClone = new DiceRoller.Library.Implementations.DiceRoller(seed);

        string expressionComplete = "30d20-10d20";
        string expressionShort = "20d20";

        // I due roller usano lo stesso seed, quindi avranno gli stessi valori random
        var res1 = _diceRoller.Roll(expressionComplete); 
        var res2 = _diceRollerClone.Roll(expressionShort);

        // Mi aspetto che siano diversi in quanto l'espressione completa non deve essere ottimizzata
        Assert.NotEqual(res1, res2);
    }

    [Fact]
    public void CheckSeedIsUsingTheSeed(){
        int seed = 71;
        var _diceRoller = new DiceRoller.Library.Implementations.DiceRoller(seed);
        var _diceRollerClone = new DiceRoller.Library.Implementations.DiceRoller(seed);

        string expressionComplete = "2d20";
        string expressionShort = "2d20";

        // I due roller usano lo stesso seed, quindi avranno gli stessi valori random
        var res1 = _diceRoller.Roll(expressionComplete); 
        var res2 = _diceRollerClone.Roll(expressionShort);

        Assert.Equal(res1.Total, res2.Total);
        Assert.Equal(res1.Values.Take(0), res2.Values.Take(0));
        Assert.Equal(res1.Values.Take(1), res2.Values.Take(1));
    }

    
    [Fact]
    public void Expression_CheckDice1ToString(){
        string expcted = "8";       
        Assert.Equal(expcted, new DiceExpression(8,1).ToString());
    }

    [Theory]
    [InlineData(-6,0)]
    [InlineData(6,0)]
    [InlineData(0,-6)]
    [InlineData(0,6)]
    public void Expression_CheckDice0ToString(int sides, int quantity){
        string expcted = string.Empty;       
        Assert.Equal(expcted, new DiceExpression(sides,quantity).ToString());
    }



    [Theory]
    [InlineData(-6,10)]
    [InlineData(2,-6)]
    [InlineData(2,6)]
    [InlineData(-5,-4)]
    public void Expression_CheckNegativeToString(int sides, int quantity){
        string expcted = $"{(sides * quantity >= 0 ? "" : "-")}{Math.Abs(quantity)}d{Math.Abs(sides)}";       
        Assert.Equal(expcted, new DiceExpression(expcted).ToString());
        Assert.Equal(expcted, new DiceExpression(quantity, sides).ToString());
    }

    [Fact]
    public void Expression_AddToStringConcistency(){
        string expr1String = "4d20";
        string expr2String = "1d4";

        var expr = new DiceExpression(expr1String);
        expr.Add(new DiceExpression(expr2String));
        expr.Add(new DiceExpression(expr1String));
        Assert.Equal($"{expr1String}+{expr2String}+{expr1String}", expr.ToString());
    }

}
