using UnityEngine;

[RequireComponent(typeof(CubePrefabs))]
public class CubeMerger : MonoBehaviour
{
    public static CubeMerger Instance { get; private set; }

    private CubePrefabs _cubePrefabs;

    public Transform _cubesParent;

    [SerializeField] private float MergingPower = 500f;

    [SerializeField] private int _cubeIndexToWin = 10;
    private int _maxCubeIndexOnMap = 2;

    [SerializeField] private WinEvents _winEvents;

    [SerializeField] private GameObject _mergingParticleEffect;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _cubePrefabs = GetComponent<CubePrefabs>();
    }

    public void MergeCubes(CubeData cube0, CubeData cube1)
    {
        if (cube0 == null || cube1 == null)
        {
            return;
        }

        int newCubeID = GetPowerOfTwoByCubeNumber(cube0.GetCubeNumber());

        if (newCubeID > _maxCubeIndexOnMap)
        {
            _maxCubeIndexOnMap = newCubeID;
        }

        GameObject _newCubePrefab = _cubePrefabs.GetCubePrefab(newCubeID);

        Vector3 newCubePosition = cube1.transform.position;

        Destroy(cube0.gameObject);
        Destroy(cube1.gameObject);

        GameObject newCube = Instantiate(_newCubePrefab, newCubePosition, Quaternion.Euler(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f)), _cubesParent);
        newCube.GetComponent<Rigidbody>()?.AddForce((Vector3.up + Vector3.forward + Random.insideUnitSphere) * MergingPower, ForceMode.Impulse);

        SpawnMergingEffect(newCube.transform.position);

        CheckForWin(newCubeID);
    }

    private void CheckForWin(int newCubeID)
    {
        if (newCubeID == _cubeIndexToWin)
        {
            _winEvents.Win();
        }
    }

    private void SpawnMergingEffect(Vector3 position)
    {
        Destroy(Instantiate(_mergingParticleEffect, position, Quaternion.identity), 1f);
    }

    public int GetMaxCubeIndexOnMap()
    {
        return _maxCubeIndexOnMap;
    }

    private int GetPowerOfTwoByCubeNumber(int number)
    {
        int idCounter = 0;

        while (number > 1)
        {
            idCounter++;
            number = number >> 1;
        }

        return idCounter;
    }
}