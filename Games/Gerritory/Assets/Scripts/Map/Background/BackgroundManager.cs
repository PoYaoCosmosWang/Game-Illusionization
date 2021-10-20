using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    //
    private string[] backgroundString =
    {
        /*
    "11112221111111",
    "11112221111111",
    "11112221111111",
    "11112221111111",
    "11112222222222",
    "11112222222222",
    "11112222222222",
    "22222222221111",
    "22222222221111",
    "22222222221111",
    "22222222221111",
    "11111112221111",
    "11111112221111",
    "11111112221111",
    */
    /*
    "XXXXXXXXXXXX",
    "X1111122222X",
    "X1222121112X",
    "X1222121112X",
    "X1222121112X",
    "X1111122222X",
    "X2222211111X",
    "X2111212221X",
    "X2111212221X",
    "X2111212221X",
    "X2222211111X",
    "XXXXXXXXXXXX",
    */
    
            /*
    "1111111",
    "1111111",
    "1122211",
    "1122211",
    "1122211",
    "1111111",
    "1111111",
    */
    };

    private string postfixFileName = "/Background.txt";
    public GameObject[] backgrounds;
    private void Start()
    {
        backgroundString = Utility.ReadTextFile(SelectLevelManager.instance.prefixFileName + postfixFileName);
        GenerateBackground();
    }

    private void GenerateBackground()
    {
        /*for(int x=0;x<mapSz.x;x+=(int)quadSz.x)
        {
            for(int y=0;y<mapSz.y;y+=(int)quadSz.y)
            {
                int idx = (int)(x / quadSz.x + y / quadSz.y) % 2;
                //print(idx);
                GameObject obj = Instantiate(backgrounds[idx], this.transform) ;
                Vector3 position = new Vector3(x, 0, y);
                obj.transform.localPosition = position;
                obj.transform.localScale = new Vector3(quadSz.x,1, quadSz.y);
            }
        }*/

        int height = backgroundString.Length;
        int width = backgroundString[0].Length;

        for (int row = 0, y = height - 1; row < height; row++, y--)
        {
            string line = backgroundString[row];
            for (int x = 0; x < width; x++)
            {
                int idx = (int)char.GetNumericValue(backgroundString[row][x]);
                if(idx==-1)
                {
                    continue;
                }
                GameObject obj = Instantiate(backgrounds[idx], this.transform);
                Vector3 position = new Vector3(x, 0, y);
                obj.transform.localPosition = position;
                
            }
        }
    }
}
