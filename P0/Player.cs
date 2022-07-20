using System;
using System.Text;
using System.Collections;

namespace RPGgame
{
    class Player
    {
        public int health { get; set; }
        public int level {get; set;}
        public int levelProgress {get; set;}
        public int levelUpReq {get; set;}
        public int enemiesKilled {get; set;}
        public Enemy currentEnemy {get; set;}
        public Weapon equippedWeapon { get; set; }
        public Armor equippedArmor { get; set; }
        public int damage { get; set; }
        public List<Weapon> weaponInventory = new List<Weapon>();
        public List<Armor> armorInventory = new List<Armor>();
        public List<Item> itemInventory = new List<Item>();
        //Misc[]
        public (int, int) poison; //duration, damage per turn
        public (int, int, int) weakness; // duration, numerator, denominator
        public (int, int, int) buff; // duration, numerator, denominator
        


        public Player()
        {
            level = 1;
            health = 95 + level * 5;
            equippedWeapon = new Weapon("Good Ol' Fisticuffs", 5, -1);
            equippedArmor = new Armor("Bare Clad Bosom", 0, -1);
            
            //Testing Weapon Inventory
            //Weapon fists = new Weapon("Good Ol' Fisticuffs", 10);
            //Armor grobe = new Armor("Ghostly Robe\t", 1, 9999);
            //armorInventory.Add(grobe);

            //Testing DoT and other advanced fighting mechanics
            poison = (0, 0);
            weakness = (0, 0, 0);
            buff = (0, 0, 0);

        }

        public void starter(){
            Weapon copter = new Weapon("Whirly-Copter Blade", 12, 5);
            weaponInventory.Add(copter);
            Weapon cutlass = new Weapon("Cutting Cutlass", 15, 5);
            weaponInventory.Add(cutlass);
            Weapon grass = new Weapon("Blade of Long Grass", 18, 5);
            weaponInventory.Add(grass);
            Armor dscale = new Armor("Dragonscale Vest", 5, 5);
            armorInventory.Add(dscale);
            Armor ifrit = new Armor("Fiery Thornmail of the Ifrit", 7, 5);
            armorInventory.Add(ifrit);
        }

        public void god(){
            Weapon admin = new Weapon("Admin Sword", 9999, 9999);
            weaponInventory.Add(admin);
            Armor inf = new Armor("The Infinity", 9999, 9999);
            armorInventory.Add(inf);
        }

        public void magic(){
            Heal lesserHeal = new Heal("Potion of Lesser Healing", 25);
            itemInventory.Add(lesserHeal);
            Cure vitaminC = new Cure("Emergen-C");
            itemInventory.Add(vitaminC);
            Spell fb = new Spell("Fireball", 8 + level);
            itemInventory.Add(fb);
            Spell elec = new Spell("Electric Bolt", 5 + 2*randomizer.roll(level));
            itemInventory.Add(elec);
            Spell dt = new Spell("Inflict Despair", randomizer.roll(1, level) * randomizer.roll(1, level));
            itemInventory.Add(dt);
            Spell fd = new Spell("Fortify Defenses", 2 + randomizer.roll(level), this, "equippedArmor", "defense");
            itemInventory.Add(fd);
            Spell rc = new Spell("Reinforce Constitution", 1 + randomizer.roll(level), this, "equippedArmor", "durability");
            itemInventory.Add(rc);
            Spell hw = new Spell("Hone Weapon", 5 + randomizer.roll(level), this, "equippedWeapon", "damage");
            itemInventory.Add(hw);
            Spell ra = new Spell("Restore Armaments",  1 + randomizer.roll(level), this, "equippedWeapon", "durability");
            itemInventory.Add(ra);
        }

        public void attack(Enemy enemy){
            int dmg = damage - enemy.defense + randomizer.roll(enemy.defense)/2;
            if(dmg > 0){
                enemy.health -= dmg;
            }
            else{
                dmg = 0;
            }
            if(equippedWeapon.hasDurability){
                equippedWeapon.durability -= 1;
            }
            Console.WriteLine("You struck " + enemy.name + " with your " + equippedWeapon.name + "   -" + dmg + " DMG");
        }

        public void heal(int h){
            health += h;
            if(health > 95 + level * 5){
                health =  95 + level * 5;
            }
        }

