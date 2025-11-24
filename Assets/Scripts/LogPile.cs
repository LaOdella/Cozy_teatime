using UnityEngine;
using UnityEngine.InputSystem;

public class LogPile : MonoBehaviour
{
    [SerializeField] private GameObject logPrefab;

    void Update()
    {
        // Check for click
        if (Input.GetMouseButtonDown(0))
        {
            // Calculate position
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Shoot the laser
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            // DIAGNOSTICS: Tell us what happened
            if (hit.collider != null)
            {
                Debug.Log("I HIT: " + hit.collider.name); // Keeps hitting something?

                if (hit.collider.gameObject == gameObject)
                {
                    Instantiate(logPrefab);
                }
            }
            else
            {
                Debug.Log("I HIT NOTHING"); // Keeps hitting air?
            }
        }
    }
}       

