using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    // Use this for initialization

    public int gridX;
    public int gridY;

    public bool IsTree;
    public Vector3 Position;

    public Node Parent;

    public int gCost;
    public int hCost;

    public int FCost { get { return gCost + hCost; } }

    public Node(bool a_IsTree, Vector3 a_Pos, int a_gridX, int a_gridY)
    {
        IsTree = a_IsTree;
        Position = a_Pos;
        gridX = a_gridX;
        gridY = a_gridY;
    }
}
