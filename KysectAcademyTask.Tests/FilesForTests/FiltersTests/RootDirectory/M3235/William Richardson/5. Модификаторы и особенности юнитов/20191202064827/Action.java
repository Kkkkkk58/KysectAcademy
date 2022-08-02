package game.battle;

public abstract class Action {
    protected String tittle;

    protected Action(String tittle) {
        this.tittle = tittle;
    }

    public String getTittle() {
        return tittle;
    }
}
