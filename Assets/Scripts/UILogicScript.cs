using UnityEngine;

public class UILogicScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup sidePanel;
    [SerializeField] private CanvasGroup setPanel;

    public void RandomToggle()
    {
        setPanel.interactable = !setPanel.interactable;
    }
}
