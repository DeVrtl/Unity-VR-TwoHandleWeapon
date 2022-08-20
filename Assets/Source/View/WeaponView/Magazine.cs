using UnityEngine;

public class Magazine : MonoBehaviour
{
    [SerializeField] private int _capacity;

    private void OnValidate()
    {
        _capacity = Mathf.Clamp(_capacity, 0, int.MaxValue);
    }

    public int Capacity => _capacity;

    public void TakeBullets(int perShot)
    {
        _capacity -= perShot;
    }
}
