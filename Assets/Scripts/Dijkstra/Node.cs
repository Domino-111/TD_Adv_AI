using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Node : MonoBehaviour
{
    public List<Node> Neighbours;

    private float pathWeight = float.PositiveInfinity;
    public float pathWeightProperty
    {
        get => pathWeight;

        set => pathWeight = value;
    }

    public Node PreviousNode { get; set; }

    private float heuristic;
    public float Heuristic
    {
        get => heuristic;
        set => heuristic = value;
    }

    public float pathHeuristicWeight
    {
        get => pathWeight + heuristic;
    }

    public float SetHeuristic(Vector3 goal)
    {
        heuristic = Vector3.Distance(transform.position, goal);
        return heuristic;
    }

    public void ResetNode()
    {
        pathWeight = float.PositiveInfinity;
        PreviousNode = null;
    }

    private void OnDrawGizmos()
    {
        if (Neighbours == null)
        {
            return;
        }

        float radius = 0.2f;

        Gizmos.color = Color.blue;
        foreach (var node in Neighbours)
        {
            if (node == null)
            {
                continue;
            }

            Vector3 direction = node.transform.position - transform.position;
            Vector3 right = Vector3.Cross(direction, Vector3.up).normalized * 0.03f;

            Gizmos.DrawRay(transform.position + right, direction);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, radius);
    }

    private void OnValidate() => ValidateNeighbours();

    private void ValidateNeighbours()
    {
        foreach (var node in Neighbours)
        {
            if (node == null)
            {
                continue;
            }

            if (!node.Neighbours.Contains(this))
            {
                node.Neighbours.Add(this);
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var node in Neighbours)
        {
            if (node.Neighbours.Contains(this))
            {
                node.Neighbours.Remove(this);
            }
        }
    }

    [ContextMenu("Validate Grid")]
    public void ValidateGrid()
    {
        Node[] nodesInScene = FindObjectsByType<Node>(FindObjectsSortMode.None);

        foreach (var node in nodesInScene)
        {
            
        }
    }
}
