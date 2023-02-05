using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Collider PlayerHitBox;
    public bool Invicible = false;
    // Start is called before the first frame update
    void Start()
    {
        PlayerHitBox = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Shield")
        {
            Invicible = true;other.gameObject.transform.position=gameObject.transform.position;
        }
    }
}
