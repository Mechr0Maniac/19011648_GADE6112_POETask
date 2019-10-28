using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject Sprites;
    public int size;
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
        size *= 3;
        for (int x = 0; x < size; x += 3)
        {
            for (int z = 0; z < size; z += 3)
            {
                Instantiate(Sprites, new Vector2(x, z), Quaternion.identity);
            }
        }

    }
}
