using UnityEngine;

public class DrawGrid : MonoBehaviour
{
    public int gridSizeX = 1, gridSizeY = 1;
    public float cellSize =1;
    public Material lineMaterial;

    void Start()
    {
        Draw();
    }

    void Draw()
    {
        for (int i = 0; i < gridSizeX + 1; i++)
        {
            GameObject line = new GameObject("Line");
            line.transform.SetParent(this.transform, false);
            LineRenderer lr = line.AddComponent<LineRenderer>();
            lr.material = lineMaterial;
            lr.startWidth = 0.05f;
            lr.endWidth = 0.05f;
            lr.SetPosition(0, new Vector3(i * cellSize, 0, 0));
            lr.SetPosition(1, new Vector3(i * cellSize, gridSizeY * cellSize, 0));
        }

        for (int j = 0; j < gridSizeY + 1; j++)
        {
            GameObject line = new GameObject("Line");
            line.transform.SetParent(this.transform, false);
            LineRenderer lr = line.AddComponent<LineRenderer>();
            lr.material = lineMaterial;
            lr.startWidth = 0.05f;
            lr.endWidth = 0.05f;
            lr.SetPosition(0, new Vector3(0, j * cellSize, 0));
            lr.SetPosition(1, new Vector3(gridSizeX * cellSize, j * cellSize, 0));
        }
    }
}
