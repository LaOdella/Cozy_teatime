using UnityEngine;

public class LogPile : MonoBehaviour
{
    // Drag your Log Prefab into this slot in the Inspector
    [SerializeField] private GameObject logPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            // Check if THIS object (the log pile) was the one clicked
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Spawn the log right where the player clicked
                Instantiate(logPrefab, worldPoint, Quaternion.identity);
            }
        }
    }
}