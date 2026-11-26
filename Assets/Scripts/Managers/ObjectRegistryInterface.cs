using UnityEngine;

public class ObjectRegistryInterface : MonoBehaviour
{
    [SerializeField] private GameObject thwompPrefab;

    void Awake()
    {
        ObjectRegistry.thwompPrefab = thwompPrefab;
    }
}
