using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EmptyBullet : MonoBehaviour
{
    private readonly float _lifeTime = 3f;

    public event UnityAction Created;

    private void Start()
    {
        Created?.Invoke();
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
