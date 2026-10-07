using UnityEngine;
using UnityEngine.InputSystem;

public class CubeThrower : MonoBehaviour
{
    [SerializeField] private InputActionReference throwAction;
    [SerializeField][Range(0,100)] private float minUpForceRange;
    [SerializeField][Range(1,100)] private float maxUpForceRange;
    [SerializeField][Range(0,10)] private float sideForceRange;
    [SerializeField][Range(0,1)] private float torqueRange;

    private Rigidbody rb;
    private bool inProcess;
    public bool InProcess
    {
        get { return inProcess; }
        set { inProcess = value;}
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            Debug.LogError("No Rigidbody on cube");
        throwAction.action.started += ThrowCube;
    }    

    private void ThrowCube(InputAction.CallbackContext obj)
    {
        if (!inProcess)
        {
            var dir = new Vector3(
                Random.Range(-sideForceRange, sideForceRange),
                Random.Range(minUpForceRange, maxUpForceRange),
                Random.Range(-sideForceRange, sideForceRange));
            var applyPos = new Vector3(
                Random.Range(-torqueRange, torqueRange),
                Random.Range(-torqueRange, torqueRange),
                Random.Range(-torqueRange, torqueRange));

            rb.AddForceAtPosition(dir,applyPos, ForceMode.Impulse);

            inProcess=true;
        }
    }
}
