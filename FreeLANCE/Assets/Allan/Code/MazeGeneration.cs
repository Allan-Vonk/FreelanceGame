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

    List<GameObject>Walls;
    MeshFilter mf;
    MeshCollider mc;
    private void Start ()
    {
        mc = GetComponent<MeshCollider>();
        mf = GetComponent<MeshFilter>();
        Walls = new List<GameObject>();
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
        MergeWalls();
    }
    private void MergeWalls ()
    {
        List<CombineInstance> combineInstances = new List<CombineInstance>();
        foreach (var item in Walls)
        {
            CombineInstance ci = new CombineInstance();
            MeshFilter meshFilter = item.GetComponent<MeshFilter>();
            ci.mesh = meshFilter.sharedMesh;
            ci.transform = meshFilter.transform.localToWorldMatrix;
            combineInstances.Add(ci);
            item.SetActive(false);
        }
        CombineInstance[] instanceArray = combineInstances.ToArray();
        mf.mesh = new Mesh();
        mf.mesh.CombineMeshes(instanceArray);
        mf.mesh.RecalculateNormals();
        mf.mesh.RecalculateTangents();
        mf.mesh.RecalculateBounds();
        mc.sharedMesh = mf.mesh;
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
        Cellset.Add(startnode);
        while (Cellset.Count > 0)
        {
            Node randomNode = Cellset[Random.Range(0,Cellset.Count)];
            List<Node> neigbours = grid.Get4Neighbours(randomNode);

            int count = 0;
            foreach (Node node in neigbours)
            {
                if (node.walkable == true)count++;
            }
            if (count <2)
            {
                randomNode.walkable = true;
                foreach (Node node in neigbours)
                {
                    if (node.visited == false)Cellset.Add(node);
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
                Walls.Add(wall);
                wall.transform.position = node.worldPosition + new Vector3(0,2,0);
            }
        }
    }
    private void GenerateExit ()
    {
        //X
        for (int i = 0; i < grid.gridSizeX; i++)
        {
            Debug.Log("1");
            if (nodeGrid[i, 1].walkable)
            {
                Debug.Log("2");
                PossibleExits.Add(nodeGrid[i, grid.gridSizeY-2]);
            }
        }
        for (int i = 0; i < grid.gridSizeX; i++)
        {
            Debug.Log("3");
            if (nodeGrid[i, grid.gridSizeY - 2].walkable)
            {
                Debug.Log("4");
                PossibleExits.Add(nodeGrid[i, 1]);
            }
        }
        //Y
        for (int i = 0; i < grid.gridSizeY; i++)
        {
            Debug.Log("5");
            if (nodeGrid[1, i].walkable)
            {
                Debug.Log("6");
                PossibleExits.Add(nodeGrid[grid.gridSizeX-2, i]);
            }
        }
        for (int i = 0; i < grid.gridSizeY; i++)
        {
            //Debug.Log("155: "+ i + " "+nodeGrid[grid.gridSizeX - 1, i].walkable);
            Debug.Log(nodeGrid.GetLength(0) + " " + nodeGrid.GetLength(1) + " " + (grid.gridSizeX - 1) + " " + i);
            if (nodeGrid[grid.gridSizeX - 2, i].walkable)
            {
                Debug.Log("Function: "+i);
                PossibleExits.Add(nodeGrid[1, i]);
            }
        }
        if (PossibleExits.Count <= 0) SceneManager.LoadScene(0);
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
        EndNode = grid.NodeFromWorldPoint(path[0]);
        Debug.Log("End");
        ClearEnd(EndNode);
    }
}
