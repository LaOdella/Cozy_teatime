using UnityEngine;

public class DragLog : MonoBehaviour
{
    void Update()
    {
        // 1. Get the mouse position in pixel coordinates (Screen space)
        Vector2 mousePos = Input.mousePosition;

        // 2. Convert pixels to World coordinates (Game space)
        // We use Camera.main because it knows how to translate the screen to the game world.
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        // 3. Move the object to the calculated position
        transform.position = worldPos;
    }
}
