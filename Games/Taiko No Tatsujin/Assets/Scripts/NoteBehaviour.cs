using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehaviour : MonoBehaviour
{
    protected IEnumerator OnHit(int id)
    {
        if(id==1){
            Player1GameStatus.UpdateOnHit();
        }else{
            Player2GameStatus.UpdateOnHit();
        }

        float currentScale = 1.0f;
        float hitScale = 1.5f;
        while (currentScale < hitScale)
        {
            currentScale += 0.12f;
            transform.localScale = new Vector3(currentScale, currentScale, 0.0f);
            yield return null;
        }
        while (currentScale > 1.0f)
        {
            currentScale -= 0.15f;
            transform.localScale = new Vector3(currentScale, currentScale, 0.0f);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
