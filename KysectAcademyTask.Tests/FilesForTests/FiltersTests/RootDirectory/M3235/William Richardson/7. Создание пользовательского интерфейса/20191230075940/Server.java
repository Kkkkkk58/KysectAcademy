package heroes.clientserver;

import heroes.engine.battle.Battle;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;

public class Server extends Thread {
    private final int PORT = 8080;
    private ServerSocket server;
    private ArrayList<NodeStructure> structures;

    private Battle battle;
    private ArrayList<Player> players;
    private final Integer NUMBER_OF_PLAYERS = 1;

    public Server() {
        try {
            server = new ServerSocket(PORT);
            structures = new ArrayList<>();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void run() {
        waitingForPlayers();
        setPlayersFromClient();
        sendThatAllPlayersHaveRegistered();
    }

    private void waitingForPlayers() {
        try {
            while (structures.size() != NUMBER_OF_PLAYERS) {
                Socket socket = server.accept();
                structures.add(new NodeStructure(socket));
                System.out.println("New client!");
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
        System.out.println("ALL CLIENT REGISTERED");
    }

    private void setPlayersFromClient() {
        for (NodeStructure nodeStructure : structures) {
            System.out.println(nodeStructure);
            try {
                Player player = (Player)nodeStructure.getInputStream().readObject();
                System.out.println(player.getName());
                nodeStructure.setPlayer(player);
                System.out.println("SERVER " + nodeStructure.getPlayer());
            } catch (IOException | ClassNotFoundException e) {
                e.printStackTrace();
            }
        }
    }

    private void sendThatAllPlayersHaveRegistered() {
        for (NodeStructure nodeStructure : structures) {
            try {
                nodeStructure.getOutputStream().writeUTF("OK");
            } catch (IOException  e) {
                e.printStackTrace();
            }
        }
    }
}


class NodeStructure extends Thread {
    private Player player;
    private Socket socket;
    private ObjectInputStream inputStream;
    private ObjectOutputStream outputStream;

    NodeStructure(Socket socket) throws IOException{
        this.socket = socket;
        outputStream = new ObjectOutputStream(socket.getOutputStream());
        inputStream = new ObjectInputStream(socket.getInputStream());
    }

    public Socket getSocket() {
        return socket;
    }

    public ObjectInputStream getInputStream() {
        return inputStream;
    }

    public ObjectOutputStream getOutputStream() {
        return outputStream;
    }

    public Player getPlayer() {
        return player;
    }

    public void setPlayer(Player player) {
        this.player = player;
    }
}
