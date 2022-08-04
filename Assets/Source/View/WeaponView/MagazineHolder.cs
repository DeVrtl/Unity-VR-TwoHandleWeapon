using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSocketInteractor))]
public class MagazineHolder : MonoBehaviour
{
    private XRSocketInteractor _socket;
    private Weapon _weapon;
    private Magazine _magazine;

    public event UnityAction MagazineEntered;
    public event UnityAction MagazineEjected;

    private void Awake()
    {
        _socket = GetComponent<XRSocketInteractor>();
        _weapon = GetComponentInParent<Weapon>();
    }

    private void OnEnable()
    {
        _socket.selectEntered.AddListener(AddMagazine);
        _socket.selectExited.AddListener(RemoveMagazine);
    }

    private void OnDisable()
    {
        _socket.selectEntered.RemoveListener(AddMagazine);
        _socket.selectExited.RemoveListener(RemoveMagazine);
    }

    private void AddMagazine(SelectEnterEventArgs interactor)
    {
        _magazine = interactor.interactableObject.transform.GetComponent<Magazine>();
        _weapon.SetMagazine(_magazine);
        MagazineEntered?.Invoke();
    }

    private void RemoveMagazine(SelectExitEventArgs interactor)
    {
        _magazine = null;
        _weapon.SetMagazine(_magazine);
        MagazineEjected?.Invoke();
    }
}
