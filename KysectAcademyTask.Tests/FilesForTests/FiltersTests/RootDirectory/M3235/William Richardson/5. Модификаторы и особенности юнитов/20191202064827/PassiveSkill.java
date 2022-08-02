package game.base.unit.skills;

import game.base.exceptions.SkillCanNotUse;
import game.battle.Battle;
import game.battle.BattleUnitsStack;

public abstract class PassiveSkill extends Skill {
    protected String statusOfUsage;

    protected PassiveSkill(String title, String statusOfUsage, Double countOfUsage) {
        super(title, countOfUsage);
        this.statusOfUsage = statusOfUsage;
    }

    protected abstract void _activate(Battle battle, BattleUnitsStack carrier, BattleUnitsStack target);

    public void activate(Battle battle, BattleUnitsStack carrier, BattleUnitsStack target) throws SkillCanNotUse {
        checkUsage();
        _activate(battle, carrier, target);
        countOfUsage--;
    }

    public String getStatusOfUsage() {
        return statusOfUsage;
    }
}
