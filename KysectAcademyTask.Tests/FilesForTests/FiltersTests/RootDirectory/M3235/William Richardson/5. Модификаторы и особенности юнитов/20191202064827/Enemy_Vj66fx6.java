package game.base.unit.skills.passive;

import game.base.unit.skills.PassiveSkill;
import game.battle.Battle;
import game.battle.BattleUnitsStack;

public class EnemyNotResist extends PassiveSkill {
    public EnemyNotResist(Double countOfUsage) {
        super("Enemy doesn't resist", "AFTER_ATTACK", countOfUsage);
    }

    public EnemyNotResist() {
        this(Double.POSITIVE_INFINITY);
    }

    @Override
    protected void _activate(Battle battle, BattleUnitsStack carrier, BattleUnitsStack target) {
        target.addSkill(new UnitNotResist(1.0));
    }
}
