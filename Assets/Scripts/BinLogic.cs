using UnityEngine;
using System.Collections;

public class BinLogic : MonoBehaviour
{
    [SerializeField] private Sprite binClosed;
    [SerializeField] private Sprite binOpen;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = binClosed; // Start closed
    }

    // Public method for the Cup to call
    public void OpenAndClose()
    {
        StartCoroutine(AnimateBin());
    }

    private IEnumerator AnimateBin()
    {
        // 1. Open
        spriteRenderer.sprite = binOpen;

        // 2. Wait for 0.5 seconds (visual feedback)
        yield return new WaitForSeconds(0.2f);

        // 3. Close
        spriteRenderer.sprite = binClosed;
    }
}
