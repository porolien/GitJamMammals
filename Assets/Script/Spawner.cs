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
                Debug.Log(block.GetComponent<Road>().ACube.transform.position.z);
                block.GetComponent<Road>().Last = false;
                GameObject Prefab = Prefabs[Random.Range(0, Prefabs.Length)];
                GameObject NewBlock = Instantiate(Prefab, new Vector3(0, 0, block.GetComponent<Transform>().position.z + 33), Quaternion.identity);
                NewBlock.GetComponent<Road>().Last = true;
            }
            
        }
    }
}
