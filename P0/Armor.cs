using System;
using System.Text;

namespace RPGgame
{
    class Armor : Equipment
    {
        public override string name { get; }
        public string description { get; }
        public int defense { get; set; }
        public int durability { get; set; }
        public override bool hasDurability {get;}

        //generates random "nonflavored" armor based on level
        public Armor (Enemy enemy){

            //random Weapon name
            string[] prefixes = { "Padded", "Reinforced", "Enduring", "Shocked", "Plated", "Armored"};
            string prefix = prefixes[randomizer.roll(prefixes.Length)];
            string[] armorTypes = { "Cloak", "Vest", "Plate", "Chainmail", "Robe", "Hide"};
            string type = armorTypes[randomizer.roll(armorTypes.Length)];

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} {1} of the {2}", prefix, type, enemy.name);
            this.name = sb.ToString();

            //damage
            this.defense = 5 + 5 * randomizer.roll(enemy.level) + enemy.level * randomizer.roll(0, 3);
            this.durability = 5;
            this.hasDurability = true;

        }
        public Armor (string name, Player plyr){

            //random Weapon name
            string[] prefixes = { "Padded", "Reinforced", "Enduring", "Shocked", "Plated", "Armored"};
            string prefix = prefixes[randomizer.roll(prefixes.Length)];
            string[] armorTypes = { "Cloak", "Vest", "Plate", "Chainmail", "Robe", "Hide"};
            string type = armorTypes[randomizer.roll(armorTypes.Length)];

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} {1} of the {2}", prefix, type, name);
            this.name = sb.ToString();

            //damage
            this.defense = 5 + 5 * randomizer.roll(plyr.level) + plyr.level * randomizer.roll(0, 3);
            this.durability = 5;
            this.hasDurability = true;

        }

        public Armor(string nm, int def, int dur)
        {
            this.name = nm;
            this.defense = def;
            this.durability = dur;
            this.description = "";

            this.hasDurability = true;
            if (this.durability == 9999 || this.durability == -1){
                this.hasDurability = false;
            }
        }

    }
}