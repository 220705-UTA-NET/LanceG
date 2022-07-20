using System;

namespace RPGgame
{
    abstract class Item
    {
        public abstract string name { get; }
        public abstract string description { get; }
        public Item(string nm) {}

        public abstract void use(Player plyr, Enemy enemy);
    }
}