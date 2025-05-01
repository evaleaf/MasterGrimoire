
using System.ComponentModel.DataAnnotations;
using System.Net.Quic;
using System.Runtime.CompilerServices;
using MasterGrimoire.DiceLibrary.Implementations;
using MasterGrimoire.DiceLibrary.Interfaces;
using MasterGrimoire.DiceLibrary.Models;
using Xunit;

namespace MasterGrimoire.Tests;

public class DiceRollerTests
{
    private readonly IDiceRoller<IDiceExpression> _diceRoller;
    private int _randomIterationResultCheckCount = 10;

    public DiceRollerTests()
    {
        _diceRoller = new DiceRoller();
    }

	[Theory]
	[InlineData(8, 0, 1)]
	[InlineData(8, 0, 0)]
	[InlineData(1, 40, 8)]
	[InlineData(2, 40, 3)]
	[InlineData(10, 0, 0)]
	[InlineData(10, 1, 5)]
	[InlineData(0, 10, 50)]
	[InlineData(1, 1, 0)]
	[InlineData(1, 1, 1)]
	public void TestOfHowManyShouldRolledAccordingToMyOpinion(int quantity, int sides, int modifier)
      {
            var resWithModifier = _diceRoller.Roll(quantity, sides, modifier);
            // se c'è lo 0 fra quantity e sides, allora non viene tirato il dado. Il risultato sarà 0.
            // se c'è sides 1, allora il dado non verrà tirato. Il risultato sarà sempre quantity.
		// L'aggiunta del modificatore implica semplicemente un +{quantity}d1. Quindi aumenterebbe solamente i nonRollati.

            Assert.Equal(1 + (quantity * sides == 0 ? 1 : 0) + (sides == 1 && quantity != 0 ? 1 : 0), resWithModifier.Values.Where(e => e.WereRolled == RolledInfo.NotRolled).Count());
		Assert.Equal((quantity * sides != 0  && sides != 1? quantity : 0), resWithModifier.Values.Where(e => e.WereRolled == RolledInfo.Rolled).Count());

		var resWithoutModifier = _diceRoller.Roll(quantity, sides);
		Assert.Equal(quantity * sides == 0 || sides == 1 ? 1 : 0, resWithoutModifier.Values.Where(e => e.WereRolled == RolledInfo.NotRolled).Count());
		Assert.Equal((quantity * sides != 0 && sides != 1 ? quantity : 0), resWithoutModifier.Values.Where(e => e.WereRolled == RolledInfo.Rolled).Count());
	}

	private Action<DiceExpressionResult> getTestRollResultAction(int quantity, int sides, int modifier = 0){
            return new Action<DiceExpressionResult>(res => {
                  var moltipleThisToApplySign = (quantity * sides) < 0 ? - 1 : 1;
			var rangeNumb1 = modifier + (quantity * moltipleThisToApplySign); // caso risultato sempre 1 X nVolte
			var rangeNumb2 = modifier + (Math.Abs(sides * quantity) * moltipleThisToApplySign); // caso sempre il massimo: sides X nVolte
			Assert.InRange(res.Total,
                        Math.Min(rangeNumb1, rangeNumb2),
				Math.Max(rangeNumb1, rangeNumb2)
			);
        });
    } 

    [Fact]
    public void RollQuantitySides_ShouldReturnValueWithinRange()
    {
        int sides = 4;
        int quantity = 20;
        Random randNum = new Random();
        var niterations = Enumerable.Range(0,_randomIterationResultCheckCount);

        DiceExpressionResult[] rolled = niterations.Select(el => _diceRoller.Roll(quantity,sides)).ToArray();
        Assert.All(rolled, getTestRollResultAction(quantity, sides));
    }

    [Theory]
    [InlineData(10,4,300)]
    [InlineData(2,4,400)]
    [InlineData(2,4,-400)]
    [InlineData(0,8,90)]
    [InlineData(1,5,600)]
    [InlineData(8,1,600)]
    [InlineData(8,0,600)]
    [InlineData(0,0,600)]
    [InlineData(10,0,0)]
    [InlineData(0,10,0)]
    [InlineData(0,0,10)]
    [InlineData(0,0,0)]
    public void RollExpressionWithModifier_ShouldReturnValueWithinRange(int quantity, int sides, int modifier)
    {
        Random randNum = new Random();
        var niterations = Enumerable.Range(0,_randomIterationResultCheckCount);

        DiceExpressionResult[] rolled = niterations.Select(el => {
              var result =_diceRoller.Roll($"{quantity}d{sides}{modifier:+0;-#}");
              return result;
        }).ToArray();
        Assert.All(rolled, getTestRollResultAction(quantity, sides, modifier));
    }

    [Fact]
    public void RollSides_ShouldReturnValueWithinRange()
    {
        // Arrange
        int sides = 3;
        Random randNum = new Random();
        var niterations = Enumerable.Range(0,_randomIterationResultCheckCount);

        DiceExpressionResult[] rolled = niterations.Select(el => _diceRoller.Roll(sides)).ToArray();
        Assert.All(rolled, getTestRollResultAction(1, sides));
    }


    [Fact]
    public void ActuallyRollingAllTheDices(){
        int seed = 85;
        var _diceRoller = new DiceRoller(seed);
        var _diceRollerClone = new DiceRoller(seed);

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
        var _diceRoller = new DiceRoller(seed);
        var _diceRollerClone = new DiceRoller(seed);

        string expressionComplete = "2d20";
        string expressionShort = "2d20";

        // I due roller usano lo stesso seed, quindi avranno gli stessi valori random
        var res1 = _diceRoller.Roll(expressionComplete); 
        var res2 = _diceRollerClone.Roll(expressionShort);

        Assert.Equal(res1.Total, res2.Total);
        Assert.Equal(res1.Values.Take(0), res2.Values.Take(0));
        Assert.Equal(res1.Values.Take(1), res2.Values.Take(1));
    }
}
