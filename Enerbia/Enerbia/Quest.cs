using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enerbia
{
    class Quest
    {
        private int id;
        private string questGiver;
        private string name;
        private string description;
        private bool accomplished;
        private Item reward;

        public Quest(int id, string name, string questGiver, string description, Item reward)
        {
            this.id = id;
            this.name = name;
            this.questGiver = questGiver;
            this.description = description;
            this.reward = reward;
            accomplished = false;
        }

        public override string ToString()
        {
            return name;
        }
        public int Id()
        {
            return id;
        }
    }
}
