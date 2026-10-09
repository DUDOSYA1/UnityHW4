using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesController : MonoBehaviour
{
    [SerializeField] private PointsCounter pc;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject startPosition;
    [SerializeField] private float step;
    [SerializeField, Min(1)] private int cubesPerRow;

    private List<CubeThrower> cubes;

    public List<CubeThrower> Cubes
    {
        get { return cubes; }
        set { }
    }

    private void Awake()
    {
        cubes = new List<CubeThrower>();
    }

    public void SpawnCubes(int amount)
    {
        DespawnCubes();

        var origin = startPosition.transform.position;

        for (int i = 0; i < amount; i++)
        {
            var col = i % cubesPerRow;
            var row = i / cubesPerRow;

            var cube = Instantiate(prefab);
            cube.transform.position = new Vector3(origin.x + step * col, origin.y, origin.z - step * row);
            cube.name = "dice" + (i + 1);

            var thrower = cube.GetComponent<CubeThrower>();
            thrower.InProcess = false;
            cubes.Add(thrower);
        }
    }

    private void DespawnCubes()
    {
        foreach (var c in cubes)
        {
            if (c != null)
                Destroy(c.gameObject);
        }
        cubes.Clear();
        if (pc != null)
            pc.ClearDicesInfo();
    }
}
