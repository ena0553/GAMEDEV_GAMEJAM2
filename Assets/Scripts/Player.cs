using UnityEngine;

[RequireComponent(typeof(CarMovement))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    void Awake()
    {
        ObjectRegistry.playerMovement = GetComponent<CarMovement>();
        ObjectRegistry.playerRigidBody = GetComponent<Rigidbody>();
        ObjectRegistry.playerTransform = transform;
    }
}
