using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
   
    [SerializeField]
    private float throwForceMagntude;
    [SerializeField]
    private float explosionRadius;
    [SerializeField]
    private float explosionPower;
    private Rigidbody rig;

    [SerializeField]
    private GameObject particle;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Throw();
        }
    }

    private void Throw()
    {
        
        GameObject anotherGrenade = Instantiate(this.gameObject, transform.position, Quaternion.identity);
        Vector3 force = transform.forward * throwForceMagntude * 4 + transform.up* throwForceMagntude * 3;
        anotherGrenade.GetComponent<BoxCollider>().enabled = true;
        Rigidbody anotherRig = anotherGrenade.GetComponent<Rigidbody>();
        anotherRig.useGravity = true;
        anotherRig.AddForce(force,ForceMode.Acceleration);
        //SendMessageUpwards("ThrowGrenade");
        //transform.SetParent (null);
    }
    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Weapon")
        {
            return;
        }
        print(other.name);
        Explode();
        Destroy(this.gameObject,0.1f);
    }

    private void Explode()
    {
        //1. instantiate explosion particle
        particle.SetActive(true);
        //2.add force to those neighbors
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach( Collider hit in colliders)
        {
            /*Rigidbody r = hit.GetComponent<Rigidbody>();
            if(r==null)
            {
                continue;
            }*/
            //print("explode " + hit.gameObject.name);
            //r.AddExplosionForce(explosionPower,transform.position,explosionRadius);
            
            //add damage to enemies
            if(hit.CompareTag("Terrorist")==false)
            {
                continue;
            }
            hit.SendMessage("Hit",explosionPower);
        }
    }
}
