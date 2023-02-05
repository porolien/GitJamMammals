using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CrossRoad : MonoBehaviour
{
    public bool DoStart = false;
    AudioSource source;
    public AudioClip[] Camion = new AudioClip[3];
    public AudioClip[] Panthere = new AudioClip[2];
    public AudioClip Axe;
    string Tag;
    public GameObject enemi;
    public float speed;


    private void Start()
    {
        Tag = this.tag.ToString();
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (DoStart)
        {
            enemi.transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !DoStart)
        {
            if (Tag == "Panthere")
            {
                source.PlayOneShot(Panthere[0]);
                DoStart = true;
            }
            else if (Tag == "Camion")
            {
                source.PlayOneShot(Camion[Random.Range(0, 1)]);
                DoStart = true;
            }
            else if (Tag == "Axe")
            {
                source.PlayOneShot(Axe);
                DoStart = true;
            }
        }

        if (other.tag == "OutOfTheRoad")
        {
            Destroy(gameObject);
        }
    }
}
