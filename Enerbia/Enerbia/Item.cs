using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enerbia
{
    class Item
    {
        //Definitions
        //-------------------------------//
        private string[] itemName = new string[20];
        private int[] itemEvidenceNumber = new int[20];
        private int[] itemPrice = new int[20];
        private int[] modelIndex = new int[20];
        private int newId;
        

        private string[,] inventoryClass = { { "10", "20" }, { "Vak", "Batoh" } };

        protected int id;
        protected int evidenceNumber;
        protected int modIndex;
        protected string name = "";
        protected string className = "";
        protected Player owner;

        //-------------------------------//

        //Item Evidence
        //-------------------------------//
        public Item()
        {
            newId = 0;
            itemEvidenceNumber[0] = 1;       itemName[0] = "Zemina";              itemPrice[0] = 0;         modelIndex[0] = 0;
            itemEvidenceNumber[1] = 2;       itemName[1] = "Arcanit";             itemPrice[1] = 10;        modelIndex[1] = 0;
            itemEvidenceNumber[2] = 3;       itemName[2] = "Karolit";             itemPrice[2] = 30;        modelIndex[2] = 2;
            itemEvidenceNumber[3] = 4;       itemName[3] = "Ferit";               itemPrice[3] = 90;        modelIndex[3] = 1;
            itemEvidenceNumber[4] = 5;       itemName[4] = "Kumpur";              itemPrice[4] = 100;       modelIndex[4] = 2;
            itemEvidenceNumber[5] = 6;       itemName[5] = "Platian";             itemPrice[5] = 250;       modelIndex[5] = 0;
            itemEvidenceNumber[6] = 7;       itemName[6] = "Enerix";              itemPrice[6] = 1380;      modelIndex[6] = 0;
            itemEvidenceNumber[7] = 8;       itemName[7] = "Inverris";            itemPrice[7] = 1460;      modelIndex[7] = 1;
            itemEvidenceNumber[8] = 9;       itemName[8] = "Otirin";              itemPrice[8] = 1030;      modelIndex[8] = 2;
            itemEvidenceNumber[9] = 10;      itemName[9] = "Dřevo";               itemPrice[9] = 10;        modelIndex[9] = 1;
            itemEvidenceNumber[10] = 11;     itemName[10] = "Kapradí";            itemPrice[10] = 0;        modelIndex[10] = 0;
            itemEvidenceNumber[11] = 12;     itemName[11] = "Řasa";               itemPrice[11] = 10;       modelIndex[11] = 2;
        }

        public int NewId()
        {
            newId += 1;
            return newId;
        }
        public int ShowNewId()
        {
            return newId + 1;
        }

        public string ItemName(int evidenceNumber)
        {
            int i = Array.IndexOf(itemEvidenceNumber, evidenceNumber);
            return itemName[i];
        }
        public int ItemPrice(int evidenceNumber)
        {
            int i = Array.IndexOf(itemEvidenceNumber, evidenceNumber);
            return itemPrice[i];
        }
        public int ItemEvidenceNumber(string itemName)
        {
            int i = Array.IndexOf(this.itemName, itemName);
            return itemEvidenceNumber[i];
        }
        public int ItemModelIndex(int evidenceNumber)
        {
            int i = Array.IndexOf(itemEvidenceNumber, evidenceNumber);
            return modelIndex[i];
        }

        //-------------------------------//

        //Items
        //-------------------------------//

        /// <summary>
        /// Constructor for an "empty Item"
        /// </summary>
        /// <param name="evidenceNumber">empyItem.evidenceNumber = 0</param>
        public Item(int evidenceNumber)
        {
            name = "";
            id = 0;
            evidenceNumber = 0;
        }

        /// <summary>
        /// Constructor for any item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="evidenceNumber"></param>
        /// <param name="name"></param>
        public Item(int id, int evidenceNumber, string name, int modIndex)
        {
            this.id = id;
            this.evidenceNumber = evidenceNumber;
            this.name = name;
            this.modIndex = modIndex;
        }

        //-------------------------------//

        //Returns
        //-------------------------------//

        public int EvidenceNumber() { return evidenceNumber; }
        public int Id() { return id; }
        public int InventorySize(int inventoryClass) { return int.Parse(this.inventoryClass[0,inventoryClass]); }
        public int ModIndex() { return modIndex; }
        public string InventoryName(int inventoryClass) { return this.inventoryClass[1, inventoryClass]; }
        public string ClassName() { return className; }
        public override string ToString() { return name; }

        //-------------------------------//

    }
}
