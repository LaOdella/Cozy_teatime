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
                // Priority Check: Block pickup if Spoon is active
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
        // --- PART 2: CARRYING ---
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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

        // Check for Customer interaction first
        if (hit.collider != null && hit.collider.CompareTag("Customer"))
        {
            NPCLogic npc = hit.collider.GetComponent<NPCLogic>();

            if (npc != null && cupLogic != null)
            {
                // CRITICAL FIX: Call ServeTea and capture the TRUE/FALSE acceptance result
                bool accepted = npc.ServeTea(cupLogic);

                if (accepted)
                {
                    // IF ACCEPTED (Win/Neutral): Empty the cup data
                    cupLogic.EmptyCup();
                }

                // The cup snaps back to the service table regardless of acceptance
                // (but only empties if accepted)
                transform.position = startPosition;
            }
        }
        // Bin Interaction (Always empties the cup)
        else if (hit.collider != null && hit.collider.CompareTag("Bin"))
        {
            BinLogic bin = hit.collider.GetComponent<BinLogic>();
            if (bin != null) bin.OpenAndClose();

            // Bin always empties the cup's data
            if (cupLogic != null) cupLogic.EmptyCup();

            transform.position = startPosition;
        }
        // Missed everything? (Go Home)
        else
        {
            transform.position = startPosition;
        }
    }
}