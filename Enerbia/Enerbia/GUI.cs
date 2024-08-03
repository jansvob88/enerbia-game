using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Enerbia
{
    public enum WindowSection { A, B, C, D, E, F, G, H, I }

    class GUI
    {
        //Definitions
        //-------------------------------//

        public int MapX { get; private set; }
        public int MapY { get; private set; }
        private int maxMoveX = 14;
        private int maxMoveY = 7;
        public int moveXCount = 0;
        public int moveYCount = 0;
        public int playerX, playerY;
        private int pWealth = -1;
        private int pInvCount = -1;
        
        private ConsoleColor[,] map;
 
        private bool boolMapDisplay = true;
        private bool toggleInventory = true;
        private bool toggleQuestLog = true;
        public bool message = false;

        public string itemMissing = "Nemáš tento předmět";
        public string inventoryFull = "Nemáš místo v inventáři";
        public string noItemHere = "Zde není žádný předmět";
        public string noWealth = "Nemáš dost peněz";
        public string cantStepHere = "Tam nelze jít";
        public string acquireItem = "Obdržel jsi: ";
        
        private int[] sectionCoord = new int[2];
        private WindowSection wSection;
        public WindowSection WSection
        {
            get { return wSection; }
            set { wSection = value; sectionCoord = SectionCoordinatesLeftTop(); }
        }
        private Sektor sek;
        private World world;


        //-------------------------------//

        //Constructors
        //-------------------------------//
        //-------------------------------//

        //Main methods
        //-------------------------------//

        /// <summary>
        /// Displays whole GUI of the game
        /// </summary>
        /// <param name="s">Actual world a player is in</param>
        /// <param name="p">Actual player</param>
        public void DisplayGUI(Player p)
        {   
            DisplayMap(p);
            PlayerGUI(p);
            PlayerPosition(p);
            PlayerWealth(p);
            ForceBars(p);
            if (toggleInventory)
                PlayerInventory(p);
        }

        /// <summary>
        /// Displays map of the world
        /// </summary>
        /// <param name="s"></param>
        /// <param name="p"></param>
        public void DisplayMap(Player p)
        {
            //Working in WindowSection E
            WSection = WindowSection.E;
            
            //Correcting map size due to true settings
            int mapWidth = SectionWidth();
            int mapHeight = SectionHeight();

            //Counting moves
            if (playerX != p.X())
                moveXCount += p.X() - playerX;
            if (playerY != p.Y())
                moveYCount += p.Y() - playerY;

            //Checking max moves reached
            if (moveXCount == maxMoveX || moveXCount == -maxMoveX)
            { moveXCount = 0; boolMapDisplay = true; }
            if (moveYCount == maxMoveY || moveYCount == -maxMoveY)
            { moveYCount = 0; boolMapDisplay = true; }

            //Map display
            if (boolMapDisplay)
            {
                int a = 0;
                //Setting starting coordinates of map
                MapX = p.X() - moveXCount - (int)decimal.Ceiling(mapWidth / 2);
                MapY = p.Y() - moveYCount - (int)decimal.Ceiling(mapHeight / 2);

                //Correcting starting coordinates of map
                if (MapX < 0)
                    MapX = 0;
                if (MapX > p.World().SektorWorld().GetLength(1) - mapWidth)
                    MapX = p.World().SektorWorld().GetLength(1) - mapWidth;
                if (MapY < 0)
                    MapY = 0;
                if (MapY > p.World().SektorWorld().GetLength(0) - mapHeight)
                    MapY = p.World().SektorWorld().GetLength(0) - mapHeight;

                for (int j = MapY; j < MapY + mapHeight; j++) //řádek
                {
                    Console.SetCursorPosition(sectionCoord[0], sectionCoord[1] + a);
                    for (int i = MapX; i < MapX + mapWidth; i++) //sloupec
                    {
                        if (i == p.X() && j == p.Y() && p.Sektor().ToString() != "Les")
                        {
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("♀");
                        }
                        else
                        {
                            Console.BackgroundColor = p.World().Sektor(j, i).Col1();
                            if (p.World().Sektor(j, i).Multikolor() == false)
                                Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = map[j, i];
                            Console.Write(p.World().Sektor(j, i).SpecialChar());
                        }
                    }
                    a += 1;
                }
                boolMapDisplay = false;
            }
            else
            {
                Console.SetCursorPosition(sectionCoord[0] + playerX - MapX, sectionCoord[1] + playerY - MapY);
                Console.BackgroundColor = ConsoleColor.Black;
                if (p.World().Sektor(playerY, playerX).Multikolor())
                    Console.BackgroundColor = p.World().Sektor(playerY, playerX).Col1();
                Console.ForegroundColor = map[playerY, playerX];
                Console.Write(p.World().Sektor(playerY, playerX).SpecialChar());

                Console.SetCursorPosition(sectionCoord[0] + p.X() - MapX , sectionCoord[1] + p.Y() - MapY);
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("♀");
            }
            Console.ResetColor();
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            //Saving current position of player
            playerX = p.X();
            playerY = p.Y();
        }

        /// <summary>
        /// Display force (magic strength) bars
        /// </summary>
        /// <param name="p"></param>
        private void ForceBars(Player p)
        {
            WSection = WindowSection.H;

            int line1 = sectionCoord[1];
            int line2 = sectionCoord[1] + 3;
            int line3 = sectionCoord[1] + 6;
            
            int enerix = (int)Math.Floor(Math.Abs(p.Model().Enerix()));
            int inverris = (int)Math.Floor(Math.Abs(p.Model().Inverris()));
            int otirin = (int)Math.Floor(Math.Abs(p.Model().Otirin()));

            Console.SetCursorPosition(sectionCoord[0] + SetStringMiddle(enerix.ToString().Length + 6, SectionWidth()), line1);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0} / {1}", enerix, p.Model().MaxEnerix());
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(sectionCoord[0] + SetStringMiddle(GraphicForceBar(p.Model().Enerix(), p.Model().MaxEnerix(), p.Model().SpecialChar(0)).Length, SectionWidth()), line1 + 1);
            Console.Write(GraphicForceBar(p.Model().Enerix(), p.Model().MaxEnerix(), p.Model().SpecialChar(0)));
            Console.ResetColor();

            Console.SetCursorPosition(sectionCoord[0] + SetStringMiddle(inverris.ToString().Length + 6, SectionWidth()), line2);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("{0} / {1}", inverris, p.Model().MaxInverris());
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(sectionCoord[0] + SetStringMiddle(GraphicForceBar(p.Model().Inverris(), p.Model().MaxInverris(), p.Model().SpecialChar(1)).Length, SectionWidth()), line2 + 1);
            Console.Write(GraphicForceBar(p.Model().Inverris(), p.Model().MaxInverris(), p.Model().SpecialChar(1)));
            Console.ResetColor();

            Console.SetCursorPosition(sectionCoord[0] + SetStringMiddle(otirin.ToString().Length + 6, SectionWidth()), line3);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("{0} / {1}", otirin, p.Model().MaxOtirin());
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(sectionCoord[0] + SetStringMiddle(GraphicForceBar(p.Model().Otirin(), p.Model().MaxOtirin(), p.Model().SpecialChar(2)).Length, SectionWidth()), line3 + 1);
            Console.Write(GraphicForceBar(p.Model().Otirin(), p.Model().MaxOtirin(), p.Model().SpecialChar(2)));
            Console.ResetColor();
        }

        public void MainMenu()
        {
            bool run = true;
            int i = 1;
            ConsoleKey ck = ConsoleKey.UpArrow;

            while (run)
            {
                bool b = true;
                string s = null;
                string t = null;
                Console.Clear();

                StreamReader sr = new StreamReader("charList.txt");
                int line = 0;
                while (b)
                {
                    t = sr.ReadLine();
                    if (t != null)
                    {
                        line += 1;
                        s = s + t + " ";
                    }
                    else
                        b = false;
                }   

                sr.Close();
                
                string[] charList = new string[s.Count( e => e == ' ')];
                for (int j = 0; j < line; j++)
                    charList[j] = s.Split(' ')[j];

                WSection = WindowSection.B;

                int secWidth = SectionWidth();
                int secHeight = SectionHeight();

                Console.SetCursorPosition(sectionCoord[0] + secWidth / 2 - 3, sectionCoord[1] + secHeight / 2);
                Console.Write("Enerbia");
                Console.SetCursorPosition(sectionCoord[0] + secWidth / 2 - 3, sectionCoord[1] + secHeight / 2 + 1);
                Console.Write("=======");

                WSection = WindowSection.F;
                sectionCoord = SectionCoordinatesLeftTop();

                secWidth = SectionWidth();
                secHeight = SectionHeight();

                Console.SetCursorPosition(sectionCoord[0] + secWidth / 2 - "Character list".Length / 2, sectionCoord[1] + 1);
                Console.Write("Character list");
                Console.SetCursorPosition(sectionCoord[0] + secWidth / 2 - "Character list".Length / 2, sectionCoord[1] + 2);
                Console.Write("==============");

                int k = 0;
                for (int j = 0; j < line; j++)
                {
                    
                    Console.SetCursorPosition(sectionCoord[0] + secWidth / 2 - charList[j].Length / 2, sectionCoord[1] + 4 + k);
                    Console.Write(charList[j]);
                    k += 2;
                }

                //Console.SetCursorPosition(temp[0] + secWidth / 2 - 3, temp[1] + 2);
                //if (i == 1)
                //    Console.BackgroundColor = ConsoleColor.Blue;
                //Console.Write("Create");
                //Console.ResetColor();

                //Console.SetCursorPosition(temp[0] + secWidth / 2 - 3, temp[1] + 4);
                //if (i == 2)
                //    Console.BackgroundColor = ConsoleColor.Blue;
                //Console.Write("Choose");
                //Console.ResetColor();


                ck = Console.ReadKey().Key;

                switch (ck)
                {
                    case ConsoleKey.UpArrow:
                        i = 1;
                        break;
                    case ConsoleKey.DownArrow:
                        i = 2;
                        break;
                    case ConsoleKey.Enter:
                        run = false;
                        break;
                }
            }

        }

        /// <summary>
        /// Displays level of player, name of player, HP of player in the upper left corner of the window
        /// </summary>
        /// <param name="p">Player to be displayed</param>
        public void PlayerGUI(Player p)
        {
            WSection = WindowSection.A;

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = (ConsoleColor)p.Model().ReturnColor1(p.Model().MainPower());
            Console.SetCursorPosition(sectionCoord[0], sectionCoord[1]);
            Console.Write(p.Level());
            Console.SetCursorPosition(sectionCoord[0] + 2, sectionCoord[1]);
            Console.Write(FillString(p.ToString(), 20));
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(sectionCoord[0], sectionCoord[1] + 2);
            Console.Write("HP: {0}/{1}", p.HP(), p.MaxHP());
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(sectionCoord[0], sectionCoord[1] + 3);
            Console.Write(GraphicBar(p.HP(), p.MaxHP(),20));
            Console.ResetColor();
        }

        /// <summary>
        /// Displays player's inventory
        /// </summary>
        /// <param name="p"></param>
        public void PlayerInventory(Player p)
        {
            if (p.Inventory().Count(item => item.Id() != 0) != pInvCount) // if item was added to inventory
            {
                pInvCount = p.Inventory().Count(item => item.Id() != 0);

                WSection = WindowSection.D;
                ClearSection();

                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(sectionCoord[0], sectionCoord[1]);
                Console.Write("{0}", p.InventoryName());
                string s = "";
                for (int i = 0; i < p.InventoryName().Length; i++)
                    s += "-";
                Console.SetCursorPosition(sectionCoord[0], sectionCoord[1] + 1);
                Console.WriteLine(s);
                for (int i = 0; i < p.Inventory().Length; i++)
                    if (p.Inventory()[i].Id() != 0)
                    {
                        Console.SetCursorPosition(sectionCoord[0], sectionCoord[1] + 2 + i);
                        Console.WriteLine("Id:{0} {1} ", p.Inventory()[i].Id(), p.Inventory()[i]);
                    }
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Displays name of Sektor a player is in
        /// </summary>
        /// <param name="p"></param>
        /// <param name="s"></param>
        public void PlayerPosition(Player p)
        {
            if (sek != p.Sektor() || world != p.World())
            {
                sek = p.Sektor();
                world = p.World();

                WSection = WindowSection.B;
                ClearSection();

                int i = p.World().ToString().Length;
                string s1 = "";
                for (int j = 0; j < i; j++)
                    s1 += "-";

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(sectionCoord[0] + SetStringMiddle(i, SectionWidth()), sectionCoord[1]);
                Console.Write("{0}", p.World().ToString());
                Console.SetCursorPosition(sectionCoord[0] + SetStringMiddle(i, SectionWidth()), sectionCoord[1] + 1);
                Console.Write(s1);
                Console.ResetColor();

                i = p.Sektor().ToString().Length;
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(sectionCoord[0] + SetStringMiddle(i, SectionWidth()), sectionCoord[1] + 3);
                Console.Write("{0}", p.Sektor().ToString());
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Displays player's wealth
        /// </summary>
        /// <param name="p"></param>
        public void PlayerWealth(Player p)
        {

            if (pWealth != p.Wealth())
            {
                pWealth = p.Wealth();

                WSection = WindowSection.C;
                ClearSection();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(sectionCoord[0], sectionCoord[1]);
                Console.Write("Wealth << {0} >>", p.Wealth());
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Converts World.SektorWorld[] into internal GUI.map
        /// </summary>
        /// <param name="p"></param>
        public void SetGuiMap(Player p)
        {
            playerX = p.X();
            playerY = p.Y();
            int x = p.World().SektorWorld().GetLength(1);
            int y = p.World().SektorWorld().GetLength(0);

            map = new ConsoleColor[y, x];

            for (int j = 0; j < map.GetLength(0); j++)
            {
                for (int i = 0; i < map.GetLength(1); i++)
                {
                        map[j, i] = p.World().Sektor(j, i).Col();
                }
            }
        }

        /// <summary>
        /// Displays XP bar of the player
        /// </summary>
        /// <param name="p"></param>
        private void XPBar(Player p)
        {
            WSection = WindowSection.H;
            int i = 5 + p.XP().ToString().Length + p.MaxXP().ToString().Length;
            Console.SetCursorPosition(SetStringMiddle( i, SectionWidth()), ((Console.WindowHeight) - 5));
            Console.Write("XP: {0}/{1}", p.XP(), p.MaxXP());
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.SetCursorPosition(SetStringMiddle((GraphicBar(p.XP(), p.MaxXP(), 50)).ToString().Length, SectionWidth()), ((Console.WindowHeight) - 4));
            Console.Write(GraphicBar(p.XP(), p.MaxXP(),50));
            Console.ResetColor();
        }

        //Section methods

        /// <summary>
        /// Draws frame
        /// </summary>
        public void DrawSectionFrame()
        {
            int temp = 0;
            for (int i = 1; i < Console.WindowWidth - 1; i++)
            {
                temp += 1;
                if (temp == 3)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    temp = 0;
                }
                else Console.ForegroundColor = ConsoleColor.Cyan;

                Console.SetCursorPosition(i, 1);
                Console.Write("\\");
                Console.SetCursorPosition(i, 11);
                Console.Write("\\");
                Console.SetCursorPosition(i, Console.WindowHeight - 18);
                Console.Write("\\");
                Console.SetCursorPosition(i, Console.WindowHeight - 2);
                Console.Write("\\");
            }
            temp = 0;
            for (int i = 1; i < Console.WindowHeight - 1; i++)
            {
                temp += 1;
                if (temp == 3)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    temp = 0;
                }
                else Console.ForegroundColor = ConsoleColor.Cyan;

                Console.SetCursorPosition(1, i);
                Console.Write("\\");
                Console.SetCursorPosition(28, i);
                Console.Write("\\");
                Console.SetCursorPosition(Console.WindowWidth - 29, i);
                Console.Write("\\");
                Console.SetCursorPosition(Console.WindowWidth - 2, i);
                Console.Write("\\");
            }
            Console.ResetColor();
        }

        /// <summary>
        /// Sets default window size, title, cursor visibility
        /// </summary>
        public void SetWindow()
        {
            Console.SetWindowSize(115, 55);
            Console.SetBufferSize(115, 55);
            Console.Title = "Enerbia";
            Console.CursorVisible = false;
            DrawSectionFrame();
        }

        public void ChangeWindowSize(int width, int height)
        {
            int a = 0, b = 0;
            //Kontrola, zda-li jsou zadané hodnoty liché
            if (decimal.Floor(width / 2) * 2 == width) { a = width; }
            else a = width + 1;
            if (decimal.Floor(height / 2) * 2 == height) { b = height; }
            else b = height + 1;

        } //Zatím nedokončeno

        /// <summary>
        /// Returns int[4] section coordinates
        /// int[0] left start
        /// int[1] left end
        /// int[2] top start
        /// int[3] top end
        /// </summary>
        /// <param name="wSection"></param>
        /// <returns></returns>
        public int[] SectionCoordinates()
        {
            int[] temp = new int[4];

            switch (WSection)
            {
                case WindowSection.A:
                    temp[0] = 2;
                    temp[1] = 27;
                    temp[2] = 2;
                    temp[3] = 10;
                    break;

                case WindowSection.B:
                    temp[0] = 29;
                    temp[1] = Console.WindowWidth - 30;
                    temp[2] = 2;
                    temp[3] = 10;
                    break;

                case WindowSection.C:
                    temp[0] = Console.WindowWidth - 28;
                    temp[1] = Console.WindowWidth - 3;
                    temp[2] = 2;
                    temp[3] = 10;
                    break;

                case WindowSection.D:
                    temp[0] = 2;
                    temp[1] = 27;
                    temp[2] = 12;
                    temp[3] = Console.WindowHeight - 19;
                    break;

                case WindowSection.E:
                    temp[0] = 29;
                    temp[1] = Console.WindowWidth - 30;
                    temp[2] = 12;
                    temp[3] = Console.WindowHeight - 19;
                    break;

                case WindowSection.F:
                    temp[0] = Console.WindowWidth - 28;
                    temp[1] = Console.WindowWidth - 3;
                    temp[2] = 12;
                    temp[3] = Console.WindowHeight - 19;
                    break;

                case WindowSection.G:
                    temp[0] = 2;
                    temp[1] = 27;
                    temp[2] = Console.WindowHeight - 17;
                    temp[3] = Console.WindowHeight - 3;
                    break;

                case WindowSection.H:
                    temp[0] = 29;
                    temp[1] = Console.WindowWidth - 30;
                    temp[2] = Console.WindowHeight - 17;
                    temp[3] = Console.WindowHeight - 3;
                    break;

                case WindowSection.I:
                    temp[0] = Console.WindowWidth - 28;
                    temp[1] = Console.WindowWidth - 3;
                    temp[2] = Console.WindowHeight - 17;
                    temp[3] = Console.WindowHeight - 3;
                    break;
            }
            return temp;
        }

        /// <summary>
        /// Returns int[2] section start coordinates
        /// int[0] left start
        /// int[2] top start
        /// </summary>
        /// <param name="wSection"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public int[] SectionCoordinatesLeftTop()
        {
            int[] temp = new int[2];
            temp[0] = SectionCoordinates()[0];
            temp[1] = SectionCoordinates()[2];
            return temp;
        }

        /// <summary>
        /// Returns column count of section
        /// </summary>
        /// <returns></returns>
        public int SectionWidth() { return SectionCoordinates()[1] - SectionCoordinates()[0] + 1; }

        /// <summary>
        /// Returns row count of section
        /// </summary>
        /// <returns></returns>
        public int SectionHeight() { return SectionCoordinates()[3] - SectionCoordinates()[2] + 1; }

        //Help methods

        /// <summary>
        /// Returns integer that is used as CursorPosition(int left)
        /// </summary>
        /// <param name="stringLength">length of string which is about to be centered</param>
        /// <param name="totalLength">length of space the string is about to be centered in</param>
        /// <returns>Integer that is used as CursorPosition(int left)</returns>
        public int SetStringMiddle(int stringLength, int totalLength)
        {
            int a = (totalLength - stringLength) / 2;
            return a; 
        }

        /// <summary>
        /// Fills the word with empty spaces in front of string and behind it, sets the word in the middle of string
        /// </summary>
        /// <param name="s">Word to be filled</param>
        /// <param name="total">Count of new string length</param>
        /// <returns>Filled word with empty spaces</returns>
        public string FillString(string s, int total)
        {
            string s1 = "";
            int b = s.Length;
            int b1;

            for (b1 = 0; b1 < b; b1 += 2);//Finds out whether count of chars in string is odd or even: b1 = b || b1 = b + 1

            int c = (total - b) / 2;

            //Filling the word with spaces
            if (b1 != b)//Count of chars is odd
            {
                c = int.Parse(Math.Round((decimal)((total - b) / 2), MidpointRounding.AwayFromZero).ToString());
                for (int j = 0; j < c; j++)
                    s1 += " ";
                s1 += s;
                s1 = s1.PadRight(total);
            }
            else//Count of chars is even
            {
                for (int j = 0; j < c; j++)
                    s1 += " ";
                s1 += s;
                s1 = s1.PadRight(total);
            }
            return s1;
        }

        /// <summary>
        /// Graphical representation of a value
        /// </summary>
        /// <param name="aktualni">Actual value</param>
        /// <param name="maximalni">Max value</param>
        /// <param name="celkem">Required length of graphic bar</param>
        /// <returns>Graphic bar</returns>
        private string GraphicBar(int aktualni, int maximalni, int celkem)
        {
            string s = "[";
            double pocet = Math.Round(((double)aktualni / maximalni) * celkem);
            if (pocet == 0 && aktualni > 0)
                pocet = 1;
            for (int i = 0; i < pocet; i++)
                s += "█";
            s = s.PadRight(celkem + 1);
            s += "]";
            return s;
        }

        private string GraphicForceBar(float aktualni, int maximalni, string specialChar)
        {
            int max = 20;
            int policka;
            
            float temp = (float)maximalni / max;

            if (aktualni < 0 && aktualni > -1 || aktualni > 0 && aktualni < 1)
                policka = 1;
            else if (aktualni < 0)
                policka = (int)Math.Floor((-aktualni) / (double)temp);
            
            else policka = (int)Math.Floor(aktualni / (double)temp);

            if (policka < 1)
                policka = 1;

            int pocet = max - policka;

            string s = "[";

            if (aktualni < 0)
            {
                for (int i = 0; i < pocet; i++)
                    s += " ";
                for (int i = 0; i < policka; i++)
                    s += "<";
                s += specialChar;
                for (int i = 0; i < max; i++)
                    s += " ";
                s += "]";
            }
            else if (aktualni > 0)
            {
                for (int i = 0; i < max; i++)
                    s += " ";
                s += specialChar;
                for (int i = 0; i < policka; i++)
                    s += ">";
                for (int i = 0; i < pocet; i++)
                    s += " ";
                s += "]";
            }
            
            else
            {
                for (int i = 0; i < max; i++)
                    s += " ";
                s += specialChar;
                for (int i = 0; i < max; i++)
                    s += " ";
                s += "]";
            }

            return s;
        }

        public void ClearSection()
        {
            Console.ResetColor();

            for (int j = sectionCoord[1]; j < sectionCoord[1] + SectionHeight(); j++)
            {
                for (int i = sectionCoord[0]; i < sectionCoord[0] + SectionWidth(); i++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(" ");
                }
            }
        }
        public void ClearAllSections()
        {
            WSection = WindowSection.A; ClearSection();
            WSection = WindowSection.B; ClearSection();
            WSection = WindowSection.C; ClearSection();
            WSection = WindowSection.D; ClearSection();
            WSection = WindowSection.E; ClearSection();
            WSection = WindowSection.F; ClearSection();
            WSection = WindowSection.G; ClearSection();
            WSection = WindowSection.H; ClearSection();
            WSection = WindowSection.I; ClearSection();
        }

        public void ClearMessage()
        {
            WSection = WindowSection.B;
            int[] temp = SectionCoordinates();

            Console.SetCursorPosition(temp[0], temp[3] - 1);
            Console.Write("{0}", FillString("", SectionWidth()));
            message = false;
        }
        public void DisplayMessage(string s)
        {
            WSection = WindowSection.B;
            int[] temp = SectionCoordinates();

            int a = s.Length;
            Console.SetCursorPosition(temp[0] + SetStringMiddle(a + 6, SectionWidth()), temp[3] - 1);
            Console.Write("<<<{0}>>>", s);
            message = true;
        }

        public void ToggleInventory() {toggleInventory = !toggleInventory; }
        public void ToggleQuestLog() { toggleQuestLog = !toggleQuestLog; }

        //-------------------------------//
    }
}
