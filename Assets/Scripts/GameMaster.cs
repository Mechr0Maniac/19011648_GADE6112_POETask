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
    int count = 0, speed = 10;
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

    void Control()
    {
        for (int i = 0; i < map.Units.Count; i++)
        {
            if (map.Units[i] is MeleeUnit mu)
            {
                if (mu.Health <= mu.MaxHealth * 0.25 && mu.IsDead == false)
                    mu.Move(Random.Range(0, 4));
                else
                {
                    (Unit closestU, int distanceToU) = mu.Closest(map.Units);
                    (Building closestB, int distanceToB) = mu.Raid(map.Builds, map.Builds.Count);
                    if (distanceToB < distanceToU)
                    {
                        if (closestB.IsDie() == false && closestB.FactionCheck() != mu.Faction)
                        {
                            if (distanceToB <= mu.AttackRange)
                            {
                                mu.IsAttacking = true;
                                mu.Raze(closestB);
                            }
                            else
                            {
                                if (closestB is FactoryBuilding fact)
                                {
                                    if (mu.XPos > fact.PosX)
                                    {
                                        mu.Move(0);
                                    }
                                    else if (mu.YPos < fact.PosY)
                                    {
                                        mu.Move(1);
                                    }
                                    else if (mu.XPos < fact.PosX)
                                    {
                                        mu.Move(2);
                                    }
                                    else if (mu.YPos > fact.PosY)
                                    {
                                        mu.Move(3);
                                    }
                                }
                                else if (closestB is ResourceBuilding res)
                                {
                                    if (mu.XPos > res.PosX)
                                    {
                                        mu.Move(0);
                                    }
                                    else if (mu.YPos < res.PosY)
                                    {
                                        mu.Move(1);
                                    }
                                    else if (mu.XPos < res.PosX)
                                    {
                                        mu.Move(2);
                                    }
                                    else if (mu.YPos > res.PosY)
                                    {
                                        mu.Move(3);
                                    }
                                }
                            }
                        }
                        else
                            mu.Move(Random.Range(0, 4));
                    }
                    else
                    {
                        if (closestU.AliveNt() == false && closestU.FactionCheck() != mu.Faction)
                        {
                            if (distanceToU <= mu.AttackRange)
                            {
                                mu.IsAttacking = true;
                                mu.Combat(closestU);
                            }
                            else
                            {
                                if (closestU is MeleeUnit closestMu)
                                {
                                    if (mu.XPos > closestMu.XPos)
                                    {
                                        mu.Move(0);
                                    }
                                    else if (mu.YPos < closestMu.YPos)
                                    {
                                        mu.Move(1);
                                    }
                                    else if (mu.XPos < closestMu.XPos)
                                    {
                                        mu.Move(2);
                                    }
                                    else if (mu.YPos > closestMu.YPos)
                                    {
                                        mu.Move(3);
                                    }
                                }
                                else if (closestU is RangedUnit closestRu)
                                {
                                    if (mu.XPos > closestRu.XPos)
                                    {
                                        mu.Move(0);
                                    }
                                    else if (mu.YPos < closestRu.YPos)
                                    {
                                        mu.Move(1);
                                    }
                                    else if (mu.XPos < closestRu.XPos)
                                    {
                                        mu.Move(2);
                                    }
                                    else if (mu.YPos > closestRu.YPos)
                                    {
                                        mu.Move(3);
                                    }
                                }
                            }
                        }
                        else
                            mu.Move(Random.Range(0, 4));
                    }
                }
            }
            else if (map.Units[i] is RangedUnit ru)
            {
                (Unit closestU, int distanceToU) = ru.Closest(map.Units);
                (Building closestB, int distanceToB) = ru.Raid(map.Builds, map.Builds.Count);
                if (distanceToB < distanceToU)
                {
                    if (closestB.IsDie() == false && closestB.FactionCheck() != ru.Faction)
                    {
                        if (distanceToB <= ru.AttackRange)
                        {
                            ru.IsAttacking = true;
                            ru.Raze(closestB);
                        }
                        else
                        {
                            if (closestB is FactoryBuilding fact)
                            {
                                if (ru.XPos > fact.PosX)
                                {
                                    ru.Move(0);
                                }
                                else if (ru.YPos < fact.PosY)
                                {
                                    ru.Move(1);
                                }
                                else if (ru.XPos < fact.PosX)
                                {
                                    ru.Move(2);
                                }
                                else if (ru.YPos > fact.PosY)
                                {
                                    ru.Move(3);
                                }
                            }
                            else if (closestB is ResourceBuilding res)
                            {
                                if (ru.XPos > res.PosX)
                                {
                                    ru.Move(0);
                                }
                                else if (ru.YPos < res.PosY)
                                {
                                    ru.Move(1);
                                }
                                else if (ru.XPos < res.PosX)
                                {
                                    ru.Move(2);
                                }
                                else if (ru.YPos > res.PosY)
                                {
                                    ru.Move(3);
                                }
                            }
                        }
                    }
                    else
                        ru.Move(Random.Range(0, 4));
                }
                if (closestU.AliveNt() == false && closestU.FactionCheck() != ru.Faction)
                {
                    if (distanceToU <= ru.AttackRange)
                    {
                        ru.IsAttacking = true;
                        ru.Raze(closestB);
                    }
                    else
                    {
                        if (closestU is MeleeUnit closestMu)
                        {
                            if (ru.XPos > closestMu.XPos)
                            {
                                ru.Move(0);
                            }
                            else if (ru.YPos < closestMu.YPos)
                            {
                                ru.Move(1);
                            }
                            else if (ru.XPos < closestMu.XPos)
                            {
                                ru.Move(2);
                            }
                            else if (ru.YPos > closestMu.YPos)
                            {
                                ru.Move(3);
                            }
                        }
                        else if (closestU is RangedUnit closestRu)
                        {
                            if (ru.XPos > closestRu.XPos)
                            {
                                ru.Move(0);
                            }
                            else if (ru.YPos < closestRu.YPos)
                            {
                                ru.Move(1);
                            }
                            else if (ru.XPos < closestRu.XPos)
                            {
                                ru.Move(2);
                            }
                            else if (ru.YPos > closestRu.YPos)
                            {
                                ru.Move(3);
                            }
                        }
                    }
                }
            }
            else if (map.Units[i] is WizardUnit wu)
            {
                if (wu.Health <= wu.MaxHealth * 0.5 && wu.IsDead == false)
                    wu.Move(Random.Range(0, 4));
                else
                {
                    (Unit closestU, int distanceToU) = wu.Closest(map.Units);
                    if (closestU.AliveNt() == false && closestU.FactionCheck() != wu.Faction)
                    {
                        if (distanceToU <= 1)
                        {
                            wu.IsAttacking = true;
                            wu.Combat(closestU);
                        }
                        else
                        {
                            if (closestU is MeleeUnit closestMu)
                            {
                                if (wu.XPos > closestMu.XPos)
                                {
                                    wu.Move(0);
                                }
                                else if (wu.YPos < closestMu.YPos)
                                {
                                    wu.Move(1);
                                }
                                else if (wu.XPos < closestMu.XPos)
                                {
                                    wu.Move(2);
                                }
                                else if (wu.YPos > closestMu.YPos)
                                {
                                    wu.Move(3);
                                }
                            }
                            else if (closestU is RangedUnit closestRu)
                            {
                                if (wu.XPos > closestRu.XPos)
                                {
                                    wu.Move(0);
                                }
                                else if (wu.YPos < closestRu.YPos)
                                {
                                    wu.Move(1);
                                }
                                else if (wu.XPos < closestRu.XPos)
                                {
                                    wu.Move(2);
                                }
                                else if (wu.YPos > closestRu.YPos)
                                {
                                    wu.Move(3);
                                }
                            }
                        }
                    }
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
        //map.Display(grpMap, textU, textB);
        round++;
    }
}
