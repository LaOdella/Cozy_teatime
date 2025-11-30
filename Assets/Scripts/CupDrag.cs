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
        // --- PICKUP ---
        if (isHeld == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Priority: Don't pick up if Spoon is active
                if (FindAnyObjectByType<SpoonLogic>() != null) return;

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
        // --- CARRYING ---
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

        // 1. Bin (Trash)
        if (hit.collider != null && hit.collider.CompareTag("Bin"))
        {
            BinLogic bin = hit.collider.GetComponent<BinLogic>();
            if (bin != null) bin.OpenAndClose();
            if (cupLogic != null) cupLogic.EmptyCup();
            transform.position = startPosition;
        }
        // 2. Customer (Serve) -- NEW!
        else if (hit.collider != null && hit.collider.CompareTag("Customer"))
        {
            NPCLogic npc = hit.collider.GetComponent<NPCLogic>();
            if (npc != null && cupLogic != null)
            {
                // Serve the tea logic
                npc.ServeTea(cupLogic);

                // Empty the cup and return it to table
                cupLogic.EmptyCup();
            }
            transform.position = startPosition;
        }
        // 3. Missed? (Go Home)
        else
        {
            transform.position = startPosition;
        }
    }
}