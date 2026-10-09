using TMPro;
using UnityEngine;

public class GameLogicScript : MonoBehaviour
{
    [SerializeField] private CubesController controller;
    [SerializeField] private PointsCounter pc;
    [SerializeField] private TextMeshProUGUI resultText;

    [SerializeField] private TMP_InputField winField;
    [SerializeField] private TMP_InputField drawField;
    [SerializeField] private TMP_InputField countField;

    [SerializeField] private int maxNumberOfCubes;

    private int scoreToWin;
    private int scoreToDraw;
    private int cubesCount;

    private void Awake()
    {
        winField.onValueChanged.AddListener(x => OnWinChange(x));
        drawField.onValueChanged.AddListener(x => OnDrawChange(x));
        countField.onValueChanged.AddListener(x => OnCountChange(x));

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
        if (score >= scoreToWin)
            resultText.text = "Win!";
        else if (score >= scoreToDraw)
            resultText.text = "Draw";
        else
            resultText.text = "Loose";
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
        if (num > scoreToDraw && num <= cubesCount*6)
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
