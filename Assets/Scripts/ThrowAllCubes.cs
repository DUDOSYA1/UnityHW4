using UnityEngine;

public class ThrowAllCubes : MonoBehaviour
{
    [SerializeField] private CubesController cc;

    private void Awake()
    {
        if (cc == null)
            Debug.LogError("No PointsCounter attached to btn");
    }
    public void ThrowAll()
    {
        foreach(var dice in cc.Cubes)
        {
            dice.ThrowCube();
        }
    }
}
