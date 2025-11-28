using UnityEngine;

public class TeaPotLogic : MonoBehaviour
{
    // --- CONNECTING THE PIECES ---
    [Header("References")]
    // We need to know about the fire to check if it's hot
    [SerializeField] private FurnaceDoorLogic furnaceScript;

    // We need the smoke object to turn it on when ready
    [SerializeField] private GameObject smokeEffect;

    // --- SETTINGS ---
    [SerializeField] private float timeToBoil = 10f; // How long it takes to boil

    // --- TRACKING DATA ---
    private float currentTimer = 0f;
    private bool isBoiled = false;

    void Start()
    {
        // Ensure smoke is hidden when the game starts
        if (smokeEffect != null)
        {
            smokeEffect.SetActive(false);
        }
        isBoiled = false;
        currentTimer = 0f;
    }

    void Update()
    {
        // 1. If we are already boiled, stop checking. We are done.
        if (isBoiled) return;

        // 2. Check: Is the furnace script attached? AND Is the fire burning?
        if (furnaceScript != null && furnaceScript.IsFireBurning())
        {
            // 3. Add time (1 second per second)
            currentTimer += Time.deltaTime;

            // 4. Have we reached 10 seconds?
            if (currentTimer >= timeToBoil)
            {
                BoilWater();
            }
        }
        else
        {
            // Optional: If fire goes out, do we cool down? 
            // For now, let's just pause the timer (do nothing).
        }
    }

    void BoilWater()
    {
        isBoiled = true;

        // Show the smoke!
        if (smokeEffect != null)
        {
            smokeEffect.SetActive(true);
        }

        Debug.Log("Tea is Boiled!");
    }

    // Helper function for the Drag Script later:
    public bool IsReady()
    {
        return isBoiled;
    }
}
