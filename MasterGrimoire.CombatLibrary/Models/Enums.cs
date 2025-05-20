using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterGrimoire.CombatLibrary.Models
{
	public enum OwnshipType
	{
		Player = 1,
		Allie = 2,
		Enemy = 3,
		Neutral = 4,
	}

	public enum DamageTypes
	{
		Acid = 1,
		Bludgeoning = 2,
		Cold = 3,
		Fire = 4,
		Force = 5,
		Lightning = 6, 
		Necrotic = 7, 
		Piercing = 8,
		Poison = 9,
		Psychic = 10, 
		Radiant = 11, 
		Slashing = 12,
		Thunder = 13
	}

	public enum Conditions
	{
		Blinded = 1,
		Charmed = 2,
		Deafened = 3,
		Frightened = 4,
		Grappled = 5,
		Incapacitated = 6,
		Paralyzed = 7,
		Petrified = 8,
		Poisoned = 9,
		Prone = 10,
		Restrained = 11,
		Stunned = 12,
		Suppressed = 13
	}

}
