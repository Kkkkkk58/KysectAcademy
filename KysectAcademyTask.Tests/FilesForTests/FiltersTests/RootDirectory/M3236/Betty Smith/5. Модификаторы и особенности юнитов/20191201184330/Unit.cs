using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Unit
{
    public enum Types
    {
        ARBALET,
        ANGEL,
        BONEDRAGON,
        CYCLOPE,
        DEVIL,
        FURY,
        GRYPHONE,
        HYDRA,
        LICH,
        SHAMAN,
        SKELETON
    }
    public Types Type { get; private set; }
    public int HitPoints { get; private set; }
    public int Attack { get; private set; }
    public int Defence { get; private set; }
    public int MinDamage { get; private set; }
    public int MaxDamage { get; private set; }
    public double Initiative { get; private set; }
    public string Description { get; protected set; }
    public int HasSpell { get; private set; }
    public string Tribe { get; protected set; }
    public enum SpellTypes
    {   
        UNIT, //when you must choose target
        SIDE, //also used to summon stack
        ALL
    }
    public SpellTypes SpellType { get; protected set; }


    public Unit(string UnitType, int UnitHitpoints, int UnitAttack, int UnitDefence, int UnitMinDamage, int UnitMaxDamage, double UnitInitiative,int UnitHasSpell)
    {
        Type = (Types)System.Enum.Parse(typeof(Types), UnitType);
        HitPoints = UnitHitpoints;
        Attack = UnitAttack;
        Defence = UnitDefence;
        MinDamage = UnitMinDamage;
        MaxDamage = UnitMaxDamage;
        Initiative = UnitInitiative;
        HasSpell = UnitHasSpell;
        Description = "";
        Tribe = "";
    }

    public virtual bool Spell(BattleUnitStack me, BattleUnitStack target) { return true; }
    public virtual bool Spell(BattleUnitStack me, BattleArmy red, BattleArmy blue) { return true; } //define your side yourself
    public virtual bool Spell(BattleUnitStack me, List<BattleUnitStack> queue) { return true; }
    public virtual void PassiveEffect(Battle curGame,int myID) { }
}