        //Checks turn based statuses
        public void checkStatus()
        {
            if(this.health <= 0){
                Console.Clear();
                Console.WriteLine("You succumbed to the inevitability of death. (Don't feel bad, there's no way to beat the game)\nBut... Play Again? (y/n)");
                string? inp = Console.ReadLine();
                gameConsole narrator = new gameConsole();
                if(inp.Equals("y")){
                    Console.Clear();
                    narrator.dialoguePrompt("Your broken bones magically heal, and your stabby holes magically stab themselves");
                    narrator.dialoguePrompt("Your memory is magically wiped and you magically teleport back to your Royal Bed where you rise");
                    narrator.dialoguePrompt("You get a feeling of deja vu, but you really can't place your tongue on it");
                    narrator.dialoguePrompt("Adventure awaits! Huzzah!");
                    game.enemyPool = new List<Type> {typeof(Witch), typeof(Witch), typeof(Fighter), typeof(Fighter), typeof(Fighter), typeof(Tank), typeof(Monster), typeof(Monster)};
                    Console.Clear();
                    game.run();
                }
                else if(inp.Equals("n")){
                    Console.Clear();
                    narrator.dialoguePrompt("Just remember, every time you give up a non-implemented villager child dies in your place");
                    Console.Clear();
                    startGAME.runMenu();
                }
                else{
                    checkStatus();
                }
            }
            //check level up
            levelUpReq = 5 + 5 * (int)Math.Pow(level, 2);
            if(levelProgress >= levelUpReq){
                level++;
                Console.WriteLine("Level Up! You grow stronger");
                heal(10);
                levelUpReq = 5 + 5 * (int)Math.Pow(level, 2);
                levelProgress = 0;
            }

            //check weapon and armor durability
            if(equippedWeapon.durability == 0){
                Console.WriteLine("You cast aside your weapon and arm your-self with your-hands");
                equippedWeapon = new Weapon("Good Ol' Fisticuffs", 5 + this.level, -1);
            }
            
            if(equippedArmor.durability == 0){
                Console.WriteLine("Your armor bursts apart dramatically, revealing your luscious packs");
                equippedArmor = new Armor("Bare Clad Bosom", 0, -1);
            }
            //debuff: poison, weakened, frozen

            //check poison(damage, duration)
            if(poison.Item1 > 0)
            {
                Console.WriteLine("You feel poison coursing through your veins   -" + poison.Item2 + " HP");
                health -= poison.Item2;
                poison.Item1--;
                if(poison.Item1 == 0)
                {
                    poison = (0, 0);
                }
            }
            checkDamage();
        }

        public void checkDamage(){
            //check damage mods
            damage = equippedWeapon.damage;
            if(weakness.Item1 > 0){
                Console.WriteLine(randomizer.weakRandom());
                damage *= weakness.Item2;
                damage /= weakness.Item3;
                weakness.Item1--;
                if(weakness.Item1 == 0)
                {
                    weakness = (0, 0, 0);
                }
            }
            if(buff.Item1 > 0){
                Console.WriteLine(randomizer.buffRandom());
                int damageBoost;
                damageBoost = damage / buff.Item3 * buff.Item2;
                damage += damageBoost;
                buff.Item3--;
                if(buff.Item1 == 0)
                {
                    buff = (0, 0, 0);
                }
            }
        }
        public string listStatus (){
            StringBuilder sb = new StringBuilder();
            if(poison.Item1 > 0){
                sb.AppendLine("Poisoned for " + poison.Item1 + " turns (" + poison.Item2 + " DMG)");
            }
            if(weakness.Item1 > 0){
                sb.AppendLine("Weakened for " + weakness.Item1 + " turns (" + weakness.Item2 + "/" + weakness.Item3+ ")");
            }
            if(buff.Item1 > 0){
                sb.AppendLine("Buffed for " + buff.Item1 + " turns (" + buff.Item2 + "/" + buff.Item3+ ")");
            }
            return sb.ToString();
        }
        public void listWeapons()
        {
            Console.Clear();
            string durabilityCheck;
            if(equippedWeapon.hasDurability){
                durabilityCheck = equippedWeapon.durability.ToString();
            }
            else{
                durabilityCheck = "INF";
            }
            Console.WriteLine("{0,-40}{1,0}", "Equipped: " + equippedWeapon.name, "DMG: " + equippedWeapon.damage + "  DUR: " + durabilityCheck);
            Console.WriteLine("\nWeapons Inventory");
            if(weaponInventory.Count == 0){
                Console.WriteLine("EMPTY");
            }
            int i = 1;
            foreach (Weapon w in weaponInventory)
            {
                if(w.hasDurability){
                    durabilityCheck = w.durability.ToString();
                }
                else{
                    durabilityCheck = "INF";
                }
                Console.WriteLine("{0,-40}{1,0}", i + ". " + w.name, "DMG: " + w.damage + "  DUR: " + durabilityCheck);
                i++;
            }
        }

