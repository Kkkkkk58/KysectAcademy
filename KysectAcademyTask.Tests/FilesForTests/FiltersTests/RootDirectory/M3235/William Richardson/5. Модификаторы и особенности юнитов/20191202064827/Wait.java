package game.base.unit.skills.active;

import game.base.exceptions.SkillCanNotUse;
import game.base.unit.skills.ActiveSkill;
import game.battle.Battle;
import game.battle.BattleUnitsStack;

public class Wait extends ActiveSkill {
    public Wait(Double countOfUsage) {
        super("Wait", countOfUsage);
    }

    public Wait() {
        this(Double.POSITIVE_INFINITY);
    }

    @Override
    protected void _activate(Battle battle, BattleUnitsStack carrier, BattleUnitsStack target) throws SkillCanNotUse {
        if (carrier.isWaiting()) {
            throw new SkillCanNotUse("You already have used wait");
        }
        carrier.setIsWaiting(Boolean.TRUE);
    }
}
