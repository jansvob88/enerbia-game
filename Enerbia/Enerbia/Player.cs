using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enerbia
{
    class Player
    {
        //Definitions
        //-------------------------------//

        private Item emptyItem = new Item(0);
        
        private int hp;
        private int maxHp;
        private int level;
        private int attack;
        private int defense;
        private int xp;
        private int maxXP;
        private int x;
        private int y;
        private int wealth;

        private string name;
        private string inventoryName;
        private string actionStatus;

        private bool actionBool;

        private World world;
        private Sektor sek;
        private Item[] inventory;
        private Model model;

        //-------------------------------//

        //Constructors
        //-------------------------------//

        public Player() { }

        /// <summary>
        /// Creates new player.
        /// </summary>
        /// <param name="jmeno">Name of a player</param>
        public void NewPlayer(string name, World world)
        {
            this.name = name;
            this.world = world; 

            hp = 100;
            maxHp = 100;
            attack = 5;
            defense = 3;
            level = 1;
            xp = 0;
            maxXP = 50;
            x = 20;
            y = 20;
            wealth = 0;
            actionBool = false;

            inventory = new Item[emptyItem.InventorySize(1)];
            inventoryName = emptyItem.InventoryName(1);
            for (int i = 0; i < inventory.Length; i++)
                inventory[i] = emptyItem;
            
            model = new Model();
        }

        //-------------------------------//

        //Main methods
        //-------------------------------//

        public void CallAction() { actionBool = !actionBool; }
        public void ChangeActionStatus(string s) { actionStatus = s; }

        public void GainItem(Item item, bool gainLose)
        {
            if (gainLose)
                inventory[Array.IndexOf(inventory, emptyItem)] = item;
            else
                inventory[Array.IndexOf(inventory, item)] = emptyItem;
        }
        public void GainWealth(int a) { wealth += a; }

        public void GainHP(int a) { hp += a; }
        public void GainXP(int a) { xp += a; }
        public void LevelUp() { level++; }
        public void ChangeX(int x) { this.x = x; }
        public void ChangeY(int y) { this.y = y; }

        //-------------------------------//

        //Return
        //-------------------------------//

        public override string ToString() { return name; }

        /// <summary>
        /// Returns a World the player is actually in
        /// </summary>
        /// <returns>World world(contains private Sektor[,])</returns>
        public World World() { return world; }
        public Sektor Sektor() { return  world.Sektor(Y(), X()); }
        public Item[] Inventory() { return inventory; }

        public Model Model() { return model; }
        
        public string InventoryName() { return inventoryName; }
        public string ActionStatus() { return actionStatus; }
        
        public bool Alive() { return (hp > 0); }
        public bool InventoryFreeSlot()
        {
            int itemCount = inventory.Length;
            for (int i = 0; i < inventory.Length; i++)
                if (inventory[i] != emptyItem)
                    itemCount += -1;
            if (itemCount > 0)
                return true;
            else return false; 
        }
        public bool ActionBool() { return actionBool; }

        public int Attack() { return attack; }
        public int Defense() { return defense; }
        public int Level() { return level; }
        public int Wealth() { return wealth; }

        public int HP() { return hp; }
        public int MaxHP() { return maxHp; }
        public int XP() { return xp; }
        public int MaxXP() { return maxXP; }

        public int X() { return x; }
        public int Y() { return y; }

        //-------------------------------//
        

    }
}
