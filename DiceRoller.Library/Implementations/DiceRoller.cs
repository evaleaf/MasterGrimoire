using DiceRoller.Library.Interfaces;
using DiceRoller.Library.Models;

namespace DiceRoller.Library.Implementations;

public class DiceRoller : IDiceRoller
{
    Random randomizer;
        public DiceManager(){
            randomizer = new Random();
        }

        public DiceManager(int seed){
            randomizer = new Random(seed);
        }

        public (int result, List<int> listOfResult) Roll(string expressionString)
        {
            return Roll(DiceExpression.Parse(expressionString));
        }

        public (int result, List<int> listOfResult) Roll(DiceExpression expression){
            var res = 0;
            List<int> listOfResult = new List<int>();
            foreach (var faceDice in expression.Dices){
                for (var indexRollForDice = 0; indexRollForDice < Math.Abs(faceDice.NumberOfDices); indexRollForDice++){
                    var resOfThisRoll = (randomizer.Next(Math.Abs(faceDice.DiceFaces) - 1) + 1) * (faceDice.NumberOfDices < 0 ? -1 : 1);
                    res += Math.Sign(faceDice.DiceFaces) * resOfThisRoll;
                    listOfResult.Add(Math.Sign(faceDice.DiceFaces) *resOfThisRoll);
                }
            }
            return (res, listOfResult);
        }

    public DiceResult Roll(int faces)
    {
        return new DiceExpression(1, faces);
    }

    public DiceResult Roll(int rollNumbers, int faces)
    {
        throw new NotImplementedException();
    }

    public DiceResult Roll(IDiceExpression expr)
    {
        throw new NotImplementedException();
    }
}