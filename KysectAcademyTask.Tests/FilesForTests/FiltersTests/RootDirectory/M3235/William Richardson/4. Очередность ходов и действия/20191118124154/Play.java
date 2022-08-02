package game;

import game.battle.Battle;
import game.battle.BattleArmy;
import game.battle.BattleUnitsStack;

import java.util.ArrayList;

// Temp realize of Play (not very good)
public class Play {
    private static ArrayList<Player> players;

    public static void main(String[] args) throws Exception {
        players = new ArrayList<>() {{
            add(new Player());
            add(new Player());
        }};

        ArrayList<BattleArmy> armies = new ArrayList<>();
        for (Player player : players) {
            armies.add(player.getArmy());
        }

        startBattle(new Battle(armies));
    }

    private static void startBattle(Battle battle) throws Exception {
        while (!battle.isFinishedBattle()) {
            printRoundStarted();
            while (!battle.isFinishedRound() && !battle.isFinishedBattle()) {
                // We can't get exception, because it check isFinishedRound()
                BattleUnitsStack stack = battle.getNextStack();
                Player player = getPlayer(stack);
                printAllPlayers(player);

                switch (player.makeDecision(stack)) {
                    case ATTACK:
                        battle.attack(stack, getVictimForAttack(player));
                        break;
                    case PROTECT:
                        battle.protect(stack);
                        break;
                    case WAIT:
                        battle.wait(stack);
                        break;
                    case GIVE_UP:
                        battle.giveUp(stack.getParentArmy());
                        break;
                    case USE_SKILLS:
                        break;
                    default:
                        break;
                }
            }
            printRoundFinished();
            battle.updateArmiesForNewRound();
        }
        printBattleFinished();
        printWinner(getPlayer(battle.getWinnerArmy()));
    }

    private static Player getPlayer(final BattleArmy army) {
        for (Player player : players) {
            if (player.getArmy().equals(army)) {
                return player;
            }
        }
        return null;
    }

    private static Player getPlayer(final BattleUnitsStack stack) {
        for (Player player : players) {
            if (player.getArmy().equals(stack.getParentArmy())) {
                return player;
            }
        }
        return null;
    }

    private static void printAllPlayers(Player curPlayer) {
        for (Player player : players) {
            if (!player.equals(curPlayer)) {
                player.printArmy();
            }
        }
        curPlayer.printArmy();
    }

    private static void printWinner(Player player) {
        players.get(0).printWinner(player);  // Crutch, because I use one console for two players
    }

    private static void printRoundStarted() {
        players.get(0).printRoundStarted();  // Crutch, because I use one console for two players
    }

    private static void printRoundFinished() {
        players.get(0).printRoundFinished();  // Crutch, because I use only one console for two players
    }

    private static void printBattleFinished() {
        players.get(0).printBattleFinished();  // Crutch, because I use only one console for two players
    }

    private static void printIncorrectInputType(Player player) {
        player.printIncorrectInputType();
    }

    // Crutch
    private static BattleUnitsStack getVictimForAttack(Player player) {
        Integer decision = player.getVictim();
        Integer army = decision / 10, stack = decision % 10 - 1;

        if (stack < 0) {
            printIncorrectInputType(player);
            return getVictimForAttack(player);
        }

        int cnt = -1;
        for (Player p : players) {
            if (!p.equals(player)) {
                cnt++;
            }
            if (cnt == army) {
                if (stack >= p.getArmy().getBattleUnitStacks().size()) {
                    printIncorrectInputType(player);
                    return getVictimForAttack(player);
                }
                return p.getArmy().getBattleUnitStacks().get(stack);
            }
        }
        return null;
    }
}
