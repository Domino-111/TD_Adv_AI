using System.Collections.Generic;
using UnityEngine;

public class Dijkstra : MonoBehaviour
{
    public Node startNode;
    public Node endNode;

    protected Node[] nodesInScene;

    protected void GetAllNodes()
    {
        nodesInScene = FindObjectsByType<Node>(FindObjectsSortMode.None);
    }

    private void Awake()
    {
        GetAllNodes();
    }

    public void DebugPath (List<Node> path)
    {
        for (int i = 0; i < path.Count - 1; i++)
        {
            Debug.DrawLine(path[i].transform.position, path[i + 1].transform.position, Color.green, 10f);
        }
    }

    public List<Node> FindShortestPath(Node start, Node goal)
    {
        if (RunAlgorithm(start, goal))
        {
            List<Node> results = new List<Node>();
            Node current = goal;
            do
            {
                results.Insert(0, current);
                current = current.PreviousNode;
            }

            while (current != null);
            {
                return results;
            }
        }

        return null;
    }

    protected virtual bool RunAlgorithm(Node start, Node goal)
    {
        List<Node> unexplored = new List<Node>();
        Node sNode = start;     //Start
        Node eNode = goal;     //End

        SetUnexplored(ref unexplored);

        sNode.pathWeightProperty = 0;
        while (unexplored.Count > 0)
        {
            unexplored.Sort((a, b) => a.pathWeightProperty.CompareTo(b.pathWeightProperty));
            Node current = unexplored[0];
            unexplored.RemoveAt(0);

            foreach (var neighbourNode in current.Neighbours)
            {
                if (!unexplored.Contains(neighbourNode))
                {
                    continue;
                }

                float neighbourWeight = Vector3.Distance(current.transform.position, neighbourNode.transform.position);

                neighbourWeight += current.pathWeightProperty;

                if (neighbourWeight < neighbourNode.pathWeightProperty)
                {
                    neighbourNode.pathWeightProperty = neighbourWeight;
                    neighbourNode.PreviousNode = current;
                }
            }

            if (current == eNode)
            {
                return true;
            }
        }

        return false;
    }

    void SetUnexplored(ref List<Node> unexplored)
    {
        foreach (var node in nodesInScene)
        {
            node.ResetNode();
            unexplored.Add(node);
        }
    }
}
