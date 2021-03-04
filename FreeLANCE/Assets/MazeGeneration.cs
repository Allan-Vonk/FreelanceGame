using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGeneration : MonoBehaviour
{
    Grid grid;
    Node[,] nodeGrid;
    public GameObject wallPrefab;
    private List<Node>Cellset;
    Pathfinding pf;
    List<Node>PossibleExits;
    List<Queue<Vector3>>PathList;
    private void Start ()
    {
        pf = GetComponent<Pathfinding>();
        PathList = new List<Queue<Vector3>>();
        grid = GetComponent<Grid>();
        nodeGrid = grid.grid;
        Cellset = new List<Node>();
        PossibleExits = new List<Node>();
        GenerateMaze();
        GenerateExit();
        GenerateWalls();
    }
    private void GenerateMaze ()
    {
        Node startnode = nodeGrid[0,0];
        Cellset.Add(startnode);
        while (Cellset.Count > 0)
        {
            Node randomNode = Cellset[Random.Range(0,Cellset.Count)];
            List<Node> neigbours = grid.Get4Neighbours(randomNode);

            int count = 0;
            foreach (Node node in neigbours)
            {
                if (node.walkable == true)
                {
                    count++;
                }
            }
            if (count <2)
            {
                randomNode.walkable = true;
                foreach (Node node in neigbours)
                {
                    if (node.visited == false)
                    {

                        Cellset.Add(node);
                        
                    }
                    node.visited = true;
                }
            }
            if (randomNode.gridX == 0 || randomNode.gridX == grid.gridSizeX - 1 || randomNode.gridY == 0 || randomNode.gridY == grid.gridSizeY - 1)
            {
                randomNode.walkable = false;
            }
            Cellset.Remove(randomNode);
        }
    }
    private void GenerateWalls ()
    {
        foreach (Node node in nodeGrid)
        {
            if (node.walkable == false)
            {
                GameObject wall = Instantiate(wallPrefab);
                wall.transform.position = node.worldPosition + new Vector3(0,2,0);
            }
        }
    }
    private void GenerateExit ()
    {
        //X
        for (int i = 0; i < grid.gridSizeX; i++)
        {
            if (nodeGrid[i,1].walkable)
            {
                PossibleExits.Add(nodeGrid[i, 1]);
            }
        }
        for (int i = 0; i < grid.gridSizeX; i++)
        {
            if (nodeGrid[i, grid.gridSizeY - 1].walkable)
            {
                PossibleExits.Add(nodeGrid[i, grid.gridSizeY - 1]);
            }
        }
        //Y
        for (int i = 0; i < grid.gridSizeY; i++)
        {
            if (nodeGrid[1, i].walkable)
            {
                PossibleExits.Add(nodeGrid[1, i]);
            }
        }
        for (int i = 0; i < grid.gridSizeY; i++)
        {
            if (nodeGrid[grid.gridSizeX - 1, i].walkable)
            {
                PossibleExits.Add(nodeGrid[grid.gridSizeX - 1, i]);
            }
        }
        foreach (var item in PossibleExits)
        {
            Debug.Log(item.worldPosition);
            Debug.Log(pf.FindPath(nodeGrid[0, 0].worldPosition, item.worldPosition));
        }
        //List<Vector3>longestPath = new List<Vector3>();
        //foreach (var item in PathList[0])
        //{
        //    longestPath.Add(item);
        //}
        //Node exitNode;
        //exitNode = grid.NodeFromWorldPoint(longestPath[0]);
        //foreach (var item in grid.Get4Neighbours(exitNode))
        //{
        //    item.walkable = true;
        //}
    }
    private void OnDrawGizmos ()
    {
        foreach (var item in PossibleExits)
        {
            Gizmos.DrawIcon(item.worldPosition, "Exit");
        }
    }
}
