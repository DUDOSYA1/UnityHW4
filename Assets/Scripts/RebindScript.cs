using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RebindScript : MonoBehaviour
{
    [SerializeField] private InputActionAsset asset;
    [SerializeField] private InputActionReference throwAction;
    [SerializeField] private TextMeshProUGUI text;
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    public void StartRebind()
    {
        asset.Disable();
        text.text = "Press any key";
        rebindingOperation = throwAction.action.PerformInteractiveRebinding().OnComplete(operation => RebindCompleted());
        rebindingOperation.Start();
    }

    private void RebindCompleted()
    {
        rebindingOperation.Dispose();
        text.text = "Rebind";
        asset.Enable();
    }
}
