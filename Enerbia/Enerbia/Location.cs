using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enerbia
{
    class Location: Sektor
    {
        //Definitions
        //-------------------------------//
        GUI gui = new GUI();
        
        private char action;

        private int column1;
        private int column2;
        private int column3;
        private int line;

        public int lineselection;
        public int columnselection;

        private int itemCount;
        private int[] itemInt;
        public int[,] buysell;
        public string[] itemString;

        private ConsoleKey ck = new ConsoleKey();

        //-------------------------------//

        //Constructors
        //-----------------------------//

        public Location() { }

        public Location(string name, string specialChar,
                        int id, int itemId, int patt1, int patt2, int col1, int col2, int col3,
                        bool stepAble)
                        : base
                        ( name, specialChar,
                          id, itemId, patt1, patt2, col1, col2, col3,
                          stepAble)
        {
            bools[1] = true;
            strings[2] = "enterAble";
        }
        
        //-----------------------------//

        //Main methods
        //-------------------------------//

        //Market
        public void MarketSettings(Item item)
        {
            gui.WSection = WindowSection.E;
            int[] temp = gui.SectionCoordinatesLeftTop();

            column1 = temp[0];
            column2 = temp[0] + 25;
            column3 = temp[0] + 35;
            line = temp[1];
            lineselection = 0;
            columnselection = 2;
            itemInt = new int[6] { 2, 3, 4, 7, 8, 9};
            itemString = new string[itemInt.Length];
            buysell = new int[itemInt.Length, 2];
            itemCount = itemInt.Length;

            for (int i = 0; i < itemInt.Length; i++)
            {
                itemString[i] = item.ItemName(itemInt[i]);
                buysell[i, 0] = item.ItemPrice(itemInt[i]) - 1;
                buysell[i, 1] = item.ItemPrice(itemInt[i]) + 1;
            }

            
        }
        public void MarketGui()
        {
            gui.ClearAllSections();

            Console.SetCursorPosition(column1, line);
            Console.Write("Item");
            Console.SetCursorPosition(column1, line + 1);
            Console.Write("----");
            Console.SetCursorPosition(column2, line);
            Console.Write("Buy");
            Console.SetCursorPosition(column2, line + 1);
            Console.Write("---");
            Console.SetCursorPosition(column3, line);
            Console.Write("Sell");
            Console.SetCursorPosition(column3, line + 1);
            Console.Write("----");

            gui.WSection = WindowSection.F;
            int[] temp = gui.SectionCoordinatesLeftTop();
            Console.SetCursorPosition(temp[0], temp[1]);
            Console.Write("Select: ↑ ↓ → ←");
            Console.SetCursorPosition(temp[0], temp[1] + 2);
            Console.Write("Action: {0}", ConsoleKey.Enter);
            Console.SetCursorPosition(temp[0], temp[1] + 4);
            Console.Write("Exit: {0}", ConsoleKey.Escape);

            for (int i = 0; i < itemCount; i++)
            {
                
                Console.ForegroundColor = ConsoleColor.White;
                if (lineselection == i)
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.SetCursorPosition(column1, line + 3 + i * 2);
                Console.Write("{0}", itemString[i]);
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.White;
                if (lineselection == i && columnselection == 2)
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.SetCursorPosition(column2, line + 3 + i * 2);
                Console.Write("{0}", buysell[i, 0]);
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.White;
                if (lineselection == i && columnselection == 3)
                    Console.BackgroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(column3, line + 3 + i * 2);
                Console.Write("{0}", buysell[i, 1]);
                Console.ResetColor();
            }
        }
        public char Market()
        {
            MarketGui();
            ck = Console.ReadKey().Key;

            switch (ck)
            {
                case ConsoleKey.UpArrow:
                    if (lineselection > 0)
                        lineselection += -1;
                    break;

                case ConsoleKey.DownArrow:
                    if (lineselection < itemCount - 1)
                        lineselection += 1;
                    break;

                case ConsoleKey.LeftArrow:
                    if (columnselection > 2)
                        columnselection += -1;
                    break;

                case ConsoleKey.RightArrow:
                    if (columnselection < 3)
                        columnselection += 1;
                    break;

                case ConsoleKey.Enter:
                    switch (columnselection)
                    {
                        case 2:
                            action = 'S';
                            break;

                        case 3:
                            action = 'B';
                            break;
                    }
                    break;

                case ConsoleKey.Escape:
                    action = 'X';
                    for (int i = ((Console.WindowWidth / 4) * 3) + 1; i < Console.WindowWidth; i++)
                    {
                        Console.SetCursorPosition(i, line);
                        Console.Write(" ");
                        Console.SetCursorPosition(i, line + 2);
                        Console.Write(" ");
                        Console.SetCursorPosition(i, line + 4);
                        Console.Write(" ");
                    }
                    break;
            }

            if (ck == ConsoleKey.UpArrow || ck == ConsoleKey.DownArrow || ck == ConsoleKey.LeftArrow || ck == ConsoleKey.RightArrow)
                action = '0';

            return action;
        }

        //-------------------------------//

        //Return
        //-------------------------------//
        //-------------------------------//
    }
}
