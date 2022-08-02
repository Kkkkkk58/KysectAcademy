package heroes.clientserver;

import heroes.engine.battle.BattleArmy;

import java.io.Serializable;

public class Player implements Serializable {
    private String name;
    private BattleArmy army;

    Player(String name, BattleArmy army) {
        this.name = name;
        this.army = army;
    }

    public String getName() {
        return name;
    }

    public BattleArmy getArmy() {
        return army;
    }
}
