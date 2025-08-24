using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideWindow : MonoBehaviour
{
    [Header("Window Settings")]
    [SerializeField] private GameObject missionWindow; // Reference to the Mission Window Panel
    [SerializeField] private float hideDelay = 5f; // Time in seconds before hiding the window
    
    [Header("Optional Settings")]
    [SerializeField] private bool hideOnStart = true; // Whether to start the hide timer on Start
    
    private Coroutine hideCoroutine;
    
    void Start()
    {
        Debug.Log("HideWindow: Start() method called");
        
        if (hideOnStart)
        {
            Debug.Log("HideWindow: hideOnStart is true, calling StartHideTimer()");
            StartHideTimer();
        }
        else
        {
            Debug.Log("HideWindow: hideOnStart is false, not starting timer automatically");
        }
        
        // Debug logging to help troubleshoot
        if (missionWindow == null)
        {
            Debug.LogError("HideWindow: Mission Window reference is not set! Please assign it in the Inspector.");
        }
        else
        {
            Debug.Log($"HideWindow: Mission Window assigned. Will hide after {hideDelay} seconds.");
        }
    }
    
    /// <summary>
    /// Starts the hide timer for the mission window
    /// </summary>
    public void StartHideTimer()
    {
        Debug.Log("HideWindow: StartHideTimer() called");
        
        if (missionWindow != null)
        {
            // Stop any existing coroutine
            if (hideCoroutine != null)
            {
                Debug.Log("HideWindow: Stopping existing coroutine");
                StopCoroutine(hideCoroutine);
            }
            
            // Start the hide timer
            Debug.Log("HideWindow: Starting new coroutine");
            hideCoroutine = StartCoroutine(HideWindowAfterDelay());
            
            if (hideCoroutine != null)
            {
                Debug.Log("HideWindow: Coroutine started successfully");
            }
            else
            {
                Debug.LogError("HideWindow: Failed to start coroutine!");
            }
        }
        else
        {
            Debug.LogError("HideWindow: Mission Window reference is not set!");
        }
    }
    
    /// <summary>
    /// Starts the hide timer with a custom delay
    /// </summary>
    /// <param name="customDelay">Custom delay in seconds</param>
    public void StartHideTimer(float customDelay)
    {
        if (missionWindow != null)
        {
            // Stop any existing coroutine
            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
            }
            
            // Start the hide timer with custom delay
            hideCoroutine = StartCoroutine(HideWindowAfterDelay(customDelay));
        }
        else
        {
            Debug.LogWarning("HideWindow: Mission Window reference is not set!");
        }
    }
    
    /// <summary>
    /// Immediately hides the mission window
    /// </summary>
    public void HideWindowImmediately()
    {
        if (missionWindow != null)
        {
            missionWindow.SetActive(false);
            Debug.Log("HideWindow: Mission Window hidden successfully.");
        }
        else
        {
            Debug.LogError("HideWindow: Cannot hide window - Mission Window reference is null!");
        }
    }
    
    /// <summary>
    /// Shows the mission window
    /// </summary>
    public void ShowWindow()
    {
        if (missionWindow != null)
        {
            missionWindow.SetActive(true);
        }
    }
    
    private IEnumerator HideWindowAfterDelay()
    {
        Debug.Log($"HideWindow: Starting hide timer for {hideDelay} seconds...");
        yield return new WaitForSeconds(hideDelay);
        Debug.Log("HideWindow: Hide timer completed, hiding window...");
        HideWindowImmediately();
    }
    
    private IEnumerator HideWindowAfterDelay(float customDelay)
    {
        yield return new WaitForSeconds(customDelay);
        HideWindowImmediately();
    }
    
    /// <summary>
    /// Test method to manually trigger the hide timer (for debugging)
    /// </summary>
    [ContextMenu("Test Hide Timer")]
    public void TestHideTimer()
    {
        Debug.Log("HideWindow: TestHideTimer() called manually");
        StartHideTimer();
    }
    
    void OnDestroy()
    {
        // Clean up coroutine if object is destroyed
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }
    }
}
