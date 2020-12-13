using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void LateUpdate ()
    {
        LockCameraToTarget ();
    }

    private void LockCameraToTarget ()
    {
        this.transform.position = new Vector3 (target.position.x, this.transform.position.y, this.transform.position.z);
    }
}