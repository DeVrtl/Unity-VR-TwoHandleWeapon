using UnityEngine;

public class HandguardView : MonoBehaviour
{
    [SerializeField] private float _recoilResistance;

    private void OnValidate()
    {
        _recoilResistance = Mathf.Clamp(_recoilResistance, 0, float.MaxValue);
    }

    public float RecoilResistance => _recoilResistance;
}
