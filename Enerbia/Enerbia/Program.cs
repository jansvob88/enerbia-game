using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Enerbia
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Engine engine = new Engine();
            engine.Start();
            

            //WindowScreen ws = new WindowScreen();

            //ws.SetWindow();
            //ws.DrawSectionFrame();
            //Console.SetCursorPosition(3, 3);
            //Console.Write("{0} {1}", ws.SectionLength(WindowSection.A, WindowSectionWidthHeight.Width),
            //                         ws.SectionLength(WindowSection.A, WindowSectionWidthHeight.Height));
            //Console.SetCursorPosition(3, 6);
            //Console.Write("{0} {1}", decimal.Floor(10 / 2) * 2, decimal.Floor(10 / 2) * 2);
            //Console.SetCursorPosition(3, 7);
            //Console.Write("{0} {1}", Math.Floor((decimal)11 / 2) * 2, (Math.Floor((double)11 / 2)) * 2);
            //Console.ReadKey();

        }

        
    }
}
