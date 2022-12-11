using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject zombiePrefab;
    void Start()
    {
        InvokeRepeating("SpawnZombie", 1, 3);
    }
    private void SpawnZombie()
    {
        Instantiate(zombiePrefab,transform.position,transform.rotation);
    }
}
