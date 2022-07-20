using System;

namespace RPGgame
{
    class game
    {
        public static List<Type> enemyPool = new List<Type> {typeof(Witch), typeof(Witch), typeof(Fighter), typeof(Fighter), typeof(Fighter), typeof(Tank), typeof(Monster), typeof(Monster)};
        public static void run()
        {
            gameConsole c = new gameConsole();
            Player plyr = new Player();
            //c.dialoguePrompt("A Pirate stumbles in, drunk on drunken wine and gold brimming his pockets.\nGive me an 'Ahoy'!", "ahoy", "Arggh, that's me matey.", "Eh, close enough.");
            turn(plyr);
        }

        /*TO-DO:    Search -> Fight enemy/discover treasure/find event
                    View Inventory (Does not progress turn)
                        - View/Use/Equip Weapons, Armor, Misc
                        - Repair Weapons/Armor
                    Rest/Heal (Lower chance of enemy encounter but no chance of other events, Heals without consuming) || Flee Battle
                    
        */
        public static void turn(Player plyr)
        {
            /*if(plyr.currentEnemy != null){
                Console.Write(  "------------------------------------------ \n" +
                                    plyr.currentEnemy.name + "  LVL: " + plyr.currentEnemy.level + "  HP: " + plyr.currentEnemy.health +
                                    "  ATT: " + plyr.currentEnemy.damage + "  DEF: " + plyr.currentEnemy.defense + "\n");
            }*/
            Console.Write(  "------------------------------------------ \n" +
                            "HP: " + plyr.health + "  LVL: " + plyr.level + " " + plyr.levelProgress + "/" + plyr.levelUpReq + " \n" + plyr.listStatus() +
                            "------------------------------------------ \n" +
                            "1. Search: Search for Enemies or Treasures \n" + 
                            "2. Inventory: View and Forge from your Inventory \n" +
                            "3. Rest/Heal: Recover 10 Health and Cleanse\n"+
                            "Select: "  );
            string? inp = Console.ReadLine();
            int dice = randomizer.roll(100);
            switch(inp){    
                case "1":
                    Console.Clear();
                    //roll item drop at % level
                    if(dice < 5){
                        randomizer.rollWeapon(plyr);
                    }
                    else if(dice < 10){
                        randomizer.rollArmor(plyr);
                    }
                    else if(dice < 20){
                        Heal healing = new Heal(plyr.level, "");
                        plyr.itemInventory.Add(healing);
                        Console.WriteLine("You have found a " + healing.name);
                    }
                    else{
                        //plyr.currentEnemy = new Witch(plyr.level); //temp
                        plyr.currentEnemy = getEnemy(plyr);
                        plyr.checkStatus();
                        combat(plyr, plyr.currentEnemy);
                    }
                    break;
                case "2":
                    viewInventory(plyr);
                    break;
                case "3":
                    Console.Clear();
                    //roll rest failed at 25%
                    if(dice < 25){
                        //plyr.currentEnemy = new Witch(plyr.level); //temp
                        plyr.currentEnemy = getEnemy(plyr);
                        Console.WriteLine(randomizer.restFailed(plyr.currentEnemy));
                        plyr.heal(5);
                        plyr.checkStatus();
                        combat(plyr, plyr.currentEnemy);
                    }
                    else{
                        Console.WriteLine("You feel refreshed. Restored Health and cursed afflictions lift from your body   +10 HP");
                        plyr.poison.Item1 = 0;
                        plyr.weakness.Item1 = 0;
                        plyr.heal(10);
                    }
                    break;
                case "abracadabra":
                    Console.Clear();
                    plyr.magic();
                    Console.WriteLine("A low voice booms 'ALAKAZAM' from the skies above");
                    break;
                case "thereisnocowlevel":
                    Console.Clear();
                    plyr.starter();
                    Console.WriteLine("A great storm is coming");
                    enemyPool.Add(typeof(Cow));
                    enemyPool.Add(typeof(Cow));
                    enemyPool.Add(typeof(Cow));
                    break;
                case "wwssadadba":
                    Console.Clear();
                    plyr.god();
                    Console.WriteLine("Starting reactors: Online. Enabling advanced systems: Online. Raising dongers. Error: Dongers missing. Aborting…");
                    break;
                default:
                    Console.Clear();
                    break;
            }
            turn(plyr);
        }
        public static Enemy getEnemy(Player plyr){
            int i = randomizer.roll(enemyPool.Count());
            Enemy enemy = (Enemy) Activator.CreateInstance(enemyPool.ElementAt(i), plyr.level);
            return enemy;
        }
        public static void combat(Player plyr, Enemy enemy)
        {
            string DUR, AMR;

            if(plyr.equippedWeapon.hasDurability){
                DUR = plyr.equippedWeapon.durability.ToString();
            }
            else{
                DUR = "INF";
            }
            
            if(plyr.equippedArmor.hasDurability){
                AMR = plyr.equippedArmor.durability.ToString();
            }
            else{
                AMR = "INF";
            }

            Console.Write(  "------------------------------------------ \n" +
                            enemy.name + "  LVL: " + enemy.level + "\nHP: " + enemy.health +
                            "  ATT: " + enemy.damage + " DEF: " + enemy.defense + "\n");
            Console.Write(  "------------------------------------------ \n" +
                            "HP: " + plyr.health + "  LVL: " + plyr.level + " (" + plyr.levelProgress + "/" + plyr.levelUpReq + ")" +
                            "  ATT: " + plyr.equippedWeapon.damage + " DUR: " + DUR + " DEF: " + plyr.equippedArmor.defense + " AMR: " + AMR + "\n" + plyr.listStatus() +
                            "------------------------------------------ \n" +
                            "1. Attack: Attack the enemy with your weapon \n" + 
                            "2. Inventory: View and Forge from your Inventory \n" +
                            "3. Flee: You aren't seriously considering this option \n"+
                            "Select: "  );
            string? inp = Console.ReadLine();
            switch(inp){    
                case "1":
                    Console.Clear();
                    plyr.attack(enemy);
                    if(enemy.health > 0){
                        enemy.attack(plyr);
                    }
                    else{
                        int xp = 10 + randomizer.roll(enemy.level) + enemy.level * 4;
                        Console.WriteLine("You have slain " + enemy.name + "   +" + xp + " XP");
                        enemy.drop(plyr);
                        plyr.levelProgress += xp;
                        plyr.checkStatus();
                        turn(plyr);
                    }
                    plyr.checkStatus();
                    combat(plyr, enemy);
                    break;
                case "2":
                    viewInventory(plyr);
                    break;
                case "3":
                    Console.Clear();
                    int penalty = enemy.damage / 2;
                    plyr.health -= penalty;
                    Console.WriteLine("You turned tail and ran from the enemy. " + enemy.name + " struck you for " + penalty + " health as you fled   -" + penalty + " HP");
                    turn(plyr);
                    break;
                case "abracadabra":
                    Console.Clear();
                    plyr.magic();
                    Console.WriteLine("A low voice booms 'ALAKAZAM' from the skies above");
                    break;
                case "thereisnocowlevel":
                    Console.Clear();
                    plyr.starter();
                    Console.WriteLine("A great storm is coming");
                    enemyPool.Add(typeof(Cow));
                    enemyPool.Add(typeof(Cow));
                    enemyPool.Add(typeof(Cow));
                    break;
                case "wwssadadba":
                    Console.Clear();
                    plyr.god();
                    Console.WriteLine("Starting reactors: Online. Enabling advanced systems: Online. Raising dongers. Error: Dongers missing. Aborting…");
                    break;
                default:
                    Console.Clear();
                    combat(plyr, enemy);
                    break;
            }
            combat(plyr, enemy);
        }

