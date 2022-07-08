using System;

namespace RPGgame
{
    class Weapon
    {
        public string name { get; }
        public string description { get; }
        public int damage { get; }
        public int durability { get; set; }

        public Weapon(string nm, int dmg, int dur)
        {
            name = nm;
            damage = dmg;
            durability = dur;
        }

    }
}