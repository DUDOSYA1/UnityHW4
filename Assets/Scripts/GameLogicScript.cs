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

    private int scoreToWin;
    private int scoreToDraw;

    private void Awake()
    {
        winField.onValueChanged.AddListener(x => OnWinChange(x));
        drawField.onValueChanged.AddListener(x => OnDrawChange(x));
        countField.onValueChanged.AddListener(x => OnCountChange(x));

        pc.ScoreChanged += OnScoreChanged;
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
        controller.SpawnCubes(int.Parse(s));
    }
    private void OnWinChange(string s)
    {
        scoreToWin = int.Parse(s);
    }
    private void OnDrawChange(string s)
    {
        scoreToDraw = int.Parse(s);
    }
}
