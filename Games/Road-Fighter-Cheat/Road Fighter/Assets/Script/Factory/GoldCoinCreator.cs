using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoinCreator : MonoBehaviour
{
    public float coinSpeed = 15f;
    public GameObject gm;
    public GameObject target;
    public Material[] materials;
    float timeStamp = 0;
    float interval = 0.5f;

    // Start is called before the first frame update

    private void Update()
    {
        timeStamp += Time.deltaTime;
        if (timeStamp >= interval)
        {
            timeStamp = 0f;
            //Create();
        }

    }
    public void Create()
    {
        GameManager.STATE currentState = gm.GetComponent<GameManager>().state;
        Quaternion spawnRotation = Quaternion.Euler(0, 0, 45); ;
        if (currentState == GameManager.STATE.ILLUSION || currentState == GameManager.STATE.ILLUSION_WAIT)
        {
            spawnRotation = Quaternion.Euler(0, 0, 0);
        }
        Vector3 spawnPosition = new Vector3(15f, 0.5f, Random.Range(-2, 4) - 0.5f);
        GameObject goldCoin = GameObject.Instantiate(target, spawnPosition, spawnRotation) as GameObject;
        Material newMaterial = materials[Random.Range(0, materials.Length)];
        goldCoin.GetComponent<TargetMovement>().represent = newMaterial;
        goldCoin.GetComponent<MeshRenderer>().material = newMaterial;
        goldCoin.SendMessage("ChangeSpeed", coinSpeed * GameManager.levelSpeed);

    }
    public void Create(int idx,float y)
    {
        GameManager.STATE currentState = gm.GetComponent<GameManager>().state;
        Quaternion spawnRotation = Quaternion.Euler(0, 0, 45); ;
        if (currentState == GameManager.STATE.ILLUSION || currentState == GameManager.STATE.ILLUSION_WAIT)
        {
            spawnRotation = Quaternion.Euler(0, 0, 0);
        }
        Vector3 spawnPosition = new Vector3(15f, 0.5f, y);
        GameObject goldCoin = GameObject.Instantiate(target, spawnPosition, spawnRotation) as GameObject;
        Material newMaterial = materials[idx];
        goldCoin.GetComponent<TargetMovement>().represent = newMaterial;
        goldCoin.GetComponent<MeshRenderer>().material = newMaterial;
        goldCoin.SendMessage("ChangeSpeed", coinSpeed * GameManager.levelSpeed);

    }
}
