using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Handguard : XRSimpleInteractable
{
    [SerializeField] private float _recoilResistance;

    private bool _isGrabbed = false;

    public bool IsGrabbed => _isGrabbed;
    public float RecoilResistance => _recoilResistance;

    private new void OnDisable()
    {
        selectEntered.RemoveListener(HandguardGrabed);
        selectExited.RemoveListener(HandguardUngrabed);
    }

    private new void Awake()
    {
        selectEntered.AddListener(HandguardGrabed);
        selectExited.AddListener(HandguardUngrabed);
    }

    private void ChangeBool(bool state)
    {
        _isGrabbed = state;
    }

    private void HandguardGrabed(SelectEnterEventArgs interactor)
    {
        ChangeBool(true);
    }

    private void HandguardUngrabed(SelectExitEventArgs interactor)
    {
        ChangeBool(false);
    }
}

