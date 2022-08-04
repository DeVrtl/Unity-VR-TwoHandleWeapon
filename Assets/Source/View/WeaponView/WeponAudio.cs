using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class WeponAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private AudioClip _ejectMagazine;
    [SerializeField] private AudioClip _enterMagazine;

    private Weapon _weapon;
    private MagazineHolder _magazineHolder;

    private void Awake()
    {
        _weapon = GetComponent<Weapon>();
        _magazineHolder = GetComponentInChildren<MagazineHolder>();
    }

    private void OnEnable()
    {
        _weapon.WeaponShooting += OnWeaponShot;
        _magazineHolder.MagazineEntered += OnMagazineEntered;
        _magazineHolder.MagazineEjected += OnMagazineEjected;
    }

    private void OnDisable()
    {
        _weapon.WeaponShooting -= OnWeaponShot;
        _magazineHolder.MagazineEntered -= OnMagazineEntered;
        _magazineHolder.MagazineEjected -= OnMagazineEjected;
    }

    private void OnWeaponShot()
    {
        _source.PlayOneShot(_shootSound);
    }

    private void OnMagazineEjected()
    {
        _source.PlayOneShot(_ejectMagazine);
    }

    private void OnMagazineEntered()
    {
        _source.PlayOneShot(_enterMagazine);
    }
}
