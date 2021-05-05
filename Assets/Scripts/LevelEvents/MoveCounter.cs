using UnityEngine;

public class MoveCounter : MonoBehaviour
{
    [SerializeField] private MovesCounterText _textToUpdate;

    public static MoveCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private int Moves;

    public void InterateMovesCounter()
    {
        Moves++;

        _textToUpdate.UpdateText(Moves);
    }
}
