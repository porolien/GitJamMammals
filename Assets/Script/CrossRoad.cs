using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CrossRoad : MonoBehaviour
{
    public bool DoStart = false;
    AudioSource source;
    public AudioClip[] Camion = new AudioClip[2];
    public AudioClip[] Panthere = new AudioClip[2];
    public AudioClip Axe;
    string Tag;


    private void Start()
    {
        Tag = this.tag.ToString();
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (DoStart)
        {
            transform.position = new Vector3(transform.position.x - 0.025f, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Tag=="Panthere")
            {
                source.PlayOneShot(Panthere[Random.Range(0, 1)]);
                DoStart = true;
            }
            else if (Tag=="Camion")
            {
                source.PlayOneShot(Camion[Random.Range(0, 1)]);
                DoStart = true;
            }
            else if (Tag=="Axe")
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
