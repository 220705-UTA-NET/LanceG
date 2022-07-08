using System;
using System.Collections;

namespace RPGgame
{
    class Player
    {
        public int health { get; set; }
        public Weapon equippedWeapon { get; set; }
        public int damage { get; set; }
        List<Weapon> weaponInventory = new List<Weapon>();
        //Armor[]
        //Misc[]
        (int, int) poison;
        (int, int, int) weakness;
        (int, int, int) buff;
        


        public Player()
        {
            health = 100;
            equippedWeapon = new Weapon("Good Ol' Fisticuffs", 10, 10);
            
            //Testing Weapon Inventory
            //Weapon fists = new Weapon("Good Ol' Fisticuffs", 10);
            Weapon copter = new Weapon("Whirly-Copter Blade", 12, 10);
            weaponInventory.Add(copter);
            Weapon cutlass = new Weapon("Cutting Cutlass", 15, 10);
            weaponInventory.Add(cutlass);
            Weapon grass = new Weapon("Blade of Long Grass", 18, 10);
            weaponInventory.Add(grass);

            //Testing DoT and other advanced fighting mechanics
            poison = (0, 0);

        }

        //Checks turn based statuses
        public void checkStatus()
        {
            //debuff: poison, weakened, frozen

            //check poison(damage, duration)
            if(poison.Item2 > 0)
            {
                Console.WriteLine("You feel poison coursing through your veins");
                health -= poison.Item1;
                poison.Item2--;
            }

            //check damage mods
            damage = equippedWeapon.damage;
            if(weakness.Item2 > 0){
                Console.WriteLine("You are affected by weakness");
                damage *= weakness.Item1;
                damage /= weakness.Item2;
                weakness.Item3--;
            }
            if(weakness.Item2 > 0){
                Console.WriteLine("You feel the grace of your Guardian Angel");
                damage *= buff.Item1;
                damage /= buff.Item2;
                buff.Item3--;
            }
        }

        public void listWeapons()
        {
            Console.Clear();
            Console.WriteLine("Currently Equipped: " + equippedWeapon.name);
            Console.WriteLine("Weapons Inventory");
            int i = 1;
            foreach (Weapon w in weaponInventory)
            {
                Console.WriteLine(i + ". " + w.name + "\t\t\tDMG: " + w.damage + "\tDUR: " + w.durability);
                i++;
            }
        }

        public void equipWeapon()
        {
            listWeapons();
            Console.WriteLine("Press ENTER to cancel");
            Console.Write("Weapon to Equip: ");
            string inp = Console.ReadLine();

            if(int.TryParse(inp, out int i))
            {
                if(i <= weaponInventory.Count) {
                    equippedWeapon = (Weapon)weaponInventory[i-1];
                    Console.Clear();
                    Console.WriteLine("Equipped " + equippedWeapon.name);
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
    }
}