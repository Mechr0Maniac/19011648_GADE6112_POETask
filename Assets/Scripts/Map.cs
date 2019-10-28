using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using UnityEngine;

namespace GADE6112_Task_3___19011648
{
    public class Map
    {
        List<Unit> units;
        List<Building> builds;
        int numUnits = 0,  numBuilds = 0;
        int mapW = 0, mapH = 0;
        //TextBox txtInfo;
        int numR = 0, numM = 0;

        public List<Unit> Units
        {
            get { return units; }
            set { units = value; }
        }

        public List<Building> Builds
        {
            get { return builds; }
            set { builds = value; }
        }

        public Map(int nu, int nb, int mw, int mh)
        {
            units = new List<Unit>();
            builds = new List<Building>();
            numUnits = nu;
            numBuilds = nb;
            //txtInfo = txt;
            mapW = mw;
            mapH = mh;
        }

        public void Generate()
        {
            for (int i = 0; i < numUnits; i++)
            {
                int r = Random.Range(0, 2);
                if (r == 0)
                {
                    MeleeUnit m = new MeleeUnit(Random.Range(0, mapW), Random.Range(0, mapH), "Soldier", 100, 3, 20, i % 2 == 0 ? 1 : 0, "M");
                    units.Add(m);
                    numM++;
                }
                else
                {
                    RangedUnit ru = new RangedUnit(Random.Range(0, mapW), Random.Range(0, mapH), "Archer", 100, 2, 20, 5, i % 2 == 0 ? 1 : 0, "R");
                    units.Add(ru);
                    numR++;
                }
            }
            for (int j = 0; j < numBuilds; j++)
            {
                int r = Random.Range(0, 2);
                if (r == 0)
                {
                    int f = Random.Range(0, 2);
                    if (f == 0)
                    {
                        FactoryBuilding fb = new FactoryBuilding(Random.Range(0, mapW), Random.Range(0, mapH), 0, 200, 5, j % 2 == 0 ? 1 : 0, "FB");
                        builds.Add(fb);
                    }
                    else
                    {
                        FactoryBuilding fb = new FactoryBuilding(Random.Range(0, mapW), Random.Range(0, mapH), 1, 200, 5, j % 2 == 0 ? 1 : 0, "FB");
                        builds.Add(fb);
                    }
                }
                else
                {
                    ResourceBuilding rb = new ResourceBuilding(Random.Range(0, mapW), Random.Range(0, mapH), "Swords", 200, 2, 60, j % 2 == 0 ? 1 : 0);
                    builds.Add(rb);
                }
            }
            if (numR < numM)
            {
                for (int k = 0; k < Random.Range(numR, numM); k++)
                {
                    WizardUnit wu = new WizardUnit(Random.Range(0, mapW), Random.Range(0, mapH), "Wizro", 70, 1, 30, 3, "W");
                    units.Add(wu);
                }
            }
            else
            {
                for (int k = 0; k < Random.Range(numM, numR); k++)
                {
                    WizardUnit wu = new WizardUnit(Random.Range(0, mapW), Random.Range(0, mapH), "Wizro", 70, 1, 30, 3, "W");
                    units.Add(wu);
                }
            }
        }

        /*public void Display(GroupBox groupBox, TextBox txtU, TextBox txtB)
        {
            groupBox.Controls.Clear();
            txtU.Text = String.Empty;
            txtB.Text = String.Empty;
            foreach (Unit u in units)
            {
                Button myButt = new Button();
                if (u is MeleeUnit mu)
                {
                    myButt.Size = new Size(20, 20);
                    myButt.Location = new Point(mu.XPos * 20, mu.YPos * 20);
                    myButt.Text = mu.Symbol;
                    if (mu.Faction == 0)
                    {
                        myButt.ForeColor = Color.Red;
                    }
                    else
                    {
                        myButt.ForeColor = Color.Green;
                    }
                }
                else if (u is RangedUnit ru)
                {
                    myButt.Size = new Size(20, 20);
                    myButt.Location = new Point(ru.XPos * 20, ru.YPos * 20);
                    myButt.Text = ru.Symbol;
                    if (ru.Faction == 0)
                    {
                        myButt.ForeColor = Color.Red;
                    }
                    else
                    {
                        myButt.ForeColor = Color.Green;
                    }
                }
                else if (u is WizardUnit wu)
                {
                    myButt.Size = new Size(20, 20);
                    myButt.Location = new Point(wu.XPos * 20, wu.YPos * 20);
                    myButt.Text = wu.Symbol;
                    myButt.ForeColor = Color.Gray;
                }
                //myButt.Click += Unit_Click;
                groupBox.Controls.Add(myButt);
                txtU.AppendText(u.ToString());
                txtU.AppendText(Environment.NewLine);
            }
            foreach (Building b in builds)
            {
                Button myButt = new Button();
                if (b is ResourceBuilding rb)
                {
                    myButt.Size = new Size(30, 30);
                    myButt.Location = new Point(rb.PosX * 30, rb.PosY * 30);
                    myButt.Text = rb.Symbol;
                    if (rb.Faction == 0)
                    {
                        myButt.ForeColor = Color.Red;
                    }
                    else
                    {
                        myButt.ForeColor = Color.Green;
                    }
                }
                else
                {
                    FactoryBuilding fb = (FactoryBuilding)b;
                    myButt.Size = new Size(30, 30);
                    myButt.Location = new Point(fb.PosX * 30, fb.PosY * 30);
                    myButt.Text = fb.Symbol;
                    if (fb.Faction == 0)
                    {
                        myButt.ForeColor = Color.Red;
                    }
                    else
                    {
                        myButt.ForeColor = Color.Green;
                    }
                }
                //myButt.Click += Building_Click;
                groupBox.Controls.Add(myButt);
                txtB.AppendText(b.ToString());
                txtB.AppendText(Environment.NewLine);
            }
        }*/

        /*public void Unit_Click(object sender, EventArgs e)
        {
            int x, y;
            Button myButt = (Button)sender;
            x = myButt.Location.X / 20;
            y = myButt.Location.Y / 20;
            foreach (Unit u in units)
            {
                if (u is RangedUnit ru )
                {
                    if (ru.XPos == x && ru.YPos == y)
                    {
                        txtInfo.Text = "";
                        txtInfo.Text = ru.ToString();
                    }
                }
                else if (u is MeleeUnit mu)
                {
                    if (mu.XPos == x && mu.YPos == y)
                    {
                        txtInfo.Text = "";
                        txtInfo.Text = mu.ToString();
                    }
                }
            }
        }
        public void Building_Click(object sender, EventArgs e)
        {
            int x, y;
            Button myButt = (Button)sender;
            x = myButt.Location.X / 20;
            y = myButt.Location.Y / 20;
            foreach (Building b in builds)
            {
                if (b is ResourceBuilding rb)
                {
                    if (rb.PosX == x && rb.PosY == y)
                    {
                        txtInfo.Text = "";
                        txtInfo.Text = rb.ToString();
                    }
                }
                else if (b is FactoryBuilding fb)
                {
                    if (fb.PosX == x && fb.PosY == y)
                    {
                        txtInfo.Text = "";
                        txtInfo.Text = fb.ToString();
                    }
                }
            }
        }*/
    }
}
