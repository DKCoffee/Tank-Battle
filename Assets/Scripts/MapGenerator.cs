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

    public GameObject[] floorTiles;
    public GameObject[] TreeTiles;
    private TileType[][] tiles;

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < 5; i++)
        {
            SmoothMap();
        }
    }


    void RandomFillMap()
    {
        if (useRandomSeed)
        {
            seed = Time.time.ToString();
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

    //void InstantiateTiles()
    //{
    //    // Go through all the tiles in the jagged array...
    //    for (int i = 0; i < tiles.Length; i++)
    //    {
    //        for (int j = 0; j < tiles[i].Length; j++)
    //        {
    //            // ... and instantiate a floor tile for it.
    //            InstantiateFromArray(floorTiles, i, j);

    //            // If the tile type is Wall...
    //            if (tiles[i][j] == TileType.Tree)
    //            {
    //                // ... instantiate a wall over the top.
    //                InstantiateFromArray(TreeTiles, i, j);
    //            }
    //        }
    //    }
    //}

    //void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
    //{
    //    // Create a random index for the array.
    //    int randomIndex = Random.Range(0, prefabs.Length);

    //    // The position to be instantiated at is based on the coordinates.
    //    Vector3 position = new Vector3(xCoord, yCoord, 0f);

    //    // Create an instance of the prefab from the random index of the array.
    //    GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

    //    // Set the tile's parent to the board holder.
    //    tileInstance.transform.parent = boardHolder.transform;
    //}



    void OnDrawGizmos()
    {
        if (map != null)
        {
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    Gizmos.color = (map[x,y] == 1) ? Color.black:Color.white;
                    Vector3 pos = new Vector3(-width / 2 + x + .5f, -height / 2 + y + .5f, 0);
                    Gizmos.DrawCube(pos, Vector3.one);
                }
            }
        }
    }
}
