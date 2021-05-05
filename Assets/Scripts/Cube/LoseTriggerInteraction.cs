using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTriggerInteraction : MonoBehaviour
{
    [SerializeField] private LoseEvents _loseEvents;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CubeData>() != null && !other.GetComponent<CubeData>().IsJustSpawned)
        {
            _loseEvents.Lose();
        }
    }
}