using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAI : MonoBehaviour
{
    [SerializeField]
    private float moveInterval = 0.5f;
    private readonly int dirCount = 4;


    private void OnEnable()
    {
        StartCoroutine(RandomDirectionCoroutine(moveInterval));
    }

    private int RandomInt(int dirCount)
    {
        return Random.Range(0, dirCount);
    }

    //傳input 到player controller上
    private void SendAIMoveInput(int input)
    {
        SendMessage("Move", input);
    }
    private IEnumerator RandomDirectionCoroutine(float moveInterval)
    {
        yield return new WaitForSeconds(0.1f);
        while(true)
        {
            int rand = RandomInt(dirCount);
            SendAIMoveInput(rand);
            yield return new WaitForSeconds(moveInterval);
        }
    }
}
