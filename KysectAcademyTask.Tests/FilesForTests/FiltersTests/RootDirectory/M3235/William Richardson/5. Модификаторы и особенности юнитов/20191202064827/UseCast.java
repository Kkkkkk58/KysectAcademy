package game.battle.actions;

import game.base.unit.skills.ActiveSkill;
import game.battle.Action;
import game.battle.Battle;
import game.battle.BattleUnitsStack;

public class UseCast extends Action {
    public UseCast() {
        super("Use cast");
    }

    public void execute(Battle battle, BattleUnitsStack carrier, BattleUnitsStack target,
                        ActiveSkill cast) throws Exception {
        cast.activate(battle, carrier, target);
        carrier.setWasInBattle(Boolean.TRUE);
    }
}
