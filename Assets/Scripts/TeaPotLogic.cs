using UnityEngine;

public class TeaPotLogic : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FurnaceDoorLogic furnaceScript;
    [SerializeField] private GameObject smokeEffect;

    [Header("Settings")]
    [SerializeField] private float timeToBoil = 10f;   // Time to heat up
    [SerializeField] private float boilingWindow = 10f; // Time it STAYS hot (New!)

    // --- TRACKING DATA ---
    private float currentTimer = 0f;     // Tracks heating progress
    private float boilWindowTimer = 0f;  // Tracks how long it's been boiling
    private bool isBoiled = false;

    void Start()
    {
        if (smokeEffect != null) smokeEffect.SetActive(false);
        isBoiled = false;
        currentTimer = 0f;
        boilWindowTimer = 0f;
    }

    void Update()
    {
        // --- PHASE 1: ALREADY BOILED? (The Window) ---
        if (isBoiled)
        {
            // Start the "Window" timer
            boilWindowTimer += Time.deltaTime;

            // Did we miss the window?
            if (boilWindowTimer >= boilingWindow)
            {
                // Turn it off!
                isBoiled = false;
                boilWindowTimer = 0f;
                currentTimer = 0f; // Reset heating progress completely

                if (smokeEffect != null) smokeEffect.SetActive(false);
                Debug.Log("Tea got cold!");
            }

            return; // Exit here so we don't run the heating logic below
        }

        // --- PHASE 2: HEATING UP ---
        if (furnaceScript != null && furnaceScript.IsFireBurning())
        {
            currentTimer += Time.deltaTime;

            if (currentTimer >= timeToBoil)
            {
                BoilWater();
            }
        }
    }

    void BoilWater()
    {
        isBoiled = true;
        boilWindowTimer = 0f; // Reset the window timer

        if (smokeEffect != null) smokeEffect.SetActive(true);
        Debug.Log("Tea is Boiled!");
    }

    public bool IsReady()
    {
        return isBoiled;
    }
}
