using System;
using System.Collections.Generic;

namespace RPGgame
{
    class randomizer
    {
        public static int roll(int max)
        {
            Random rand = new Random();
            int dice = rand.Next(max);
            return dice;
        }
        public static int roll(int min, int max)
        {
            Random rand = new Random();
            int dice = rand.Next(min, max);
            return dice;
        }
        public static T grabRandom<T>(T[] arr)
        {
            Random rand = new Random();
            int dice = rand.Next(arr.Length);
            return arr[dice];
        }
        public static string buffRandom(){
            string[] textPool = {
                "You feel the grace of your Guardian Angel",
                "A tsunami of power washes over you",
                "You feel a weird tickle down your spine. It feels nice"
            };

            return grabRandom(textPool);
        }
        public static string weakRandom(){
            string[] textPool = {
                "You are afflicted by a great weakness, and shame",
                "You had prepared yourself to battle, but your legs jellified",
                "For you, 'pissing in one's pants' has never been as literal as an expression up until now",
                "A torrent of laziness impeded upon your willpower to not be lazy"
            };
            /*Random rnd = new Random();
            int i = rnd.Next(0, textPool.Count()-1);

            return textPool.ElementAt(i);*/
            return grabRandom(textPool);
        }

        public static string restFailed(Enemy enemy){
            string[] textPool = {
                "While you were resting, a yoodling " + enemy.name + " emerges from the bushes. You ready yourself to fight",
                "A shrill battlecry of a " + enemy.name + " approaches from the distance",
                "An angry " + enemy.name + " decided to crash your afternoon tea party",
                "Your yoga stretches came to an abrupt end as a " + enemy.name + " charges to battle"
            };

            return grabRandom(textPool) + "  +5 HP";
        }
        public static void rollWeapon(Player plyr){
            string[] owner = { "Titan", "Mad King", "Demon Lord", "God-King", "Butcher", "Dwarf Smith", "Legend",
            "Overlord", "Amazon", "Squire", "Immortal", "Noble Knight", "Corrupt Knight"};
            Weapon wp = new Weapon(grabRandom(owner), plyr);
            plyr.weaponInventory.Add(wp);
            Console.WriteLine("You have found the " + wp.name);
        }
        public static void rollArmor(Player plyr){
            string[] owner = { "Shieldbearer", "Unstoppable", "Great Protector", "Tyrant", "Just King", "Unmoving", "Bastion",
            "Unbreakable", "Unflinching", "Holy Padalin" };
            Armor ar = new Armor(grabRandom(owner), plyr);
            plyr.armorInventory.Add(ar);
            Console.WriteLine("You have found the " + ar.name);
        }
    }
}