using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    public GameObject[] Prefabs = new GameObject[3];
    public GameObject[] AllBlock;
    public GameObject Parallaxx;
    public bool StopSpawning;

    public void SpawnNextBloc(string Type)
    {
        if (!StopSpawning)
        {
            if (Type == "Parallax")
            {
                Debug.Log("oui");
                AllBlock = GameObject.FindGameObjectsWithTag("Parallax");
                foreach (GameObject block in AllBlock)
                {
                    if (block.GetComponent<Road>().Last == true)
                    {

                        block.GetComponent<Road>().Last = false;
                        GameObject Prefab = Parallaxx;
                        GameObject NewBlock = Instantiate(Prefab, new Vector3(block.GetComponent<Transform>().position.x, 0, block.GetComponent<Transform>().position.z + 33), Quaternion.identity);
                        NewBlock.GetComponent<Transform>().Rotate(-90, 0, 0, Space.Self);
                        NewBlock.GetComponent<Road>().Last = true;
                    }

                }
            }
            else
            {
                AllBlock = GameObject.FindGameObjectsWithTag("Block");
                foreach (GameObject block in AllBlock)
                {
                    if (block.GetComponent<Road>().Last == true)
                    {

                        block.GetComponent<Road>().Last = false;
                        GameObject Prefab = Prefabs[Random.Range(0, Prefabs.Length)];
                        GameObject NewBlock = Instantiate(Prefab, new Vector3(0, 3.67f, block.GetComponent<Transform>().position.z + 33), Quaternion.identity);
                        NewBlock.GetComponent<Road>().Last = true;
                    }

                }
            }
        }
    }
}
