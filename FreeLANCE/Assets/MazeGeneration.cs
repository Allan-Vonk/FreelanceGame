using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeGeneration : MonoBehaviour
{
    Grid grid;
    Node[,] nodeGrid;
    public GameObject wallPrefab;
    private List<Node>Cellset;
    Node EndNode;
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

        initialize();
    }
    private void initialize ()
    {
        GenerateMaze();
        ClearStart();
        GenerateExit();
        GenerateWalls();
    }
    private void ClearStart ()
    {
        Node startNode = nodeGrid[1,1];
        List<Node>neigbours = grid.GetNeighbours(startNode);
        foreach (Node node in neigbours)
        {
            node.walkable = true;
        }
    }
    private void ClearEnd (Node node)
    {
        List<Node>neigbours = grid.Get4Neighbours(node);
        foreach (Node node1 in neigbours)
        {
            node1.walkable = true;
        }
    }
    private void GenerateMaze ()
    {
        Node startnode = nodeGrid[0,0];
        Debug.Log(startnode.worldPosition);
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
            Queue<Vector3>Path = pf.FindPath(nodeGrid[0, 0].worldPosition, item.worldPosition);
            if (Path != null)
            {
                PathList.Add(Path);
            }
        }
        List<Queue<Vector3>> ordered = PathList.OrderBy(f => f.Count).ToList();
        ordered.Reverse();
        List<Vector3>path = ordered[0].ToList();
        if (path.Count < 40)
        {
            SceneManager.LoadScene(0);
        }
        path.Reverse();
        Debug.Log(path.Count);
        EndNode = grid.NodeFromWorldPoint(path[0]);
        ClearEnd(EndNode);
    }
    private void OnDrawGizmos ()
    {
        //foreach (var item in PossibleExits)
        //{
        //    Gizmos.DrawIcon(item.worldPosition, "Exit");
        //}
    }
}
