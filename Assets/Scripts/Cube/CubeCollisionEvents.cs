using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CubeData))]
public class CubeCollisionEvents : MonoBehaviour
{
    private bool _isCollided = false;

    private void OnCollisionEnter(Collision collision)
    {
        CubeData cubeData = collision.gameObject.GetComponent<CubeData>();
        
        if (cubeData != null && !_isCollided)
        {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<CubeData>().IsJustSpawned = false;

            if (cubeData.GetCubeNumber() == GetComponent<CubeData>().GetCubeNumber())
            {
                StartCoroutine(TimerToMerging(cubeData, GetComponent<CubeData>()));
            }
        }
    }

    private IEnumerator TimerToMerging(CubeData cube0, CubeData cube1)
    {
        float time = Random.Range(0f, .2f);

        yield return new WaitForSeconds(time);

        _isCollided = true;
        CubeMerger.Instance.MergeCubes(cube0, cube1);
    }
}