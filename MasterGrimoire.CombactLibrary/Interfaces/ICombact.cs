using MasterGrimoire.CombactLibrary.Models;
using MasterGrimoire.Library.Interfaces;
using MasterGrimoire.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterGrimoire.CombactLibrary.Interfaces
{
	internal interface ICombact : ICloneable 
	{
		public string CombactName { get; }
		public List<ICreature> Creatures { get; }
		public int GetCurrentTurnNumber();
		public ICreature GetCurrentCreature();
		public ICreature NextCreature();
		public ICreature NextTurn();
		public void Reset();

		public DiceExpressionResult Roll(string diceExpression);

		public void AddCreature(ICreature creature);
		public void RollInitiative(ICreature creature);
		public void RollInitiative();
		public void ShowInitiatives();
		public void ShowCreatureDetails(ICreature creature);
		public void AddCreatures(List<ICreature> creature);

		public void Damage(ICreature creature, List<(DamageTypes, int)> damages);
		public void Damage(List<ICreature> creatures, List<(DamageTypes, int)> damages);

		public void Heal(ICreature creature, int heal);
		public void Heal(List<ICreature> creatures, int heal);

		public void AddNote(ICreature creature, string AddNote);
		public void OverrideNote(ICreature creature, string AddNote);

		public string GetConditionInfo(Conditions condition);
		public void AddCondition(ICreature creature, Conditions condition);
		public void AddCondition(ICreature creature, Conditions condition, int turnTimer);
		public void RemoveCondition(ICreature creature, Conditions condition);

		public ICreature CloneCreature(ICreature creature);

	}
}
