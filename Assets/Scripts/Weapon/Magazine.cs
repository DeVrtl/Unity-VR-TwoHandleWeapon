using UnityEngine;

public class Magazine : MonoBehaviour
{
    [SerializeField] private int _capacity = 30;

    public int Capacity => _capacity;

    public void SubtractCapacity(int subtractionPerShot)
    {
        _capacity -= subtractionPerShot;
    }
}
