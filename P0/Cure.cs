using System;

namespace RPGgame
{
    class Cure : Item
    {
        public override string name { get; }
        public override string description { get; }
        public Cure(string name) : base (name)
        {
            this.name = name;
        }

        public override void use(Player plyr, Enemy enemy){
            Console.WriteLine("You rend all traces of evil from your body");
            plyr.poison = (0, 0);
            plyr.weakness = (0, 0, 0);
        }

    }
}