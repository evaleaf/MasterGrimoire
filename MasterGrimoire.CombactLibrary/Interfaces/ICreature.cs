using MasterGrimoire.CombactLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterGrimoire.CombactLibrary.Interfaces
{
	public interface ICreature: ICloneable
	{
		public string ID { get; }
		public string Name { get; set; }
		public OwnshipType OwnshipType { get; }
		public int? Initiative { get; }
		public int? HP { get; set; }
		public int? MaxHP { get; set; }
		public string Notes { get; set; }
		public List<(Conditions, int?)> Conditions { get; set; }

		public List<DamageTypes>? ResistenceTo { get; set; }
		public List<DamageTypes>? ImmuneTo { get; set; }

		public void Reset();
	}
}
