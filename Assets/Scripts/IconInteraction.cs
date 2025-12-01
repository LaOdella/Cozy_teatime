using UnityEngine;
using UnityEngine.SceneManagement; // Crucial for loading scenes

public class IconInteraction : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite unlitSprite; // The default state
    public Sprite litSprite;   // The state when clicked

    [Header("Action")]
    public string sceneToLoad; // e.g., "Garden"

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Ensure the icon starts in the unlit state
        spriteRenderer.sprite = unlitSprite;
    }

    // This runs automatically when the collider is clicked
    void OnMouseDown()
    {
        // 1. Give visual feedback
        spriteRenderer.sprite = litSprite;

        // 2. Load the scene immediately
        SceneManager.LoadScene(sceneToLoad);
    }
}