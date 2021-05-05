using System;
using UnityEngine;

public class LoseEvents : MonoBehaviour
{
    [SerializeField] private float _timeToRestartAfterLosing = 1f;

    public event Action OnLose;

    private void Start()
    {
        OnLose += () =>
        {
            RestartLevel();
        };
    }

    public void Lose()
    {
        OnLose.Invoke();
    }

    private void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
