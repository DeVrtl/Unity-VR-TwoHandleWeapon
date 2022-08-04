using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(EmptyBullet))]
public class EmptyBulletView : MonoBehaviour
{
    [SerializeField] private float _force;

    private Rigidbody _rigidbody;
    private EmptyBullet _emptyBullet;

    private void OnValidate()
    {
        _force = Mathf.Clamp(_force, 0, float.MaxValue);
    }

    private void OnEnable()
    {
        _emptyBullet.EmptyBulletCreated += OnEmptyBulletCreated;
    }

    private void OnDisable()
    {
        _emptyBullet.EmptyBulletCreated -= OnEmptyBulletCreated;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _emptyBullet = GetComponent<EmptyBullet>();
    }

    private void OnEmptyBulletCreated()
    {
        _rigidbody.AddRelativeForce(Vector3.right * _force);
    }
}
