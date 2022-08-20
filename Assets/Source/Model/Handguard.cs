using UnityEngine.XR.Interaction.Toolkit;

public class Handguard : XRSimpleInteractable
{
    private bool _isGrabbed = false;

    public bool IsGrabbed => _isGrabbed;

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

    private void HandguardGrabed(SelectEnterEventArgs interactor)
    {
        _isGrabbed = true;
    }

    private void HandguardUngrabed(SelectExitEventArgs interactor)
    {
        _isGrabbed = false;
    }
}

