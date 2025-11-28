using UnityEngine;

public class DragLog : MonoBehaviour
{
    private bool isHeld = true;
    private bool canDrop = false;

    void Update()
    {
        // 1. Log follows the mouse while isHeld is true
        if (isHeld)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = worldPos;

            if (Input.GetMouseButtonUp(0))
            {
                canDrop = true;
            }
        }

        // 2. DROP LOGIC: If allowed to drop AND player clicks again
        if (canDrop && Input.GetMouseButtonDown(0))
        {
            // --- RAYCAST FIX: Ignore the log itself ---
            // Define the layer mask: Hit everything BUT the Log layer.
            int logLayerMask = 1 << LayerMask.NameToLayer("Log");
            int inverseMask = ~logLayerMask; // The tilde (~) inverts the mask

            // Perform the raycast check, applying the mask
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, inverseMask);
            // ------------------------------------------

            // Check if we hit the FURNACE DOOR
            if (hit.collider != null && hit.collider.CompareTag("FurnaceDoor"))
            {
                // SUCCESS: Notify the door and destroy the log
                FurnaceDoorLogic doorScript = hit.collider.GetComponent<FurnaceDoorLogic>();
                doorScript.ToggleFire();

                isHeld = false;
                Destroy(gameObject);
            }
            else
            {
                // FAILURE: Dropped elsewhere, log disappears
                Destroy(gameObject);
            }
        }
    }
}