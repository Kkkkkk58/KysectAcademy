package game.base.unit.skills.passive;

import game.base.unit.skills.PassiveSkill;
import game.base.unit.skills.active.Attack;
import game.battle.Battle;
import game.battle.BattleArmy;
import game.battle.BattleUnitsStack;

public class HitAll extends PassiveSkill {
    public HitAll(Double countOfUsage) {
        super("Hit all", "ATTACK", countOfUsage);
    }

    public HitAll() {
        this(Double.POSITIVE_INFINITY);
    }

    @Override
    protected void _activate(Battle battle, BattleUnitsStack carrier, BattleUnitsStack target) {
        for (BattleArmy army : battle.getBattleArmies()) {
            if (!army.equals(carrier.getParentArmy())) {
                for (BattleUnitsStack stack : army.getBattleUnitStacks()) {
                    try {
                        new Attack().activate(battle, carrier, stack);
                    } catch (Exception e) {
                        // It's impossible
                    }
                }
            }
        }
    }
}
