using UnityEngine;

public class Display : MonoBehaviour {

    public Color[] palette = new Color[] {
        new Color(0, 0, 0),
        new Color(255, 255, 255),
        new Color(136, 0, 0),
        new Color(170, 255, 238),
        new Color(204, 68, 204),
        new Color(0, 204, 85),
        new Color(0, 0, 170),
        new Color(238, 238, 119),
        new Color(221, 136, 85),
        new Color(102, 68, 0),
        new Color(255, 119, 119),
        new Color(51, 51, 51),
        new Color(119, 119, 119),
        new Color(170, 255, 102),
        new Color(0, 136, 255),
        new Color(187, 187, 187)
    };

    private Texture2D texture;

    [SerializeField]
    private Vector2Int pos;

    [SerializeField]
    private Vector2Int resolution;

    void Awake() {
        texture = new Texture2D(resolution.x, resolution.y, TextureFormat.RGB24, false);
    }

    public void DrawLine(int x1, int y1, int x2, int y2, Color color) {
        int deltaX = Mathf.Abs(x2 - x1);
        int signX = x1 < x2 ? 1 : -1;
        int deltaY = -Mathf.Abs(y2 - y1);
        int signY = y1 < y2 ? 1 : -1;
        int err = deltaX + deltaY;

        while (true) {
            texture.SetPixel(x1, y1, color);
            if (x1 == x2 && y1 == y2) break;

            int e2 = 2 * err;

            if (e2 >= deltaY) {
                err += deltaY;
                x1 += signX;
            }

            if (e2 <= deltaX) {
                err += deltaX;
                y1 += signY;
            }
        }
        texture.Apply();
    }

    public void DrawLine(int x1, int y1, int x2, int y2, int color) {
        DrawLine(x1, y1, x2, y2, palette[color]);
    }

    public void PlotPixel(int x, int y, Color color) {
        texture.SetPixel(x, y, color);
        texture.Apply();
    }

    public void PlotPixel(int x, int y, int color) {
        PlotPixel(x, y, palette[color]);
    }

    void OnGUI() {
        if (Event.current.type.Equals(EventType.Repaint)) {
            Graphics.DrawTexture(new Rect(pos, resolution), texture);
        }
    }

}