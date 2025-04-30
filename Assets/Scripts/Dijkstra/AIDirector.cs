using System.Collections.Generic;
using UnityEngine;

public class AIDirector : MonoBehaviour
{
    public Dijkstra pathFinder;
    public GridGenerator grid;

    void Start()
    {
        grid.GenerateGrid();

        Node[] nodes = FindObjectsByType<Node>(FindObjectsSortMode.None);

        List<Node> path = pathFinder.FindShortestPath(nodes[Random.Range(0, nodes.Length)], nodes[Random.Range(0, nodes.Length)]);
        pathFinder.DebugPath(path);
    }

    void Update()
    {
        
    }
}
