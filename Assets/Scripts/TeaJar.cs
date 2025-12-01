using UnityEngine;

public class TeaJar : MonoBehaviour
{
    public TeaIngredient ingredientData; // The data (Lavender, etc.)
    public GameObject spoonPrefab;       // The specific spoon to spawn

    private BookLogic book; // Reference to the book in the scene

    void Start()
    {
        // Find the book automatically when the game starts
        book = FindAnyObjectByType<BookLogic>();
    }

    void Update()
    {
        // Check for left click
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            // Check if THIS jar was clicked
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // PRIORITY CHECK: Is the Book waiting for info?
                if (book != null && book.isListening)
                {
                    // A. Send info to the book (No Spoon!)
                    book.ReadTea(ingredientData);
                }
                else
                {
                    // B. Normal Game: Spawn the spoon
                    GameObject newSpoon = Instantiate(spoonPrefab, worldPoint, Quaternion.identity);
                    SpoonLogic logic = newSpoon.GetComponent<SpoonLogic>();

                    if (logic != null)
                    {
                        logic.teaData = ingredientData;
                    }
                }
            }
        }
    }
}