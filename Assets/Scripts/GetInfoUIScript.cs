using TMPro;
using UnityEngine;

public class GetInfoUIScript : MonoBehaviour
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
        text.text = "Score: " + pc.Score;
    }
}
