using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace Enerbia
{
    class Engine
    {
        //Definitions
        //-------------------------------//
        
        private Item item;
        private Item emptyItem = new Item(0);
        private Item itemList = new Item();
        private Item[] itemEvidence = new Item[1000];

        private World world = new World();
        private Player p = new Player();

        private Location loc = new Location();
        private Sektor sek = new Sektor();

        private GUI gui = new GUI();
        private Controls con = new Controls();

        private ConsoleKey ck = ConsoleKey.Backspace;

        //-------------------------------//

        //General method
        //-------------------------------//

        public void Start()
        {
            //Definitions
            //-----------//
            
            NewGame();
            gui.SetWindow();
            gui.SetGuiMap(p);

            //Game run
            //--------//
            while (p.Alive() && ck != ConsoleKey.Escape)
            {
                gui.DisplayGUI(p);
                ShowPlayerPosition();
                //ShowInfo();
                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                ck = Console.ReadKey(true).Key;

                if (gui.message)
                    gui.ClearMessage();

                //Game controls
                //-------------//
                con.Action(p, gui, ck, world);

                while (p.ActionBool())
                {

                    switch (p.ActionStatus())
                    {
                        //-----------------//
                        case " ":
                            gui.DisplayMessage(gui.noItemHere);
                            p.CallAction();
                            break;

                        //-----------------//
                        case "createAble":
                            if (p.InventoryFreeSlot())
                            {
                                item = CreateItem(p.Sektor().ItemId());
                                p.GainItem(item, true);
                                p.Model().PowerUp(item.ModIndex(), 0.075F);
                                gui.DisplayMessage(gui.acquireItem + item.ToString());
                                gui.ClearSection();
                                gui.PlayerInventory(p);
                            }
                            else
                                gui.DisplayMessage(gui.inventoryFull);

                            p.CallAction();
                            break;

                        //-----------------//
                        case "enterAble":

                            char action = new char();
                            loc = (Location)p.Sektor();

                            //Set location
                            switch (loc.ToString())
                            {
                                case "Market":
                                    loc.MarketSettings(itemList);
                                    break;
                            }

                            //Run location

                            while (p.ActionBool())
                            {
                                gui.PlayerPosition(p);
                                gui.PlayerWealth(p);
                                gui.PlayerInventory(p);

                                switch (loc.ToString())
                                {
                                    //Case Market
                                    case "Market":
                                        action = loc.Market();

                                        switch (action)
                                        {
                                            case 'B':
                                                if (p.Wealth() >= loc.buysell[loc.lineselection, 1] && p.InventoryFreeSlot())
                                                {
                                                    item = CreateItem(itemList.ItemEvidenceNumber(loc.itemString[loc.lineselection]));
                                                    p.GainItem(item, true);
                                                    p.GainWealth(-loc.buysell[loc.lineselection, 1]);
                                                }
                                                else if (p.InventoryFreeSlot())
                                                    gui.DisplayMessage(gui.noWealth);
                                                else gui.DisplayMessage(gui.inventoryFull);
                                                break;

                                            case 'S':
                                                int itemCount = 0;
                                                int evnum = itemList.ItemEvidenceNumber(loc.itemString[loc.lineselection]);

                                                for (int i = 0; i < p.Inventory().Length; i++)
                                                    if (p.Inventory()[i].EvidenceNumber() == evnum)
                                                        itemCount += 1;

                                                if (itemCount > 0)
                                                {
                                                    item = Array.Find(p.Inventory(), item => item.EvidenceNumber() == evnum);
                                                    DeleteItem(item.Id());
                                                    p.GainItem(item, false);
                                                    p.GainWealth(loc.buysell[loc.lineselection, 0]);
                                                }
                                                else
                                                    gui.DisplayMessage(gui.itemMissing);
                                                break;

                                            case '0':
                                                gui.ClearMessage();
                                                break;

                                            case 'X':
                                                p.CallAction();
                                                break;
                                        }
                                        break;
                                        //Break Market
                                }
                            }
                            break;
                            //Break EnterLocation
                    }
                }//End of While (p.ActionBool())


               
            }

            //Save world
            world.SaveWorld();

        }


        //-------------------------------//

        //Main methods
        //-------------------------------//

        private Item CreateItem(int evidenceNumber)
        {
            int i = evidenceNumber;

            if (i == 1 || i == 2 || i == 3 || i == 4 || i == 5)
                item = new Mineral(itemList.NewId(), i, itemList.ItemName(i), itemList.ItemModelIndex(i));

            else if (i == 6 || i == 7 || i == 8)
                item = new Living(itemList.NewId(), i, itemList.ItemName(i), itemList.ItemModelIndex(i));

            else
                item = new Item(itemList.NewId(), i, itemList.ItemName(i), itemList.ItemModelIndex(i));

            AddItem(item);



            return item;
        }
        private void AddItem(Item i)
        {
            itemEvidence[Array.IndexOf(itemEvidence, emptyItem)] = i;
        }
        private void DeleteItem(int itemId)
        {
            itemEvidence[SearchItem(itemId)] = emptyItem;
        }
        private int SearchItem(int itemId)
        {
            int j = 0;
            for (int i = 0; i < itemEvidence.Length; i++)
                if (itemEvidence[i].Id() == itemId)
                    j = i;
            return j;
        }

        private void NewGame()
        {
            world = new World(300, "Equilibrium");
            world.Equilibrium1();

            p.NewPlayer("Nimero", world);

            for (int i = 0; i < itemEvidence.Length; i++)
                itemEvidence[i] = emptyItem;
        } 

        //-------------------------------//

        //Return
        //-------------------------------//

        //Shows game's itemEvidence items info
        public void ShowItemEvidence()
        {
            int count = 0;
            int j = 0;
  
            for (int i = 0; i < itemEvidence.Length; i++)
                if (itemEvidence[i].EvidenceNumber() != 0)
                    count += 1;
        
            for (int i = 0; i < 9; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 4, (((Console.WindowHeight / 4) * 3) + i) - 1);
                Console.Write("{0}", gui.FillString("", 50));
            }

            for (int i = 0; i < 9; i++)
            {
                Console.SetCursorPosition((Console.WindowWidth / 4), (Console.WindowHeight / 4) * 3 + i - 1);

                if (count > 9)
                {
                    j = count - 9;
                    Console.Write("{0} Id:{1} Name:{2} Class:{3} Ev#: {4}", j + i, itemEvidence[j + i].Id(), itemEvidence[j + i].ToString(), itemEvidence[j + i].ClassName(), itemEvidence[j + i].EvidenceNumber());
                }
                else Console.Write("{0} Id:{1} Name:{2} Class:{3} Ev#: {4}", i, itemEvidence[i].Id(), itemEvidence[i].ToString(), itemEvidence[i].ClassName(), itemEvidence[i].EvidenceNumber());
            }
        }
        public void ShowInfo()
        {
            gui.WSection = WindowSection.G;
            int[] temp = gui.SectionCoordinatesLeftTop();
            gui.ClearSection();
            
            //Shows actual ConsoleKey().Key
            Console.SetCursorPosition(temp[0], temp[1]);
            Console.Write("eng.ck = {0}", ck);
            //Shows player's position X
            Console.SetCursorPosition(temp[0], temp[1] + 2);
            Console.Write("p.X() = {0}", p.X());
            //Shows player's position Y
            Console.SetCursorPosition(temp[0], temp[1] + 3);
            Console.Write("p.Y() = {0}", p.Y());
            //Shows MoveCountX
            Console.SetCursorPosition(temp[0], temp[1] + 5);
            Console.Write("gui.MoveXCount {0}", gui.moveXCount);
            //Shows MoveCountY
            Console.SetCursorPosition(temp[0], temp[1] + 6);
            Console.Write("gui.MoveYCount {0}", gui.moveYCount);
            //Shows Engine private ItemList newItem's id
            Console.SetCursorPosition(temp[0], temp[1] + 8);
            Console.Write("itemList.ShowNewId {0}", itemList.ShowNewId());
            //Shows count of total existing items

            Console.SetCursorPosition(temp[0], temp[1] + 10);
            Console.Write("itemEvidence.Count {0}", itemEvidence.Count(item => item.Id() != 0));


            //Shows actual sektor info
            gui.WSection = WindowSection.F;
            temp = gui.SectionCoordinatesLeftTop();
            gui.ClearSection();

            Console.SetCursorPosition(temp[0], temp[1]);
            Console.Write("Name: {0}", p.Sektor().ToString());
            Console.SetCursorPosition(temp[0], temp[1] + 2);
            Console.Write("Id: {0}", p.Sektor().Id());
            Console.SetCursorPosition(temp[0], temp[1] + 4);
            Console.Write("ItemId: {0} {1}", p.Sektor().ItemId(), itemList.ItemName(p.Sektor().ItemId()));
            Console.SetCursorPosition(temp[0], temp[1] + 6);
            Console.Write("Force: {0} {1}", itemList.ItemModelIndex(p.Sektor().ItemId()), p.Model().ToString(itemList.ItemModelIndex(p.Sektor().ItemId())));

            Console.SetCursorPosition(temp[0], temp[1] + 9);
            Console.Write("StepAble: {0}", p.Sektor().StepAble());
            Console.SetCursorPosition(temp[0], temp[1] + 11);
            Console.Write("EnterAble: {0}", p.Sektor().EnterAble());
            Console.SetCursorPosition(temp[0], temp[1] + 13);
            Console.Write("CreateAble: {0}", p.Sektor().CreateAble());
            Console.SetCursorPosition(temp[0], temp[1] + 15);
            Console.Write("is Location: {0}", p.Sektor() is Location);

            //Shows actual force powers
            gui.WSection = WindowSection.I;
            temp = gui.SectionCoordinatesLeftTop();
            gui.ClearSection();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(temp[0], temp[1]);
            Console.Write("p.Power() {0}", p.Model().Enerix());
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.SetCursorPosition(temp[0], temp[1] + 2);
            Console.Write("p.Power() {0}", p.Model().Inverris());
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(temp[0], temp[1] + 4);
            Console.Write("p.Power() {0}", p.Model().Otirin());
            Console.ResetColor();
        }
        public void ShowPlayerPosition()
        {
            gui.WSection = WindowSection.G;
            int[] temp = gui.SectionCoordinatesLeftTop();

            string px = p.X().ToString();
            string py = p.Y().ToString();

            if (p.X() < 10)
                px = "00" + p.X().ToString();
            else if (p.X() < 100)
                px = "0" + p.X().ToString();
            if (p.Y() < 10)
                py = "00" + p.Y().ToString();
            else if (p.Y() < 100)
                py = "0" + p.Y().ToString();

            Console.SetCursorPosition(temp[0], temp[1]);
            Console.WriteLine("p.X() {0}", px);
            Console.SetCursorPosition(temp[0], temp[1] + 2);
            Console.WriteLine("p.Y() {0}", py);
        }
        //-------------------------------//
    }
}
