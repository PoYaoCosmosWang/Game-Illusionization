using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;


public class Map : MonoBehaviour
{

    //地圖長相
    private string[] initMapString =
    {
     /*
    "XXXXXXXXXXXXXX",
    "X0NNN2NN21200X",
    "X2NNN0XXNX2XNX",
    "X2XNX1NNNN2XNX",
    "X0NNN0XXNX0XNX",
    "XN211NNN00112X",
    "XNXNXXNNNNXNXX",
    "XNXNXXNXXNXNXX",
    "X21100N000122X",
    "X1XNXNNXXX2XNX",
    "X00211NXNN2XNX",
    "XNXNX2XNXX1XNX",
    "X22100NNNN0NNX",
    "XXXXXXXXXXXXXX",
    */
    


    };

    /*
    //最後要長怎樣會過關
    private readonly string[] ansMapString =
    {
        /*
    "XXXXXXXXXXXXXX",
    "X1NNN2NN11111X",
    "X1NNN2XXNX1XNX",
    "X1XNX2NNNN1XNX",
    "X1NNN2XXNX2XNX",
    "XN112NNN22222X",
    "XNXNXXNNNNXNXX",
    "XNXNXXNXXNXNXX",
    "X22222N222111X",
    "X2XNXNNXXX1XNX",
    "X22222NXNN1XNX",
    "XNXNX1XNXX1XNX",
    "X11111NNNN1NNX",
    "XXXXXXXXXXXXXX",
    
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

    };
    */


    [SerializeField]
    private List<Player> players;

    public Color[] colorMapping;

    private TileFactory tileFactory;    //新關卡時產生 tile 的工廠

    [SerializeField]
    private float outlinePercent = 0.2f;
    //getLength(0) =>height
    //getLength(1) =>width
    private Tile[,] grids; //存reference用

    private string postfixFileName = "/Map.txt";

    #region for cooperate
    private int totalColorTilesNum=0;
    private int correctColorTilesNum = 0;
    private bool mapSetting = true;

    public UnityAction onWinGame = delegate { };

    public bool winningMergeColor = false;
    #endregion
    public int height = 0;
    // Start is called before the first frame update
    void Awake()
    {
        tileFactory = GetComponent<TileFactory>();
        initMapString = Utility.ReadTextFile(SelectLevelManager.instance.prefixFileName + postfixFileName);
        height = initMapString.GetLength(0);

    }
    private void Start()
    {
        MakeMap(initMapString);
    }
    public bool fade = false;
    private void Update()
    {
        if(fade)
        {
            fade = false;
            BroadcastMessage("FadeOutThenFadeIn", 1f);
        }
    }

    //grids[y,x] 的位置就在 (x,0,y);   
    //grids[0,0] 的位置就在 (0,0,0);
    //grids[3,2] 的位置就在 (2,0,3);
    //建議使用 Tiles(x,y) 可更直覺的去表達
    public void MakeMap(string[] gridStrings)
    {
        int height = gridStrings.Length;
        int width = gridStrings[0].Length;
        grids = new Tile[height, width];

        //先創造出格子
        for (int row = 0, y = height - 1 ; row < height; row++, y--)
        {
            string line = gridStrings[row];
            for (int x = 0; x < width; x++)
            {   
                grids[y, x] = tileFactory.CreateTile(line[x]);
                grids[y, x].transform.position = new Vector3(x, 0, y);
                grids[y, x].transform.parent = this.transform;
                grids[y, x].transform.localScale = Vector3.one * (1 - outlinePercent);

            }
        }

        //之後再個個加入要的東西
        for (int row = 0, y = height - 1; row < height; row++, y--)
        {
            string line = gridStrings[row];
            for (int x = 0; x < width; x++)
            {
                if(char.IsDigit(line[x]))//是數字的代表為colorableTile
                {
                    
                    ColorableTile tile = grids[y, x].GetComponent<ColorableTile>();
                    totalColorTilesNum++;
                    //監聽顏色改變
                    tile.OnColorChanged += OnColorTileChanged;
                    int idx = (int)char.GetNumericValue(line[x]);
                    tile.Occupy(players[idx]);
                    /*
                    //required color,init color
                    int requiredColorHash = (int)char.GetNumericValue(ansMapString[row][x]);
                    Color requiredColor = colorMapping[requiredColorHash];
                    int initColorHash = (int)char.GetNumericValue(line[x]);
                    Color initColor =colorMapping[initColorHash];
                    tile.InitSetting(initColor, requiredColor);
                    */
                }
                if(line[x]=='N') //uncolorable tile
                {

                    UncolorableTile tile = grids[y, x].GetComponent<UncolorableTile>();
                    onWinGame += tile.FadeOut;
                }

            }
        }

        OnMapSet();//設定完了

    }
    public bool IsValid(Vector2 position)
    {
        
        return initMapString[(int)position.y][(int)position.x] != 'X';
    }

    public void OnColorTileChanged(bool changeToAns)
    {
        
        if(changeToAns)
        {
            correctColorTilesNum++;
            CheckWin();
        }
        else
        {
            
            correctColorTilesNum--;

        }
        
    }

    private void CheckWin()
    {
        
        if(!mapSetting && totalColorTilesNum == correctColorTilesNum)
        {
            Win();
        }
    }
    private void Win()
    {
        onWinGame();
        if(winningMergeColor)
        {
            Color mergeColor = (colorMapping[1] + colorMapping[2]) / 2;
            BroadcastMessage("FadeTo",mergeColor);
        }
        print("Win");
    }
    private void OnMapSet()
    {
        mapSetting = false;
    }
}
