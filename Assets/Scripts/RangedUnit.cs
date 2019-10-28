using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RangedUnit : Unit
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

        public int AttackRange
        {
            get { return attackRange; }
            set { attackRange = value; }
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

        public RangedUnit(int x, int y, string n, int h, int s, int a, int ar, int f, string sy)
        {
            XPos = x;
            YPos = y;
            name = n;
            Health = h;
            maxHealth = h;
            Speed = s;
            Attack = a;
            AttackRange = ar;
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
        }

        public override bool InRange(Unit other)
        {
            int distance;
            int otherX = 0;
            int otherY = 0;
            if (other is MeleeUnit nmeM)
            {
                otherX = nmeM.XPos;
                otherY = nmeM.YPos;
            }
            else if (other is RangedUnit nmeR)
            {
                otherX = nmeR.XPos;
                otherY = nmeR.YPos;
            }

            distance = Math.Abs(XPos - otherX) + Math.Abs(YPos - otherY);
            if (distance <= AttackRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InRange(Building builds)
        {
            int distance;
            int otherX = 0;
            int otherY = 0;
            if (builds is FactoryBuilding nmeF)
            {
                otherX = nmeF.PosX;
                otherY = nmeF.PosY;
            }
            else if (builds is ResourceBuilding nmeR)
            {
                otherX = nmeR.PosX;
                otherY = nmeR.PosY;
            }

            distance = Math.Abs(XPos - otherX) + Math.Abs(YPos - otherY);
            if (distance <= AttackRange)
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
                if (u is MeleeUnit && u != this)
                {
                    MeleeUnit otherMu = (MeleeUnit)u;
                    int distance = Math.Abs(XPos - otherMu.XPos)
                               + Math.Abs(YPos - otherMu.YPos);
                    if (distance < shortest)
                    {
                        shortest = distance;
                        closest = otherMu;
                    }
                }
                else if (u is RangedUnit && u != this)
                {
                    RangedUnit otherRu = (RangedUnit)u;
                    int distance = Math.Abs(XPos - otherRu.XPos)
                               + Math.Abs(YPos - otherRu.YPos);
                    if (distance < shortest)
                    {
                        shortest = distance;
                        closest = otherRu;
                    }
                }
            }
            return (closest, shortest);
        }

        public (Building, int) Raid(List<Building> builds, int num)
        {
            Random rand = new Random();
            int shortest = 100;
            Building target = builds[rand.Next(0, num)];
            foreach (Building b in builds)
            {
                if (b is ResourceBuilding resource)
                {
                    int distance = Math.Abs(XPos - resource.PosX) + Math.Abs(YPos - resource.PosY);
                    if (distance < shortest)
                    {
                        shortest = distance;
                        target = resource;
                    }
                }
                else if (b is FactoryBuilding factory)
                {
                    int distance = Math.Abs(XPos - factory.PosX) + Math.Abs(YPos - factory.PosY);
                    if (distance < shortest)
                    {
                        shortest = distance;
                        target = factory;
                    }
                }
            }
            return (target, shortest);
        }

        public void Raze(Building build)
        {
            build.Damage(Attack, InRange(build));
        }

        public override string ToString()
        {
            string temp = "";
            temp += "Ranged: ";
            temp += Name;
            temp += "{" + Symbol + "}";
            temp += "(" + XPos + "," + YPos + ") ";
            temp += Health + ", " + Attack + ", " + AttackRange + ", " + Speed;
            temp += (IsDead ? " DEAD!" : " ALIVE!");
            return temp;
        }

        public override bool AliveNt()
        {
            return IsDead;
        }
        public override int FactionCheck()
        {
            return Faction;
        }
        public override void Damage(int hit, bool inRange)
        {
            if (inRange)
            {
                isAttacking = true;
                SetDamage(hit);
            }
        }
        public override void SetDamage(int dam)
        {
            Health -= dam;
            if (Health <= 0)
                Death();
        }
    }
