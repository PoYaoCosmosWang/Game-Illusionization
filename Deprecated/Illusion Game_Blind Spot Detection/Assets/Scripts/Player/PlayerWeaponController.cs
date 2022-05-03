using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//player 如何撿槍
public class PlayerWeaponController : MonoBehaviour
{
    #region sphere cast parameters
    [SerializeField]
    private float radius;
    [SerializeField]
    private float depth;
    #endregion


    [SerializeField]
    //All the weapons in the inventory
	private GameObject[] weapons;


    [SerializeField]
    //可以互動的半徑
    private float interactionRadius;



    //-1: null, 0: SMG, 1: shotgun, 2: grenade, 3:knife
    private int _nowWeapon;
    private int NowWeapon{
        get
        {
            return _nowWeapon;
        }
        set
        {
            if(value == -1)
            {
                weaponAction = PickUp;
            }
            else
            {
                weaponAction = PutDown;
            }
            _nowWeapon = value;
        }
    }

    //要pick up or put down
    private delegate void WeaponActionDelegate( WeaponContourController wcc );
    private WeaponActionDelegate weaponAction ;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            //WeaponContourController wcc =  OverlapWeapon();
            WeaponContourController wcc = DetectWeapon();

            if (wcc !=null)
            {
                weaponAction(wcc);
            }
        }
    }

    private void Initialize()
    {
        //Iterate through all weapons and disable all but the current one
		for (int i=0;i<weapons.Length;i++) 
        {
			weapons [i].SetActive (false);
		}

        weaponAction =  new WeaponActionDelegate(PickUp);

        // no weapon
        //NowWeapon = -1;

        
        // SMG
        NowWeapon = 0;
        weapons[NowWeapon].SetActive(true);
        
    }

    private void PickUp( WeaponContourController wcc )
    {
        if (wcc.ContainWeapon==false)//空的地方不能拿武器
        {
            return;
        }
        NowWeapon = wcc.PickUp();
        //Activate the new current weapon
		weapons [NowWeapon].SetActive(true);
		if(weapons [NowWeapon].GetComponent<Animator> ()!=null)
		//Play ther current weapon's Raise animation
		weapons [NowWeapon].GetComponent<Animator> ().CrossFade ("Raise",0f);
    }

    private void PutDown( WeaponContourController wcc )
    {
        // //Play ther current weapon's Lower animation
        // if(weapons [NowWeapon].GetComponent<Animator> ()!=null)
        // weapons [NowWeapon].GetComponent<Animator> ().CrossFade ("Lower",0.15f);

        // //Give it time to finish
        // StartCoroutine(Utility.Timer(0.5f, (timer) => {}, () =>{
        //     //Disable the current weapon
        //     weapons [NowWeapon].SetActive(false);
        // }));
        if(wcc.ContainWeapon)//這一格不能放武器
        {
            return;
        }
        weapons [NowWeapon].SetActive(false);
        NowWeapon = -1;
        wcc.PutDown();
    }
    /*
    private WeaponContourController OverlapWeapon()
    {
        int layerMask = 1<<8; //只開layer 8
        Vector3 interactionBox = Vector3.one * interactionRadius;
        Collider[] hits = Physics.OverlapBox(transform.position,interactionBox, Quaternion.identity,layerMask);
        foreach( Collider hit in hits)
        {
            if(hit.CompareTag("OnGroundWeapon"))
            {
                print("near " + hit.name);
                return hit.GetComponent<WeaponContourController>();
            }
        }
        Debug.LogWarning("no Weapon around");
        return null;
    }*/

    private WeaponContourController DetectWeapon()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, transform.forward, depth);
        for (int i = 0; i < hits.Length; ++i)
        {
            //Debug.LogError(hits[i].collider.name);
            //if ( hits[i].collider.CompareTag("Terrorist") )
            if (hits[i].collider.CompareTag("OnGroundWeapon"))
            {
                print("near " + hits[i].collider.name);
                return hits[i].collider.GetComponent<WeaponContourController>();
            }
        }
        Debug.LogWarning("no Weapon around");
        return null;
    }

    public void ThrowGrenade()
    {
        weapons[NowWeapon].SetActive(false);
        NowWeapon = -1;

    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(transform.position, transform.position + transform.forward * depth);
        Gizmos.DrawWireSphere(transform.position + transform.forward * depth, radius);
    }
}


