using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour
{

    public Material[] materials;
    public GameObject playerHead;
    public GameObject playerHand;
    public GameObject playerBody;
    SkinnedMeshRenderer head;
    SkinnedMeshRenderer hand;
    SkinnedMeshRenderer body;
    public Material represent;
    // Start is called before the first frame update
    void Start()
    {
        head = playerHead.GetComponent<SkinnedMeshRenderer>();
        hand = playerHand.GetComponent<SkinnedMeshRenderer>();
        body = playerHand.GetComponent<SkinnedMeshRenderer>();

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChangeColor"))
        {
            Debug.Log("change color");
            Material newMaterial = materials[Random.Range(0, materials.Length)];
            hand.material = newMaterial;
            head.material = newMaterial;
            body.material = newMaterial;
            this.represent = newMaterial;
        }
    }
}
