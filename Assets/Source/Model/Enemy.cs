using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHitable
{
    public event UnityAction<Vector3> Hited;

    public void TakeHit(Vector3 direction)
    {
        Hited?.Invoke(direction);
        transform.parent = null;
    }
}
