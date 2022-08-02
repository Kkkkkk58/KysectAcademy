package game.battle;

import game.base.UnitsStack;
import game.base.unit.Unit;

public class BattleUnitsStack extends UnitsStack {
    private BattleArmy parentArmy;

    private Boolean wasInBattle;
    private Boolean isWaiting;

    private Integer totalHitPoints;
    private Integer defenceBuff;
    private Integer attackBuff;

    private Double initiativeBuff;

    {
        wasInBattle = Boolean.FALSE;
    }

    // - Constructors -------------------------------------------------------------------------------------------------

    public BattleUnitsStack(Unit unit, Integer count) throws Exception {
        super(unit, count);  // Validate inside
        this.totalHitPoints = unit.getHitPoints() * count;
        this.defenceBuff = 0;
        this.attackBuff = 0;
        this.initiativeBuff = 0.0;
        this.isWaiting = Boolean.FALSE;
    }

    public BattleUnitsStack(BattleUnitsStack battleUnitsStack) throws Exception {
        super(battleUnitsStack);  // Validate inside
        this.totalHitPoints = battleUnitsStack.getCountOfUnits() * battleUnitsStack.getUnit().getHitPoints();
        this.defenceBuff = battleUnitsStack.getDefenceBuff();
        this.attackBuff = battleUnitsStack.getAttackBuff();
        this.initiativeBuff = battleUnitsStack.getInitiativeBuff();
        this.isWaiting = battleUnitsStack.getIsWaiting();
    }

    // - Getters ------------------------------------------------------------------------------------------------------

    public Integer getTotalHitPoints() {
        return totalHitPoints;
    }

    public Integer getTotalDefence() {
        return Math.max(0, this.getUnit().getDefence() + defenceBuff);
    }

    public Integer getTotalAttack() {
        return Math.max(0, this.getUnit().getAttack() + attackBuff);
    }

    public Double getTotalInitiative() {
        return Math.max(0, this.getUnit().getInitiative() + initiativeBuff);
    }

    public Integer getDefenceBuff() {
        return defenceBuff;
    }

    public Integer getAttackBuff() {
        return attackBuff;
    }

    public Double getInitiativeBuff() {
        return initiativeBuff;
    }

    public Boolean wasInBattle() {
        return wasInBattle;
    }

    public Boolean isAlive() {
        return getCountOfUnits() != 0;
    }

    public BattleArmy getParentArmy() {
        return this.parentArmy;
    }

    public Integer getCountOfUnits() {
        return (totalHitPoints + getUnit().getHitPoints() - 1) / getUnit().getHitPoints();
    }

    Boolean getIsWaiting() {
        return isWaiting;
    }

    // - Setters ------------------------------------------------------------------------------------------------------

    void setParentArmy(BattleArmy parentArmy) {
        this.parentArmy = parentArmy;
    }

    void setWasInBattle(Boolean wasInBattle) {
        this.wasInBattle = wasInBattle;
    }

    void setIsWaiting(Boolean value) {
        isWaiting = value;
    }

    // - Implementation -----------------------------------------------------------------------------------------------

    private Integer decreaseHitPoints(Integer diff) {
        totalHitPoints = Math.max(0, totalHitPoints - diff);
        return totalHitPoints;
    }

    private void changeDefenceInPercents(Double percents) throws Exception {
        if (percents < 0) {
            throw new Exception("Percents must be positive number");
        }
        defenceBuff = (int)(getTotalDefence() * percents - getUnit().getDefence());
    }

    Boolean canFight() {
        return this.getCountOfUnits() != 0 && !this.wasInBattle();
    }

    /**
     * Function, that compare with parameters (initiative, isWaiting) for getting maximal initiative
     */
    Boolean hasBetterParametersThan(Double otherInitiative, Boolean otherIsWaiting) {
        if (otherIsWaiting == Boolean.TRUE && isWaiting == Boolean.FALSE) {
            return Boolean.TRUE;
        } else if (isWaiting == Boolean.FALSE &&
                otherIsWaiting == Boolean.FALSE &&
                getTotalInitiative() > otherInitiative) {
            return Boolean.TRUE;
        } else if (isWaiting == Boolean.TRUE &&
                otherIsWaiting == Boolean.TRUE &&
                getTotalInitiative() < otherInitiative) {
            return Boolean.TRUE;
        } else {
            return Boolean.FALSE;
        }
    }

    // - Decisions ----------------------------------------------------------------------------------------------------

    void attack(BattleUnitsStack victim) {
        Integer countOfHitPoints = 0;
        if (this.getTotalAttack() == victim.getTotalDefence()) {
            // TODO: some actions
        } else if (this.getTotalAttack() > victim.getTotalDefence()) {
            countOfHitPoints = this.getUnit().getDamage().calcResultDamage(
                    (damage) -> (int)(getCountOfUnits() * 1.0 * damage *
                                    (1 + 0.05 * (getTotalAttack() - victim.getTotalDefence())))
            );
        } else if (this.getTotalAttack() < victim.getTotalDefence()) {
            countOfHitPoints = this.getUnit().getDamage().calcResultDamage(
                    (damage) -> (int)(getCountOfUnits() * 1.0 * damage /
                                    (1 + 0.05 * (victim.getTotalDefence() - getTotalAttack())))
            );
        }
        System.out.println("Hit : " + countOfHitPoints);  // TODO: delete. Just for logs.
        setWasInBattle(Boolean.TRUE);
        victim.decreaseHitPoints(countOfHitPoints);
    }

    void protect() throws Exception {
        System.out.print("Defence: " + getTotalDefence());  // TODO: delete. Just for logs.
        changeDefenceInPercents(1.4);
        System.out.println(" -> " + getTotalDefence());  // TODO: delete. Just for logs.
        setWasInBattle(Boolean.TRUE);
    }
}
