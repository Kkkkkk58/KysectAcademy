package game;

import game.base.unit.types.Angel;
import game.base.unit.types.Devil;
import game.base.unit.types.Fury;
import game.base.unit.types.Lich;
import game.battle.BattleArmy;
import game.battle.BattleUnitsStack;
import game.battle.UserDecision;

import java.util.ArrayList;
import java.util.Scanner;

// Temp realize of Player (Bad)
public class Player {
    private String username;
    private BattleArmy army;

    public Player() {
        setUsername();
        createArmy();
    }

    private void setUsername() {
        System.out.println("Enter your username:");

        Scanner in = new Scanner(System.in);
        this.username = in.nextLine();
    }

    private void createArmy() {
        try {
            this.army = new BattleArmy(new ArrayList<>() {{
                add(new BattleUnitsStack(new Angel(), 10));
                add(new BattleUnitsStack(new Devil(), 2));
                add(new BattleUnitsStack(new Lich(), 5));
                add(new BattleUnitsStack(new Fury(), 4));
            }});
        } catch (Exception e) {
            System.out.println(e.toString());
        }
    }

    public BattleArmy getArmy() {
        return this.army;  // It's normal. It should change.
    }

    public UserDecision makeDecision(BattleUnitsStack stack) {
        System.out.println(this.username + " is your move");
        printStack(stack);
        System.out.println(
                "Wait = 0\n" +
                "Attack = 1\n" +
                "Give up = 2\n" +
                "Protect = 3\n" +
                "Use skills = 4"
        );
        Scanner in = new Scanner(System.in);
        Integer decision = in.nextInt();

        switch (decision) {
            case 0:
                return UserDecision.WAIT;
            case 1:
                return UserDecision.ATTACK;
            case 2:
                return UserDecision.GIVE_UP;
            case 3:
                return UserDecision.PROTECT;
            case 4:
                return UserDecision.USE_SKILLS;
            default:
                return null;
        }
    }

    // Use only if decision is attack
    public Integer getVictim() {
        Scanner in = new Scanner(System.in);
        return in.nextInt();
    }

    public void printArmy() {
        System.out.println("==================Army of " + this.username + "===================");
        for (BattleUnitsStack stack : army.getBattleUnitStacks()) {
            System.out.println(
                    stack.getUnit().getClass().getSimpleName() + "\t" +
                    "Counts=" + stack.getCountOfUnits().toString() + "\t" +
                    "HP=" + stack.getTotalHitPoints() + "\t" +
                    "Defence=" + stack.getTotalDefence() + "\t" +
                    "Attack=" + stack.getTotalAttack() + "\t" +
                    (stack.getCountOfUnits() == 0 ? "DEAD" : "ALIVE")
            );
        }

        System.out.println("=================================================\n");
    }

    public void printStack(BattleUnitsStack stack) {
        System.out.println(
                "Your current stack - " +
                stack.getUnit().getClass().getSimpleName() + ": " +
                stack.getCountOfUnits().toString()
        );
    }

    public void printWinner(Player player) {
        System.out.println("Winner is " + player.username);
    }

    public void printRoundStarted() {
        System.out.println("\n\n\n====================New round====================\n\n");
    }

    public void printRoundFinished() {
        System.out.println("\n\n\n==================Round finished==================\n\n");
    }

    public void printBattleFinished() {
        System.out.println("\n\n\n==================Battle finished==================\n\n");
    }

    public void printIncorrectInputType() {
        System.out.println("Incorrect input type!");
    }
}
