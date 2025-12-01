using UnityEngine;

public class TeaPotDrag : MonoBehaviour
{
    private bool isHeld = false;
    private bool canDrop = false;
    private Vector3 startPosition;

    // Reference to the logic script
    private TeaPotLogic teaLogic;

    void Start()
    {
        startPosition = transform.position;
        teaLogic = GetComponent<TeaPotLogic>();
    }

    void Update()
    {
        // --- PART 1: PICKUP ---
        if (isHeld == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(clickPos, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    // Only pick up if boiled!
                    if (teaLogic != null && teaLogic.IsReady())
                    {
                        isHeld = true;
                        canDrop = false;
                    }
                    else
                    {
                        Debug.Log("The tea is not ready yet!");
                    }
                }
            }
        }
        else
        // --- PART 2: CARRYING ---
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = worldPos;

            if (Input.GetMouseButtonUp(0))
            {
                canDrop = true;
            }

            if (canDrop == true && Input.GetMouseButtonDown(0))
            {
                isHeld = false;
                CheckDrop();
            }
        }
    }

    void CheckDrop()
    {
        gameObject.SetActive(false); // Hide pot to see what's behind

        Vector2 dropPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(dropPos, Vector2.zero);

        gameObject.SetActive(true);

        // 1. Did we hit the Stove? (Stay there)
        if (hit.collider != null && hit.collider.CompareTag("StoveTop"))
        {
            startPosition = transform.position;
        }
        // 2. Did we hit a Cup? (Pour and go back)
        else if (hit.collider != null && hit.collider.CompareTag("Cup"))
        {
            CupLogic cup = hit.collider.GetComponent<CupLogic>();

            if (cup != null)
            {
                cup.AddWater(); // POUR!
            }

            // Snap back to the stove automatically
            transform.position = startPosition;
        }
        // 3. Missed everything? (Go back)
        else
        {
            transform.position = startPosition;
        }
    }
}