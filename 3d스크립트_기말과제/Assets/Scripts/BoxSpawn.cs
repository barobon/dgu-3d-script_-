using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawn : MonoBehaviour
{
    public GameObject BoxPrefab;
    public int count = 10;

    public float yMinDelta = 5;
    public float xzMinDelta = 1.5f;
    public float xz = 6f;
    private float backupXZ;
    public float yRan = 2f;
    public float spawnIntervalMax=2.5f;
    public float spawnIntervalMin = 0.5f;
    private float spawnInterval;
    private float recentSpawn;

    private float xPos=0;
    private float yPos=-3;
    private float zPos=0;

    private int currentIdx = 0;
    private GameObject[] Box;
    private Transform poolPosition; 
    // Start is called before the first frame update
    void Start()
    {
        backupXZ = xz;
        spawnInterval = 0f;
        recentSpawn = 0f;
        poolPosition = GetComponent<Transform>();
        Box = new GameObject[count];

        for(int i=0; i<count; i++)
        {
            Box[i] = Instantiate(BoxPrefab, poolPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Movement.dead)
        {
            return;
        }
        if(Movement.boosted)
        {
            xz = backupXZ + backupXZ * Movement.boostRate * Movement.level;
        }
        if (Movement.restarted)
        {
            xz = backupXZ;
        }
            

        if(Time.time >= recentSpawn + spawnInterval)
        {
            recentSpawn = Time.time;
            spawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);

            float xDelta = Random.Range(0f, xz);
            float zDelta = xz - xDelta;
            xPos += xzMinDelta + xDelta;
            yPos += yMinDelta + Random.Range(0f, yRan);
            zPos += xzMinDelta + zDelta;

            Box[currentIdx].SetActive(false);
            Box[currentIdx].SetActive(true);
            Box[currentIdx].transform.position = new Vector3(xPos, yPos, zPos);
            currentIdx++;

            if (currentIdx >= count)
                currentIdx = 0;
        }

    }
}
