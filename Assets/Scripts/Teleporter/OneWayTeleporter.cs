using UnityEngine;

public class OneWayTeleporter : MonoBehaviour
{
    [SerializeField] private GameObject objectToTeleport;
    [SerializeField] private string nameOfLayerObjectToTeleportIsIn;
    [SerializeField] private GameObject end;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(nameOfLayerObjectToTeleportIsIn))
        {
            objectToTeleport.transform.position = end.transform.position;
            objectToTeleport.transform.rotation = end.transform.rotation;

        }
    }
}
