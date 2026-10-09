using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;


public class CubeThrower : MonoBehaviour
{
    [SerializeField][Range(0, 100)] private float minUpForceRange;
    [SerializeField][Range(1, 100)] private float maxUpForceRange;
    [SerializeField][Range(0, 10)] private float sideForceRange;
    [SerializeField][Range(0, 50)] private float torqueRange;

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
    }

    public void ThrowCube()
    {
        if (inProcess)
            return;

        var force = new Vector3(
            UnityEngine.Random.Range(-sideForceRange, sideForceRange),
            UnityEngine.Random.Range(minUpForceRange, maxUpForceRange),
            UnityEngine.Random.Range(-sideForceRange, sideForceRange));

        var torque = new Vector3(
            UnityEngine.Random.Range(-torqueRange, torqueRange),
            UnityEngine.Random.Range(-torqueRange, torqueRange),
            UnityEngine.Random.Range(-torqueRange, torqueRange));

        rb.AddForce(force, ForceMode.Impulse);
        rb.AddTorque(torque, ForceMode.Impulse);

        inProcess = true;
    }
}
