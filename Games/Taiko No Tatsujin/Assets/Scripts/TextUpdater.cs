using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
    [SerializeField]
    int playerId;
    [SerializeField]
    Text comboCountText;
    [SerializeField]
    Text comboText;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text stateText;

    void Update()
    {
        scoreText.text = playerId == 1 ? Player1GameStatus.score.ToString() : Player2GameStatus.score.ToString();

        int combo = playerId == 1 ? Player1GameStatus.combo : Player2GameStatus.combo;
        if (combo > 0)
        {
            comboCountText.text = combo.ToString();
            comboText.text = "Combo";
        }
        else
        {
            comboCountText.text = "";
            comboText.text = "";
        }

        stateText.text = playerId == 1 ? Player1GameStatus.state : Player2GameStatus.state;
        if (stateText.text == "GOOD")
        {
            stateText.color = Color.green;
        }
        else if (stateText.text == "OK")
        {
            stateText.color = Color.yellow;
        }
        else if (stateText.text == "BAD")
        {
            stateText.color = Color.red;
        }
    }
}
