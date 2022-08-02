using System;
using System.Collections.Generic;

namespace Lab2 {
    public class ArmyPicking {

        public List<Unit> units = new List<Unit>();

        public Army[] army = new Army[2];

        public ArmyPicking() {
            army[0] = new Army();
            army[1] = new Army();
        }
        public void Pick(int armySide) {
            int choice;
            while (true) {
                Console.WriteLine("-------PICK YOUR UNITS------- ");
                Console.WriteLine(0 + " End Picking");
                for (int i = 0; i < units.Count; i++) {
                    Console.WriteLine(i + 1 + " " + units[i].GetStringType());
                }

                Console.WriteLine();

                try {
                    choice = Convert.ToInt32(Console.ReadLine());
                    if (choice == 0) break;
                    Console.WriteLine("---------ENTER COUNT--------- ");
                    Console.WriteLine();
                    
                    int count = Convert.ToInt32(Console.ReadLine());
                    
                    try { 
                        army[armySide].AddStack(new UnitStack(units[choice - 1], count)); 
                    }
                    catch { 
                        Console.WriteLine("Can't add army"); 
                        throw;
                    }
                }
                catch {
                    Console.WriteLine("Wrong option");
                }
            }
        }
    }
}