using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class Map
{
    List<Unit> units;
    List<Building> builds;
    int numUnits = 0, numBuilds = 0;
    int sizeX = 0, sizeY = 0;
    int numR = 0, numM = 0;
    GameObject[] Sprites;

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

    public Map(GameObject[] spr, int nu, int nb, int sx, int sy)
    {
        units = new List<Unit>();
        builds = new List<Building>();
        numUnits = nu;
        numBuilds = nb;
        sizeX = sx;
        sizeY = sy;
        Sprites = spr;
    }

    public void Generate()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                Object.Instantiate(Sprites[0], new Vector3(x, y, 5), Quaternion.identity);
            }
        }
        for (int i = 0; i < numUnits; i++)
        {
            int r = Random.Range(0, 2);
            if (r == 0)
            {
                MeleeUnit m = new MeleeUnit(Random.Range(0, sizeX), Random.Range(0, sizeY), "Soldier", 40, 3, 3, i % 2 == 0 ? 1 : 0, "M");
                units.Add(m);
                numM++;
            }
            else
            {
                RangedUnit ru = new RangedUnit(Random.Range(0, sizeX), Random.Range(0, sizeY), "Archer", 40, 2, 5, 5, i % 2 == 0 ? 1 : 0, "R");
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
                    FactoryBuilding fb = new FactoryBuilding(Random.Range(0, sizeX), Random.Range(0, sizeY), 0, 80, 5, j % 2 == 0 ? 1 : 0, "FB");
                    builds.Add(fb);
                }
                else
                {
                    FactoryBuilding fb = new FactoryBuilding(Random.Range(0, sizeX), Random.Range(0, sizeY), 1, 80, 5, j % 2 == 0 ? 1 : 0, "FB");
                    builds.Add(fb);
                }
            }
            else
            {
                ResourceBuilding rb = new ResourceBuilding(Random.Range(0, sizeX), Random.Range(0, sizeY), "Swords", 80, 2, 60, j % 2 == 0 ? 1 : 0);
                builds.Add(rb);
            }
        }
        if (numR < numM)
        {
            for (int k = 0; k < Random.Range(numR, numM); k++)
            {
                WizardUnit wu = new WizardUnit(Random.Range(0, sizeX), Random.Range(0, sizeY), "Wizro", 20, 1, 10, 3, "W");
                units.Add(wu);
            }
        }
        else
        {
            for (int k = 0; k < Random.Range(numM, numR); k++)
            {
                WizardUnit wu = new WizardUnit(Random.Range(0, sizeX), Random.Range(0, sizeY), "Wizro", 20, 1, 10, 3, "W");
                units.Add(wu);
            }
        }
    }

    public void Display()
    {
        GameObject health;
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("Pieces");
        foreach (GameObject g in pieces)
        {
            Object.Destroy(g);
        }
        foreach (Unit u in units)
        {
            if (u is MeleeUnit mu)
            {
                if (mu.Faction == 0)
                {
                    Object.Instantiate(Sprites[6], new Vector2(mu.XPos, mu.YPos), Quaternion.identity);
                    health = Sprites[6].transform.GetChild(0).transform.GetChild(0).GetChild(0).gameObject;
                    var rectTransform = health.transform as RectTransform;
                    rectTransform.sizeDelta = new Vector2(mu.Health, rectTransform.sizeDelta.y);
                }
                else
                {
                    Object.Instantiate(Sprites[2], new Vector2(mu.XPos, mu.YPos), Quaternion.identity);
                    health = Sprites[2].transform.GetChild(0).transform.GetChild(0).GetChild(0).gameObject;
                    var rectTransform = health.transform as RectTransform;
                    rectTransform.sizeDelta = new Vector2(mu.Health, rectTransform.sizeDelta.y);
                }
                if (mu.IsDead == true)
                    Object.Instantiate(Sprites[10], new Vector2(mu.XPos, mu.YPos), Quaternion.identity);
            }
            else if (u is RangedUnit ru)
            {
                if (ru.Faction == 0)
                {
                    Object.Instantiate(Sprites[7], new Vector2(ru.XPos, ru.YPos), Quaternion.identity);
                    health = Sprites[7].transform.GetChild(0).transform.GetChild(0).GetChild(0).gameObject;
                    var rectTransform = health.transform as RectTransform;
                    rectTransform.sizeDelta = new Vector2(ru.Health, rectTransform.sizeDelta.y);
                }
                else
                {
                    Object.Instantiate(Sprites[3], new Vector2(ru.XPos, ru.YPos), Quaternion.identity);
                    health = Sprites[3].transform.GetChild(0).transform.GetChild(0).GetChild(0).gameObject;
                    var rectTransform = health.transform as RectTransform;
                    rectTransform.sizeDelta = new Vector2(ru.Health, rectTransform.sizeDelta.y);
                }
                if (ru.IsDead == true)
                    Object.Instantiate(Sprites[10], new Vector2(ru.XPos, ru.YPos), Quaternion.identity);
            }
            else if (u is WizardUnit wu)
            {
                Object.Instantiate(Sprites[1], new Vector2(wu.XPos, wu.YPos), Quaternion.identity);
                health = Sprites[1].transform.GetChild(0).transform.GetChild(0).GetChild(0).gameObject;
                var rectTransform = health.transform as RectTransform;
                rectTransform.sizeDelta = new Vector2(wu.Health*2, rectTransform.sizeDelta.y);
                if (wu.IsDead == true)
                    Object.Instantiate(Sprites[10], new Vector2(wu.XPos, wu.YPos), Quaternion.identity);
            }
        }
        foreach (Building b in builds)
        {
            if (b is ResourceBuilding rb)
            {
                if (rb.Faction == 0)
                {
                    Object.Instantiate(Sprites[9], new Vector2(rb.PosX, rb.PosY), Quaternion.identity);
                    health = Sprites[9].transform.GetChild(0).transform.GetChild(0).GetChild(0).gameObject;
                    var rectTransform = health.transform as RectTransform;
                    rectTransform.sizeDelta = new Vector2(rb.Health / 2, rectTransform.sizeDelta.y);
                }
                else
                {
                    Object.Instantiate(Sprites[5], new Vector2(rb.PosX, rb.PosY), Quaternion.identity);
                    health = Sprites[5].transform.GetChild(0).transform.GetChild(0).GetChild(0).gameObject;
                    var rectTransform = health.transform as RectTransform;
                    rectTransform.sizeDelta = new Vector2(rb.Health / 2, rectTransform.sizeDelta.y);
                }
                if (rb.IsDead == true)
                    Object.Instantiate(Sprites[10], new Vector2(rb.PosX, rb.PosY), Quaternion.identity);
            }
            else if (b is FactoryBuilding fb)
            {
                if (fb.Faction == 0)
                {
                    Object.Instantiate(Sprites[8], new Vector2(fb.PosX, fb.PosY), Quaternion.identity);
                    health = Sprites[8].transform.GetChild(0).transform.GetChild(0).GetChild(0).gameObject;
                    var rectTransform = health.transform as RectTransform;
                    rectTransform.sizeDelta = new Vector2(fb.Health / 2, rectTransform.sizeDelta.y);
                }
                else
                {
                    Object.Instantiate(Sprites[4], new Vector2(fb.PosX, fb.PosY), Quaternion.identity);
                    health = Sprites[4].transform.GetChild(0).transform.GetChild(0).GetChild(0).gameObject;
                    var rectTransform = health.transform as RectTransform;
                    rectTransform.sizeDelta = new Vector2(fb.Health / 2, rectTransform.sizeDelta.y);
                }
                if (fb.IsDead == true)
                    Object.Instantiate(Sprites[10], new Vector2(fb.PosX, fb.PosY), Quaternion.identity);
            }
        }
    }
}
