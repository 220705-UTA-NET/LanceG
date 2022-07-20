using System;

namespace RPGgame
{
    class Witch : Enemy
    {
        public override string name { get; set; }
        public override int health { get; set; }
        public override int level {get; set;}
        public override int damage {get; set;}
        public override int defense{get; set;}
        public Witch(int plyrLevel) : base(plyrLevel)
        {
            //Roll name
            string[] names = { "Witch", "Sorceress", "Shaman", "Mage", "Hag", "Enchantress", "Necromancer", "Cultist", "Crone", "Harpy", "Gorgon" };
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
            this.damage = 8 + randomizer.roll(this.level) + randomizer.roll(this.level)/2;

            //Roll defense
            this.defense = 2 + randomizer.roll(this.level)/2;
        }

        public override void attack(Player plyr){
            int dice = randomizer.roll(100);

            if(dice < 10){
                debuff(plyr);
            }
            else if(dice < 30){
                poison(plyr);   
            }
            else {
                strike(plyr);
            }
        }

        public void strike(Player plyr){
            plyr.health -= damage;
            Console.WriteLine(this.name + " casted a hex at you   -" + damage + " HP");
        }
        public void poison(Player plyr){
            int dur = randomizer.roll(2, 10);
            int dmg = 1 + this.level/3;
            plyr.poison.Item1 = dur;
            plyr.poison.Item2 = dmg;
            Console.WriteLine(this.name + " poisoned you for " + dmg + " health, lasting " + dur + " turns");
        }
        public void debuff(Player plyr){
            int dur = randomizer.roll(2, 10);
            int num = randomizer.roll(1, 3);
            int den = num + randomizer.roll(1,2);
            plyr.weakness.Item1 = dur;
            plyr.weakness.Item2 = num;
            plyr.weakness.Item3 = den;
            Console.WriteLine(this.name + " minimorphed you for " + num + "/" + den + " your damage lasting " + dur + " turns");
        }

        public override void drop(Player plyr){
            int dice = randomizer.roll(100);
            if(dice < 15){
                rollWeapon(plyr);
            }
            else if (dice < 25){
                rollArmor(plyr, this.level);
            }
            else if(dice < 30){
                rollArmor(plyr);
            }
            else if (dice < 60){
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
            Armor ar = new Armor("Witch's Cloak", 10 + this.level, 10);
            plyr.armorInventory.Add(ar);
            Console.WriteLine(this.name + " dropped " + ar.name);
        }
        void rollItem(Player plyr){
            Item it;

            int dice = randomizer.roll(100);
            if (dice < 50){
                Heal hp = new Heal(this.level + randomizer.roll(0,2), "");
                it = hp;
            }
            else{
                Spell sp = rollSpell(plyr);
                it = sp;
            }
            plyr.itemInventory.Add(it);
            Console.WriteLine(this.name + " dropped " + it.name);
        }
        Spell rollSpell(Player plyr){

            int dice = randomizer.roll(100);
            if(dice < 25){
                Spell fb = new Spell("Fireball", 8 + this.level);
                return fb;
            }
            else if (dice < 50){
                Spell elec = new Spell("Electric Bolt", 5 + 2*randomizer.roll(this.level));
                return elec;
            }
            /*else if(dice < 70){
                int dur = randomizer.roll(2, 10);
                int den = randomizer.roll(1, 3);
                int num = num + randomizer.roll(1,2);
                Spell bf = new Spell("Bolster Strength", dur, num, den, plyr);
                return bf;
            }*/
            else if (dice < 75){
                Spell fd = new Spell("Fortify Defenses", 2 + randomizer.roll(this.level), plyr, "equippedArmor", "defense");
                return fd;
            }
            else if (dice < 80){
                Spell rc = new Spell("Reinforce Constitution", 1 + randomizer.roll(this.level), plyr, "equippedArmor", "durability");
                return rc;
            }
            else if (dice < 85){
                Spell hw = new Spell("Hone Weapon", 5 + randomizer.roll(this.level), plyr, "equippedWeapon", "damage");
                return hw;
            }
            else if (dice < 85){
                Spell ra = new Spell("Restore Armaments",  1 + randomizer.roll(this.level), plyr, "equippedWeapon", "durability");
                return ra;
            }
            else{
                Spell dt = new Spell("Inflict Despair", randomizer.roll(1, this.level) * randomizer.roll(1, this.level));
                return dt;
            }
        }
    }
}