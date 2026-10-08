using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesController : MonoBehaviour
{
    [SerializeField] private PointsCounter pc;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject startPosition;
    [SerializeField] private float step;

    private List<CubeThrower> cubes;
    
    public List<CubeThrower> Cubes
    {
        get { return cubes; }
        set { }
    }

    private void Awake()
    {
        cubes = new List<CubeThrower>();

        SpawnCubes(3);
    }

    public void SpawnCubes(int amount)
    {
        DespawnCubes();
        for (int i = 1; i <= amount; i++)
        {
            var cube = Instantiate(prefab);
            cube.transform.position = new Vector3(startPosition.transform.position.x + step*i, startPosition.transform.position.y, startPosition.transform.position.z);
            cube.name = "dice" + i;
            cubes.Add(cube.GetComponent<CubeThrower>());
        }
    }
    
    private void DespawnCubes()
    {
        foreach(var c in cubes)
        {
            Destroy(c.gameObject);
        }
        cubes.Clear();
        pc.ClearDicesInfo();
    }
}
