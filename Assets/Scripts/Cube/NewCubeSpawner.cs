using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CubePrefabs))]
[RequireComponent(typeof(CubeMerger))]
public class NewCubeSpawner : MonoBehaviour
{
    [SerializeField] private Transform _startNewCubePosition;
    [SerializeField] private Transform _cubesParent;

    [SerializeField] private CubeMovementByScreen _cubeMovementByScreen;

    [SerializeField] private float _releasedCubeForce = 800f;

    private GameObject _spawnedCube;

    private int _cubesSpawnedCounter;

    private void Start()
    {
        _cubeMovementByScreen.OnCubeReleased += StartSpawnNewCube;
        _cubeMovementByScreen.OnCubeReleased += AddForceToSpawnedCube;

        StartSpawnNewCube();
    }

    private void StartSpawnNewCube()
    {
        StartCoroutine(SpawnNewCubeWithWaiting());
    }

    private IEnumerator SpawnNewCubeWithWaiting()
    {
        yield return new WaitForSeconds(1f);

        GameObject _cubeToSpawn = 
            _cubesSpawnedCounter == 0 ? 
            GetComponent<CubePrefabs>().GetCubePrefab(0) 
            : 
            GetComponent<CubePrefabs>().GetCubePrefab(Random.Range(0, GetComponent<CubeMerger>().GetMaxCubeIndexOnMap()));

        _spawnedCube = Instantiate(_cubeToSpawn, _startNewCubePosition.transform.position, Quaternion.Euler(0f, 180f, 0f), _cubesParent);

        _spawnedCube.GetComponent<CubeData>().IsJustSpawned = true;
        _spawnedCube.GetComponent<Rigidbody>().useGravity = false;

        _cubeMovementByScreen.SetCurrentCube(_spawnedCube);

        _cubesSpawnedCounter++;
    }

    private void AddForceToSpawnedCube()
    {
        if (_spawnedCube != null)
        {
            MoveCounter.Instance.InterateMovesCounter();

            _spawnedCube.GetComponent<Rigidbody>().velocity = -_spawnedCube.transform.forward * _releasedCubeForce;
        }
    }
}