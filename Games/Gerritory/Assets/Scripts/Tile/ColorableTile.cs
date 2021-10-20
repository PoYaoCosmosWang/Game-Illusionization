using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorableTile : Tile
{
    //完成所需要的顏色
    private Color requiredColor;
    protected override Color CurColor
    {
        get
        {
            return base.CurColor;
            
        }
        set
        {
            //只有顏色不同才會被設定顏色

            //當現在顏色是對的卻要被改掉->-1
            //現在不對但要變對-> +1
            if(base.CurColor == requiredColor|| value == requiredColor)
            {
                OnColorChanged(value == requiredColor);
            }
            
            base.CurColor = value;
        }
    }
    
    public Player curPlayer;
    
    public UnityAction<bool> OnColorChanged =delegate { };
    

    //Detect collisions between the GameObjects with Colliders attached
    void OnTriggerEnter(Collider collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.CompareTag("Player"))
        {
            Occupy(collision.GetComponent<Player>());
        }
    }

    //給map設定初始的color和最後要達成的color
    public void InitSetting(Color initColor,Color requiredColor)
    {
        //要先給required color 再給init才能正確觸發map的cnt
        this.requiredColor = requiredColor;
        CurColor = initColor;
    }

    //Updates color of the tile.
    void SetTileColor(Color color){
        CurColor = color;
        
    }

    public void FadeTo(Color color)
    {
        StartCoroutine(FadeToCoroutine(color));
    }
    private IEnumerator FadeToCoroutine(Color color)
    {
        yield return new WaitForSeconds(1.5f);

        float t = 0f;
        while (t <= 1f)
        {

            Color prevColor, afterColor;
            prevColor = CurColor;
            afterColor = color;
            Color c = Color.Lerp(prevColor, afterColor, t);
            t += Time.deltaTime;
            meshRenderer.material.color = c;
            yield return null;
        }

    }


    //updateScore
    public void Occupy(Player newP)
    {
        
        //改成顏色不同才上色
        if(newP.playerColor!=CurColor)
        //if (newP!= curPlayer)
        {
            //print("occupy player:" + newP.gameObject.name);

            newP.OwnScore++;
            //防止第一次出錯
            if(curPlayer!=null)
            {
                curPlayer.OwnScore--;
            }
            
            CurColor = newP.playerColor;
            curPlayer = newP;
        }
    }
}
