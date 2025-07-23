using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopManager : MonoBehaviour
{
    public Transform playerTransform;
    public GameController gameController;
    public float despawnRange = 50f;

    void Awake()
    {
        //Find player car
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Car");
            if (playerObj != null)
                playerTransform = playerObj.transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var cop in CopDriveHandler.Cops.ToArray())
        {
            if (cop == null || !cop.isActive) continue;

            float dist = Vector2.Distance(cop.transform.position, playerTransform.position);
            if (dist > despawnRange)
            {
                cop.gameObject.SetActive(false);
            }
        }
    }

    public int GetMaxCopsAllowed()
    {
        return Mathf.Clamp(Mathf.RoundToInt(gameController.pursuitLevel / 10), 0, 10);
    }

    public int GetActiveCops()
    {
        int activeCops = 0;
        //Debug.Log(CopDriveHandler.Cops.Count);
        foreach (var cop in CopDriveHandler.Cops.ToArray())
        {
            if (cop.isActive) activeCops++;
        }
        return activeCops;
    }
}
