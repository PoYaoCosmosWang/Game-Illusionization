using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerScoreUI : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private Camera cam;
    private RectTransform rectTransform;
    private Text scoreTxt;
    private Image background;

    private readonly float fadeBackgroundAlpha = 0.3f;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        scoreTxt = GetComponentInChildren<Text>();
        background = GetComponentInChildren<Image>();
        player.OnUpdateDisplayScore += UpdateScoreText;
        player.OnPlayerEnable += ShowBackground;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(player.transform.position);
        rectTransform.position = screenPos;
    }


    private void UpdateScoreText(int score)
    {
        scoreTxt.text = score.ToString();
    }
    //將背景調暗或調亮，用於被吃掉的時候
    private void ShowBackground(bool active)
    {
        float alpha = 1f;
        if(!active)
        {
            alpha = fadeBackgroundAlpha;
        }
        Color bgColor = background.color;
        bgColor.a = alpha;
        background.color = bgColor;
    }
}
