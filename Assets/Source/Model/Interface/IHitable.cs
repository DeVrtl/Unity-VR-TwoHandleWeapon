using UnityEngine;
using UnityEngine.Events;

public interface IHitable 
{
    public event UnityAction<Vector3> Hited;

    public void TakeHit(Vector3 direction);
}
