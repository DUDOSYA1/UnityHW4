using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CubeThrower : MonoBehaviour
{
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
    }    

    public void ThrowCube()
    {
        if (!inProcess)
        {
            var dir = new Vector3(
            UnityEngine.Random.Range(-sideForceRange, sideForceRange),
            UnityEngine.Random.Range(minUpForceRange, maxUpForceRange),
            UnityEngine.Random.Range(-sideForceRange, sideForceRange));

            var applyPos = new Vector3(
                UnityEngine.Random.Range(-torqueRange, torqueRange),
                UnityEngine.Random.Range(-torqueRange, torqueRange),
                UnityEngine.Random.Range(-torqueRange, torqueRange));

            Debug.Log(gameObject.name);
            Debug.Log($"{dir.x} {dir.y} {dir.z}");

            rb.AddForceAtPosition(dir,applyPos, ForceMode.Impulse);

            inProcess=true;
        }
    }
}
