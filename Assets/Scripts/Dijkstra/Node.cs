using System.Collections.Generic;
using UnityEngine;

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
}
