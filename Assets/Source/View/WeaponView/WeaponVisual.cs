using UnityEngine;

[RequireComponent(typeof(Weapon), typeof(Rigidbody))]
public class WeaponVisual : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private HandguardView _handguardView;
    [SerializeField] private Handguard _handguard;
    [SerializeField] private ParticleSystem _shootEffect;
    [SerializeField] private Transform _emptyBulletsSpawnPoint;
    [SerializeField] private EmptyBullet _emptyBullet;
    [SerializeField] private float _recoilForce;
    [SerializeField] private float _raycastDistance;

    private Rigidbody _rigidbody;
    private Weapon _weapon;

    private void OnValidate()
    {
        _recoilForce = Mathf.Clamp(_recoilForce, 0, float.MaxValue);
        _raycastDistance = Mathf.Clamp(_raycastDistance, 0, float.MaxValue);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _weapon = GetComponent<Weapon>();
    }

    private void OnEnable()
    {
        _weapon.WeaponShooting += OnWeaponShooting;
        _weapon.CreatingRaycast += OnCreatingRaycast;
        _weapon.WeponShoted += OnWeponShoted;
    }

    private void OnDisable()
    {
        _weapon.WeaponShooting -= OnWeaponShooting;
        _weapon.CreatingRaycast -= OnCreatingRaycast;
        _weapon.WeponShoted -= OnWeponShoted;
    }

    private void OnWeaponShooting()
    {
        Instantiate(_emptyBullet, _emptyBulletsSpawnPoint.position, Quaternion.identity);

        _shootEffect.Play();
    }

    private void OnCreatingRaycast()
    {
        Ray ray = new Ray(_shootPoint.position, _shootPoint.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _raycastDistance))
        {
            _weapon.HitEnemy(ray, hit);
        }
    }

    private void OnWeponShoted()
    {
        if (_handguard.IsGrabbed == true)
        {
            float recoil = _recoilForce - _handguardView.RecoilResistance;

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
}
