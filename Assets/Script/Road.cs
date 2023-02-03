using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public Spawner spawner;
    public float speed = 1f;

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
        if (other.tag == "Player")
        {
            spawner.SpawnNextBloc();
        }

        if (other.tag == "Spawner")
        {
            Destroy(this.gameObject);
        }
    }

}
