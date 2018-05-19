using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour {

    [SerializeField] public GameObject enemyTank;
    [SerializeField] public GameObject helicopter;
    [SerializeField] public GameObject waypoint;
    [SerializeField] public Grid grid;
    [SerializeField] public MapGenerator map_generator;
    [SerializeField] private Transform player;

    [SerializeField] int gridSizeX, gridSizeY;

    private float minimumSpawnDistance = 8;
    

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerScript>().transform;
        GenerateTank(1);
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    private void GenerateTank(int Enemies)
    {
        int CountEnemy = 0;
        while (CountEnemy != Enemies)
        {
            GameObject[,] generared_map = map_generator.Generared_map;

            int _x = Mathf.RoundToInt((9646 * Random.value + 5947) % (gridSizeX - 1));
            int _y = Mathf.RoundToInt((9646 * Random.value + 5947) % (gridSizeY - 1));
                    if (generared_map[_x, _y].layer != 8)//CHIFFRE MAGIQUE
                    {
                        float distance = Vector3.Distance(player.position, generared_map[_x, _y].transform.position);

                        if (distance >= minimumSpawnDistance)
                        {
                            if (CountEnemy != Enemies)
                            {
                                Debug.Log(_x + " " + _y + " " + generared_map[_x, _y].transform.position);
                                Instantiate(enemyTank, generared_map[_x, _y].transform.position, Quaternion.identity);
                                CountEnemy++;
                            }

                        }
                    }
        }
    }

    private void GenerateHelicopter(int HelicopterEnemies)
    {
        int CountEnemy = 0;
        while (CountEnemy != HelicopterEnemies)
        {
            GameObject[,] generared_map = map_generator.Generared_map;
            for (int y = 0; y < gridSizeY; y++)
            {
                for (int x = 0; x < gridSizeX; x++)
                {
                    if (generared_map[x, y].layer == 10)//CHIFFRE MAGIQUE
                    {
                        float distance = Vector3.Distance(player.position, generared_map[x, y].transform.position);

                        if (distance >= minimumSpawnDistance)
                        {
                            if (CountEnemy != HelicopterEnemies)
                            {
                                Instantiate(enemyTank, generared_map[y, x].transform.position, Quaternion.identity);
                                CountEnemy++;
                            }

                        }
                    }
                }
            }
        }
    }
}
