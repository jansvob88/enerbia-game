using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enerbia
{
    class Controls
    {
        //Definitions
        //-------------------------------//

        private ConsoleKey[] controlsMenu = new ConsoleKey[20];

        private ConsoleKey moveUp, moveDown, moveLeft, moveRight;

        private ConsoleKey mainAction, secondaryAction, tercialAction;

        private ConsoleKey toggleInventory, toggleQuestLog;

        //-------------------------------//

        //Constructors
        //-------------------------------//

        public Controls()
        {
            moveUp = ConsoleKey.UpArrow;
            moveDown = ConsoleKey.DownArrow;
            moveLeft = ConsoleKey.LeftArrow;
            moveRight = ConsoleKey.RightArrow;

            mainAction = ConsoleKey.A;
            secondaryAction = ConsoleKey.S;
            tercialAction = ConsoleKey.D;

            toggleInventory = ConsoleKey.I;
            toggleQuestLog = ConsoleKey.L;


            controlsMenu[0] = moveUp;
            controlsMenu[1] = moveDown;
            controlsMenu[2] = moveLeft;
            controlsMenu[3] = moveRight;

            controlsMenu[4] = mainAction;
            controlsMenu[5] = secondaryAction;
            controlsMenu[6] = tercialAction;

            controlsMenu[7] = toggleInventory;
            controlsMenu[8] = toggleQuestLog;
        }

        //-------------------------------//

        //Main methods
        //-------------------------------//

        /// <summary>
        /// Main switch of Controls. 
        /// Chooses and calls back an action depending on pressed ConsoleKey.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="gui"></param>
        /// <param name="ck"></param>
        /// <param name="world"></param>
        public void Action(Player p, GUI gui, ConsoleKey ck, World world)
        {
            int temp = -1;

            for (int i = 0; i < controlsMenu.Length; i++)
                if (controlsMenu[i] == ck)
                    temp = i;

            switch (temp)
            {
                case 0:
                    MoveUp(p, gui);
                    break;
                case 1:
                    MoveDown(p, gui);
                    break;
                case 2:
                    MoveLeft(p, gui);
                    break;
                case 3:
                    MoveRight(p, gui);
                    break;
                case 4:
                    MainAction(p, world);
                    break;
                case 5:
                    SecondaryAction(p, world);
                    break;
                case 6:
                    TercialAction(p, world);
                    break;
                case 7:
                    ToggleInventory(gui);
                    break;
                case 8:
                    ToggleQuestLog(gui);
                    break;
            }
        }

        private void MoveUp(Player p, GUI gui)
        {
            if (p.World().Sektor(p.Y() - 1, p.X()).StepAble())
                p.ChangeY(p.Y() - 1);
            else
            {
                gui.DisplayMessage(gui.cantStepHere);
            }
        }
        private void MoveDown(Player p, GUI gui)
        {
            if (p.World().Sektor(p.Y() + 1, p.X()).StepAble())
                p.ChangeY(p.Y() + 1);
            else
            {
                gui.DisplayMessage(gui.cantStepHere);
            }
        }
        private void MoveLeft(Player p, GUI gui)
        {
            if (p.World().Sektor(p.Y(), p.X() - 1).StepAble())
                p.ChangeX(p.X() - 1);
            else
            {
                gui.DisplayMessage(gui.cantStepHere);
            }
        }
        private void MoveRight(Player p, GUI gui)
        {
            if (p.World().Sektor(p.Y(), p.X() + 1).StepAble())
                p.ChangeX(p.X() + 1);
            else
            {
                gui.DisplayMessage(gui.cantStepHere);
            }
        }

        private void MainAction(Player p, World world)
        {
            PlaceSektor(p, world);
           // p.ChangeActionStatus(p.Sektor().Interaction());
           // p.CallAction();
        }

        private void SecondaryAction(Player p, World world)
        {
            PlaceSektorSquare(p, world);
        }

        private void TercialAction(Player p, World world)
        {
            PlaceSektorCircle(p, world);
        }

        private void ToggleInventory(GUI gui) { gui.ToggleInventory(); }
        private void ToggleQuestLog(GUI gui) { gui.ToggleQuestLog(); }

        //Edit controls
        private void EditMoveUp(ConsoleKey ck) { moveUp = ck; }
        private void EditMoveDown(ConsoleKey ck) { moveDown = ck; }
        private void EditMoveLeft(ConsoleKey ck) { moveLeft = ck; }
        private void EditMoveRight(ConsoleKey ck) { moveRight = ck; }

        private void EditMainAction(ConsoleKey ck) { mainAction = ck; }
        private void EditSecondaryAction(ConsoleKey ck) { secondaryAction = ck; }
        private void EditTercialAction(ConsoleKey ck) { tercialAction = ck; }

        private void EditToggleInventory(ConsoleKey ck) { toggleInventory = ck; }
        private void EditToggleQuestLog(ConsoleKey ck) { toggleQuestLog = ck; }

        //-------------------------------//

        //Creative mode methods
        //-------------------------------//

        private void PlaceSektor(Player p, World world)
        {
            ConsoleKey con;
            con = Console.ReadKey().Key;

            world.SektorWorld()[p.Y(), p.X()] = world.GiveSektor(con);
        }

        private void PlaceSektorSquare(Player p, World world)
        {
            if (status == 0)
            {
                x1 = p.X();
                y1 = p.Y();
            }
            if (status == 1)
            {
                x2 = p.X();
                y2 = p.Y();
                
                ConsoleKey con;
                con = Console.ReadKey().Key;
                int tempo;

                if (x2 < x1) { tempo = x2; x2 = x1; x1 = tempo; }
                if (y2 < y1) { tempo = y2; y2 = y1; y1 = tempo; }

                for (int j = y1; j < y2 + 1; j++)
                    for (int i = x1; i < x2 + 1; i++)
                    {
                        world.SektorWorld()[j,i] = world.GiveSektor(con);
                    }
            }
            status += 1;
            if (status == 2) status = 0;
        }

        private void PlaceSektorCircle(Player p, World world)
        {
            if (status == 0)
            {
                x1 = p.X();
                y1 = p.Y();
            }
            if (status == 1)
            {
                x2 = p.X();
                y2 = p.Y();

                ConsoleKey con;
                con = Console.ReadKey().Key;
                
                int radius = 0;

                if (Math.Abs(x2 - x1) < Math.Abs(y2 - y1))
                    radius = Math.Abs(y2 - y1);
                else
                    radius = Math.Abs(x2 - x1);

                int a = 0 - radius;
                int b = 0 - radius;

                for (int j = y1 - radius; j < y1 + radius; j++)
                {

                    for (int i = x1 - radius; i < x1 + radius; i++)
                    {

                        if (Math.Sqrt(Math.Abs(a) * Math.Abs(a) + Math.Abs(b) * Math.Abs(b)) < (radius - 0.75))
                        world.SektorWorld()[j, i] = world.GiveSektor(con);

                        b++;
                    }
                    b = 0 - radius;
                    a++;
                }
            }
            status += 1;
            if (status == 2) status = 0;
        }

        //Map creation variables
        public int x1, x2, y1, y2;
        public int status = 0;

        //-------------------------------//

        //Returns
        //-------------------------------//

        private ConsoleKey ReturnMoveUp() { return moveUp; }
        private ConsoleKey ReturnMoveDown() { return moveDown; }
        private ConsoleKey ReturnMoveLeft() { return moveLeft; }
        private ConsoleKey ReturnMoveRight() { return moveRight; }

        private ConsoleKey ReturnMainAction() { return mainAction; }
        private ConsoleKey ReturnSecondaryAction() { return secondaryAction; }
        private ConsoleKey ReturnTercialAction() { return tercialAction; }

        private ConsoleKey ReturnToggleInventory() { return toggleInventory; }
        private ConsoleKey ReturnToggleQuestLog() { return toggleQuestLog; }

        //Yet no references
        private ConsoleKey SwitchControlsMenu(int i)
        {
            ConsoleKey temp = ConsoleKey.Backspace;

            switch (i)
            {
                case 0:
                    temp = ReturnMoveUp();
                    break;
                case 1:
                    temp = ReturnMoveDown();
                    break;
                case 2:
                    temp = ReturnMoveLeft();
                    break;
                case 3:
                    temp = ReturnMoveRight();
                    break;
                case 4:
                    temp = ReturnMainAction();
                    break;
                case 5:
                    temp = ReturnSecondaryAction();
                    break;
                case 6:
                    temp = ReturnTercialAction();
                    break;
                case 7:
                    temp = ReturnToggleInventory();
                    break;
                case 8:
                    temp = ReturnToggleQuestLog();
                    break;
            }

            return temp;
        }

        //-------------------------------//
    }
}
