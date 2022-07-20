using System;

namespace RPGgame
{
    class Cow : Enemy
    {
        public override string name { get; set; }
        public override int health { get; set; }
        public override int level {get; set;}
        public override int damage {get; set;}
        public override int defense{get; set;}
        public Cow(int plyrLevel) : base(plyrLevel)
        {
            //Roll name
            string[] names = { "Knight", "Warrior", "King", "Paladin", "Berserker" };
            this.name = "Cow " + names[randomizer.roll(names.Length)];

            //Roll level
            int levelRoll = randomizer.roll(4);
            this.level = plyrLevel + levelRoll - 2;
            if(this.level < 1){
                this.level = 1;
            }

            //Roll HP
            this.health = 15 + randomizer.roll(3, 5) * randomizer.roll(this.level);

            //Roll damage
            this.damage = 20 + randomizer.roll(this.level) + randomizer.roll(this.level)/2;

            //Roll defense
            this.defense = 5 + randomizer.roll(this.level)/2;
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
            Console.WriteLine("Cow Stampede!   -" + dmg + " HP");
            if(plyr.equippedArmor.hasDurability){
                plyr.equippedArmor.durability -= 1;
            }
        }
        public void debuff(Player plyr){
            int dur = randomizer.roll(2, 10);
            int num = randomizer.roll(1, 3);
            int den = num + randomizer.roll(1,2);
            plyr.weakness.Item1 = dur;
            plyr.weakness.Item2 = num;
            plyr.weakness.Item3 = den;
            Console.WriteLine(this.name + " moo'ed at you " + num + "/" + den + " damage for " + dur + " turns");
        }

        public override void drop(Player plyr){
            int dice = randomizer.roll(100);
            if(dice < 15){
                rollWeapon(plyr);
            }
            else if (dice < 10){
                rollArmor(plyr, this.level);
            }
            else if(dice < 20){
                rollArmor(plyr);
            }
            else if (dice < 80){
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
        void rollArmor(Player plyr, int level){
            Armor ar = new Armor("Cowhide Hide", 7 + this.level, 10);
            plyr.armorInventory.Add(ar);
            Console.WriteLine(this.name + " dropped " + ar.name);
        }
        void rollItem(Player plyr){
            Item it;

            int dice = randomizer.roll(100);
            if (dice < 50){
                Heal hp = new Heal("Potion of Moo-re Healing", 1337);
                it = hp;
            }
            else{
                Cure m = new Cure("Milk");
                it = m;
            }
            plyr.itemInventory.Add(it);
            Console.WriteLine(this.name + " dropped " + it.name);
        }
    }
}