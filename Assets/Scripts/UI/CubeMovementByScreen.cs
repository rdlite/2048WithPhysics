using System;
using UnityEngine;

public class CubeMovementByScreen : MonoBehaviour
{
    public event Action OnCubeReleased;

    private GameObject _currentCube;

    [SerializeField] private float _minClampedXPosition = -2.4f, _maxClampedXPosition = 2.4f;

    [SerializeField] private Camera _mainCamera;

    [SerializeField] private LayerMask _raycastLayerMask;

    [SerializeField] private WinEvents _winEvents;

    private bool _isInteractible = true;

    private void Start()
    {
        _winEvents.OnWon += () =>
        {
            _isInteractible = false;
        };
    }

    public void SetCurrentCube(GameObject cube)
    {
        _currentCube = cube;
    }

    private void Update()
    {
        if (_isInteractible)
        {
            if (Input.GetMouseButton(0))
            {
                RaycastHit rayHit;
                Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out rayHit, _raycastLayerMask);

                if (rayHit.collider != null)
                {
                    if (_currentCube != null)
                    {
                        _currentCube.transform.localPosition =
                            new Vector3(
                                Mathf.Clamp(rayHit.point.x, _minClampedXPosition, _maxClampedXPosition),
                                _currentCube.transform.localPosition.y,
                                _currentCube.transform.localPosition.z);
                    }
                }
            }

            if (_currentCube != null && Input.GetMouseButtonUp(0))
            {
                _currentCube = null;
                OnCubeReleased.Invoke();
            }
        }
    }
}
