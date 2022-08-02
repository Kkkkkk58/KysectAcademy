package game.battle;

import game.base.exceptions.ArmyCanNotFight;
import game.base.exceptions.FinishedRoundException;

import java.util.ArrayList;
import java.util.Random;

public class Battle {
    private final ArrayList<BattleArmy> armies;

    // - Constructors -------------------------------------------------------------------------------------------------

    public Battle(ArrayList<BattleArmy> armies) {
        this.armies = new ArrayList<>(armies);
    }

    // - Decisions ----------------------------------------------------------------------------------------------------

    public void attack(BattleUnitsStack attacking, BattleUnitsStack victim) throws Exception {
        attacking.attack(victim);
        updateArmiesAfterAct();
    }

    public void protect(BattleUnitsStack stack) throws Exception {
        stack.protect();
    }

    public void wait(BattleUnitsStack stack) {
        stack.setIsWaiting(Boolean.TRUE);
    }

    public void giveUp(BattleArmy coward) {
        coward.giveUp();
    }

    // - Flags --------------------------------------------------------------------------------------------------------

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

    // - Other --------------------------------------------------------------------------------------------------------

    public BattleArmy getWinnerArmy() {
        for (BattleArmy army: armies) {
            if (army.isAlive() && !army.hasGivenUp()) {
                return army;
            }
        }
        return null;
    }

    public void updateArmiesForNewRound() {
        for (BattleArmy army : armies) {
            if (!army.hasGivenUp()) {
                army.updateStacksForNewRound();
            }
        }
    }

    private void updateArmiesAfterAct() throws Exception {
        for (BattleArmy army : armies) {
            army.updateStacksAfterAct();
        }
    }

    public BattleUnitsStack getNextStack() throws FinishedRoundException {
        ArrayList<BattleUnitsStack> tempStacksForBattle = new ArrayList<>();
        Double currentInitiative = Double.POSITIVE_INFINITY;  // Because in waiting the opposite
        Boolean currentIsWaiting = Boolean.TRUE;

        for (BattleArmy army : armies) {
            ArrayList<BattleUnitsStack> unitsWithGoodParameters;
            BattleUnitsStack firstStack;

            // Check that this army has stack for battle
            try {
                unitsWithGoodParameters = army.getMaxInitiativeUnits();
                firstStack = unitsWithGoodParameters.get(0);
            } catch (ArmyCanNotFight exception) {
                continue;
            }

            // Get list of stacks with maximal initiative
            if (tempStacksForBattle.isEmpty() ||
                    firstStack.hasBetterParametersThan(currentInitiative, currentIsWaiting)) {

                // Full update of list
                tempStacksForBattle.clear();
                tempStacksForBattle.addAll(unitsWithGoodParameters);

                // Update variables
                currentInitiative = firstStack.getTotalInitiative();
                currentIsWaiting = firstStack.getIsWaiting();
            } else if (currentInitiative.equals(firstStack.getTotalInitiative()) &&
                            currentIsWaiting == firstStack.getIsWaiting()) {
                tempStacksForBattle.addAll(unitsWithGoodParameters);
            }
        }

        // Round was finished
        if (tempStacksForBattle.size() == 0) {
            throw new FinishedRoundException("Armies not have stacks for battle, because everyone has completed move");
        }

        // Get 1 random stack from array with the best initiative
        Integer randomIndex = new Random().nextInt(tempStacksForBattle.size());
        return tempStacksForBattle.get(randomIndex);
    }
}
