using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData 
{
    
    public int prefabIndex;//這是哪一種character
    public Vector3 originPosition;
    public Vector3 navigationalPosition;
    public float time;//目前是指第幾波出現
}

// [System.Serializable]
// public class LevelData
// {
//     public CharacterData[] data;
// }

//[CreateAssetMenu(fileName = "BattleData",menuName = "Create Battle Level Data",order = 1)]
public class BattleData : MonoBehaviour
{
    public CharacterData[] terroristData;
    public CharacterData[] policeData;
}

