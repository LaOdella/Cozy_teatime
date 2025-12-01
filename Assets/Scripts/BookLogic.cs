using UnityEngine;
using System.Collections;
using TMPro;

public class BookLogic : MonoBehaviour
{
    private const string IDLE_MESSAGE = "Ask me about tea..";
    private const float MIN_READ_TIME = 4.0f; // NEW: Minimum time the text stays visible

    private Animator animator;
    public bool isListening = false;

    // --- REFERENCES ---
    public TMP_Text bookText;

    [Header("Animation Settings")]
    [Range(0.1f, 1f)]
    public float playbackSpeed = 0.4f;

    // NEW: Tracks the current text display timer
    private Coroutine textTimerRoutine;

    void Start()
    {
        animator = GetComponent<Animator>();
        ResetBook();
    }

    void Update()
    {
        // 1. Check for click on the BOOK
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Toggle Listening Mode
                isListening = !isListening;

                if (isListening)
                {
                    if (bookText != null) bookText.text = "Select a jar to begin reading...";
                }
                else
                {
                    // Stops timer if running and reverts to default prompt
                    if (textTimerRoutine != null) StopCoroutine(textTimerRoutine);
                    if (bookText != null) bookText.text = IDLE_MESSAGE;
                }
            }
        }
    }

    // Called by TeaJar 
    public void ReadTea(TeaIngredient tea)
    {
        // 1. Stop any existing text display timer 
        if (textTimerRoutine != null) StopCoroutine(textTimerRoutine);

        // 2. Start the 4-second display timer
        textTimerRoutine = StartCoroutine(DisplayHintForDuration(tea));

        // 3. Start the visual page turn animation 
        StartCoroutine(AnimateAndReset());

        isListening = false;
    }

    // NEW FUNCTION: Handles the text and the 4-second timer
    private IEnumerator DisplayHintForDuration(TeaIngredient tea)
    {
        // Set the text immediately
        string flavorText = GetMysticalHint(tea.property);
        string message = "— " + tea.teaName + " —\n" + flavorText;
        if (bookText != null) bookText.text = message;

        // Wait the minimum reading time (4 seconds)
        yield return new WaitForSeconds(MIN_READ_TIME);

        // After the minimum read time, reset the text
        if (bookText != null) bookText.text = IDLE_MESSAGE;
        textTimerRoutine = null;
    }

    // Coroutine for the visual page turn animation
    private IEnumerator AnimateAndReset()
    {
        animator.speed = playbackSpeed;
        animator.Play("BookOpen", 0, 0f);

        // Wait for animation duration (adjusts for playbackSpeed)
        float waitTime = 1.0f / playbackSpeed;
        yield return new WaitForSeconds(waitTime);

        // Reset the animation visually to frame 0
        ResetBookAnimation();
    }

    // Helper to pull text
    private string GetMysticalHint(string property)
    {
        switch (property)
        {
            case "Relaxing":
                return "A soothing warmth. Best used to quiet a racing mind and ease tension.";
            case "Sleepy":
                return "The deep night scent. Assists the body in drawing out tension for a faster path to slumber.";
            case "Warming":
                return "Stirs the inner core. Excellent for chasing away a deep chill and encouraging focus.";
            case "Earthy":
                return "The scent of grounded soil. Provides a reliable, foundational energy that anchors the spirit.";
            case "Simple":
                return "The clean palate. A neutral flavor recommended for balancing stronger herbs or starting a fresh day.";
            default:
                return "A forgotten page. This tea holds secrets yet to be revealed.";
        }
    }

    // Forces animation back to frame 0 and pauses it
    private void ResetBookAnimation()
    {
        animator.Play("BookOpen", 0, 0f);
        animator.speed = 0; // Pause at frame 0
    }

    // We keep ResetBook() clean for startup and reset
    private void ResetBook()
    {
        ResetBookAnimation();
        if (bookText != null) bookText.text = IDLE_MESSAGE;
    }
}