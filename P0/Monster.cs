using System;

namespace RPGgame
{
    class Monster : Enemy
    {
        public override string name { get; set; }
        public override int health { get; set; }
        public override int level {get; set;}
        public override int damage {get; set;}
        public override int defense{get; set;}
        public Monster(int plyrLevel) : base(plyrLevel)
        {
            //Roll name
            string[] names = { "Slime", "Skeleton", "Shadow", "Zombie", "Arachnid", "Basilisk", "Goblin" };
            this.name = names[randomizer.roll(names.Length)];

            //Roll level
            int levelRoll = randomizer.roll(4);
            this.level = plyrLevel + levelRoll - 2;
            if(this.level < 1){
                this.level = 1;
            }

            //Roll HP
            this.health = 15 + randomizer.roll(3, 5) * randomizer.roll(this.level);

            //Roll damage
            this.damage = 5 + randomizer.roll(this.level) + randomizer.roll(this.level)/2;

            //Roll defense
            this.defense = 3 + randomizer.roll(this.level)/2;
        }

        public override void attack(Player plyr){
            int dice = randomizer.roll(100);

            if(dice < 15){
                poison(plyr);   
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
            Console.WriteLine(this.name + " showed you a taste of evil   -" + dmg + " HP");
            if(plyr.equippedArmor.hasDurability){
                plyr.equippedArmor.durability -= 1;
            }
        }
        public void poison(Player plyr){
            int dur = randomizer.roll(5, 10);
            int dmg = 3 + this.level/3 + randomizer.roll(3);
            plyr.poison.Item1 = dur;
            plyr.poison.Item2 = dmg;
            Console.WriteLine(this.name + " poisoned you for " + dmg + " health, lasting " + dur + " turns");
        }


        public override void drop(Player plyr){
            int dice = randomizer.roll(100);
            if(dice < 10){
                rollWeapon(plyr);
            }
            else if (dice < 20){
                rollArmor(plyr);
            }
            else if(dice < 25){
                rollWeapon(plyr, this.level);
            }
            else if(dice < 30){
                rollArmor(plyr, this.level);
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
            Weapon hb = new Weapon("Manifest Blade of the Hero", 10 + 2*this.level, 10);
            plyr.weaponInventory.Add(hb);
            Console.WriteLine(this.name + " dropped " + hb.name);
        }
        //rolls enemy specific items
        void rollArmor(Player plyr, int level){
            Armor hh = new Armor("Hymns of the Hero Revered", 5 + 2*this.level, 10);
            plyr.armorInventory.Add(hh);
            Console.WriteLine(this.name + " dropped " + hh.name);
        }
        void rollItem(Player plyr){
            Heal hp = new Heal(this.level, "");
            plyr.itemInventory.Add(hp);
            Console.WriteLine(this.name + " dropped " + hp.name);
        }
    }
}