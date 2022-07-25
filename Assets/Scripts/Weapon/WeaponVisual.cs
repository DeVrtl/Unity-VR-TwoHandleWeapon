using UnityEngine;

[RequireComponent(typeof(Weapon), typeof(Rigidbody))]
public class WeaponVisual : MonoBehaviour
{
    [SerializeField] private Handguard _handguard;
    [SerializeField] private ParticleSystem _shootEffect;
    [SerializeField] private Transform _emptyBulletsSpawnPoint;
    [SerializeField] private EmptyBullet _emptyBullet;
    [SerializeField] private float _recoilForce;

    private Rigidbody _rigidbody;
    private Weapon _weapon;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _weapon = GetComponent<Weapon>();
    }

    private void OnEnable()
    {
        _weapon.WeaponShot += OnWeaponShot;
        _weapon.WeponShoted += OnWeponShoted;
    }

    private void OnDisable()
    {
        _weapon.WeaponShot -= OnWeaponShot;
        _weapon.WeponShoted -= OnWeponShoted;
    }

    private void OnWeaponShot()
    {
        Instantiate(_emptyBullet, _emptyBulletsSpawnPoint.position, Quaternion.identity);

        _shootEffect.Play();
    }

    private void OnWeponShoted()
    {
        if (_handguard.IsGrabbed == false)
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
}
