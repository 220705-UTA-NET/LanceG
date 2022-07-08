using System;

namespace RPGgame
{
    class startGAME{
        static void Main(String[] args)
        {
            Console.Clear();
            runMenu();
        }

        static void runMenu()
        {
            Console.Write(  "-----------------------------------------\n" +
                            "G.rand A.dventure with M.agical E.nemies!\n" +
                            "-----------------------------------------\n" +
                            "Press ENTER to play\n" + 
                            "Press H for instructions \n" +
                            "Press X to exit\n" +
                            "Select: "  );

            string inp = Console.ReadLine();
            Console.Clear();

            gameConsole menu = new gameConsole();
            switch (inp.ToLower())
            {
                case "":
                    //begins prologue of game
                    menu.dialoguePrompt("You, a Sir Royal Knight, live in your Royal Castle...");
                    menu.dialoguePrompt("Graciously given by the great and Royal King...");
                    menu.dialoguePrompt("You have servants and maids to attend to your every need...");
                    menu.dialoguePrompt("And Royal Flowers in your garden.....");
                    menu.dialoguePrompt("And Royal Furniture in your bedroom.......");
                    menu.dialoguePrompt("And Royal Lamps in their... well... Lampposts...");
                    menu.dialoguePrompt("Screw it! Let's battle...\n(You should give your best battlecry)");
                    Console.Clear();
                    game.run();
                    break;
                case "h":
                    menu.dialoguePrompt("You don't need instructions!\nBut seriously if you don't know what to do, push random buttons. I heard that works.");
                    Console.Clear();
                    runMenu();
                    break;
                case "x":
                    Console.WriteLine("Thanks for Playing!\n");
                    break;
                default:
                    Console.WriteLine("Invalid response!\nMaybe you want to type 'H' to read up on the instructions...");
                    runMenu();
                    break;
            }
        }

        //Enters game enemy search cycle (upon prologue end, flee, or enemy killed)
        //Put this in its own game class eventually
        /* TODO:    - Randomize enemy level based on rng according to enemies killed
                        - Different level mobs drops different qualities gears?
                    - Keep track of player health
                        - Explore gear customization and leveling
                    - Keep track of enemies killed, use counter for boss spawn */
        static void search()
        {
            gameConsole c = new gameConsole();
            //testing dialogue prompt overload, rewrite to rng enemies
            c.dialoguePrompt("A Pirate stumbles in, drunk on drunken wine and gold brimming his pockets.\nGive me an 'Ahoy'!", "ahoy", "Arggh, that's me matey.", "Eh, close enough.");
            runMenu();
            
        }
    }
}
