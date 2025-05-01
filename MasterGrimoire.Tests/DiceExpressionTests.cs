
using System.Net.Quic;
using MasterGrimoire.DiceLibrary.Implementations;
using MasterGrimoire.DiceLibrary.Interfaces;
using MasterGrimoire.DiceLibrary.Models;
using Xunit;

namespace MasterGrimoire.Tests;

public class DiceExpressionTests
{

    [Theory]
    [InlineData(6,10)]
    public void CheckPositiveToString(int sides, int quantity){
        string expcted = $"{Math.Abs(quantity)}d{Math.Abs(sides)}";       
        Assert.Equal(expcted, new DiceExpression(expcted).ToString());
        Assert.Equal(expcted, new DiceExpression(quantity, sides).ToString());
    }

    [Fact]
    public void CheckDice1ToString(){
        string expcted = "8";       
        Assert.Equal(expcted, new DiceExpression(8,1).ToString());
    }

    [Theory]
    [InlineData(-6,0)]
    [InlineData(6,0)]
    [InlineData(0,-6)]
    [InlineData(0,6)]
    public void CheckDice0ToString(int sides, int quantity){
        string expcted = string.Empty;       
        Assert.NotEqual(expcted, new DiceExpression(sides,quantity).ToString());
    }


    [Theory]
    [InlineData(-6,10)]
    [InlineData(2,-6)]
    [InlineData(2,6)]
    [InlineData(-5,-4)]
    public void CheckNegativeToString(int sides, int quantity){
        string expcted = $"{(sides * quantity >= 0 ? "" : "-")}{Math.Abs(quantity)}d{Math.Abs(sides)}";       
        Assert.Equal(expcted, new DiceExpression(expcted).ToString());
        Assert.Equal(expcted, new DiceExpression(quantity, sides).ToString());
    }

    [Fact]
    public void AddToStringConcistency(){
        string expr1String = "4d20";
        string expr2String = "1d4";

        var expr = new DiceExpression(expr1String);
        expr.Add(new DiceExpression(expr2String));
        expr.Add(new DiceExpression(expr1String));
        Assert.Equal($"{expr1String}+{expr2String}+{expr1String}", expr.ToString());
    }

}
