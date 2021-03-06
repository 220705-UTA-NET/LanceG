using System;
using System.Text;

namespace RPGgame
{
    class Weapon : Equipment
    {
        public override string name { get; }
        public string description { get; }
        public int damage { get; set; }
        public int durability { get; set; }
        public override bool hasDurability {get;}

        //generates random "nonflavored" weapon based on level
        public Weapon (Enemy enemy){

            //random Weapon name
            string[] prefixes = { "Reinforced", "Imbued", "Great", "Sharpened", "Violent", "Seasoned"};
            string prefix = prefixes[randomizer.roll(prefixes.Length)];
            string[] weaponTypes = { "Sword", "Spear", "Bow", "Axe", "Hammer", "Lance", "Daggers", "Blade"};
            string type = weaponTypes[randomizer.roll(weaponTypes.Length)];

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} {1} of the {2}", prefix, type, enemy.name);
            this.name = sb.ToString();

            //damage
            this.damage = 10 + 5 * randomizer.roll(enemy.level) + enemy.level * randomizer.roll(0, 3);
            this.durability = 5;
            this.hasDurability = true;

        }
        public Weapon (string name, Player plyr){

            //random Weapon name
            string[] prefixes = { "Reinforced", "Imbued", "Great", "Sharpened", "Violent", "Seasoned"};
            string prefix = prefixes[randomizer.roll(prefixes.Length)];
            string[] weaponTypes = { "Sword", "Spear", "Bow", "Axe", "Hammer", "Lance", "Daggers", "Blade"};
            string type = weaponTypes[randomizer.roll(weaponTypes.Length)];

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} {1} of the {2}", prefix, type, name);
            this.name = sb.ToString();

            //damage
            this.damage = 10 + randomizer.roll(plyr.level) + plyr.level * randomizer.roll(0, 3);
            this.durability = 5;
            this.hasDurability = true;

        }

        public Weapon(string nm, int dmg, int dur)
        {
            this.name = nm;
            this.damage = dmg;
            this.durability = dur;
            this.description = "";

            this.hasDurability = true;
            if (this.durability == 9999 || this.durability == -1){
                this.hasDurability = false;
            }
        }

    }
}