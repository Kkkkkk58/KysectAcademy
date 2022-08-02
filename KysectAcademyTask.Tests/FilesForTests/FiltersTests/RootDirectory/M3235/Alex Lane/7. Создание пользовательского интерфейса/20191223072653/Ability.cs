namespace Lab2 {
    public abstract class Ability {
        public Ability() {}
        public abstract double UseAbility(BattleUnitStack attacking, BattleUnitStack defending);
    }
}