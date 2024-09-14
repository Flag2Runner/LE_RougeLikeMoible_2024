using UnityEngine;

public class AimingComponent : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private float aimRange = 10f;
    [SerializeField] private LayerMask aimMask;
    [SerializeField] private bool bOverrides = true;
    [SerializeField] private float heightOverride = 1.7f;
    [SerializeField] private float fwdOffset = 0.5f;
    //TODO: Remove this
    private Vector3 _debugAimStart;
    private Vector3 _debugAimDir;
    
    public GameObject GetAimTarget(Transform aimTransform = null)
    {
        Vector3 aimStart = muzzle.position;
        Vector3 aimDir = muzzle.forward;
        if (aimTransform)
        {
            aimStart = aimTransform.position;
            aimDir = aimTransform.forward;
        }

        if (bOverrides)
        {
            aimStart.y = heightOverride;
            aimDir += aimDir * fwdOffset;
        }

        _debugAimStart = aimStart;
        _debugAimDir = aimDir;
        
        if (Physics.Raycast(_debugAimStart, _debugAimDir, out RaycastHit hitInfo, aimRange, aimMask))
        {
            return hitInfo.collider.gameObject;
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_debugAimStart, _debugAimStart + _debugAimDir * aimRange);
    }
}