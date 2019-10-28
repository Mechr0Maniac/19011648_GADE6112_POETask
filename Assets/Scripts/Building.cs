using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
    public abstract int GetY();
    public abstract int GetX();
}
