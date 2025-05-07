using System.Collections.Generic;
using UnityEngine;

public class AStar : Dijkstra
{
    protected void SetUpHeuristic()
    {

    }

    protected override bool RunAlgorithm(Node start, Node goal)
    {
        List<Node> unexplored = new List<Node>();
        Node sNode = start;     //Start
        Node eNode = goal;     //End

        SetUnexplored(ref unexplored);

        sNode.pathWeightProperty = 0;
        while (unexplored.Count > 0)
        {
            unexplored.Sort((a, b) => a.pathHeuristicWeight.CompareTo(b.pathHeuristicWeight));
            Node current = unexplored[0];
            unexplored.RemoveAt(0);

            //Debug.Log(current.name);

            foreach (var neighbourNode in current.Neighbours)
            {
                if (!unexplored.Contains(neighbourNode))
                {
                    continue;
                }

                neighbourNode.SetHeuristic(eNode.transform.position);

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
}
