using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneration : MonoBehaviour
{
    Grid grid;
    Node[,] nodeGrid;
    private List<Node>Cellset;
    private void Start ()
    {
        grid = GetComponent<Grid>();
        nodeGrid = grid.grid;
        Cellset = new List<Node>();
        GenerateMaze();
    }
    //private void GenerateMaze ()
    //{
    //    Node startnode = nodeGrid[5,5];
    //    List<Node> neigbours = grid.Get4Neighbours(startnode);
    //    foreach (var item in neigbours)
    //    {
    //        item.walkable = false;
    //    }
    //}
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
                Debug.Log("Has less then 2 neigbouring walls");
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
            else
            {
                Debug.Log("Has more then 2 neigbouring walls");
            }
            Cellset.Remove(randomNode);
        }
    }
}
