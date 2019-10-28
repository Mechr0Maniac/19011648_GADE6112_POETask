using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject[] PossibleTiles;
    public GameObject[] Sprites;
    public int size;

    private Tiles[,] tileInfoMap;
    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
        Display();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Display()
    {
        for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
            {
                Instantiate(PossibleTiles[tileInfoMap[x, z].TyleType], new Vector3(x, 1f, z), Quaternion.identity);
            }
        }

    }

    void GenerateMap()
    {
        tileInfoMap = new Tiles[size, size];
        for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
            {
                tileInfoMap[x, z] = new Tiles();
                tileInfoMap[x, z].TyleType = Random.Range(0, 3);
            }
        }
    }
}
