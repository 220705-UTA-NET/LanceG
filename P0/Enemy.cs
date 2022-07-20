using System;

namespace RPGgame
{
    abstract class Enemy
    {
        public abstract string name { get; set; }
        public abstract int health { get; set; }
        public abstract int level {get; set;}
        public abstract int damage {get; set;}
        public abstract int defense{get; set;}
        public Enemy(int playerLevel)
        {
        }
        public abstract void attack(Player plyr);
        public abstract void drop(Player plyr);

    }
}