using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    [SerializeField]
    private AudioSource coinEatSound;
    [SerializeField]
    private AudioSource coinWrongSound;
    public Text scorePanel;
    void Start()
    {

        scorePanel.text = "100";
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collid with " + other.name);
        if (other.CompareTag("Coin") && other.GetComponent<TargetMovement>().represent.name == gameObject.GetComponent<PlayerColor>().represent.name)
        {
            Destroy(other);
            increaseScore();
        }
        else if(other.name!="SoftStar(Clone)")
        {
            Debug.Log(other.name);
            decreaseScore();
        }
    }
    void increaseScore()
    {
        coinEatSound.Play();
        scorePanel.text = (int.Parse(scorePanel.text) + 1).ToString();
    }
    void decreaseScore()
    {
        coinWrongSound.Play();
        scorePanel.text = (int.Parse(scorePanel.text) - 1).ToString();
        if (int.Parse(scorePanel.text) == 0)
        {
            scorePanel.text = (int.Parse(scorePanel.text) - 1).ToString();
            //to negative
        }
    }
}
