package heroes.clientserver;

import heroes.engine.battle.BattleUnitsStack;

public class FrontendStacksQueue {
    private Client client;
    private BattleUnitsStack stack;

    FrontendStacksQueue(Client client, BattleUnitsStack stack) {
        this.client = client;
        this.stack = stack;
    }

    public Client getClient() {
        return client;
    }

    public BattleUnitsStack getStack() {
        return stack;
    }

    public String getStackTitle() {
        return stack.getUnit().getTitle();
    }

    public Double getStackInitiative() {
        return stack.getInitiative();
    }

    public Boolean getStackIsWaiting() {
        return stack.getIsWaiting();
    }
}
