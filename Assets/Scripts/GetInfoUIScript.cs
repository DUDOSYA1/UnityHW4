using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GetInfoUIScript : MonoBehaviour
{
    [SerializeField] private PointsCounter pc;
    [SerializeField] private TMP_InputField winField;
    [SerializeField] private TMP_InputField drawField;
    [SerializeField] private TMP_InputField countField;

    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private string[] prefixes; 

    private void Awake()
    {
        if (pc == null)
            Debug.LogError("No PointsCounter attached to UI");

        pc.ScoreChanged += OnScoreChanged;

        winField.onValueChanged.AddListener(x => OnWinChange(x));
        drawField.onValueChanged.AddListener(x => OnDrawChange(x));
        countField.onValueChanged.AddListener(x => OnCountChange(x));
    }

    private void OnScoreChanged(int score)
    {
        texts[0].text = prefixes[0] + score.ToString();
    }

    private void OnWinChange(string s)
    {
        texts[1].text = prefixes[1] + s;
    }
    private void OnDrawChange(string s)
    {
        texts[2].text = prefixes[2] + s;
        texts[3].text = prefixes[3] + s;
    }
    private void OnCountChange(string s)
    {
        texts[4].text = prefixes[4] + s;
    }
}
