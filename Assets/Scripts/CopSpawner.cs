using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopSpawner : MonoBehaviour
{
    public GameObject copPrefab;
    public float minSpawnDistance = 20f;
    public float maxSpawnDistance = 40f;
    public float spawnInterval = 1f;

    public CopManager copManager;
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        if (copManager == null)
        {
            copManager = FindObjectOfType<CopManager>();
        }

        //Reference by copManager's playerTransform to save resources
        playerTransform = copManager.playerTransform;

        StartCoroutine(SpawnLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            float dist = Vector2.Distance(transform.position, playerTransform.position);

            if (dist >= minSpawnDistance && dist <= maxSpawnDistance)
            {
                if (copManager.GetActiveCops() < copManager.GetMaxCopsAllowed())
                {
                    GameObject cop = GetNewCop();
                    if (cop != null)
                    {
                        cop.SetActive(true);

                        Vector3 spawnPosition = transform.position;
                        spawnPosition.z = 0f;
                        cop.transform.position = spawnPosition;

                        Vector2 dirToPlayer = (playerTransform.position - transform.position).normalized;
                        float angle = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg - 90f;

                        cop.GetComponent<CopDriveHandler>().SetInitialRotation(angle);
                    }
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    GameObject GetNewCop()
    {
        foreach(var cop in CopDriveHandler.Cops)
        {
            if (!cop.isActive)
            {
                return cop.gameObject;
            }
        }

        //No cops to reuse, must instantiate a new object
        GameObject newCop = Instantiate(copPrefab);
        return newCop;
    }
}
