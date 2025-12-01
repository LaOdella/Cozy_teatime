using UnityEngine;
using TMPro;
using System.Collections;

public class NPCLogic : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text dialogueText;

    [Header("Possible Requests")]
    public string[] validProperties;
    private string currentRequest;

    void Start()
    {
        GenerateOrder();
    }

    public void GenerateOrder()
    {
        // Pick a random property from the Inspector list
        int randomIndex = Random.Range(0, validProperties.Length);
        currentRequest = validProperties[randomIndex];

        // Call the function to display the text
        DisplayCurrentRequest();
    }

    // NEW: Function to display the request text without generating a new request
    public void DisplayCurrentRequest()
    {
        if (dialogueText != null)
        {
            dialogueText.text = "I need something " + currentRequest + ".";
        }
    }

    // Called by the CupDrag script when the cup is dropped on the NPC
    public bool ServeTea(CupLogic cup)
    {
        // ... (Dry/Empty checks remain the same) ...
        if (cup.teaInside == null)
        {
            dialogueText.text = "This cup is empty! Where's my tea?";
            return false;
        }
        if (!cup.hasWater)
        {
            dialogueText.text = "It's cold and dry. Did you forget the water?";
            return false;
        }

        // 2. Ready to Serve (Has water and leaves)
        string receivedProperty = cup.teaInside.property;

        // A. IDEAL MATCH (Win)
        if (receivedProperty == currentRequest)
        {
            dialogueText.text = "Perfect! Just what I needed.";
            Debug.Log("WIN: Correct Tea Served!");
            StartCoroutine(WaitAndGenerateNewOrder());
            return true;
        }
        // B. ACCEPTABLE MATCH (Simple Tea Override - Neutral Acceptance)
        else if (receivedProperty == "Simple")
        {
            dialogueText.text = "That'll do I guess... it's a bit too simple.";
            Debug.Log("NEUTRAL: Simple Tea served.");
            StartCoroutine(WaitAndGenerateNewOrder());
            return true;
        }
        // C. HARD REFUSAL (Wrong specialty tea)
        else
        {
            // Show rejection message temporarily
            dialogueText.text = "Hmm... this isn't what I wanted...";
            Debug.Log("REJECTED: Wrong Specialty Property.");

            // NEW: Start timer to revert dialogue back to the original request
            StartCoroutine(RevertToRequest(3.0f));

            return false; // Rejected (Cup snaps back full)
        }
    }

    // NEW: Coroutine to revert the text after a delay
    private IEnumerator RevertToRequest(float delay)
    {
        yield return new WaitForSeconds(delay);
        DisplayCurrentRequest(); // Re-displays the original order
    }

    // Existing Coroutine for new order generation
    private IEnumerator WaitAndGenerateNewOrder()
    {
        yield return new WaitForSeconds(3f);
        GenerateOrder();
    }
}