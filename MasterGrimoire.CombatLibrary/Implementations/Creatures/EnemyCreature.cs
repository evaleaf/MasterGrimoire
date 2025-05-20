using MasterGrimoire.CombatLibrary.Interfaces;
using MasterGrimoire.CombatLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterGrimoire.CombatLibrary.Implementations.Creatures
{
	public class MasterManagedCreature : ICreature
	{
		public string ID { get; internal set; }
		public string Name { get; set; }
		public OwnshipType OwnshipType { get; set; }

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
			Notes = "";
			Conditions = null;
		}

		object ICloneable.Clone()
		{
			return this.MemberwiseClone();
		}

		public MasterManagedCreature(string name, OwnshipType ownshipType , int maxHP, int? hp = null, List<DamageTypes> resistenceTo = null, List<DamageTypes> immuneTo = null)
		{
			ID = Guid.NewGuid().ToString();
			Name = name;
			OwnshipType = ownshipType;
			MaxHP = maxHP;
			HP = hp ?? MaxHP;
			ResistenceTo = resistenceTo;
			ImmuneTo = immuneTo;
		}
	}
}
