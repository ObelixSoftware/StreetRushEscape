using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopHudDebug : MonoBehaviour
{
    public Text copCountText;
    public CopManager copManager;

    void Awake()
    {
        if (copManager == null)
        {
            copManager = FindObjectOfType<CopManager>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int copCount = copManager.GetActiveCops();
        int maxCops = copManager.GetMaxCopsAllowed();
        int copArrayCount = CopDriveHandler.Cops.Count;

       // Debug.Log($"HUD Update: {copCount}/{maxCops}");

        copCountText.text = $"Cops: {copCount} / {maxCops} / {copArrayCount}";
    }
}
