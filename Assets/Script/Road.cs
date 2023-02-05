using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public Spawner spawner;
    public float speed = 1f;
    public GameObject ACube;
    public bool Last;

    private void Start()
    {
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();

    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.2f * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (tag == "Parallax")
        {
            
            if (other.tag == "Spawner")
            {
                spawner.SpawnNextBloc("Parallax");
                StartCoroutine(DestroyIt());
            }
        }
        else
        {
            if (other.tag == "Player")
            {
                Debug.Log("");
                spawner.SpawnNextBloc("Block");
            }

            if (other.tag == "Spawner")
            {
                Debug.Log("df");
                Destroy(this.gameObject);
            }
        }
    }
    IEnumerator DestroyIt()
    {
        yield return new WaitForSeconds(20f);
        Destroy(this.gameObject);
    }

}
