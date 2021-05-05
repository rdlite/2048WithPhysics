using UnityEngine;

public class CubePrefabs : MonoBehaviour
{
    [SerializeField] private GameObject[] _cubes;

    public GameObject GetCubePrefab(int id)
    {
        return _cubes[id];
    }
}