package game.base.unit.skills;

import game.battle.Battle;
import game.battle.BattleUnitsStack;

public abstract class ActiveSkill extends Skill {
    protected ActiveSkill(String title, Double countOfUsage) {
        super(title, countOfUsage);
    }

    protected abstract void _activate(Battle battle, BattleUnitsStack carrier,
                                      BattleUnitsStack target) throws Exception;

    public void activate(Battle battle, BattleUnitsStack carrier, BattleUnitsStack target) throws Exception {
        checkUsage();
        _activate(battle, carrier, target);
        countOfUsage--;
    }
}
