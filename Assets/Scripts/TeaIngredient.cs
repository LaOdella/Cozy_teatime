using UnityEngine;

// This line allows us to right-click in the Project window and create a new Tea Ingredient
[CreateAssetMenu(fileName = "New Tea", menuName = "Tea System/Ingredient")]
public class TeaIngredient : ScriptableObject
{
    [Header("Tea Data")]
    public string teaName;      // e.g., "Lavender"

    [TextArea]
    public string description;  // e.g., "A calming purple flower."

    public string property;     // e.g., "Relaxing", "Warming", "Refreshing"

    public Sprite icon;         // The image for the jar/leaf
}