        public void equipWeapon()
        {
            listWeapons();
            Console.WriteLine("\nPress ENTER to cancel");
            Console.Write("Weapon to Equip: ");
            string? inp = Console.ReadLine();

            if(int.TryParse(inp, out int i))
            {
                if(i <= weaponInventory.Count && i > 0) {
                    if(equippedWeapon.durability != -1){
                        weaponInventory.Add(equippedWeapon);
                    }
                    equippedWeapon = (Weapon)weaponInventory[i-1];
                    checkDamage();
                    weaponInventory.RemoveAt(i-1);
                    Console.Clear();

                    string durabilityCheck;
                    if(equippedWeapon.hasDurability){
                        durabilityCheck = equippedWeapon.durability.ToString();
                    }
                    else{
                        durabilityCheck = "INF";
                    }
                    Console.WriteLine("Equipped: " + equippedWeapon.name);

                }
                else {
                    equipWeapon();
                }
            }
            else if(inp.Equals("")) {
                Console.Clear();
            }
            else
            {
                equipWeapon();
            }
        }
        public void listArmors()
        {
            Console.Clear();
            string durabilityCheck;
            if(equippedArmor.hasDurability){
                durabilityCheck = equippedArmor.durability.ToString();
            }
            else{
                durabilityCheck = "INF";
            }
            //Console.WriteLine("Equipped: " + equippedArmor.name + "\t\tDEF: " + equippedArmor.defense + "\tDUR: " + durabilityCheck);
            Console.WriteLine("{0,-40}{1,0}", "Equipped: " + equippedArmor.name, "DEF: " + equippedArmor.defense + "  DUR " + durabilityCheck);
            Console.WriteLine("\nArmor Inventory");
            if(armorInventory.Count == 0){
                Console.WriteLine("EMPTY");
            }

            int i = 1;
            foreach (Armor a in armorInventory)
            {
                if(a.hasDurability){
                    durabilityCheck = a.durability.ToString();
                }
                else{
                    durabilityCheck = "INF";
                }
                Console.WriteLine("{0,-40}{1,0}", i + ". " + a.name, "DEF: " + a.defense + "  DUR: " + durabilityCheck);
                i++;
            }
        }

        public void equipArmor()
        {
            listArmors();
            Console.WriteLine("\nPress ENTER to cancel");
            Console.Write("Armor to Equip: ");
            string? inp = Console.ReadLine();

            if(int.TryParse(inp, out int i))
            {
                if(i <= armorInventory.Count && i > 0) {
                    if(equippedArmor.durability != -1){
                        armorInventory.Add(equippedArmor);
                    }
                    equippedArmor = (Armor)armorInventory[i-1];
                    armorInventory.RemoveAt(i-1);
                    Console.Clear();
                    
                    string durabilityCheck;
                    if(equippedArmor.hasDurability){
                        durabilityCheck = equippedArmor.durability.ToString();
                    }
                    else{
                        durabilityCheck = "INF";
                    }
                    Console.WriteLine("Equipped: " + equippedArmor.name);
                }
                else {
                    equipArmor();
                }
            }
            else if(inp.Equals("")) {
                Console.Clear();
            }
            else
            {
                equipArmor();
            }
        }
        public void useItem()
        {
            listItems();
            Console.WriteLine("\nPress ENTER to cancel");
            Console.Write("Item to Use: ");
            string? inp = Console.ReadLine();

            if(int.TryParse(inp, out int i))
            {
                if(i <= itemInventory.Count && i > 0) {
                    //some Use command
                    Console.Clear();
                    itemInventory[i-1].use(this, currentEnemy);
                    itemInventory.RemoveAt(i-1);
                }
                else {
                    useItem();
                }
            }
            else if(inp.Equals("")) {
                Console.Clear();
            }
            else
            {
                useItem();
            }
        }
        public void listItems()
        {
            Console.Clear();
            Console.WriteLine("\nItems Inventory");
            if(itemInventory.Count == 0){
                Console.WriteLine("EMPTY");
            }
            int i = 1;
            foreach (Item it in itemInventory)
            {
                Console.WriteLine(i + ". " + it.name);
                i++;
            }
        }
    }
}