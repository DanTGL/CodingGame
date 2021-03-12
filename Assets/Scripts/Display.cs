using UnityEngine;

public class Display : MonoBehaviour {

    public Texture2D texture;

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

    void OnGUI() {
        if (Event.current.type.Equals(EventType.Repaint)) {
            Graphics.DrawTexture(new Rect(pos, resolution), texture);
        }
    }

}