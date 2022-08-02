package game.battle;

import game.base.Army;
import game.base.exceptions.ArmyCanNotFight;
import game.base.exceptions.ArmyHasNotFreeStack;
import game.base.exceptions.ArmyHasNotThatStack;
import javafx.util.Pair;

import java.util.ArrayList;

public class BattleArmy extends Army {
    // Constants
    private final Integer MAX_COUNT_OF_UNITS_STACKS = 9;

    // Variables
    private Boolean hasGivenUp;
    private ArrayList<BattleUnitsStack> stacks;

    // - Constructors -------------------------------------------------------------------------------------------------

    public BattleArmy(ArrayList<BattleUnitsStack> stacks) throws Exception {
        super(new ArrayList<>(stacks));  // validate inside
        this.stacks = new ArrayList<>(stacks);
        this.hasGivenUp = Boolean.FALSE;

        // Add idArmy in BattleUnitsStacks
        for (BattleUnitsStack battleUnitsStack : stacks) {
            battleUnitsStack.setParentArmy(this);
        }
    }

    // - Getters ------------------------------------------------------------------------------------------------------

    public ArrayList<BattleUnitsStack> getBattleUnitStacks() {
        return new ArrayList<>(stacks);
    }

    public Boolean hasGivenUp() {
        return hasGivenUp;
    }

    // - Setters ------------------------------------------------------------------------------------------------------

    void setHasGivenUp(Boolean hasGivenUp) {
        this.hasGivenUp = hasGivenUp;
    }

    // - Flags --------------------------------------------------------------------------------------------------------

    public Boolean isAlive() {
        for (BattleUnitsStack battleUnitsStack : stacks) {
            if (battleUnitsStack.getCountOfUnits() != 0) {
                return Boolean.TRUE;
            }
        }
        return Boolean.FALSE;
    }

    private Boolean hasUnitsForBattle() {
        for (BattleUnitsStack battleUnitsStack : stacks) {
            if (battleUnitsStack.canFight()) {
                return Boolean.TRUE;
            }
        }
        return Boolean.FALSE;
    }

    private Boolean canFight() throws ArmyCanNotFight {
        if (hasGivenUp() || !hasUnitsForBattle()) {
            throw new ArmyCanNotFight("Army can't fight");
        }
        return Boolean.TRUE;
    }


    // - Other --------------------------------------------------------------------------------------------------------

    void addNewStack(BattleUnitsStack battleUnitsStack) throws Exception {
        // Validation
        if (stacks.size() == MAX_COUNT_OF_UNITS_STACKS) {
            throw new ArmyHasNotFreeStack("Battle army doesn't have free stack for this stack");
        }

        stacks.add(new BattleUnitsStack(battleUnitsStack));
    }

    void eraseStack(BattleUnitsStack battleUnitsStack) throws ArmyHasNotThatStack {
        for (BattleUnitsStack curBattleUnitsStack : stacks) {
            if (curBattleUnitsStack.equals(battleUnitsStack)) {
                stacks.remove(battleUnitsStack);
                return;
            }
        }

        // Validation
        throw new ArmyHasNotThatStack("Battle army doesn't have that stack");
    }

    /**
     * This function work not only with initiative, because we have method like 'wait'.
     * It compares two fields (initiative and waiting of stack).
     */
    private Pair<Double, Boolean> getMaxInitiativeParameters() {
        Double maxInitiative = Double.POSITIVE_INFINITY;  // Because in waiting the opposite
        Boolean maxIsWaiting = Boolean.TRUE;

        for (BattleUnitsStack battleUnitsStack : stacks) {
            Double initiative = battleUnitsStack.getTotalInitiative();
            Boolean isWaiting = battleUnitsStack.getIsWaiting();
                if (battleUnitsStack.canFight() &&
                        battleUnitsStack.hasBetterParametersThan(maxInitiative, maxIsWaiting)) {
                maxInitiative = initiative;
                maxIsWaiting = isWaiting;
            }
        }
        return new Pair<>(maxInitiative, maxIsWaiting);
    }

    ArrayList<BattleUnitsStack> getMaxInitiativeUnits() throws ArmyCanNotFight {
        // Validate, that army can fight in a battle
        canFight();

        Pair<Double, Boolean> parameters = getMaxInitiativeParameters();
        ArrayList<BattleUnitsStack> unitsForBattle = new ArrayList<>();

        for (BattleUnitsStack battleUnitsStack : stacks) {
            if (battleUnitsStack.canFight() &&
                    battleUnitsStack.getIsWaiting() == parameters.getValue() &&
                    battleUnitsStack.getTotalInitiative().equals(parameters.getKey())) {
                unitsForBattle.add(battleUnitsStack);
            }
        }
        return unitsForBattle;
    }

    void updateStacksForNewRound() {
        for (BattleUnitsStack stack : stacks) {
            stack.setWasInBattle(Boolean.FALSE);
            stack.setIsWaiting(Boolean.FALSE);
        }
    }

    void updateStacksAfterAct() throws Exception {
        // TODO: something
    }

    void giveUp() {
        hasGivenUp = Boolean.TRUE;
    }
}
