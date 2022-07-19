using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _forwardPushForse;
    [SerializeField] private float _upPushForse;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void TakeHit(Vector3 direction)
    {
        Vector3 force = direction * _forwardPushForse + Vector3.up * _upPushForse;
        _rigidbody.AddForce(force, ForceMode.Impulse);
        transform.parent = null;
    }
}
