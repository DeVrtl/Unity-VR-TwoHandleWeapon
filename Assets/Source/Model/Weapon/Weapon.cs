using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    private readonly float _fireRate = 8.5f;
    private readonly int _bulletsSubtractPerShoot = 1;

    private Coroutine _shooting;
    private Magazine _magazine;

    public event UnityAction CreatingRaycast;
    public event UnityAction WeaponShooting;
    public event UnityAction WeponShoted;

    public virtual void Shoot()
    {
        _shooting = StartCoroutine(ShootWithFireRate());
    }

    public virtual void StopShoot()
    {
        if (_shooting == null)
            return;

        StopCoroutine(_shooting);
    }

    public void HitEnemy(Ray ray, RaycastHit hit)
    {
        if (hit.transform.TryGetComponent(out IHitable enemy))
            enemy.TakeHit(ray.direction);
    }
    
    public void Reload(Magazine magazine)
    {
        _magazine = magazine;
    }

    private IEnumerator ShootWithFireRate()
    {
        while (_magazine.Capacity > 0 && _magazine != null)
        {
            WeaponShooting?.Invoke();
            _magazine.SubtractCapacity(_bulletsSubtractPerShoot);
            WeponShoted?.Invoke();

            CreatingRaycast?.Invoke();

            yield return new WaitForSeconds(1 / _fireRate);
        }
    }
}
