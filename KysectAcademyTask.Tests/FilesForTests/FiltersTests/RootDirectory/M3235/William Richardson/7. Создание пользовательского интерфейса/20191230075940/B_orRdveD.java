package heroes.frontend.forms.battle;

import heroes.clientserver.Client;
import heroes.clientserver.FrontendStacksQueue;

import heroes.engine.base.unit.ActiveSkill;
import heroes.engine.battle.Action;
import heroes.engine.battle.BattleUnitsStack;
import javafx.fxml.FXML;
import javafx.scene.control.Button;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.cell.PropertyValueFactory;

public class BattleController {
    private Client client;

    @FXML private Button attackButton;
    @FXML private Button protectButton;
    @FXML private Button waitButton;
    @FXML private Button giveUpButton;
    @FXML private Button useCastButton;

    @FXML private TableView<BattleUnitsStack> myArmyTable;
    @FXML private TableColumn<BattleUnitsStack, String> myArmyTitleCol;
    @FXML private TableColumn<BattleUnitsStack, String> myArmyCountCol;
    @FXML private TableColumn<BattleUnitsStack, String> myArmyHitPointsCol;
    @FXML private TableColumn<BattleUnitsStack, String> myArmyDefenceCol;
    @FXML private TableColumn<BattleUnitsStack, String> myArmyAttackCol;
    @FXML private TableColumn<BattleUnitsStack, String> myArmyInitiativeCol;
    @FXML private TableColumn<BattleUnitsStack, String> myArmyWaitingCol;
    @FXML private TableColumn<BattleUnitsStack, String> myArmyAliveCol;

    @FXML private TableView<BattleUnitsStack> enemyArmyTable;
    @FXML private TableColumn<BattleUnitsStack, String> enemyArmyTitleCol;
    @FXML private TableColumn<BattleUnitsStack, String> enemyArmyCountCol;
    @FXML private TableColumn<BattleUnitsStack, String> enemyArmyHitPointsCol;
    @FXML private TableColumn<BattleUnitsStack, String> enemyArmyDefenceCol;
    @FXML private TableColumn<BattleUnitsStack, String> enemyArmyAttackCol;
    @FXML private TableColumn<BattleUnitsStack, String> enemyArmyInitiativeCol;
    @FXML private TableColumn<BattleUnitsStack, String> enemyArmyWaitingCol;
    @FXML private TableColumn<BattleUnitsStack, String> enemyArmyAliveCol;

    @FXML private TableView<ActiveSkill> castTable;
    @FXML private TableColumn<ActiveSkill, String> castTitleCol;

    @FXML private TableView<FrontendStacksQueue> queueTable;
    @FXML private TableColumn<FrontendStacksQueue, String> queuePlayerCol;
    @FXML private TableColumn<FrontendStacksQueue, String> queueTitleCol;
    @FXML private TableColumn<FrontendStacksQueue, String> queueInitiativeCol;
    @FXML private TableColumn<FrontendStacksQueue, String> queueWaitingCol;

    public BattleController(Client client) {
        this.client = client;
    }

    @FXML
    private void initialize() {
        // Own
        myArmyTitleCol.setCellValueFactory(new PropertyValueFactory<>("unit"));
        myArmyCountCol.setCellValueFactory(new PropertyValueFactory<>("countOfUnits"));
        myArmyHitPointsCol.setCellValueFactory(new PropertyValueFactory<>("hitPoints"));
        myArmyDefenceCol.setCellValueFactory(new PropertyValueFactory<>("defence"));
        myArmyAttackCol.setCellValueFactory(new PropertyValueFactory<>("attack"));
        myArmyInitiativeCol.setCellValueFactory(new PropertyValueFactory<>("initiative"));
        myArmyWaitingCol.setCellValueFactory(new PropertyValueFactory<>("isWaiting"));
        myArmyAliveCol.setCellValueFactory(new PropertyValueFactory<>("isAlive"));
        myArmyTable.getItems().addAll(client.getBattleArmy().getBattleUnitStacks());

        // Enemy
        enemyArmyTitleCol.setCellValueFactory(new PropertyValueFactory<>("unit"));
        enemyArmyCountCol.setCellValueFactory(new PropertyValueFactory<>("countOfUnits"));
        enemyArmyHitPointsCol.setCellValueFactory(new PropertyValueFactory<>("hitPoints"));
        enemyArmyDefenceCol.setCellValueFactory(new PropertyValueFactory<>("defence"));
        enemyArmyAttackCol.setCellValueFactory(new PropertyValueFactory<>("attack"));
        enemyArmyInitiativeCol.setCellValueFactory(new PropertyValueFactory<>("initiative"));
        enemyArmyWaitingCol.setCellValueFactory(new PropertyValueFactory<>("isWaiting"));
        enemyArmyAliveCol.setCellValueFactory(new PropertyValueFactory<>("isAlive"));
        enemyArmyTable.getItems().addAll(client.getEnemyStacks());

        // Queue
        queuePlayerCol.setCellValueFactory(new PropertyValueFactory<>("client"));
        queueTitleCol.setCellValueFactory(new PropertyValueFactory<>("stackTitle"));
        queueInitiativeCol.setCellValueFactory(new PropertyValueFactory<>("stackInitiative"));
        queueWaitingCol.setCellValueFactory(new PropertyValueFactory<>("stackIsWaiting"));
        queueTable.getItems().addAll(client.getQueue());

        // Casts
        castTitleCol.setCellValueFactory(new PropertyValueFactory<>("title"));
//        castTable.getItems().addAll(queueTable.getItems().get(0).getStack().getActiveSkills());
    }

    @FXML
    private void clickedOnAttackButton() {
        Action attack = Action.getActionByTitle("Attack");
        client.sendToServerAnswerAction(attack, queueTable.getItems().get(0).getStack(), getSelectedEnemyStack());
        waitingAnswer();
    }

    @FXML
    private void clickedOnProtectButton() {
        Action protect = Action.getActionByTitle("Protect");
        client.sendToServerAnswerAction(protect, queueTable.getItems().get(0).getStack());
        waitingAnswer();
    }

    @FXML
    private void clickedOnWaitButton() {
        Action wait = Action.getActionByTitle("Wait");
        client.sendToServerAnswerAction(wait, queueTable.getItems().get(0).getStack());
        waitingAnswer();
    }

    @FXML
    private void clickedOnGiveUpButton() {
        Action giveUp = Action.getActionByTitle("Give up");
        client.sendToServerAnswerAction(giveUp, queueTable.getItems().get(0).getStack());
        waitingAnswer();
    }

    @FXML
    private void clickedOnUseCastButton() {
        Action useCast = Action.getActionByTitle("Use cast");
        client.sendToServerAnswerAction(
                useCast,
                getSelectedCast(),
                queueTable.getItems().get(0).getStack(),
                getSelectedEnemyStack()
        );
        waitingAnswer();
    }

    private BattleUnitsStack getSelectedEnemyStack() {
        return enemyArmyTable.getItems().get(enemyArmyTable.getSelectionModel().getSelectedIndex());
    }

    private ActiveSkill getSelectedCast() {
        return castTable.getItems().get(castTable.getSelectionModel().getSelectedIndex());
    }

    private void waitingAnswer() {
        changeStateOfButtons(false);
        updateTables();
        changeStateOfButtons(true);
    }

    private void updateTables() {

    }

    private void changeStateOfButtons(Boolean state) {
        attackButton.setDisable(state);
        protectButton.setDisable(state);
        waitButton.setDisable(state);
        giveUpButton.setDisable(state);
        useCastButton.setDisable(state);
    }
}
