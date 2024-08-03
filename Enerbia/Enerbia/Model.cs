using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enerbia
{
    class Model
    {
        //Definitions
        //-------------------------------//

        private string[,] strings = { { "Enerix", "Inverris", "Otirin" }, { "▲", "♦", "▼" } };

        private float[] power = new float[3] { 0, 0, 0 };

        private int[,] integers = { { 0, 1, 2},
                                    { 10, 10, 10},
                                    { (int)ConsoleColor.Cyan, (int)ConsoleColor.DarkMagenta, (int)ConsoleColor.Red},
                                    { (int)ConsoleColor.DarkCyan, (int)ConsoleColor.Black, (int)ConsoleColor.DarkRed} };


        //-------------------------------//

        //Constructors
        //-------------------------------//

        public Model() { }

        //-------------------------------//

        //Main methods
        //-------------------------------//

        public void ChangeMaxPower(int index, int maxPower) { integers[1, index] = maxPower; }

        public void PowerUp(int index, float power)
        {
            this.power[index] += power;

            switch (index)
            {
                case 0://modrá
                    this.power[2] -= (power / 5) * 3; //červená
                    this.power[1] += power / 4; //fialová
                    break;

                case 1://fialová
                    this.power[0] -= (power / 5) * 3; //modrá
                    this.power[2] += power / 4; //červená
                    break;

                case 2://červená
                    this.power[1] -= (power / 5) * 3; //fialová
                    this.power[0] += power / 4; //modrá
                    break;
            }
        }

        //-------------------------------//

        //Return
        //-------------------------------//

        public float Power(int i) { return power[i]; }

        public string ToString(int index) { return strings[0, index]; }
        public string SpecialChar(int index) { return strings[1, index]; }

        public float Enerix() { return power[0]; }
        public float Inverris() { return power[1]; }
        public float Otirin() { return power[2]; }

        public int MaxEnerix() { return integers[1, 0]; }
        public int MaxInverris() { return integers[1, 1]; }
        public int MaxOtirin() { return integers[1, 2]; }

        public int ReturnColor1(int index) { return integers[2, index]; }
        public int ReturnColor2(int index) { return integers[3, index]; }
        public int ReturnMax(int index) { return integers[1, index]; }

        public int MainPower()
        {
            float temp = power[0];
            int k = 0;
            for (int i = 0; i < power.Length; i++)
            {
                if (power[i] > temp)
                {
                    temp = power[i];
                    k = i;
                }

            }
            return k; 
        }
        //-------------------------------//
    }
}
