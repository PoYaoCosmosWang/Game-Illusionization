using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFactory :MonoBehaviour
{
    public List<GameObject> tileTypes;
    
    //依照字元代號來建立不同類型的 tile
    public Tile CreateTile(char tileChar)
    {
        Tile tile = null;
        switch (tileChar)
        {
            case '0':
            case '1':
            case '2':
                //return new Tile();
                tile = Instantiate(tileTypes[0]).GetComponent<Tile>();
                break;

            case 'X':
            case 'x':
                //return new EmptyTile();
                tile = Instantiate(tileTypes[1]).GetComponent<Tile>();
                break;
            case 'N':
            case 'n':
                //return new EmptyTile();
                tile = Instantiate(tileTypes[2]).GetComponent<Tile>();
                break;
            default:
                Debug.LogError("讀到無法識別的 tileChar '" + tileChar + "'");
                break;

        }//switch

        return tile;
    }
}
