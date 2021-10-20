using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettlementTextSetter : MonoBehaviour
{
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text maxComboText;
    [SerializeField]
    Text goodText;
    [SerializeField]
    Text okayText;
    [SerializeField]
    Text badText;

    void Start(){
        scoreText.text = Player1GameStatus.score.ToString();
        maxComboText.text = Player1GameStatus.maxCombo.ToString();
        goodText.text = Player1GameStatus.goodCount.ToString();
        okayText.text = Player1GameStatus.okCount.ToString();
        badText.text = Player1GameStatus.badCount.ToString();
    }
}
