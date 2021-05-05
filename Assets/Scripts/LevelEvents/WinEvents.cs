using System;
using UnityEngine;

public class WinEvents : MonoBehaviour
{
    [SerializeField] private float _timeToRestartAfterWinning = 1f;

    public event Action OnWon;

    private void Start()
    {
        OnWon += () =>
        {
            Invoke("RestartLevel", _timeToRestartAfterWinning);
        };
    }

    public void Win()
    {
        OnWon.Invoke();
    }

    private void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}