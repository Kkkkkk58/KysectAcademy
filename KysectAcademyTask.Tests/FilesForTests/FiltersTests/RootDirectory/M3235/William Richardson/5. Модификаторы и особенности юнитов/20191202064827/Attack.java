package game.battle.actions;

import game.battle.Action;
import game.battle.Battle;
import game.battle.BattleUnitsStack;

public class Attack extends Action {
    public Attack() {
        super("Attack");
    }

    public void execute(Battle battle, BattleUnitsStack attacking, BattleUnitsStack victim) throws Exception {
        if (attacking.hasOverloadedAction("ATTACK")) {
            attacking.usePassiveSkills(battle, victim, "ATTACK");
        } else {
            new game.base.unit.skills.active.Attack().activate(battle, attacking, victim);
        }
        attacking.usePassiveSkills(battle, victim, "AFTER_ATTACK");
        victim.usePassiveSkills(battle, attacking, "ANSWER_TO_ATTACK");
        attacking.setWasInBattle(Boolean.TRUE);
    }
}