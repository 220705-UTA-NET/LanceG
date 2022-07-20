using System;

namespace RPGgame
{
    class Fighter : Enemy
    {
        public override string name { get; set; }
        public override int health { get; set; }
        public override int level {get; set;}
        public override int damage {get; set;}
        public override int defense{get; set;}
        public Fighter(int plyrLevel) : base(plyrLevel)
        {
            //Roll name
            string[] names = { "Pirate", "Scoundrel", "Captain", "Scallywag", "Rogue", "Bandit", "Marauder", "Privateer", "Corsair", "Bucaneer", "Reaver", "Raider" };
            this.name = names[randomizer.roll(names.Length)];

            //Roll level
            int levelRoll = randomizer.roll(4);
            this.level = plyrLevel + levelRoll - 2;
            if(this.level < 1){
                this.level = 1;
            }

            //Roll HP
            this.health = 20 + randomizer.roll(3, 5) * randomizer.roll(this.level);

            //Roll damage
            this.damage = 8 + randomizer.roll(this.level) + randomizer.roll(this.level)/2;

            //Roll defense
            this.defense = 3 + randomizer.roll(this.level)/2;
        }

        public override void attack(Player plyr){
            strike(plyr);
        }

        public void strike(Player plyr){
            int dmg = damage - plyr.equippedArmor.defense + randomizer.roll(plyr.equippedArmor.defense)/2;
            if(dmg < 0){
                dmg = 0;
            }
            plyr.health -= dmg;
            Console.WriteLine(this.name + " struck you with his weapon   -" + dmg + " HP");
            if(plyr.equippedArmor.hasDurability){
                plyr.equippedArmor.durability -= 1;
            }
        }

        public override void drop(Player plyr){
            int dice = randomizer.roll(100);
            if(dice < 15){
                rollArmor(plyr);
            }
            else if (dice < 25){
                rollWeapon(plyr);
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
            Weapon sb = new Weapon("Scourge's Blade", 7 + this.level, 10);
            plyr.weaponInventory.Add(sb);
            Console.WriteLine(this.name + " dropped " + sb.name);
        }
        void rollItem(Player plyr){
            Item it;

            int dice = randomizer.roll(100);
            if (dice < 50){
                Heal hp = new Heal(this.level, "");
                it = hp;
            }
            else{
                Cure c = new Cure("Emergen-C");
                it = c;
            }
            plyr.itemInventory.Add(it);
            Console.WriteLine(this.name + " dropped " + it.name);
        }
    }
}