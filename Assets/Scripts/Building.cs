using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6112_Task_3___19011648
{
    [Serializable()]
    public abstract class Building
    {
        protected int bPosX;
        protected int bPosY;
        protected int bHealth;
        protected int bHealthMax;
        protected int bFaction;
        protected string bSym;

        public abstract void DieDie();
        public abstract override string ToString();
        public abstract bool IsDie();
        public abstract int FactionCheck();
        public abstract void Damage(int hit, bool inRange);
    }
}
