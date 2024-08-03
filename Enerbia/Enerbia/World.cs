using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Enerbia
{
    class World
    {
        //Definitions
        //-------------------------------//

        private string name;
        private Sektor[,] world;

        private Sektor sek = new Sektor();
        private Sektor sek1 = new Sektor();
        private Sektor sek2 = new Sektor();
        private Sektor sek3 = new Sektor();
        

        

        //Non-create-item-able Sektors
        private Sektor hranice = new Sektor("Hranice", "/", 0, 0, 30, 65, (int)ConsoleColor.Black, (int)ConsoleColor.Magenta, (int)ConsoleColor.Cyan, false);

        private Sektor skala = new Sektor("Skála", "▒", 2, 0, 40, 80, (int)ConsoleColor.Black, (int)ConsoleColor.DarkGray, (int)ConsoleColor.Gray, true);

        private Sektor stezka = new Sektor("Stezka", "░", 6, 0, 15, 100, (int)ConsoleColor.DarkYellow, (int)ConsoleColor.Gray, (int)ConsoleColor.Gray, true);

        private Sektor cesta = new Sektor("Cesta", "▓", 7, 0, 0, 50, (int)ConsoleColor.Gray, (int)ConsoleColor.DarkGray, (int)ConsoleColor.Black, true);

        private Sektor most = new Sektor("Most", "║", 8, 0, 0, 100, (int)ConsoleColor.DarkYellow, (int)ConsoleColor.Black, (int)ConsoleColor.DarkGray, true);

        private Sektor budova = new Sektor("Budova", "╦", 9, 0, 0, 0, (int)ConsoleColor.Green, (int)ConsoleColor.Green, (int)ConsoleColor.Green, true);

        private Sektor dvere = new Sektor("Dveře", "┬", 10, 0, 0, 0, (int)ConsoleColor.Yellow, (int)ConsoleColor.Yellow, (int)ConsoleColor.Yellow, true);

        //Create-item-able Sektors
        private Sektor zemina = new Sektor("Zemina", "░", 1, 1, 60, 80, (int)ConsoleColor.DarkYellow, (int)ConsoleColor.DarkMagenta, (int)ConsoleColor.Black, true);

        private Sektor les = new Sektor("Les", "▓", 3, 10, 5, 45, (int)ConsoleColor.Green, (int)ConsoleColor.DarkGreen, (int)ConsoleColor.Black, true);

        private Sektor voda = new Sektor("Voda", "▒", 4, 12, 20, 70, (int)ConsoleColor.Blue, (int)ConsoleColor.Cyan, (int)ConsoleColor.Black, true);

        private Sektor louka = new Sektor("Louka", "▓", 5, 11, 0, 75, (int)ConsoleColor.Yellow, (int)ConsoleColor.DarkGreen, (int)ConsoleColor.DarkRed, true);

        
        private Sektor arcanit = new Sektor("Arcanit", "░", 11, 2, 0, 50, (int)ConsoleColor.DarkMagenta, (int)ConsoleColor.Cyan, (int)ConsoleColor.DarkCyan, true);

        private Sektor karolit = new Sektor("Karolit", "║", 12, 3, 0, 0, (int)ConsoleColor.DarkGreen, (int)ConsoleColor.DarkGreen, (int)ConsoleColor.DarkGreen, true);

        private Sektor ferit = new Sektor("Ferit", "╬", 13, 4, 0, 0, (int)ConsoleColor.DarkRed, (int)ConsoleColor.DarkRed, (int)ConsoleColor.DarkRed, true);



        private Sektor kumpur = new Sektor("Kumpur", "▓", 14, 5, 0, 0, (int)ConsoleColor.Red, (int)ConsoleColor.Red, (int)ConsoleColor.Red, true);

        private Sektor platian = new Sektor("Platian", "▓", 15, 6, 0, 0, (int)ConsoleColor.Cyan, (int)ConsoleColor.Cyan, (int)ConsoleColor.Cyan, true);

        
        private Sektor enerix = new Sektor("Enerix", "♦", 16, 7, 0, 0, (int)ConsoleColor.Cyan, (int)ConsoleColor.Cyan, (int)ConsoleColor.Cyan, true);

        private Sektor inverris = new Sektor("Inverris", "♦", 17, 8, 0, 100, (int)ConsoleColor.Magenta, (int)ConsoleColor.Cyan, (int)ConsoleColor.Magenta, true);

        private Sektor otirin = new Sektor("Otirin", "♦", 18, 9, 0, 0, (int)ConsoleColor.Magenta, (int)ConsoleColor.Magenta, (int)ConsoleColor.Magenta, true);

        
        //Locations
        private Location market = new Location("Market", "§", 20, 0, 0, 0, (int)ConsoleColor.Yellow, (int)ConsoleColor.Yellow, (int)ConsoleColor.Yellow, true);

        //-------------------------------//

        //Constructors
        //-------------------------------//
        public World() { }

        /// <summary>
        /// Creates a world of the specified size and name
        /// </summary>
        /// <param name="world">Size of the world (Sektor[,])</param>
        /// <param name="name">String name of the world</param>
        public World(int i, string name)
        {
            this.name = name;
            world = new Sektor[i, i];
        }

        //-------------------------------//

        //Main methods
        //-------------------------------//

        private void SetPlainWorld()
        {
            //sets all Sektors in Sektor[,] s to: zemina
            for (int j = 0; j < world.GetLength(0); j++)
            {
                for (int i = 0; i < world.GetLength(1); i++)
                    world[j, i] = zemina;
            }
            //sets horizontal boundaries(upper and lower) of Sektor[,] s to: hranice 
            for (int i = 0; i < world.GetLength(1); i++)
            {
                world[0, i] = hranice;
                world[world.GetLength(0) - 1, i] = hranice;
            }
            //sets vertical boundaries(left and right) of Sektor[,] s to: hranice
            for (int i = 0; i < world.GetLength(0); i++)
            {
                world[i, 0] = hranice;
                world[i, world.GetLength(1) - 1] = hranice;
            }
        }

        private void SetBuilding(int x, int y, int i)
        {
            sek1 = dvere;
            sek2 = budova;
            sek3 = market;

            switch (i)
            {
                case 1:
                    world[x, y] = sek1;
                    break;

                case 2:
                    world[x, y] = sek1;
                    world[x, y - 1] = sek2;
                    break;

                case 3:
                    world[x, y] = sek1;
                    world[x, y - 1] = sek2;
                    world[x - 1, y - 1] = sek2;
                    break;

                case 4:
                    world[x, y] = sek1;
                    world[x, y + 1] = sek2;
                    world[x - 1, y] = sek2;
                    world[x - 1, y + 1] = sek2;
                    break;

                case 5:
                    world[x, y] = sek3;
                    world[x, y - 1] = sek2;
                    world[x, y - 2] = sek2;
                    world[x - 1, y] = sek2;
                    world[x - 1, y - 1] = sek2;
                    break;

                case 6:
                    world[x, y] = sek1;
                    world[x, y + 1] = sek2;
                    world[x, y + 2] = sek2;
                    world[x, y + 3] = sek2;
                    world[x - 1, y] = sek2;
                    world[x - 1, y + 1] = sek2;
                    break;

                case 7:
                    world[x, y] = sek1;
                    world[x, y + 1] = sek2;
                    world[x, y - 1] = sek2;
                    world[x - 1, y] = sek2;
                    world[x - 1, y + 1] = sek2;
                    world[x - 1, y - 1] = sek2;
                    world[x - 2, y - 1] = sek2;
                    break;

                case 8:
                    world[x, y] = sek1;
                    world[x, y -1] = sek2;
                    world[x, y + 1] = sek2;
                    world[x, y + 2] = sek2;
                    world[x - 1, y] = sek2;
                    world[x - 1, y - 1] = sek2;
                    world[x - 1, y + 1] = sek2;
                    world[x - 2, y + 1] = sek2;
                    break;

                case 9:
                    world[x, y] = sek1;
                    world[x, y - 1] = sek2;
                    world[x, y - 2] = sek2;
                    world[x, y + 1] = sek2;
                    world[x, y + 2] = sek2;
                    world[x - 1, y] = sek2;
                    world[x - 1, y - 1] = sek2;
                    world[x - 1, y + 1] = sek2;
                    world[x - 2, y] = sek2;
                    break;

                case 10:
                    world[x, y] = sek1;
                    world[x, y - 1] = sek2;
                    world[x, y + 1] = sek2;
                    world[x - 1, y] = sek2;
                    world[x - 1, y - 1] = sek2;
                    world[x - 1, y + 1] = sek2;
                    world[x - 2, y] = sek2;
                    world[x - 2, y - 1] = sek2;
                    world[x - 2, y + 1] = sek2;
                    world[x - 3, y] = sek2;
                    break;

                case 18:
                    world[x, y] = sek1;
                    world[x, y + 1] = sek1;
                    world[x, y + 2] = sek2;
                    world[x, y + 3] = sek2;
                    world[x, y - 1] = sek2;
                    world[x, y - 2] = sek2;
                    world[x - 1, y] = sek2;
                    world[x - 1, y - 1] = sek2;
                    world[x - 1, y - 2] = sek2;
                    world[x - 1, y + 1] = sek2;
                    world[x - 1, y + 2] = sek2;
                    world[x - 1, y + 3] = sek2;
                    world[x - 2, y] = sek2;
                    world[x - 2, y - 1] = sek2;
                    world[x - 2, y + 1] = sek2;
                    world[x - 2, y + 2] = sek2;
                    world[x - 3, y] = sek2;
                    world[x - 3, y + 1] = sek2;
                    break;
            }

        }

        private void SetSektor(int x, int y, Sektor sek)
        {


        }


        //-------------------------------//

        //Return
        //-------------------------------//

        /// <summary>
        /// Returns specified Sektor from World's Sektor[,]
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns>Sektor from World's Sektor[,]</returns>
        public Sektor Sektor(int x, int y)
        {
            return world[x, y];
        }
        
        /// <summary>
        /// Returns World's Sektor[,]
        /// </summary>
        /// <returns>Sektor[,] sek</returns>
        public Sektor[,] SektorWorld()
        {
            return world;
        }

        public override string ToString()
        {
            return name;
        }


        public Sektor GiveSektor(ConsoleKey con)
        {

            if (con == ConsoleKey.H)
                return hranice;
            else if (con == ConsoleKey.Q)
                return skala;
            else if (con == ConsoleKey.W)
                return zemina;
            else if (con == ConsoleKey.E)
                return les;
            else if (con == ConsoleKey.R)
                return voda;
            else if (con == ConsoleKey.T)
                return louka;
            else if (con == ConsoleKey.Z)
                return stezka;
            else if (con == ConsoleKey.U)
                return most;
            else if (con == ConsoleKey.Y)
                return arcanit;
            else if (con == ConsoleKey.X)
                return karolit;
            else if (con == ConsoleKey.C)
                return ferit;
            else if (con == ConsoleKey.V)
                return kumpur;
            else if (con == ConsoleKey.B)
                return platian;
            else
                return zemina;

        }

        //-------------------------------//

        //Equilibrium world
        //-------------------------------//

        //General void

        public void Equilibrium1()
        {
            StreamReader sr = new StreamReader("Equilibrium.txt");

            string[,] poleString = new string[world.GetLength(0), world.GetLength(1)];

            for (int j = 0; j < world.GetLength(0); j++)
            {
                for (int i = 0; i < world.GetLength(1); i++)
                {
                    string[] line = sr.ReadLine().Split(' ');
                    poleString[j, i] = line[2];
                }
            }
            sr.Close();

            for (int j = 0; j < world.GetLength(0); j++)
            {
                for (int i = 0; i < world.GetLength(1); i++)
                {
                    string line = poleString[j, i];

                    switch (line)
                    {
                        case "Hranice":
                            world[j, i] = hranice;
                            break;

                        case "Skála":
                            world[j, i] = skala;
                            break;

                        case "Stezka":
                            world[j, i] = stezka;
                            break;

                        case "Cesta":
                            world[j, i] = cesta;
                            break;

                        case "Most":
                            world[j, i] = most;
                            break;

                        case "Budova":
                            world[j, i] = budova;
                            break;

                        case "Dveře":
                            world[j, i] = dvere;
                            break;

                        case "Zemina":
                            world[j, i] = zemina;
                            break;

                        case "Les":
                            world[j, i] = les;
                            break;

                        case "Voda":
                            world[j, i] = voda;
                            break;

                        case "Louka":
                            world[j, i] = louka;
                            break;

                        case "Arcanit":
                            world[j, i] = arcanit;
                            break;

                        case "Karolit":
                            world[j, i] = karolit;
                            break;

                        case "Ferit":
                            world[j, i] = ferit;
                            break;

                        case "Kumpur":
                            world[j, i] = kumpur;
                            break;

                        case "Platian":
                            world[j, i] = platian;
                            break;

                        case "Enerix":
                            world[j, i] = enerix;
                            break;

                        case "Inverris":
                            world[j, i] = inverris;
                            break;

                        case "Otirin":
                            world[j, i] = otirin;
                            break;

                        case "Market":
                            world[j, i] = market;
                            break;

                    }
                }
            }
        }

        public void SaveWorld()
        {
            StreamWriter sw = new StreamWriter("Equilibrium.txt");

            for (int j = 0; j < world.GetLength(0); j++)
            {
                for (int i = 0; i < world.GetLength(1); i++)
                {
                    sw.WriteLine("{0} {1} {2}", j, i, world[j, i].ToString());
                }
            }
            sw.Close();
        }

        /// <summary>
        /// Sets Sektor[,] of World to Equilibrium settings. This Sektor[,]'s size must be 300x300.
        /// </summary>
        public void Equilibrium()
        {
            SetPlainWorld();

            //Temporary boundaries of Area 1
            for (int i = 0; i < 201; i++)
            {
                sek = dvere;
                world[i, 201] = sek;
                world[201, i] = sek;
            }
            PlainsA();
            MountainsA();
            WaterA();
            WoodsA();
            RoadsA();
            BuildingsA();

            //////
            //SetBuilding(30, 22, 5);
            world[34, 32] = arcanit;
            world[34, 20] = karolit;
            world[34, 24] = ferit;
            world[34, 30] = kumpur;
            world[34, 34] = platian;
            world[34, 40] = enerix;
            world[34, 44] = otirin;
            world[34, 48] = inverris;

            for (int i = 164; i < 180; i++)
                for (int j = 82; j < 112; j++)
                    world[i, j] = arcanit;
        }

        //Area A == X 1-100 ; Y 1-100
        
        //Mountains
        private void MountainsA()
        {
            sek = skala;

            //1
            for (int i = 1; i < 18; i++)
                for (int j = 1; j < 30; j++)
                    world[i, j] = sek;
            //2
            for (int i = 18; i < 32; i++)
                for (int j = 1; j < 16; j++)
                    world[i, j] = sek;
            //3
            for (int i = 32; i < 46; i++)
                for (int j = 1; j < 14; j++)
                    world[i, j] = sek;
            //4
            for (int i = 46; i < 58; i++)
                for (int j = 1; j < 12; j++)
                    world[i, j] = sek;
            //5
            for (int i = 58; i < 78; i++)
                for (int j = 1; j < 8; j++)
                    world[i, j] = sek;
            //6
            for (int i = 1; i < 12; i++)
                for (int j = 30; j < 50; j++)
                    world[i, j] = sek;
            //7
            for (int i = 1; i < 8; i++)
                for (int j = 50; j < 82; j++)
                    world[i, j] = sek;
            //8
            for (int i = 1; i < 12; i++)
                for (int j = 82; j < 126; j++)
                    world[i, j] = sek;
            //9
            for (int i = 1; i < 50; i++)
                for (int j = 144; j < 184; j++)
                    world[i, j] = sek;
            //10
            for (int i = 1; i < 18; i++)
                for (int j = 126; j < 144; j++)
                    world[i, j] = sek;
            //11
            for (int i = 18; i < 30; i++)
                for (int j = 132; j < 144; j++)
                    world[i, j] = sek;
            //12
            for (int i = 30; i < 36; i++)
                for (int j = 134; j < 144; j++)
                    world[i, j] = sek;
            //13
            for (int i = 36; i < 46; i++)
                for (int j = 138; j < 144; j++)
                    world[i, j] = sek;
            //14
            for (int i = 12; i < 18; i++)
                for (int j = 184; j < 188; j++)
                    world[i, j] = sek;
            //15
            for (int i = 18; i < 50; i++)
                for (int j = 184; j < 192; j++)
                    world[i, j] = sek;
            //16
            world[1, 192] = sek;
            world[1, 194] = sek;
            //17
            for (int i = 1; i < 14; i++)
                world[i, 196] = sek;
            //18
            for (int i = 1; i < 74; i++)
                for (int j = 198; j < 201; j++)
                    world[i, j] = sek;
            //19
            for (int i = 56; i < 62; i++)
                for (int j = 188; j < 198; j++)
                    world[i, j] = sek;
            //20
            for (int i = 62; i < 74; i++)
                for (int j = 184; j < 198; j++)
                    world[i, j] = sek;
            //21
            for (int i = 80; i < 92; i++)
                for (int j = 196; j < 201; j++)
                    world[i, j] = sek;
            //22
            for (int i = 92; i < 100; i++)
                for (int j = 192; j < 201; j++)
                    world[i, j] = sek;
            //23
            for (int i = 100; i < 128; i++)
                for (int j = 188; j < 201; j++)
                    world[i, j] = sek;
            //24
            for (int i = 128; i < 144; i++)
                for (int j = 194; j < 201; j++)
                    world[i, j] = sek;
            //25
            for (int i = 144; i < 156; i++)
                for (int j = 198; j < 201; j++)
                    world[i, j] = sek;
            //26
            for (int i = 162; i < 178; i++)
                for (int j = 198; j < 201; j++)
                    world[i, j] = sek;
            //27
            for (int i = 178; i < 190; i++)
                for (int j = 192; j < 201; j++)
                    world[i, j] = sek;
            //28
            for (int i = 190; i < 201; i++)
                for (int j = 154; j < 201; j++)
                    world[i, j] = sek;
            //29
            for (int i = 194; i < 201; i++)
                for (int j = 118; j < 154; j++)
                    world[i, j] = sek;
            //30
            for (int i = 198; i < 201; i++)
                for (int j = 70; j < 118; j++)
                    world[i, j] = sek;
            //31
            for (int i = 198; i < 201; i++)
                for (int j = 34; j < 48; j++)
                    world[i, j] = sek;
            //32
            for (int j = 16; j < 26; j++)
                world[200, j] = sek;
            //33
            for (int i = 196; i < 201; i++)
                for (int j = 10; j < 16; j++)
                    world[i, j] = sek;
            //34
            for (int i = 188; i < 201; i++)
                for (int j = 1; j < 10; j++)
                    world[i, j] = sek;
            //35
            for (int i = 160; i < 188; i++)
                for (int j = 1; j < 6; j++)
                    world[i, j] = sek;
            //36
            for (int i = 108; i < 160; i++)
                for (int j = 1; j < 16; j++)
                    world[i, j] = sek;
            //37
            for (int i = 114; i < 120; i++)
                for (int j = 16; j < 26; j++)
                    world[i, j] = sek;
            //38
            for (int i = 120; i < 126; i++)
                for (int j = 16; j < 20; j++)
                    world[i, j] = sek;
            //39
            for (int i = 108; i < 114; i++)
                world[i, 16] = sek;
            //40
            for (int i = 104; i < 108; i++)
                for (int j = 1; j < 8; j++)
                    world[i, j] = sek;
        }

        //Plains
        private void PlainsA()
        {
            sek = louka;

            for (int i = 84; i < 101; i++)
                for (int j = 71; j < 101; j++)
                    world[i, j] = sek;
        }

        //Woods
        private void WoodsA()
        {
            sek = les;

            //1
            for (int i = 24; i < 38; i++)
                for (int j = 110; j < 126; j++)
                    world[i, j] = sek;
            //2
            for (int i = 38; i < 44; i++)
                for (int j = 94; j < 126; j++)
                    world[i, j] = sek;
            //3
            for (int i = 44; i < 54; i++)
                for (int j = 54; j < 84; j++)
                    world[i, j] = sek;
            //4
            for (int i = 54; i < 90; i++)
                for (int j = 24; j < 36; j++)
                    world[i, j] = sek;
            //5
            for (int i = 54; i < 148; i++)
                for (int j = 36; j < 84; j++)
                    world[i, j] = sek;
            //6
            for (int i = 44; i < 76; i++)
                for (int j = 84; j < 126; j++)
                    world[i, j] = sek;
            //7
            for (int i = 56; i < 76; i++)
                for (int j = 126; j < 148; j++)
                    world[i, j] = sek;
            //8
            for (int i = 56; i < 114; i++)
                for (int j = 148; j < 178; j++)
                    world[i, j] = sek;
            //9
            for (int i = 76; i < 80; i++)
                for (int j = 108; j < 148; j++)
                    world[i, j] = sek;
            //10
            for (int i = 80; i < 90; i++)
                for (int j = 128; j < 148; j++)
                    world[i, j] = sek;
            //11
            for (int i = 90; i < 124; i++)
                for (int j = 118; j < 148; j++)
                    world[i, j] = sek;
            //12
            for (int i = 114; i < 130; i++)
                for (int j = 148; j < 164; j++)
                    world[i, j] = sek;
            //13
            for (int i = 110; i < 124; i++)
                for (int j = 98; j < 118; j++)
                    world[i, j] = sek;
            //14
            for (int i = 124; i < 158; i++)
                for (int j = 114; j < 148; j++)
                    world[i, j] = sek;
            //15
            for (int i = 158; i < 182; i++)
                for (int j = 114; j < 134; j++)
                    world[i, j] = sek;
            //16
            for (int i = 124; i < 162; i++)
                for (int j = 84; j < 114; j++)
                    world[i, j] = sek;
            //18
            for (int i = 148; i < 162; i++)
                for (int j = 52; j < 84; j++)
                    world[i, j] = sek;
            //19
            for (int i = 162; i < 178; i++)
                for (int j = 52; j < 80; j++)
                    world[i, j] = sek;
            //20
            for (int i = 178; i < 192; i++)
                for (int j = 70; j < 80; j++)
                    world[i, j] = sek;
            //21
            for (int i = 76; i < 80; i++)
                for (int j = 84; j < 94; j++)
                    world[i, j] = sek;
            //22
            for (int i = 80; i < 86; i++)
                world[i, 84] = sek;
            //23
            for (int i = 100; i < 110; i++)
                for (int j = 114; j < 118; j++)
                    world[i, j] = sek;
            //24
            for (int i = 106; i < 110; i++)
                for (int j = 108; j < 114; j++)
                    world[i, j] = sek;
            //25
            for (int i = 100; i < 124; i++)
                world[i, 84] = sek;
        }

        //Water
        private void WaterA()
        {
            sek = voda;

            //1
            for (int i = 49; i < 53; i++)
                world[i, 46] = sek;
            //2
            for (int i = 52; i < 55; i++)
                world[i, 45] = sek;
            //3
            for (int i = 54; i < 58; i++)
                world[i, 44] = sek;
            //4
            for (int j = 38; j < 45; j++)
                world[58, j] = sek;
            //5
            world[59, 38] = sek;
            world[60, 38] = sek;
            //6
            for (int j = 32; j < 38; j++)
                world[60, j] = sek;
            //7
            for (int i = 60; i < 62; i++)
                for (int j = 24; j < 32; j++)
                    world[i, j] = sek;
            //8
            for (int i = 62; i < 65; i++)
                for (int j = 24; j < 26; j++)
                    world[i, j] = sek;
            //9
            for (int i = 63; i < 65; i++)
                for (int j = 15; j < 24; j++)
                    world[i, j] = sek;
            //10
            for (int i = 63; i < 66; i++)
                for (int j = 13; j < 15; j++)
                    world[i, j] = sek;
            //11
            for (int i = 66; i < 77; i++)
                for (int j = 11; j < 15; j++)
                    world[i, j] = sek;
            //12
            for (int i = 77; i < 99; i++)
                for (int j = 11; j < 23; j++)
                    world[i, j] = sek;
            //13
            for (int i = 83; i < 92; i++)
                for (int j = 6; j < 11; j++)
                    world[i, j] = sek;
            //14
            for (int i = 92; i < 98; i++)
                for (int j = 23; j < 32; j++)
                    world[i, j] = sek;
            //15
            world[98, 23] = sek;
            //16
            for (int i = 99; i < 101; i++)
                for (int j = 13; j < 17; j++)
                    world[i, j] = sek;
            //17
            world[99, 11] = sek;
            world[99, 12] = sek;
        }

        //Roads
        private void RoadsA()
        {
            sek = stezka;

            //1
            //2
            //3
            for (int i = 57; i < 65; i++)
                world[10, i] = sek;
            //4
            for (int i = 11; i < 19; i++)
                world[i, 64] = sek;
            //5
            for (int i = 18; i < 21; i++)
                world[i, 65] = sek;
            //6
            for (int i = 20; i < 23; i++)
                world[i, 66] = sek;
            //7
            for (int i = 22; i < 25; i++)
                world[i, 67] = sek;
            //8
            for (int i = 24; i < 27; i++)
                world[i, 68] = sek;
            //9
            for (int i = 69; i < 91; i++)
                world[26, i] = sek;
            //10
            for (int i = 27; i < 59; i++)
                world[i, 90] = sek;
            //11
            for (int i = 91; i < 101; i++)
                world[38, i] = sek;
            //12
            for (int i = 39; i < 42; i++)
                world[i, 96] = sek;
            //13
            for (int i = 94; i < 96; i++)
                world[41, i] = sek;
            //14
            for (int i = 42; i < 45; i++)
                world[i, 94] = sek;
            //15
            for (int i = 92; i < 94; i++)
                world[44, i] = sek;
            //16
            for (int i = 45; i < 47; i++)
                world[i, 92] = sek;
            //17
            for (int i = 91; i < 93; i++)
                world[47, i] = sek;
            //18
            for (int i = 83; i < 90; i++)
                world[58, i] = sek;
            //19
            for (int i = 59; i < 66; i++)
                world[i, 83] = sek;
            //20
            for (int i = 91; i < 97; i++)
                world[74, i] = sek;
            //21
            for (int i = 75; i < 80; i++)
                world[i, 96] = sek;
            //22
            for (int i = 97; i < 101; i++)
                world[79, i] = sek;
            //23
            for (int i = 68; i < 75; i++)
                world[80, i] = sek;
            //24
            for (int i = 81; i < 93; i++)
                world[i, 68] = sek;
            //25
            for (int i = 53; i < 68; i++)
                world[92, i] = sek;
            //26
            for (int i = 93; i < 96; i++)
                world[i, 53] = sek;
            //27
            for (int i = 43; i < 53; i++)
                world[95, i] = sek;
            //28
            for (int i = 96; i < 98; i++)
                world[i, 43] = sek;
            //29
            for (int i = 34; i < 43; i++)
                world[97, i] = sek;
            //30
            for (int i = 90; i < 100; i++)
                world[i, 33] = sek;
            //31
            for (int i = 26; i < 33; i++)
                world[99, i] = sek;
            //32
            for (int i = 99; i < 101; i++)
                world[i, 25] = sek;
            //33
            for (int i = 24; i < 33; i++)
                world[90, i] = sek;
            //34
            for (int i = 75; i < 90; i++)
                world[i, 24] = sek;
            //35
            for (int i = 16; i < 24; i++)
                world[75, i] = sek;
            //36
            for (int i = 66; i < 75; i++)
                world[i, 16] = sek;
            for (int i = 62; i < 66; i++)
                world[i, 16] = most;
            //37
            for (int i = 11; i < 17; i++)
                world[61, i] = sek;
            //38
            for (int i = 62; i < 65; i++)
                world[i, 11] = sek;
            //39
            for (int i = 9; i < 11; i++)
                world[64, i] = sek;
            //40
            for (int i = 65; i < 70; i++)
                world[i, 9] = sek;
            //41
            for (int i = 47; i < 61; i++)
                world[i, 14] = sek;
            //42
            for (int i = 9; i < 14; i++)
                world[47, i] = sek;
            //43
            for (int i = 41; i < 47; i++)
                world[i, 9] = sek;
        }

        //Buildings
        private void BuildingsA()
        {
            SetBuilding(6, 30, 2);
        }

        

        //-------------------------------//
    }
}
