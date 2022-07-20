using System;
using System.Reflection;

namespace RPGgame
{
    class Spell : Item
    {
        public override string name { get; }
        public override string description { get; }
        int strength;
        bool durabilityBool; int varToEdit; string type;
        string equipType; string variableName;
        Equipment eq;
        public Spell(string name, int strength) : base (name)
        {
            this.name = name;
            this.strength = strength;
            this.type = "";
        }
        public Spell(string name, int strength, Player plyr, string equipType, string variableName) : base (name)
        {
            this.name = name;
            this.strength = strength;
            this.type = "add";
            this.equipType = equipType;
            this.variableName = variableName;
        }

        public override void use(Player plyr, Enemy enemy){
            if(type.Equals("add")){
                PropertyInfo equipment= plyr.GetType().GetProperty(this.equipType);
                eq = (Equipment) equipment.GetValue(plyr);
                this.durabilityBool = eq.hasDurability;
                var eqVar = (int) eq.GetType().GetProperty(this.variableName).GetValue(eq);
                this.varToEdit = eqVar;

                if(this.durabilityBool){
                    this.varToEdit += strength;
                    eq.GetType().GetProperty(this.variableName).SetValue(eq, this.varToEdit);
                    Console.WriteLine("You casted " + this.name + " on your " + eq.name);
                    //Console.WriteLine(eq.name + ", " + this.varToEdit);
                }
                else{
                    Console.WriteLine("You tried to cast " + this.name + " but it seems to have no effect");
                    Console.WriteLine(eq.name + ", " + this.varToEdit);
                }
            }
            else{
                Console.WriteLine("You casted " + this.name + " at the enemy -" + strength + " DMG");
                enemy.health -= strength;
                if(enemy.health <= 0){
                    Console.WriteLine(enemy.name + " disintegrates and, along with it, all its belongings");
                    game.turn(plyr);
                }
            }
        }

    }
}