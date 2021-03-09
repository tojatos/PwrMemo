using System;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class GameController : MonoBehaviour
{
    public Transform CardContainer;
    public GameObject CardPrefab;
    public TMP_Text ScoreText;
    public Button BackToMenuButton;

    private const int CardWithPaddingWidth = 170;
    private int _score = 0;
    private void Awake()
    {
        BackToMenuButton.onClick.AddListener(() => SceneManager.LoadScene(Scenes.MainMenu));
            
        int height = SessionSettings.Instance.tilesHeight;
        int width = SessionSettings.Instance.tilesWidth;

        var cards = new GameObject[height, width];
        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                var go = Instantiate(CardPrefab, CardContainer);
                cards[i, j] = go;
                var newPosition = new Vector2((2 * j - width + 1) * 80, (2 * i - height + 1) * 115);
                go.transform.localPosition = newPosition;
                int i1 = i;
                int j1 = j;
                go.AddTrigger(EventTriggerType.PointerClick, () =>
                {
                    Debug.Log($"Clicked {i1},{j1}");
                });
            }
        }
    }

    private void UpdateScore(int newScore)
    {
        _score = newScore;
        ScoreText.text = $"Score: {_score}";
    }
}
