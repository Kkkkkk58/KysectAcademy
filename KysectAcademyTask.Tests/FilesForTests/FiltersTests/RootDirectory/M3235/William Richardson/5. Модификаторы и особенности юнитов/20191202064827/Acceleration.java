package game.base.unit.skills.active;

import game.base.unit.skills.ActiveSkill;
import game.battle.Battle;
import game.battle.BattleUnitsStack;

public class Acceleration extends ActiveSkill {
    public Acceleration(Double countOfUsage) {
        super("Acceleration", countOfUsage);
    }

    public Acceleration() {
        this(1.0);
    }

    @Override
    protected void _activate(Battle battle, BattleUnitsStack carrier, BattleUnitsStack target) {
        target.changeInitiativeInPercents(1.4);
    }
}
