using UnityEngine;
using System.Collections;
using TMPro;

public class BookLogic : MonoBehaviour
{
    private Animator animator;
    public bool isListening = false;
    public TMP_Text bookText;

    // --- SETTINGS ---
    [Header("Animation Settings")]
    [Range(0.1f, 1f)]
    public float playbackSpeed = 0.4f; // Default to slower speed

    void Start()
    {
        animator = GetComponent<Animator>();
        ResetBook(); // Sets book to Frame 0 (Open/Idle)
    }

    void Update()
    {
        // Check for click on the BOOK
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
                    // Book is ready for you to click a jar
                    if (bookText != null) bookText.text = "Select a jar...";
                }
                else
                {
                    // Book goes back to idle (text clears, but book stays "open")
                    if (bookText != null) bookText.text = "";
                }
            }
        }
    }

    // Called by TeaJar
    public void ReadTea(TeaIngredient tea)
    {
        // 1. Play the "Page Turn" animation
        StartCoroutine(AnimatePageTurn());

        // 2. Show the info
        string message = tea.teaName + ": " + tea.property + "\n" + tea.description;
        if (bookText != null) bookText.text = message;

        // 3. Stop listening (require a click to ask again)
        isListening = false;
    }

    private IEnumerator AnimatePageTurn()
    {
        // Play Animation at chosen speed
        animator.speed = playbackSpeed;
        animator.Play("BookOpen", 0, 0f);

        // Calculate wait time based on speed
        float waitTime = 1.0f / playbackSpeed;

        yield return new WaitForSeconds(waitTime);

        // Reset to Frame 0 (Idle)
        ResetBook();
    }

    private void ResetBook()
    {
        animator.Play("BookOpen", 0, 0f);
        animator.speed = 0; // Pause at frame 0
    }
}