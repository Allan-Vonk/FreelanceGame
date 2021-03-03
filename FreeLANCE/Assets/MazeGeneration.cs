using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneration : MonoBehaviour
{
    Grid grid;
    Node[,] nodeGrid;
    private List<Node>CellList;
    private void Start ()
    {
        grid = GetComponent<Grid>();
        nodeGrid = grid.grid;
    }
    private void GenerateMaze ()
    {
        //Assign random values
        foreach (Node node in nodeGrid)
        {
            node.mzValue = Random.Range(0, 100);
        }
        //
        Node startNode = nodeGrid[0,0];

    }
}
