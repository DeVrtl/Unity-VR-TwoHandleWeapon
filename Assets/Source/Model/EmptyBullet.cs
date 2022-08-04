using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EmptyBullet : MonoBehaviour
{
    private readonly float _lifeTime = 3f;

    public event UnityAction EmptyBulletCreated;

    private void Start()
    {
        EmptyBulletCreated?.Invoke();
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
