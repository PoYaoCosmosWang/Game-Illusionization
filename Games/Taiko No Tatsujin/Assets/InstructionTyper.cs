using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class InstructionTyper : MonoBehaviour
{
    Text instruction;
    string content = "There are 2 types of notes\nOne is blue and the other one is yellow\nPress the \"D\" key when the blue note is on the indicator\nPress the \"F\" key for the yellow note";

    void Awake()
    {
        instruction = GetComponent<Text>();
        instruction.text = "";
    }

    void Start(){
        StartCoroutine(PlayInstructions());
    }


    IEnumerator PlayInstructions(){
        // yield return new WaitForSeconds(0.571f);
        yield return new WaitForSeconds(2.2857142857f);
        foreach(char c in content){
            instruction.text += c;
            if(c == ' ' || c == '\n'){
                yield return new WaitForSeconds(0.5714285714f);
            }
        }
    }
}
