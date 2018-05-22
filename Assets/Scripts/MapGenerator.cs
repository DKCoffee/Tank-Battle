using UnityEngine;
using System.Collections;
using System;

public class MapGenerator : MonoBehaviour
{
    public int width;
    public int height;

    public string seed;
    public bool useRandomSeed;

    [Range(0, 100)]
    public int randomFillPercent;

    int[,] map;

    public enum TileType
    {
        Tree, Floor,
    }
    private GameObject[,] generared_map;
    [SerializeField] public GameObject floorTiles;
    [SerializeField] public GameObject TreeTiles;
    [SerializeField] public GameObject player;
    private TileType[][] tiles;

    public GameObject[,] Generared_map
    {
        get
        {
            return generared_map;
        }

        set
        {
            generared_map = value;
        }
    }

    void Awake()
    {
        //do
        //{
            GenerateMap();
        //} while (PercentOfGround() >= 50);

    }

    //float PercentOfGround()
    //{
    //    int number_Ground = 0;
    //    for (int y = 0; y < height; y++)
    //    {
    //        for (int x = 0; x < width; x++)
    //        {
    //            if (this.generared_map[x, y].layer != 8)
    //            {
    //                number_Ground++;
    //            }
    //        }
    //    }
    //    return (float)number_Ground / (height * width) * 100;
    //}

    void GenerateMap()
    {
        Generared_map = new GameObject[width, height];
        map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < 8; i++)
        {
            SmoothMap();
        }
        for(int x = 0; x <width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Generared_map[x, y] = Instantiate(floorTiles, new Vector2(x - width / 2 , y - height / 2), Quaternion.identity);
                
                if (map[x,y] == 1)
                {

                    Generared_map[x,y] = Instantiate(TreeTiles, new Vector2(x - width / 2, y - height / 2),Quaternion.identity);
                }
                //if(map[x,y] == 0)
                //{
                //    Generaredmap[x, y] = Instantiate(floorTiles, new Vector2(x, y), Quaternion.identity);
                //}
            }
        }
        Instantiate(player, new Vector2(-8.5f,-13f), Quaternion.identity);
    }


    void RandomFillMap()
    {
        if (useRandomSeed)
        {
            seed = DateTime.Now.Ticks.ToString();
        }

        System.Random pseudoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent)? 1: 0;
                }
            }
        }
    }

    void SmoothMap()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                map[x, y] = 0;
            }
        }
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);

                if (neighbourWallTiles > 4)
                    map[x, y] = 1;
                else if (neighbourWallTiles < 3)
                    map[x, y] = 0;

            }
        }

    }

    int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++) {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++) {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height) {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        wallCount += map[neighbourX, neighbourY];
                    }
                }
                else {
                    wallCount++;
                }
            }
        }

        return wallCount;
    }
}
