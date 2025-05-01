using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Swift;
using MasterGrimoire.Library.Interfaces;
using MasterGrimoire.Library.Models;
using static MasterGrimoire.Library.Models.DiceExpressionResult;

namespace MasterGrimoire.Library.Implementations;

public class DiceExpression : IDiceExpression
{
    private Random randomizer;
    internal List<AtomDiceExpression> Dices { get; set; } = new List<AtomDiceExpression>();
      internal class AtomDiceExpression
      {
		public int Quantity { get; set; }
		public int DiceFaces { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return this is null;
            }
            return Equals(obj as AtomDiceExpression);
        }
    }

    
    public DiceExpression()
    {
        randomizer = new Random();
    }



    public DiceExpression(DiceExpression expr) : this()
    {
        Add(expr);
    }

    public DiceExpression(string diceExpression) : this()
    {
        var expr = DiceExpression.Parse(diceExpression);
        Add(expr);
    }

    public DiceExpression(int numberOfDices, int diceFaces) : this()
    {
            if (numberOfDices * diceFaces < 0)
            {
                  Add(-Math.Abs(numberOfDices), Math.Abs(diceFaces));
            }
            else
            {
                  Add(Math.Abs(numberOfDices), Math.Abs(diceFaces));
            }
        
    }
	public DiceExpression(int numberOfDices, int diceFaces, int modifier) : this(numberOfDices, diceFaces)
	{
            Add(modifier);
	}

	public void Add(int number, int diceFaces)
    {
        Dices.Add(new AtomDiceExpression() { DiceFaces = diceFaces, Quantity = number });
    }

    public void Add(int modifier)
    {
        Dices.Add(new AtomDiceExpression() { Quantity = modifier, DiceFaces = 1 });
    }

    public void Add(string expression)
    {
        var e = new DiceExpression(expression);
        this.Add(e);
    }

    public void Add(IDiceExpression exprToAdd)
    {
        DiceExpression toAdd;
        if (exprToAdd is DiceExpression){
            toAdd = (DiceExpression)exprToAdd;
        }
        else {
            var writtenExpr = exprToAdd.ToString();
            toAdd = new DiceExpression(writtenExpr);
        }
        foreach (var diceToAdd in toAdd.Dices)
        {
            if (diceToAdd != null){
                this.Dices.Add(diceToAdd);
            }
            
        }
    }

    public override string ToString()
    {
        var ret = string.Empty;
        if (Dices != null & Dices.Any())
        {
            ret = string.Join("", Dices
                .Select((e, index) =>
                {
                    string signPlus = string.Empty;
			  if (e.DiceFaces * e.Quantity < 0)
                    {
                        signPlus = "-";
                    }
                    else if (index != 0) // il primo non ha davanti il + se positivo
                    {
                        signPlus = "+";
                    }

                    if (e.DiceFaces == 1)
                    {
                        return signPlus + Math.Abs(e.Quantity).ToString();
                    }
                    else
                    {
                        return signPlus + Math.Abs(e.Quantity).ToString() + "d" + Math.Abs(e.DiceFaces).ToString();
                    }

                })
            );
        }
        return ret;
    }

      public IDiceExpression ToIDiceExpression()
      {
            return this;
      }

	public static DiceExpression Parse(string DiceExpression)
    {
        DiceExpression = DiceExpression.Replace(" ", string.Empty).ToLower();
        var splittedExpression = DiceExpression.Split('+');
        var diceExpr = new DiceExpression();
        foreach (var subexpre in splittedExpression)
        {
            var splittedExpressionWithMinus = subexpre.Split('-');
            for (var i = 0; i < splittedExpressionWithMinus.Length; i++)
            {
                var elemToAdd = splittedExpressionWithMinus[i];
                if (!string.IsNullOrEmpty(elemToAdd))
                {
                    var isMinus = (i != 0); // only the first occurrence is with plus since the (+) split. The elements in this array are splitted using (-).
			if (elemToAdd.Contains('d'))
			{
				var components = elemToAdd.Split('d');
				int numDices = int.Parse(components[0]);
				int diceFaces = int.Parse(string.Join("",elemToAdd.Skip(components[0].Length + 1)));
				diceExpr.Add(new DiceExpression((isMinus ? -1 : 1) * numDices, Math.Abs(diceFaces)));
			}
			else
                  {
                        var modif = int.Parse(elemToAdd);
                        diceExpr.Add((isMinus ? -1 : 1) * modif);
                  }
                }

            }

        }
        return diceExpr;
    }

    public override bool Equals(object obj)
    {
        if (obj is null){
            return this is null;
        }
        return Equals(obj as DiceExpression);
    }

    public bool Equals(DiceExpression obj)
    {
        if (obj.Dices.Count != this.Dices.Count)
            return false;
        var toCheck = this.Dices.Zip(obj.Dices);

        return toCheck.All(zipped =>
         {
            var sameDice = zipped.First.DiceFaces == zipped.Second.DiceFaces;
            var sameQnt = zipped.First.Quantity == zipped.Second.Quantity;
            return sameDice && sameQnt;
         });
    }

    public override int GetHashCode()
    {
        return String.GetHashCode(this.ToString());
    }

    public DiceExpressionResult Roll(){
        List<SingleDiceResult> listOfResult = new List<SingleDiceResult>();
        foreach (var faceDice in this.Dices){
            for (var indexRollForDice = 0; indexRollForDice < Math.Abs(faceDice.Quantity); indexRollForDice++){
                var resOfThisRoll = (randomizer.Next(Math.Abs(faceDice.DiceFaces) - 1) + 1) * (faceDice.Quantity < 0 ? -1 : 1);
                listOfResult.Add(new SingleDiceResult(Math.Sign(faceDice.DiceFaces) *resOfThisRoll, RolledInfo.Rolled));
            }
        }
        return new DiceExpressionResult(listOfResult, this);
    }
}