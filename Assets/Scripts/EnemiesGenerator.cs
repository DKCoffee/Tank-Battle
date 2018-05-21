using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour {

    [SerializeField] public GameObject enemyTank;
    [SerializeField] public GameObject helicopter;
    [SerializeField] public GameObject tanksWaypoints;
    [SerializeField] public GameObject helicopterWaypoints;
    [SerializeField] public Grid grid;
    [SerializeField] public MapGenerator map_generator;
    [SerializeField] private Transform player;
    public GameObject[] tankWaypointsList;

    [SerializeField] int gridSizeX, gridSizeY;

    private float minimumSpawnDistance = 8;
    

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerScript>().transform;
        GenerateTank(3);
        GenerateHelicopter(1);
        GenerateWaypointsForTanks(5);
        GenerateWaypointsForHelicopter(5);
        

    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(tankWaypointsList);
    }

    private void GenerateTank(int Enemies)
    {
        int CountEnemy = 0;
        while (CountEnemy != Enemies)
        {
            GameObject[,] generared_map = map_generator.Generared_map;

            int _x = Mathf.RoundToInt((9633 * Random.value + 4547) % (gridSizeX - 1));
            int _y = Mathf.RoundToInt((9056 * Random.value + 5567) % (gridSizeY - 1));

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

    private void GenerateHelicopter(int helicopterEnemies)
    {
        int CountEnemy = 0;
        while (CountEnemy != helicopterEnemies)
        {
            GameObject[,] generared_map = map_generator.Generared_map;

            int _x = Mathf.RoundToInt((9563 * Random.value + 4332) % (gridSizeX - 1));
            int _y = Mathf.RoundToInt((2256 * Random.value + 8667) % (gridSizeY - 1));

            if (generared_map[_x, _y].layer == 8)//CHIFFRE MAGIQUE
            {
                float distance = Vector3.Distance(player.position, generared_map[_x, _y].transform.position);

                if (distance >= minimumSpawnDistance)
                {
                    if (CountEnemy != helicopterEnemies)
                    {
                        Debug.Log(_x + " " + _y + " " + generared_map[_x, _y].transform.position);
                        Instantiate(helicopter, generared_map[_x, _y].transform.position, Quaternion.identity);
                        CountEnemy++;
                    }

                }

            }
        }
    }

    private void GenerateWaypointsForTanks(int tankWaypoint)
    {
        int CountEnemy = 0;
        while (CountEnemy != tankWaypoint)
        {
            GameObject[,] generared_map = map_generator.Generared_map;

            int _x = Mathf.RoundToInt((9563 * Random.value + 4332) % (gridSizeX - 1));
            int _y = Mathf.RoundToInt((9846 * Random.value + 8667) % (gridSizeY - 1));

            if (generared_map[_x, _y].layer != 8)//CHIFFRE MAGIQUE
            {
                float distance = Vector3.Distance(player.position, generared_map[_x, _y].transform.position);

                if (distance >= minimumSpawnDistance)
                {
                    if (CountEnemy != tankWaypoint)
                    {
                        Debug.Log(_x + " " + _y + " " + generared_map[_x, _y].transform.position);
                        Instantiate(tanksWaypoints, generared_map[_x, _y].transform.position, Quaternion.identity);
                        CountEnemy++;
                    }

                }

            }
        }
    }

    private void GenerateWaypointsForHelicopter(int helicopterWaypoint)
    {
        int CountEnemy = 0;
        while (CountEnemy != helicopterWaypoint)
        {
            GameObject[,] generared_map = map_generator.Generared_map;

            int _x = Mathf.RoundToInt((9563 * Random.value + 4332) % (gridSizeX - 1));
            int _y = Mathf.RoundToInt((9846 * Random.value + 8667) % (gridSizeY - 1));

            if (generared_map[_x, _y].layer == 8)//CHIFFRE MAGIQUE
            {
                float distance = Vector3.Distance(player.position, generared_map[_x, _y].transform.position);

                if (distance >= minimumSpawnDistance)
                {
                    if (CountEnemy != helicopterWaypoint)
                    {
                        Debug.Log(_x + " " + _y + " " + generared_map[_x, _y].transform.position);
                        Instantiate(helicopterWaypoints, generared_map[_x, _y].transform.position, Quaternion.identity);
                        CountEnemy++;
                    }

                }

            }
        }
    }
}
