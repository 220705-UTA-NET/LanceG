using System;

namespace RPGgame
{
    class gameConsole
    {
        //Basic "Any key input" prompt
        public void dialoguePrompt(string prompt)
        {
            Console.WriteLine(prompt);
            Console.ReadLine();
        }

        //Target key input overloaded prompt
        public void dialoguePrompt (string prompt, string target, string success, string fail)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            Console.Clear();

            if(input.ToLower().Equals(target))
            {
                Console.WriteLine(success);
            }
            else
            {
                Console.WriteLine(fail);
            }
        }
    }
}