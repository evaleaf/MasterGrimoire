// File: DiceRoller.Tests/DiceRollerTests.cs
using DiceRoller.Library.Implementations;
using Xunit;

namespace DiceRoller.Tests;

public class DiceRollerTests
{
    private readonly DiceRoller _diceRoller;

    public DiceRollerTests()
    {
        _diceRoller = new DiceRoller();
    }

    [Fact]
    public void Roll_ShouldReturnValueWithinRange()
    {
        // Arrange
        int sides = 6;

        // Act
        int result = _diceRoller.Roll(sides);

        // Assert
        Assert.InRange(result, 1, sides);
    }

    [Fact]
    public void Roll_ShouldThrowException_WhenSidesLessThanTwo()
    {
        // Arrange
        int sides = 1;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => _diceRoller.Roll(sides));
    }

    [Fact]
    public void RollMultiple_ShouldReturnCorrectNumberOfRolls()
    {
        // Arrange
        int sides = 6;
        int rolls = 5;

        // Act
        var results = _diceRoller.RollMultiple(sides, rolls);

        // Assert
        Assert.Equal(rolls, results.Count());
        Assert.All(results, result => Assert.InRange(result, 1, sides));
    }
}
