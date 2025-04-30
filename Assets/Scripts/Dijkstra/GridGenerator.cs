using TMPro;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public Node prefab;

    public int rows = 10, columns = 10;
    public float gap = 1f;

    public void GenerateGrid()
    {
        Vector3 startPos = transform.position;

        Node prev = null;
        Node[] prevColumn = new Node[columns];

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                Vector3 newPosition = new Vector3(startPos.x + gap * x, 0f, startPos.z + gap * y);

                Node node = Instantiate(prefab, newPosition, Quaternion.identity);

                if (prev != null)
                {
                    node.Neighbours.Add(prev);
                    prev.Neighbours.Add(node);
                }

                if (prevColumn[y] != null)
                {
                    prevColumn[y].Neighbours.Add(node);
                    node.Neighbours.Add(prevColumn[y]);
                }

                prev = node;
                prevColumn[y] = node;
            }

            prev = null;
        }
    }
}
