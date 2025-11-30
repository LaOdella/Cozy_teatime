using UnityEngine;

[CreateAssetMenu(fileName = "New Tea", menuName = "Tea System/Ingredient")]
public class TeaIngredient : ScriptableObject
{
    [Header("Tea Data")]
    public string teaName;      // e.g., "Lavender"

    [TextArea]
    public string description;  // e.g., "A calming purple flower."

    public string property;     // e.g., "Relaxing"

    [Header("Visuals")]
    public Sprite icon;         // The image for the Jar
    public Sprite brewedSprite; // The image for the Full Cup (NEW!)
}