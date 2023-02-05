using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    public GameObject[] Prefabs = new GameObject[3];
    public GameObject[] AllBlock;
    

    public void SpawnNextBloc()
      
    {
        
        AllBlock = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in AllBlock)
        {
            if(block.GetComponent<Road>().Last == true)
            {
                Debug.Log("1er");
                block.GetComponent<Road>().Last = false;
                GameObject Prefab = Prefabs[Random.Range(0, Prefabs.Length)];
                Debug.Log("2er");
                GameObject NewBlock = Instantiate(Prefab, new Vector3(0, 3.67f, block.GetComponent<Transform>().position.z + 33), Quaternion.identity);
                Debug.Log("3er");
                NewBlock.GetComponent<Road>().Last = true;
                Debug.Log("4er");
            }
            
        }
    }
}
