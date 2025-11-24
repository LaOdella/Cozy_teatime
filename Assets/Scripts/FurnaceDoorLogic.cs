using UnityEngine;

public class FurnaceDoorLogic : MonoBehaviour
{
    // --- VARIABLES (Assigned in the Inspector) ---
    [SerializeField] private Sprite fireOnSprite;
    [SerializeField] private Sprite fireOffSprite;

    // --- INTERNAL STATE ---
    private bool isFireOn = false;

    // IMPORTANT: We delete the 'Start()' method, as it was failing to hold the reference!

    // --- PUBLIC METHOD (Called by DragLog.cs) ---
    public void ToggleFire()
    {
        // FORCE THE COMPONENT LOOKUP HERE, ensuring it is fresh every time.
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Check if the lookup succeeded before using it
        if (spriteRenderer == null)
        {
            Debug.LogError("FurnaceDoorLogic failed: SpriteRenderer component is missing!");
            return;
        }

        isFireOn = !isFireOn; // Flip the current state

        if (isFireOn)
        {
            spriteRenderer.sprite = fireOnSprite;
        }
        else
        {
            spriteRenderer.sprite = fireOffSprite;
        }
    }
}