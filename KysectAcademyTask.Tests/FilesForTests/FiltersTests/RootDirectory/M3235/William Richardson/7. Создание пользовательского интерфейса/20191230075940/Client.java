package heroes.clientserver;

import heroes.engine.battle.Action;
import heroes.engine.battle.BattleArmy;
import heroes.engine.battle.BattleUnitsStack;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.util.ArrayList;

public class Client {
    private Socket socket;
    private ObjectInputStream inputStream;
    private ObjectOutputStream outputStream;

    private String name;
    private BattleArmy army;

    public Client(String name) throws IOException {
        this.name = name;
        socket = new Socket("localhost", 8080);
        outputStream = new ObjectOutputStream(socket.getOutputStream());  // First must be created output stream
        inputStream = new ObjectInputStream(socket.getInputStream());
    }

    public String getName() {
        return name;
    }

    public BattleArmy getBattleArmy() {
        return army;
    }

    public void setBattleArmy(BattleArmy battleArmy) {
        army = battleArmy;
    }

    public void sendToServer() {
        try {
            outputStream.writeObject(new Player(name, army));
            System.out.println("Was sended");
            String answer = inputStream.readUTF();  // Waiting for while server has registered all players
            System.out.println("HELLO " + answer);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void sendToServerAnswerAction(Action action, Object... args) {

    }

    public ArrayList<BattleUnitsStack> getEnemyStacks() {
        return new ArrayList<>();
    }

    public ArrayList<FrontendStacksQueue> getQueue() {
        return new ArrayList<>();
    }

    @Override
    public String toString() {
        return name;
    }

}
