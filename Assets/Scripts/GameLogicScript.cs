using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLogicScript : MonoBehaviour
{
    [SerializeField] private CubesController controller;
    [SerializeField] private PointsCounter pc;
    [SerializeField] private TextMeshProUGUI resultText;

    [SerializeField] private TMP_InputField winField;
    [SerializeField] private TMP_InputField drawField;
    [SerializeField] private TMP_InputField countField;

    [SerializeField] private int maxNumberOfCubes;

    [SerializeField] private Toggle isRandomToggle;
    [SerializeField] private float randDelay;

    private int scoreToWin;
    private int scoreToDraw;
    private int cubesCount;

    private bool ignoreNextScore;
    private Coroutine randCoroutine;

    private void Awake()
    {
        winField.onEndEdit.AddListener(x => OnWinChange(x));
        drawField.onEndEdit.AddListener(x => OnDrawChange(x));
        countField.onEndEdit.AddListener(x => OnCountChange(x));

        pc.ScoreChanged += OnScoreChanged;
    }

    private void Start()
    {
        OnCountChange(countField.text);
        OnWinChange(winField.text);
        OnDrawChange(drawField.text);
    }

    private void OnScoreChanged(int score)
    {
        if (ignoreNextScore)
        {
            ignoreNextScore = false;
            return;
        }

        if (score >= scoreToWin)
            resultText.text = "Win!";
        else if (score >= scoreToDraw)
            resultText.text = "Draw";
        else
            resultText.text = "Loose";

        if (isRandomToggle.isOn && randCoroutine == null)
            randCoroutine = StartCoroutine(RandomizeAfterDelay());
    }

    private IEnumerator RandomizeAfterDelay()
    {
        yield return new WaitForSeconds(randDelay);
        randCoroutine = null;

        if (!isRandomToggle.isOn)
            yield break;

        RandomizeValues();
    }

    private void RandomizeValues()
    {
        var cubes = Random.Range(1, maxNumberOfCubes);
        var maxScore = cubes * 6;
        var win = Random.Range(2, maxScore + 1);
        var draw = Random.Range(1, win);

        cubesCount = cubes;
        scoreToWin = win;
        scoreToDraw = draw;

        countField.text = cubes.ToString();
        winField.text = win.ToString();
        drawField.text = draw.ToString();

        resultText.text = "";
        ignoreNextScore = true;
        controller.SpawnCubes(cubes);
    }

    private void OnCountChange(string s)
    {
        var num = int.Parse(s);
        if (num > 0 && num < maxNumberOfCubes)
        {
            cubesCount = num;
            controller.SpawnCubes(cubesCount);
        }
        else
            Debug.LogError("Wrong format in Cubes Input Field");
    }
    private void OnWinChange(string s)
    {
        var num = int.Parse(s);
        if (num > scoreToDraw && num <= cubesCount * 6)
            scoreToWin = num;
        else
            Debug.LogError("Wrong format in Win Input Field");
    }
    private void OnDrawChange(string s)
    {
        var num = int.Parse(s);
        if (num > 0 && num < scoreToWin)
            scoreToDraw = num;
        else
            Debug.LogError("Wrong format in Draw Input Field");
    }
}
