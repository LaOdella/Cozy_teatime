using UnityEngine;

using TMPro; // Needed for text

public class NPCLogic : MonoBehaviour
{
    [Header("Settings")]
    public TMP_Text dialogueText; // Where the NPC speaks

    // A list of things the NPC might want (we fill this in the Inspector)
    public string[] validProperties;

    // What the NPC wants RIGHT NOW
    private string currentRequest;

    void Start()
    {
        GenerateOrder();
    }

    public void GenerateOrder()
    {
        // 1. Pick a random property from our list
        int randomIndex = Random.Range(0, validProperties.Length);
        currentRequest = validProperties[randomIndex];

        // 2. Update the text to show the order
        // (For the prototype, they just ask directly for the property)
        if (dialogueText != null)
        {
            dialogueText.text = "I need something " + currentRequest + ".";
        }
    }

    // We will call this when the Cup is dropped on the NPC
    public void ServeTea(CupLogic cup)
    {
        // Check if the cup has tea inside
        if (cup.teaInside != null)
        {
            // COMPARE: Does the tea's property match the request?
            if (cup.teaInside.property == currentRequest)
            {
                dialogueText.text = "Perfect! Just what I needed.";
                Debug.Log("WIN: Correct Tea Served!");
            }
            else
            {
                dialogueText.text = "Hmm... this isn't what I wanted.";
                Debug.Log("FAIL: Wrong Property.");
            }
        }
        else
        {
            dialogueText.text = "This cup is empty!";
        }
    }
}