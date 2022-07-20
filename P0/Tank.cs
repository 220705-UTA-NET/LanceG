using System;

namespace RPGgame
{
    class Tank : Enemy
    {
        public override string name { get; set; }
        public override int health { get; set; }
        public override int level {get; set;}
        public override int damage {get; set;}
        public override int defense{get; set;}
        public Tank(int plyrLevel) : base(plyrLevel)
        {
            //Roll name
            string[] names = { "Golem", "Construct", "Husk", "Giant", "Giant", "Ogre", "Ent", "Oppressor", "Dragon", "Manticore" };
            this.name = names[randomizer.roll(names.Length)];

            //Roll level
            int levelRoll = randomizer.roll(2, 4);
            this.level = plyrLevel + levelRoll;
            if(this.level < 1){
                this.level = 1;
            }

            //Roll HP
            this.health = 30 + randomizer.roll(3, 5) * randomizer.roll(this.level);

            //Roll damage
            this.damage = 7 + randomizer.roll(this.level) + randomizer.roll(this.level)/2;

            //Roll defense
            this.defense = 7 + randomizer.roll(this.level)/2;
        }

        public override void attack(Player plyr){
            int dice = randomizer.roll(100);

            if(dice < 15){
                debuff(plyr);
            }
            else {
                strike(plyr);
            }
        }

        public void strike(Player plyr){
            int dmg = damage - plyr.equippedArmor.defense + randomizer.roll(plyr.equippedArmor.defense)/2;
            if(dmg < 0){
                dmg = 0;
            }
            plyr.health -= dmg;
            Console.WriteLine(this.name + " swung at you with its giant fist   -" + dmg + " HP");
            if(plyr.equippedArmor.hasDurability){
                Console.WriteLine("The blow is shattering! " + plyr.equippedArmor.name + " is rendered unusable");
                plyr.equippedArmor.durability = 0;
            }
        }
        public void debuff(Player plyr){
            int dur = randomizer.roll(2, 10);
            int num = 1;
            int den = num + randomizer.roll(5,10);
            plyr.weakness.Item1 = dur;
            plyr.weakness.Item2 = num;
            plyr.weakness.Item3 = den;
            Console.WriteLine(this.name + " looms over you intimidatingly. You feel insignificant " + num + "/" + den + " damage for " + dur + " turns");
        }

        public override void drop(Player plyr){
            int dice = randomizer.roll(100);
            if(dice < 15){
                rollWeapon(plyr);
            }
            else if (dice < 25){
                rollArmor(plyr);
            }
            else if(dice < 30){
                rollWeapon(plyr, this.level);
            }
            else if (dice < 70){
                rollItem(plyr);
            }
            else{
                Console.WriteLine("You sifted through the belongings of " + this.name + ", but you found nothing");
            }
        }

        // Specific drops of enemy
        void rollWeapon(Player plyr){
            Weapon wp = new Weapon(this);
            plyr.weaponInventory.Add(wp);
            Console.WriteLine(this.name + " dropped " + wp.name);
        }
        void rollArmor(Player plyr){
            Armor ar = new Armor(this);
            plyr.armorInventory.Add(ar);
            Console.WriteLine(this.name + " dropped " + ar.name);
        }
        //rolls enemy specific items
        void rollWeapon(Player plyr, int level){
            Weapon gs = new Weapon("Giant Slayer", 20 + this.level, 10);
            plyr.weaponInventory.Add(gs);
            Console.WriteLine(this.name + " dropped " + gs.name);
        }
        void rollItem(Player plyr){
            Heal hp = new Heal(this.level + randomizer.roll(0,3), "");
            plyr.itemInventory.Add(hp);
            Console.WriteLine(this.name + " dropped " + hp.name);
        }
    }
}