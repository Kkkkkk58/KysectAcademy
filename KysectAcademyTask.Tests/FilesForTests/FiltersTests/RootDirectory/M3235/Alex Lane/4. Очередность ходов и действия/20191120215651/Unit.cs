using System;
using System.Collections.Generic;

namespace Lab2
{
  public class Unit {

    private readonly string _type;
    private readonly int _attack, _defence;
    private readonly int _randomDeviation;
    private readonly float _initiative;
    
    private readonly List<string> _abilities = new List<string>();

    public List<string> PAbilities {
      get => _abilities;
    }

    public int PHp { get; }
    public int PAttack { get; }
    public int PDefence { get; }
    public int PRandomDeviation { get; }
    public float PInitiative { get ; }

    public Unit(string type, int hp, int att, int def, int deviation, float init) {
      _type = type;
      PHp = hp;
      _attack = att;
      _defence = def;
      _randomDeviation = deviation;
      _initiative = init;
    }

    public string GetStringType() {
      return _type;
    }
  }

  internal class Program
  {
    public static void Main(string[] args)
    {
      UnitStack marineStack = new UnitStack(new Marine(), 10);
      Army firstArmy = new Army();
      firstArmy.AddStack(marineStack);
      
      firstArmy.ShowArmy();
      firstArmy.DeleteStack(0);
      Console.WriteLine();
      firstArmy.ShowArmy();
    }
  }
}