using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILogicScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup sidePanel;
    [SerializeField] private CanvasGroup setPanel;

    [SerializeField] private TMP_InputField winField;
    [SerializeField] private TMP_InputField drawField;
    [SerializeField] private TMP_InputField countField;

    [SerializeField] private Button throwBtn;

    [SerializeField] private PointsCounter pc;

    private void Awake()
    {
        winField.onSelect.AddListener(BlockAction);
        drawField.onSelect.AddListener(BlockAction);
        countField.onSelect.AddListener(BlockAction);

        winField.onEndEdit.AddListener(UnblockAction);
        drawField.onEndEdit.AddListener(UnblockAction);
        countField.onEndEdit.AddListener(UnblockAction);

        pc.ThrowStart += StartThrow;
        pc.ScoreChanged += EndThrow;
    }

    private void StartThrow()
    {
        sidePanel.interactable = false;
    }

    private void EndThrow(int a)
    {
        sidePanel.interactable = true;
    }

    private void BlockAction(string s)
    {
        throwBtn.interactable = false;
    }
    private void UnblockAction(string s)
    {
        throwBtn.interactable = true;
    }
    public void RandomToggle()
    {
        setPanel.interactable = !setPanel.interactable;
    }

}
