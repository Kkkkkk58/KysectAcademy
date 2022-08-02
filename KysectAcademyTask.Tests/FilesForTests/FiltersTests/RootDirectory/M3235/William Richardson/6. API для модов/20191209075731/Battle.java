package heroes.battle;

import heroes.base.exceptions.FinishedRoundException;
import heroes.base.unit.Unit;

import java.util.ArrayList;

public class Battle {
    private final ArrayList<BattleArmy> armies;
    private final StacksQueue queue;

    // - Constructors -------------------------------------------------------------------------------------------------

    public Battle(ArrayList<BattleArmy> armies) {
        this.armies = new ArrayList<>(armies);
        queue = new StacksQueue(armies);
    }

    // - Getters ----------------------------------------------------------------------------------------------------

    public Boolean isFinishedRound() {
        try {
            getNextStack();
        } catch (FinishedRoundException exception) {
            return Boolean.TRUE;
        }
        return Boolean.FALSE;
    }

    public Boolean isFinishedBattle() {
        Integer countOfAliveArmies = 0;
        for (BattleArmy army : armies) {
            countOfAliveArmies += (army.isAlive() && !army.hasGivenUp() ? 1 : 0);
        }
        return countOfAliveArmies == 1;
    }

    public BattleArmy getWinnerArmy() {
        for (BattleArmy army: armies) {
            if (army.isAlive() && !army.hasGivenUp()) {
                return army;
            }
        }
        return null;
    }

    public ArrayList<Action> getAllActions() {
        return (ArrayList<Action>)(ArrayList<?>)Mods.getMods(Action.class);
    }

    public ArrayList<Unit> getAllUnits() {
         return (ArrayList<Unit>)(ArrayList<?>)Mods.getMods(Unit.class);
    }

    public ArrayList<BattleArmy> getBattleArmies() {
        return new ArrayList<>(armies);
    }

    public BattleUnitsStack getNextStack() throws FinishedRoundException {
        return queue.getNextStack();
    }

    public ArrayList<BattleUnitsStack> getQueue() {
        return queue.getQueue();
    }

    // - Other --------------------------------------------------------------------------------------------------------

    public void updateArmiesForNewRound() {
        for (BattleArmy army : armies) {
            if (!army.hasGivenUp()) {
                army.updateStacksForNewRound();
            }
        }
    }
}
