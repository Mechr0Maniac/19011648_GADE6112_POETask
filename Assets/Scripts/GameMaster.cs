using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject[] Sprites;
    public Map map;
    public int sizeX, sizeY;
    public int numUnits, numBuilds;
    public int round;
    int count = 0, speed = 30;
    bool pause = false;
    // Start is called before the first frame update
    void Start()
    {
        map = new Map(Sprites, numUnits, numBuilds, sizeX, sizeY);
        map.Generate();
        map.Display();
    }

    // Update is called once per frame
    void Update()
    {
        if (pause == false)
        {
            if (count == 0)
            {
                Control();
                map.Display();
                count = speed;
            }
            else
            {
                count--;
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            pause = !pause;
        }
    }

    void Control()
    {
        List<Unit> units = map.Units;
        for (int i = 0; i < units.Count; i++)
        {
            if (units[i] is WizardUnit wu)
            {
                if (wu.Health <= wu.MaxHealth * 0.5 && wu.IsDead == false)
                    wu.Move(Random.Range(0, 4));
                else
                {
                    (Unit closestU, int distanceToU) = wu.Closest(units);
                    if (closestU.AliveNt() == false)
                    {
                        if (distanceToU <= 1)
                        {
                            wu.Combat(closestU);
                        }
                        else
                        {
                            if (!(closestU is WizardUnit))
                            {
                                if (wu.YPos > closestU.GetY())
                                {
                                    wu.Move(0);
                                }
                                else if (wu.XPos < closestU.GetX())
                                {
                                    wu.Move(1);
                                }
                                else if (wu.YPos < closestU.GetY())
                                {
                                    wu.Move(2);
                                }
                                else if (wu.XPos > closestU.GetX())
                                {
                                    wu.Move(3);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                (Unit closestU, int distanceToU) = units[i].Closest(map.Units);
                (Building closestB, int distanceToB) = units[i].Raid(map.Builds, map.Builds.Count);
                if (units[i].GetHealth() <= units[i].GetMaxHealth() * 0.25 && map.Units[i].AliveNt() == false)
                    map.Units[i].Move(Random.Range(0, 4));
                else
                {
                    if (distanceToB < distanceToU)
                    {
                        if (closestB.IsDie() == false && closestB.FactionCheck() != units[i].FactionCheck())
                        {
                            if (distanceToB <= units[i].GetRange())
                            {
                                map.Units[i].Raze(closestB);
                            }
                            else
                            {
                                if (units[i].GetY() > closestB.GetY())
                                {
                                    map.Units[i].Move(0);
                                }
                                else if (units[i].GetX() < closestB.GetX())
                                {
                                    units[i].Move(1);
                                }
                                else if (units[i].GetY() < closestB.GetY())
                                {
                                    units[i].Move(2);
                                }
                                else if (units[i].GetX() > closestB.GetX())
                                {
                                    units[i].Move(3);
                                }
                            }
                        }
                    }
                    else if (closestU.AliveNt() == false)
                    {
                        if (distanceToU <= units[i].GetRange())
                        {
                            units[i].Combat(closestU);
                        }
                        else
                        {
                            if (units[i].GetY() > closestU.GetY())
                            {
                                units[i].Move(0);
                            }
                            else if (units[i].GetX() < closestU.GetX())
                            {
                                units[i].Move(1);
                            }
                            else if (units[i].GetY() < closestU.GetY())
                            {
                                units[i].Move(2);
                            }
                            else if (units[i].GetX() > closestU.GetX())
                            {
                                units[i].Move(3);
                            }
                        }
                    }
                    else
                        units[i].Move(Random.Range(0, 4));
                }
            }
        }
        for (int i = 0; i < map.Builds.Count; i++)
        {
            if (map.Builds[i] is ResourceBuilding rb)
            {
                rb.Generator();
            }
            else if (map.Builds[i] is FactoryBuilding fb)
            {
                if (fb.ProductSpeed >= 3)
                {
                    if (fb.WillSpawn(map.Builds))
                    {
                        map.Units.Add(fb.Spawn(fb, map.Builds));
                    }
                    fb.ProductSpeed = 0;
                }
                fb.ProductSpeed++;
            }
        }
    }
}
