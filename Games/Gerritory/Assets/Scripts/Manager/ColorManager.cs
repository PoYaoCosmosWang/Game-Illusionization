using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
   // public bool isSakai=false;

    #region reference
    public List<Player> players;
    public Map map;
    public BackgroundManager background;
    #endregion
    [SerializeField]
    private List<Material> mats;
    [SerializeField]
    private List<GameObject> backgrounds;

    public List<GameObject> colorTestObjs;

    private void Awake()
    {
        if(SelectLevelManager.instance.isSakai)
        {
            players[0].SetMaterial(mats[0]);
            players[1].SetMaterial(mats[1]);
            map.colorMapping[1] = mats[0].color;
            map.colorMapping[2] = mats[1].color;
            background.backgrounds[1] = backgrounds[0];
            background.backgrounds[2] = backgrounds[1];
        }
        else
        {
            players[0].SetMaterial(mats[2]);
            players[1].SetMaterial(mats[3]);
            map.colorMapping[1] = mats[2].color;
            map.colorMapping[2] = mats[3].color;
            background.backgrounds[1] = backgrounds[2];
            background.backgrounds[2] = backgrounds[3];
        }

        //Color a = (mats[0].color + mats[1].color) / 2;
        //Color b = (mats[2].color + mats[3].color) / 2;
        //colorTestObjs[0].GetComponent<MeshRenderer>().material.color = a;
        //colorTestObjs[1].GetComponent<MeshRenderer>().material.color = b;
    }


}
