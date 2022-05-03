using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Sprite
{
    [SerializeField]
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public override void Hit(int dmg)
    {
        base.Hit(dmg);
        StartCoroutine(HurtEffectCoroutine());
    }

    private IEnumerator HurtEffectCoroutine()
    {
        float time = 0.1f;
        float maxAlpha = 0.5f;
        float cnt =0;
        while(cnt<time)
        {
            image.color = new Color(1f,0,0,maxAlpha*cnt/time);
            cnt+=Time.deltaTime;
            yield return null;
        }
        while(cnt>0)
        {
            image.color = new Color(1f,0,0,maxAlpha*cnt/time);
            cnt-=Time.deltaTime;
            yield return null;
        }
        image.color = new Color(1f,0,0,0);
    }
}
