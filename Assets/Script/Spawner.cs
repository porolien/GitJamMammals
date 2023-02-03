using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Prefabs = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        //SpawnNextBloc()
    }

    public void SpawnNextBloc()
    {
        Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], transform.position, Quaternion.identity);
    }
}
