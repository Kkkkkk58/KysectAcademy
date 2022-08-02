package game.base.unit.skills.active;

import game.base.unit.skills.ActiveSkill;
import game.battle.Battle;
import game.battle.BattleUnitsStack;

public class Curse extends ActiveSkill {
    public Curse(Double countOfUsage) {
        super("Curse", countOfUsage);
    }

    public Curse() {
        this(1.0);
    }

    @Override
    protected void _activate(Battle battle, BattleUnitsStack carrier, BattleUnitsStack target) {
        target.changeAttack(-12);
    }
}
