using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    [SerializeField] private PointsCounter pc;

    private TextMeshProUGUI text;

    private void Start()
    {
        if (pc == null)
            Debug.LogError("No PointsCounter attached to UI");

        text = gameObject.GetComponent<TextMeshProUGUI>();
        if (text == null)
            Debug.LogError("No TMP in UI");
    }

    private void Update()
    {
        text.text = "Score:\n" + pc.Score;
    }
}
