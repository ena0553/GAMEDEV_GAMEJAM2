using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class GenericParkZoneTrigger : MonoBehaviour
{
    private Collider triggerCollider;
    private bool isTriggered = false;
    private readonly float movementThreshold = 0.01f; /* if car's linear and angular velocity
        is less than this, it is considered not moving */

    void Start()
    {
        triggerCollider = GetComponent<Collider>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered &&
            ObjectRegistry.playerRigidBody.linearVelocity.sqrMagnitude < movementThreshold &&
            ObjectRegistry.playerRigidBody.angularVelocity.sqrMagnitude < movementThreshold)
        {
            isTriggered = true;
            StopCar();
            DoTriggerBehavior();
            triggerCollider.enabled = false; // trigger only works once
        }
    }

    protected abstract void DoTriggerBehavior();

    private void StopCar()
    {
        ObjectRegistry.playerRigidBody.linearVelocity = Vector3.zero;
        ObjectRegistry.playerRigidBody.angularVelocity = Vector3.zero;
    }
}
