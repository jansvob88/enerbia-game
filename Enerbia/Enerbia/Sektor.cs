using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enerbia
{
    class Sektor
    {
        //Definitions
        //-------------------------------//
        
        /// <summary>
        /// [0] id [1] itemId [2] patt1 [3] patt2 [4] col1 [5] col2 [6] col3
        /// </summary>
        protected int[] integers = new int[7];

        /// <summary>
        /// [0] stepAble [1] enterAble [2] createItem [3] multikolor
        /// </summary>
        protected bool[] bools = new bool[4];

        /// <summary>
        /// [0] name [1] specialChar [2] createAble
        /// </summary>
        protected string[] strings = new string[3];
        
        private Random random = new Random();

        //-------------------------------//

        //Constructors
        //-------------------------------//

        public Sektor() { }

        public Sektor(string name, string specialChar,
                      int id, int itemId, int patt1, int patt2, int col1, int col2, int col3,
                      bool stepAble)
        {
            integers[0] = id;
            integers[1] = itemId;
            integers[2] = patt1;
            integers[3] = patt2;
            integers[4] = col1;
            integers[5] = col2;
            integers[6] = col3;

            bools[0] = stepAble; //stepAble
            bools[1] = false;    //enterAble
            if (itemId > 0)
                bools[2] = true;    //createItem
            else bools[2] = false;

            if (col1 == col2 && col2 == col3)
                bools[3] = false;   //multikolor
            else bools[3] = true;

            strings[0] = name;
            strings[1] = specialChar;
            
            if (itemId > 0)
                strings[2] = "createAble";
            else strings[2] = " ";

        }

        //-------------------------------//

        //Main methods
        //-------------------------------//
        //-------------------------------//

        //Return
        //-------------------------------//
            
        public ConsoleColor Col()
        {
            if (bools[3])
            {
                int i = random.Next(100);


                if (i < integers[2])
                    return (ConsoleColor)integers[4];

                else if (i > integers[2] - 1 && i < integers[3])
                    return (ConsoleColor)integers[5];

                else
                    return (ConsoleColor)integers[6];
            }
            else
                return (ConsoleColor)integers[4];
        }
        public ConsoleColor Col1() { return (ConsoleColor)integers[4]; }

        public override string ToString() { return strings[0]; }
        public string SpecialChar() { return strings[1]; }
        public string Interaction() { return strings[2]; }

        public int Id() { return integers[0]; }
        public int ItemId() { return integers[1]; }

        public bool StepAble() { return bools[0]; }
        public bool EnterAble() { return bools[1]; }
        public bool CreateAble() { return bools[2]; }
        public bool Multikolor() { return bools[3]; }

        //-------------------------------//
    }
}
