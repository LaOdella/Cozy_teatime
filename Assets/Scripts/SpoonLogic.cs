using UnityEngine;

public class SpoonLogic : MonoBehaviour
{
    private bool isHeld = true;

    // This variable holds the data (assigned by the jar)
    public TeaIngredient teaData;

    void Update()
    {
        if (isHeld)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = worldPos;

            if (Input.GetMouseButtonDown(0))
            {
                CheckInteraction();
            }
        }
    }

    void CheckInteraction()
    {
        // Hide spoon for a split second so the raycast sees what's behind it
        gameObject.SetActive(false);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        gameObject.SetActive(true);

        // Did we click the Cup?
        if (hit.collider != null && hit.collider.CompareTag("Cup"))
        {
            CupLogic cup = hit.collider.GetComponent<CupLogic>();

            if (cup != null)
            {
                // Pour the tea data into the cup
                cup.AddLeaves(teaData);

                // have leaves been dropped?
                Debug.Log("Success, Leaves dropped");

                // Destroy the spoon
                Destroy(gameObject);
            }
        }
        else
        {
            // Clicked nothing/air... Just destroy the spoon
            Destroy(gameObject);
        }
    }
}