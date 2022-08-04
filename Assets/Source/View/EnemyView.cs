using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Enemy))]
public class EnemyView : MonoBehaviour
{
    [SerializeField] private float _forwardPushForce;
    [SerializeField] private float _upPushForce;

    private Rigidbody _rigidbody;
    private Enemy _enemy;

    private void OnValidate()
    {
        _forwardPushForce = Mathf.Clamp(_forwardPushForce, 0, float.MaxValue);
        _upPushForce = Mathf.Clamp(_upPushForce, 0, float.MaxValue);
    }

    private void OnEnable()
    {
        _enemy.Hited += OnHited;
    }

    private void OnDisable()
    {
        _enemy.Hited -= OnHited;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _enemy = GetComponent<Enemy>();
    }

    private void OnHited(Vector3 direction)
    {
        Vector3 force = direction * _forwardPushForce + Vector3.up * _upPushForce;
        _rigidbody.AddForce(force, ForceMode.Impulse);
    }
}
