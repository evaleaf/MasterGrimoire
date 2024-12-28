using DiceRoller.Library.Interfaces;
using DiceRoller.Library.Models;

namespace DiceRoller.Library.Implementations;

public class DiceRoller : IDiceRoller<IDiceExpression>
{
    Random randomizer;
        public DiceRoller(){
            randomizer = new Random();
        }

        public DiceRoller(int seed){
            randomizer = new Random(seed);
        }

        public DiceExpressionResult Roll(int faces)
        {
            return Roll(new DiceExpression(1, faces));
        }

        public DiceExpressionResult Roll(int rollNumbers, int faces)
        {
            
            return Roll(new DiceExpression(rollNumbers, faces));
        }

        public DiceExpressionResult Roll(string expressionString)
        {
            // Default parse
            return Roll(DiceExpression.Parse(expressionString));
        }

        public DiceExpressionResult Roll(DiceExpression expression){
            List<int> listOfResult = new List<int>();
            foreach (var faceDice in expression.Dices){
                for (var indexRollForDice = 0; indexRollForDice < Math.Abs(faceDice.Quantity); indexRollForDice++){
                    var resOfThisRoll = (randomizer.Next(Math.Abs(faceDice.DiceFaces) - 1) + 1) * (faceDice.Quantity < 0 ? -1 : 1);
                    listOfResult.Add(Math.Sign(faceDice.DiceFaces) *resOfThisRoll);
                }
            }
            return new DiceExpressionResult(listOfResult);
        }




    public DiceExpressionResult Roll(IDiceExpression expr)
    {
        throw new NotImplementedException();
    }
}