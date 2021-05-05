using UnityEngine;

public class CubeData : MonoBehaviour
{
    [SerializeField] private int _cubeNumber;

    [HideInInspector] public bool IsJustSpawned;

    public int GetCubeNumber()
    {
        return _cubeNumber;
    }
}