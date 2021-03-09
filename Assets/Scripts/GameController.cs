using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private static readonly List<Color> Colors = new List<Color>
    {
        Color.black,
        Color.green,
        Color.red,
        Color.white,
        Color.yellow,
        Color.magenta,
        Color.blue,
        Color.grey
    };

    private static readonly Color BaseCardColor = new Color(0.7f, 0.5f, 0.11f);
    private int _score;

    private (int, int)? _selectedLocation;

    private int _height;
    private int _width;

    private int[,] _locations;
    private GameObject[,] _cards;
    private const float DestroyAfterSeconds = 0.6f;
    private const float HideAfterSeconds = 1;
    private bool _waitingForAnimation;

    private void HandleClick(int x, int y)
    {
        if(_waitingForAnimation) return;
        if(_selectedLocation == (x,y)) return; //the same card is clicked
        if (_selectedLocation == null)
        {
            _selectedLocation = (x, y);
        }
        else
        {
            _waitingForAnimation = true;
            (int a, int b) = _selectedLocation.Value;
            (int l1, int l2) = (_locations[a, b], _locations[x, y]);
            (GameObject c1, GameObject c2) = (_cards[a, b], _cards[x, y]);

            c1.GetComponent<Image>().color = Colors[l1-1];
            c2.GetComponent<Image>().color = Colors[l2-1];

            bool hit = l1 == l2;
            if (hit)
            {
                _locations[a, b] = _locations[x, y] = 0;
                StartCoroutine(DestroyAfter(DestroyAfterSeconds, c1));
                StartCoroutine(DestroyAfter(DestroyAfterSeconds, c2));
                UpdateScore(_score + 10);
            }
            else
            {
                StartCoroutine(HideAfter(HideAfterSeconds, c1));
                StartCoroutine(HideAfter(HideAfterSeconds, c2));
                UpdateScore(_score - 2);
            }

            _selectedLocation = null;
        }
    }

    private IEnumerator DestroyAfter(float seconds, GameObject go)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(go);
        _waitingForAnimation = false;
    }
    
    private IEnumerator HideAfter(float seconds, GameObject go)
    {
        yield return new WaitForSeconds(seconds);
        go.GetComponent<Image>().color = BaseCardColor;
        _waitingForAnimation = false;
    }

    private void Awake()
    {
        BackToMenuButton.onClick.AddListener(() => SceneManager.LoadScene(Scenes.MainMenu));
        _height = SessionSettings.Instance.tilesHeight;
        _width = SessionSettings.Instance.tilesWidth;
        _locations = new int[_height, _width];

        GeneratePairsLocations();
        CreateCards();
    }

    private void CreateCards()
    {
        _cards = new GameObject[_height, _width];
        for (var i = 0; i < _height; i++)
        {
            for (var j = 0; j < _width; j++)
            {
                var go = Instantiate(CardPrefab, CardContainer);
                _cards[i, j] = go;
                var newPosition = new Vector2((2 * j - _width + 1) * 80, (2 * i - _height + 1) * 115);
                go.transform.localPosition = newPosition;
                go.GetComponent<Image>().color = BaseCardColor;
                int i1 = i;
                int j1 = j;
                go.AddTrigger(EventTriggerType.PointerClick, () =>
                {
                    Debug.Log($"Clicked {i1},{j1}");
                    HandleClick(i1, j1);
                });
            }
        }
    }

    private void GeneratePairsLocations()
    {
        var pairs = Enumerable.Range(1, _height * _width / 2).SelectMany(t => Enumerable.Repeat(t, 2)).ToList();
        pairs.Shuffle();

        for (var i = 0; i < _height; i++)
        {
            for (var j = 0; j < _width; j++)
            {
                _locations[i, j] = pairs[i * _width + j];
            }
        }
    }

    private void UpdateScore(int newScore)
    {
        _score = newScore;
        ScoreText.text = $"Score: {_score}";
    }
}