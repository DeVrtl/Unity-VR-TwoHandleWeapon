using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EmptyBullet : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _lifeTime;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rigidbody.AddRelativeForce(Vector3.right * _force);
    }

    private void Update()
    {
        StartCoroutine(DelayDestroy());
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(_lifeTime);

        Destroy(gameObject);
    }
}
