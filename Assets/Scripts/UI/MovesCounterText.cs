using UnityEngine;
using UnityEngine.UI;

public class MovesCounterText : MonoBehaviour
{
    public void UpdateText(int value)
    {
        GetComponent<Text>().text = value.ToString();
    }
}