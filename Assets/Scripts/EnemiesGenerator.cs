using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour {

    [SerializeField] public GameObject enemyTank;
    [SerializeField] public GameObject helicopter;
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
            for (int y = 0; y < gridSizeY; y++)
            {
                for (int x = 0; x < gridSizeX; x++)
                {
                    if (generared_map[x, y].layer != 8)//CHIFFRE MAGIQUE
                    {
                        float distance = Vector3.Distance(player.position, generared_map[x, y].transform.position);
                        
                        if (distance >= minimumSpawnDistance)
                        {
                            if (CountEnemy != Enemies)
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

    private void GenerateHelicopter()
    {

    }
}
