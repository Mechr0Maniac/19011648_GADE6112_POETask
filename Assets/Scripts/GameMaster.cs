using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject[] Sprites;
    public int sizeX, sizeY;
    // Start is called before the first frame update
    void Start()
    {
        Display();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Display()
    {
        sizeX *= 3;
        sizeY *= 3;
        for (int x = 0; x < sizeX; x += 3)
        {
            for (int z = 0; z < sizeY; z += 3)
            {
                Instantiate(Sprites[0], new Vector2(x, z), Quaternion.identity);
            }
        }

    }
}
