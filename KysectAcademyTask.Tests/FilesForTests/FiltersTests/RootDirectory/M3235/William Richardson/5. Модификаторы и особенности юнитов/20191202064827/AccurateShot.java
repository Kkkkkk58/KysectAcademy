package game.base.unit.skills.passive;

import game.base.unit.skills.PassiveSkill;
import game.base.unit.skills.active.Attack;
import game.battle.Battle;
import game.battle.BattleUnitsStack;

public class AccurateShot extends PassiveSkill {
    public AccurateShot(Double countOfUsage) {
        super("Accurate shot", "ATTACK", countOfUsage);
    }

    public AccurateShot() {
        this(Double.POSITIVE_INFINITY);
    }

    @Override
    protected void _activate(Battle battle, BattleUnitsStack carrier, BattleUnitsStack target) {
        Integer targetDefence = target.getDefence();
        target.setDefence(0);

        try {
            new Attack().activate(battle, carrier, target);
        } catch (Exception e) {
            // It's impossible
        }

        target.setDefence(targetDefence);
    }
}
