using UnityEngine;

public class CupDrag : MonoBehaviour
{
    private bool isHeld = false;
    private bool canDrop = false;
    private Vector3 startPosition;

    private CupLogic cupLogic;

    void Start()
    {
        startPosition = transform.position;
        cupLogic = GetComponent<CupLogic>();
    }

    void Update()
    {
        // --- PART 1: PICKUP ---
        if (isHeld == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // PRIORITY CHECK:
                // Is there a Spoon currently active in the scene?
                // If yes, we block the cup pickup so you can pour safely.
                // (Updated command for Unity 2025)
                if (FindAnyObjectByType<SpoonLogic>() != null)
                {
                    return; // Stop here. Do not pick up the cup.
                }

                // Normal Pickup Logic
                Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(clickPos, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    isHeld = true;
                    canDrop = false;
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
        gameObject.SetActive(false);

        Vector2 dropPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(dropPos, Vector2.zero);

        gameObject.SetActive(true);

        if (hit.collider != null && hit.collider.CompareTag("Bin"))
        {
            BinLogic bin = hit.collider.GetComponent<BinLogic>();
            if (bin != null) bin.OpenAndClose();

            if (cupLogic != null) cupLogic.EmptyCup();

            transform.position = startPosition;
        }
        else
        {
            transform.position = startPosition;
        }
    }
}