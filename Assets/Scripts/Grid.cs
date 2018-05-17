using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public Transform StartPosition;
    public LayerMask TreeMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float Distance;

    [SerializeField] private MapGenerator map_generator;

    Node[,] grid;
    public List<Node> FinalPath;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2 ;
        GameObject[,] generared_map = map_generator.Generared_map;
        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool Tree = false;

                if (generared_map[x, y].layer == 8)//CHIFFRE MAGIQUE
                {
                    Tree = true;
                }

                grid[y, x] = new Node(Tree, worldPoint, x, y);
            }
        }
    }

    public Node NodeFromWorldPosition(Vector3 a_WorldPosition)
    {
        float xpoint = ((a_WorldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float ypoint = ((a_WorldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y);

        xpoint = Mathf.Clamp01(xpoint);
        ypoint = Mathf.Clamp01(ypoint);

        int x = Mathf.RoundToInt((gridSizeX - 1) * xpoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * ypoint);

        return grid[x, y];
    }

    public List<Node>GetNeighboringNodes(Node a_Node)
    {
        List<Node> NeighboringNodes = new List<Node>();
        int xCheck;
        int yCheck;
        // Right side
        xCheck = a_Node.gridX + 1;
        yCheck = a_Node.gridY;
        if(xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }
        //Left side
        xCheck = a_Node.gridX - 1;
        yCheck = a_Node.gridY;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }
        //Top side
        xCheck = a_Node.gridX;
        yCheck = a_Node.gridY +1;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }
        //Bottom side
        xCheck = a_Node.gridX;
        yCheck = a_Node.gridY -1;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }
        return NeighboringNodes;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));
        if(grid != null)
        {
            foreach(Node node in grid)
            {
                if(node.IsTree)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                }

                if (FinalPath != null)
                {
                    Gizmos.color = Color.red;
                }

                Gizmos.DrawSphere(node.Position, 0.05f);
            }
        }
    }
}
