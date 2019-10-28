using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WizardUnit : Unit
{ 
    public bool IsDead { get; set; }

    public int XPos
    {
        get { return xPos; }
        set { xPos = value; }
    }
    public int YPos
    {
        get { return yPos; }
        set { yPos = value; }
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
    }

    public int Attack
    {
        get { return attack; }
        set { attack = value; }
    }
    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public int Faction
    {
        get { return faction; }
    }

    public string Symbol
    {
        get { return symbol; }
        set { symbol = value; }
    }

    public bool IsAttacking
    {
        get { return isAttacking; }
        set { isAttacking = value; }
    }

    public string Name
    {
        get { return name; }
    }

    public WizardUnit(int x, int y, string n, int h, int s, int a, int f, string sy)
    {
        XPos = x;
        YPos = y;
        name = n;
        Health = h;
        maxHealth = h;
        Speed = s;
        Attack = a;
        faction = f;
        Symbol = sy;
        IsAttacking = false;
        IsDead = false;
    }

    public override void Death()
    {
        symbol = "X";
        IsDead = true;
    }

    public override void Move(int dir)
    {
        if (IsDead == false)
        {
            switch (dir)
            {
                case 0:
                    if (YPos > 0)
                        YPos--;
                    break;
                case 1:
                    if (XPos < 19)
                        XPos++;
                    break;
                case 2:
                    if (YPos < 19)
                        YPos++;
                    break;
                case 3:
                    if (XPos > 0)
                        XPos--;
                    break;
            }
        }
    }

    public override void Combat(Unit attacker)
    {
        attacker.Damage(Attack, InRange(attacker));
        Damage(Attack, InRange(attacker), attacker);
    }

    public override bool InRange(Unit other)
    {
        int distanceX, distanceY;
        int otherX = other.GetX();
        int otherY = other.GetY();

        distanceX = Math.Abs(XPos - otherX);
        distanceY = Math.Abs(YPos - otherY);
        if (distanceX <= 1 && distanceY > Attack)
        {
            return true;
        }
        else if (distanceX > 1 && distanceY <= Attack)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override (Unit, int) Closest(List<Unit> units)
    {
        int shortest = 100;
        Unit closest = this;
        foreach (Unit u in units)
        {
            if (u.FactionCheck() != Faction)
            {
                int distance = Math.Abs(XPos - u.GetX()) + Math.Abs(YPos - u.GetY());
                if (distance < shortest)
                {
                    shortest = distance;
                    closest = u;
                }
            }
        }
        return (closest, shortest);
    }

    public override string ToString()
    {
        string temp = "";
        temp += "Wizard: ";
        temp += Name;
        temp += "{" + Symbol + "}";
        temp += "(" + XPos + "," + YPos + ") ";
        temp += Health + ", " + Attack + ", " + Speed;
        temp += (IsDead ? " DEAD!" : " ALIVE!");
        return temp;
    }

    public override void Damage(int hit, bool inRange)
    {
        if (inRange)
        {
            isAttacking = true;
            SetDamage(hit);
        }
    }

    public void Damage(int hit, bool inRange, Unit attacker)
    {
        if (inRange)
        {
            isAttacking = true;
            attacker.SetDamage(hit);
        }
    }

    public override bool AliveNt()
    {
        return IsDead;
    }
    public override int FactionCheck()
    {
        return Faction;
    }
    public override void SetDamage(int dam)
    {
        Health -= dam;
        if (Health <= 0)
            Death();
    }
    public override int GetY()
    {
        return YPos;
    }
    public override int GetX()
    {
        return XPos;
    }
    public override void Raze(Building build)
    {
        throw new NotImplementedException();
    }
    public override (Building, int) Raid(List<Building> builds, int num)
    {
        throw new NotImplementedException();
    }
    public override int GetHealth()
    {
        return Health;
    }
    public override int GetMaxHealth()
    {
        return MaxHealth;
    }
    public override int GetRange()
    {
        throw new NotImplementedException();
    }
}
