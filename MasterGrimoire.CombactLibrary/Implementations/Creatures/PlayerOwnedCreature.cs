using MasterGrimoire.CombatLibrary.Interfaces;
using MasterGrimoire.CombatLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterGrimoire.CombatLibrary.Implementations.Creatures
{
	public class PlayerOwnedCreature : ICreature
	{
		public string ID { get; internal set; }
		public string Name { get; set; }
		public OwnshipType OwnshipType { get => OwnshipType.Player; }

		public int? Initiative { get; set; }

		public int? HP { get; set; }
		public int? MaxHP { get; set; }
		public string Notes { get; set; }
		public List<(Conditions, int?)>? Conditions { get; set; }
		public List<DamageTypes>? ResistenceTo { get; set; }
		public List<DamageTypes>? ImmuneTo { get; set; }

		
		public void Reset()
		{
			Initiative = null;
			HP = null;
			Notes = null;
			Conditions = null;
		}

		object ICloneable.Clone()
		{
			return this.MemberwiseClone();
		}

		public PlayerOwnedCreature(string name)
		{
			ID = Guid.NewGuid().ToString();
			Name = name;
		}
	}
}
