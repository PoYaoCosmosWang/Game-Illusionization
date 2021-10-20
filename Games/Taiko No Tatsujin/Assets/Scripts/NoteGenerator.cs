using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    [SerializeField]
    TextAsset noteList;
    [SerializeField]
    float x;
    [SerializeField]
    float y;
    [SerializeField]
    GameObject bigKa;
    [SerializeField]
    GameObject bigDon;
    [SerializeField]
    GameObject smallKa;
    [SerializeField]
    GameObject smallDon;
    [SerializeField]
    GameObject endNote;

    void Start()
    {
        string notes = noteList.text;
        string[] lines = Regex.Split(notes,"\n|\r|\r\n");
        for(int i = 0; i < lines.Length; i++){
            string[] oneLine = Regex.Split(lines[i], " ");
            Debug.Log(oneLine[0]);
            float position = float.Parse(oneLine[0]) + x;
            if(oneLine[1].Equals("D") || oneLine[1].Equals("K")){
                Instantiate(smallDon, new Vector3(position, y+0.8f, -3f), Quaternion.identity, transform);
                Instantiate(smallKa, new Vector3(position, y-0.6f, -3f), Quaternion.identity, transform);
            }else if(oneLine[1].Equals("d")){
                Instantiate(smallDon, new Vector3(position, y+0.8f, -3f), Quaternion.identity, transform);
            }else if(oneLine[1].Equals("k")){
                Instantiate(smallKa, new Vector3(position, y-0.6f, -3f), Quaternion.identity, transform);
            }else if(oneLine[1].Equals("E")){
                Instantiate(endNote, new Vector3(position, y, -3f), Quaternion.identity, transform);
            }
            // if(oneLine[1].Equals("D")){
            //     Instantiate(bigDon, new Vector3(position, y+0.4f, -3f), Quaternion.identity, transform);
            // }else if(oneLine[1].Equals("d")){
            //     Instantiate(smallDon, new Vector3(position, y+0.4f, -3f), Quaternion.identity, transform);
            // }else if(oneLine[1].Equals("K")){
            //     Instantiate(bigKa, new Vector3(position, y-0.3f, -3f), Quaternion.identity, transform);
            // }else if(oneLine[1].Equals("k")){
            //     Instantiate(smallKa, new Vector3(position, y-0.3f, -3f), Quaternion.identity, transform);
            // }else if(oneLine[1].Equals("E")){
            //     Instantiate(endNote, new Vector3(position, y, -3f), Quaternion.identity, transform);
            // }
        }
    }

}
