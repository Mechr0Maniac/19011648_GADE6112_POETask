using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6112_Task_3___19011648
{
    [Serializable()]
    public class FactoryBuilding : Building
    {
        private int tempSpot;
        private int unitType;
        private int productSpeed;
        private int spawnPoint;
        public bool IsDead { get; set; }
        public int PosX
        {
            get { return bPosX; }
            set { bPosX = value; }
        }
        public int PosY
        {
            get { return bPosY; }
            set { bPosY = value; }
        }
        public int Health
        {
            get { return bHealth; }
            set { bHealth = value; }
        }
        public int MaxHealth
        {
            get { return bHealthMax; }
        }
        public int Faction
        {
            get { return bFaction; }
        }
        public string Symbol
        {
            get { return bSym; }
        }
        public int ProductSpeed
        {
            get { return productSpeed; }
            set { productSpeed = value; }
        }

        public FactoryBuilding(int x, int y, int ut, int h, int ps, int f, string sy)
        {
            PosX = x;
            PosY = y;
            unitType = ut;
            ProductSpeed = ps;
            Health = h;
            bHealthMax = h;
            bFaction = f;
            bSym = sy;
            if (PosY <= 18)
                spawnPoint = PosY + 1;
            else if (PosY > 18)
                spawnPoint = PosY - 1;
        }


        public override void DieDie()
        {
            bSym = "XX";
            IsDead = true;
        }
        public override string ToString()
        {
            string temp = "";
            temp += "Unit: " + unitType;
            temp += "\nEfficiency: " + ProductSpeed + " turns";
            temp += "\n{" + Symbol + "}";
            temp += "\n(" + PosX + "," + PosY + "), ";
            temp += "\nHeatlth: " + Health + "\n";
            temp += IsDead ? "DESTROYED!" : "FUNCTIONING!";
            return temp;
        }
        public Unit Spawn(FactoryBuilding spawner, List<Building> builds)
        {
            ResourceBuilding res = (ResourceBuilding)builds[tempSpot];
            if (spawner.UnitType() == 0)
            {
                res.Pool -= 4;
                RangedUnit r = new RangedUnit(spawner.PosX, spawner.spawnPoint, "archer", 100, 1, 20, 5, spawner.Faction, "R");
                return r;
            }
            else
            {
                res.Pool -= 5;
                MeleeUnit m = new MeleeUnit(spawner.PosX, spawner.spawnPoint, "soldier", 100, 1, 20, spawner.Faction, "M");
                return m;
            }
        }

        public bool WillSpawn(List<Building> builds)
        {
            int loop = 0;
            bool temp = false;
            int numLoops = builds.Count();
            foreach (Building b in builds)
            {
                while (temp == false && loop < numLoops)
                {
                    if (b is ResourceBuilding res)
                    {
                        if (res.Pool >= 4)
                        {
                            tempSpot = loop;
                            temp = true;
                        }
                    }
                    loop++;
                }
            }
            return temp;
        }
        public override bool IsDie()
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
                Health -= hit;
                if (Health <= 0)
                    DieDie();
            }
        }
        public int UnitType()
        {
            return unitType;
        }
    }
}
