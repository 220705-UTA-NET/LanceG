using System;

namespace RPGgame
{
    class Heal : Item
    {
        public override string name { get; }
        public override string description { get; }
        int strength;
        public Heal(int level, string name) : base (name)
        {
            (string, int, int, int) lesser = ("Lesser", 15, 0, 5); //Name, Strength, Min level, Max level
            (string, int, int, int) minor = ("Minor", 20, 1, 7);
            (string, int, int, int) moderate = ("Moderate", 25, 2, 9);
            (string, int, int, int) great = ("Great", 40, 3, 15);
            (string, int, int, int) major = ("Major", 50, 5, 9999);
            (string, int, int, int) supreme = ("Supreme", 75, 10, 9999);
            (string, int, int, int) exalt = ("Exalt", 999, 10, 9999);
            (string, int, int, int)[] healPotions = {lesser, minor, moderate, great, major, supreme, exalt};

            (string, int, int, int) select = ("", 0, 0, 0);

            while (select.Item1.Equals("")){
                int i = randomizer.roll(healPotions.Length);
                if(level >= healPotions[i].Item3 && level <= healPotions[i].Item4){
                    select = healPotions[i];
                }
            }

            this.name = select.Item1 + " Potion of Healing";
            this.strength = select.Item2;


        }
        public Heal(string name, int strength) : base (name)
        {
            this.name = name;
            this.strength = strength;
        }

        public override void use(Player plyr, Enemy enemy){
            Console.WriteLine("Glug glug glug. Hey, that was quite good!   +" + strength + " HP");
            plyr.heal(strength);
        }

    }
}