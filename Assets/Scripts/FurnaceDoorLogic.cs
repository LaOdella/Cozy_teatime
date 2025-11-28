using UnityEngine;
using System.Collections;

public class FurnaceDoorLogic : MonoBehaviour
{
    [SerializeField] private Sprite fireOnSprite;
    [SerializeField] private Sprite fireOffSprite;

    [SerializeField] private float fireDuration = 30f;
    private Coroutine fireTimerRoutine;

    private SpriteRenderer spriteRenderer;
    private bool isFireOn = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = fireOffSprite;
        isFireOn = false;
    }

    public void ToggleFire()
    {
        // --- NOTE: Debug.Log line removed for clean console ---

        isFireOn = !isFireOn; // Flip the current state

        if (isFireOn)
        {
            spriteRenderer.sprite = fireOnSprite;

            // Start the timer routine
            if (fireTimerRoutine != null) StopCoroutine(fireTimerRoutine);
            fireTimerRoutine = StartCoroutine(FireTimer());
        }
        else
        {
            spriteRenderer.sprite = fireOffSprite;

            // Stop the timer immediately if the player clicks it off early
            if (fireTimerRoutine != null) StopCoroutine(fireTimerRoutine);
            fireTimerRoutine = null;
        }
    }

    private IEnumerator FireTimer()
    {
        yield return new WaitForSeconds(fireDuration);

        if (isFireOn)
        {
            ToggleFire();
        }

        fireTimerRoutine = null;
    }
}