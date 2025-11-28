using UnityEngine;

public class TeaPotDrag : MonoBehaviour
{
    private bool isHeld = false;
    private bool canDrop = false;
    private Vector3 startPosition;

    // NEW: We need to talk to the Logic script
    private TeaPotLogic teaLogic;

    void Start()
    {
        startPosition = transform.position;

        // Find the logic script attached to this same object
        teaLogic = GetComponent<TeaPotLogic>();
    }

    void Update()
    {
        // --- PART 1: PICKUP (Only check if NOT holding) ---
        if (isHeld == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(clickPos, Vector2.zero);

                // Check if we clicked THIS pot
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    // --- NEW CHECK: Only pick up if the tea is boiled! ---
                    if (teaLogic != null && teaLogic.IsReady())
                    {
                        isHeld = true;
                        canDrop = false;
                    }
                    else
                    {
                        Debug.Log("The tea is not ready yet!"); // Optional feedback
                    }
                }
            }
        }
        else
        // --- PART 2: CARRYING & DROPPING (Only if holding) ---
        {
            // 1. Follow the mouse
            Vector2 mousePos = Input.mousePosition;
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = worldPos;

            // 2. Safety: Wait for the player to let go of the pickup click
            if (Input.GetMouseButtonUp(0))
            {
                canDrop = true;
            }

            // 3. Drop: Player clicks a SECOND time
            if (canDrop == true && Input.GetMouseButtonDown(0))
            {
                isHeld = false;
                CheckDrop();
            }
        }
    }

    void CheckDrop()
    {
        gameObject.SetActive(false);
        Vector2 dropPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(dropPos, Vector2.zero);
        gameObject.SetActive(true);

        if (hit.collider != null && hit.collider.CompareTag("StoveTop"))
        {
            startPosition = transform.position;
        }
        else
        {
            transform.position = startPosition;
        }
    }
}