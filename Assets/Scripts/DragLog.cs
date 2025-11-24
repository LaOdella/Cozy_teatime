using UnityEngine;

public class DragLog : MonoBehaviour
{
    private bool isHeld = true;      // Controls movement (Log follows mouse when true)
    private bool canDrop = false;    // Safety flag: Prevents the log from dropping on the same frame it spawns

    void Update()
    {
        // 1. Follow the mouse while isHeld is true
        if (isHeld)
        {
            // Convert screen pixel position to a position in the game world
            Vector2 mousePos = Input.mousePosition;
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = worldPos;

            // Wait for mouse button to be released after the log was spawned
            if (Input.GetMouseButtonUp(0))
            {
                canDrop = true;
            }
        }

        // 2. DROP LOGIC: If allowed to drop AND player clicks again
        if (canDrop && Input.GetMouseButtonDown(0))
        {
            // Perform a manual raycast (a "laser shot") at the cursor's position
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            // Check if we hit the FURNACE DOOR
            if (hit.collider != null && hit.collider.CompareTag("FurnaceDoor"))
            {
                // --- SUCCESSFUL DROP ON DOOR ---

                // 1. Find the FurnaceDoorLogic script on the object we just hit
                FurnaceDoorLogic doorScript = hit.collider.GetComponent<FurnaceDoorLogic>();

                // 2. Call the public method to change the sprite state
                doorScript.ToggleFire();

                isHeld = false; // Stop following mouse

                // Destroy the log, as its job is done
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