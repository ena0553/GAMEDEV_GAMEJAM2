using UnityEngine;

public class ThwompParkZoneTrigger : GenericParkZoneTrigger
{
    // where to spawn the Thwomp based on player position
    [SerializeField] private Vector3 offsetFromPlayer = new(0, 20, 0);

    protected override void DoTriggerBehavior()
    {
        // stop player from moving to prevent them from dodging the Thwomp
        ObjectRegistry.playerMovement.enabled = false;

        // instantiate Thwomp above player
        Vector3 thwompPosition = ObjectRegistry.playerTransform.position + offsetFromPlayer;
        Quaternion thwompRotation = Quaternion.identity;
        Instantiate(ObjectRegistry.thwompPrefab, thwompPosition, thwompRotation);
    }
}
