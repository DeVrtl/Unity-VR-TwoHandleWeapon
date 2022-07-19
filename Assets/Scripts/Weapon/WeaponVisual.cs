using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class WeaponVisual : MonoBehaviour
{
    [SerializeField] private ParticleSystem _shootEffect;
    [SerializeField] private AudioSource _source;
    [SerializeField] private Transform _emptyBulletsSpawnPoint;
    [SerializeField] private EmptyBullet _emptyBullet;
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private AudioClip _ejectMagazine;
    [SerializeField] private AudioClip _enterMagazine;
    [SerializeField] private AudioClip _emptyMagazine;

    private Weapon _weapon;

    private void Awake()
    {
        _weapon = GetComponent<Weapon>();
    }

    private void OnEnable()
    {
        _weapon.WeaponShot += OnWeaponShot;
        _weapon.MagazineEntered += OnMagazineEntered;
        _weapon.MagazineEjected += OnMagazineEjected;
        _weapon.MagazineEmpty += OnMagazineEmpty;
    }

    private void OnDisable()
    {
        _weapon.WeaponShot -= OnWeaponShot;
        _weapon.MagazineEntered -= OnMagazineEntered;
        _weapon.MagazineEjected -= OnMagazineEjected;
        _weapon.MagazineEmpty -= OnMagazineEmpty;
    }

    private void OnMagazineEjected()
    {
        _source.PlayOneShot(_ejectMagazine);
    }

    private void OnMagazineEntered()
    {
        _source.PlayOneShot(_enterMagazine);
    }

    private void OnMagazineEmpty()
    {
        _source.PlayOneShot(_emptyMagazine);
    }

    private void OnWeaponShot()
    {
        Instantiate(_emptyBullet, _emptyBulletsSpawnPoint.position, Quaternion.identity);

        _shootEffect.Play();
        _source.PlayOneShot(_shootSound);
    }
}
