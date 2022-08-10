import entity.AccountID;
import entity.Bank;
import entity.account.AccountRemote;
import javafx.util.Pair;
import service.Service;

import java.util.ArrayList;
import java.util.Arrays;

public class Main {

    public static void main(String[] args)
    {
        Bank b1 = Service.instance.addBank("Bank 1");
        Bank b2 = Service.instance.addBank("Bank 2");
        b1.setDebitRules(0.01f);
        b2.setCreditRules(100, 1000);
        b1.setDepositRules(Arrays.asList(new Pair(110.f, 0.01f), new Pair(1800.f, 0.05f), new Pair(0.f, 0.1f)), 40);

        int c1 = b1.registerClient("John Wick", null, null);
        int c2 = b2.registerClient("Tyrell Wellick", null, null);
        AccountID aid1 = b1.openDebitAccount(c1);
        AccountID aid2 = b2.openCreditAccount(c2);
        AccountID aid3 = b1.openDepositAccount(c1);

        System.out.println("Acc2 id:" + aid2);
        //System.out.println("Acc3 id:" + aid3.accNumber);

        AccountRemote remote = b1.login(aid1);
        AccountRemote remote2 = b2.login(aid2);
        AccountRemote remote3 = b1.login(aid3);

        remote.deposit(30);
        remote.withdraw(5);
        System.out.println("bal1: " + remote.balance());
        System.out.println(remote.transfer(new AccountID(1001), 15));
        System.out.println(remote.transfer(new AccountID(1000), 15));
        System.out.println("bal1: " + remote.balance());
        System.out.println("bal2: " + remote2.balance());
        System.out.println(remote2.transfer(aid3, 100));
        System.out.println(remote3.transfer(aid2, 100));
        System.out.println("bal1: " + remote.balance());
        System.out.println("bal2: " + remote2.balance());
        System.out.println("bal3: " + remote3.balance());

        System.out.println("\nMonth later...");
        Service.instance.countDays(30);
        System.out.println("bal1: " + remote.balance());
        System.out.println("bal2: " + remote2.balance());
        System.out.println("bal3: " + remote3.balance());


        System.out.println("\nMonth later...");
        Service.instance.countDays(30);
        System.out.println("bal1: " + remote.balance());
        System.out.println("bal2: " + remote2.balance());
        System.out.println("bal3: " + remote3.balance());


        System.out.println("\n6 month later...");
        Service.instance.countDays(180);

        System.out.println("bal1: " + remote.balance());
        System.out.println("bal2: " + remote2.balance());
        System.out.println("bal3: " + remote3.balance());

        System.out.println(Service.instance.cancelTransaction(1));
        //System.out.println(Service.instance.cancelTransaction(0));
        System.out.println("bal1: " + remote.balance());
        System.out.println("bal2: " + remote2.balance());
        System.out.println("bal3: " + remote3.balance());
    }
}
