using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent (null);
        Destroy(this.gameObject,1f);
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
