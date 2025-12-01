using UnityEngine;

public class CupLogic : MonoBehaviour
{
    // --- MEMORY ---
    public TeaIngredient teaInside;
    public bool hasWater = false;

    // --- VISUALS ---
    public Sprite emptyCupSprite; // Assign the empty cup image here!
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // --- FUNCTIONS ---

    public void AddLeaves(TeaIngredient newTea)
    {
        if (teaInside == null)
        {
            teaInside = newTea;
            Debug.Log("Cup now contains: " + newTea.teaName);
        }
        else
        {
            Debug.Log("Cup is already full!");
        }
    }

    public void AddWater()
    {
        if (teaInside != null)
        {
            hasWater = true;
            spriteRenderer.sprite = teaInside.brewedSprite;
            Debug.Log("Tea is Brewed!");
        }
    }

    // NEW: Called when thrown in the bin
    public void EmptyCup()
    {
        teaInside = null;
        hasWater = false;
        spriteRenderer.sprite = emptyCupSprite; // Reset to look empty
        Debug.Log("Cup emptied!");
    }
}

