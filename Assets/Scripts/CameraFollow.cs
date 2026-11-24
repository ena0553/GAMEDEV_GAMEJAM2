using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform objToFollow;
    [SerializeField] private Vector3 localPosition;
    [SerializeField] private Vector3 localRotation;

    void Start()
    {
        if (objToFollow == null)
        {
            print("objToFollow is missing for the CameraFollow script");
        }
    }

    void LateUpdate()
    {
        transform.position = objToFollow.TransformPoint(localPosition);
        transform.rotation = objToFollow.rotation * Quaternion.Euler(localRotation);
    }
}
