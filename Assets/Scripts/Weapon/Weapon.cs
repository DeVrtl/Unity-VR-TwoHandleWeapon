using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private Handguard _handguard;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private XRSocketInteractor _magazineHolder;
    [SerializeField] private float _raycastDistance;
    [SerializeField] private float _recoilForce;
    [SerializeField] private float _fireRate;

    private Rigidbody _rigidbody;
    private Coroutine _shooting;
    private Magazine _magazine;

    public event UnityAction WeaponShot;
    public event UnityAction MagazineEmpty;
    public event UnityAction MagazineEntered;
    public event UnityAction MagazineEjected;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _magazineHolder.selectEntered.AddListener(AddMagazine);
        _magazineHolder.selectExited.AddListener(RemoveMagazine);
    }

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

    private IEnumerator ShootWithFireRate()
    {
        while (_magazine.Capacity > 0)
        {
            if (_magazine.Capacity < 0)
            {
                MagazineEmpty?.Invoke();
                yield return null;
            }

            WeaponShot?.Invoke();
            _magazine.SubtractCapacity(1);
            ApllyRecoil();

            Ray ray = new Ray(_shootPoint.position, _shootPoint.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, _raycastDistance)) 
            {
                if (hit.transform.TryGetComponent(out Enemy enemy))
                    enemy.TakeHit(ray.direction);
            }

            yield return new WaitForSeconds(1 / _fireRate);
        }
    }

    private void ApllyRecoil()
    {
        if (_handguard.IsGrabbed == true)
        {
            float recoil = _recoilForce - _handguard.RecoilResistance;

            CalculateRecoil(recoil);
        }
        else
        {
            CalculateRecoil(_recoilForce);
        }
    }

    private void CalculateRecoil(float recoil)
    {
        _rigidbody.AddRelativeForce(Vector3.back * recoil, ForceMode.Acceleration);
    }

    private void AddMagazine(SelectEnterEventArgs interactor)
    {
        _magazine = interactor.interactableObject.transform.GetComponent<Magazine>();
        MagazineEntered?.Invoke();
    }

    private void RemoveMagazine(SelectExitEventArgs interactor)
    {
        _magazine = null;
        MagazineEjected?.Invoke();
    }
}
