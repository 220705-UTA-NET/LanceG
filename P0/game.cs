using System;

namespace RPGgame
{
    class game
    {
        public static void run()
        {
            gameConsole c = new gameConsole();
            Player plyr = new Player();
            //c.dialoguePrompt("A Pirate stumbles in, drunk on drunken wine and gold brimming his pockets.\nGive me an 'Ahoy'!", "ahoy", "Arggh, that's me matey.", "Eh, close enough.");
            turn(plyr);
        }

        /*TO-DO:    Search -> Fight enemy/discover treasure/find event
                    View Inventory
                        - View/Use/Equip Weapons, Armor, Misc
                        - Repair Weapons/Armor
                    Rest/Heal (Lower chance of enemy encounter but no chance of other events, Heals without consuming) || Flee Battle
                    
        */
        public static void turn(Player plyr)
        {
            Console.Write(  "1. Search: Search for Enemies or Treasures \n" + 
                            "2. Inventory: View and Forge from your Inventory \n" +
                            "3. Rest/Heal: Recover 10 Health \n"+
                            "Select: "  );
            string inp = Console.ReadLine();
            switch(inp){    
                case "1":
                    Console.Clear();
                    break;
                case "2":
                    viewInventory(plyr);
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("You feel refreshed. Restored 10 Health.");
                    plyr.health += 10;
                    if(plyr.health > 100)
                    {
                        plyr.health = 100;
                    }
                    break;
                default:
                    Console.Clear();
                    break;
            }
            turn(plyr);
        }

        public static int roll()
        {
            Random rand = new Random();
            int dice = rand.Next(10);
            return dice;
        }

        public static void viewInventory(Player plyr)
        {
            Console.Clear();
            Console.Write(  "1. View Weapons \n" + 
                            "2. View Armor \n" +
                            "3. View Miscellaneous \n" +
                            "4. Forge Equipment \n" +
                            "Select: "  );
            string inp = Console.ReadLine();
            Console.Clear();

            switch(inp){
                case "1":
                    plyr.equipWeapon();
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
                default:
                    break;
            }

        }
    }
}