        public static void viewInventory(Player plyr)
        {
            Console.Clear();
            Console.Write(  "1. View Weapons \n" + 
                            "2. View Armor \n" +
                            "3. View Miscellaneous \n" +
                            //"4. Forge Equipment \n" +
                            "Select: "  );
            string? inp = Console.ReadLine();
            Console.Clear();

            switch(inp){
                case "1":
                    plyr.equipWeapon();
                    break;
                case "2":
                    plyr.equipArmor();
                    break;
                case "3":
                    plyr.useItem();
                    break;
                case "4":
                    break;
                case "abracadabra":
                    Console.Clear();
                    plyr.magic();
                    Console.WriteLine("A low voice booms 'ALAKAZAM' from the skies above");
                    break;
                case "thereisnocowlevel":
                    Console.Clear();
                    plyr.starter();
                    Console.WriteLine("A great storm is coming");
                    enemyPool.Add(typeof(Cow));
                    enemyPool.Add(typeof(Cow));
                    enemyPool.Add(typeof(Cow));
                    break;
                case "wwssadadba":
                    Console.Clear();
                    plyr.god();
                    Console.WriteLine("Starting reactors: Online. Enabling advanced systems: Online. Raising dongers. Error: Dongers missing. Aborting…");
                    break;
                default:
                    break;
            }

        }
    }
}