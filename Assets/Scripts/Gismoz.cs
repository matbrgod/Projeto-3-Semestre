using UnityEngine;

public class MovementGismoz : MonoBehaviour
{
    PlayerMovement playerMovement;
    public Color gizmoColor;

     private void OnValidate()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void OnDrawGizmos()
    {
        Vector3 gizmoPosition = new Vector3(playerMovement.transform.position.x, playerMovement.raycastHeightOffSet, playerMovement.transform.position.z);
        Gizmos.color = gizmoColor;
        //Gizmos.DrawWireSphere(transform.position, playerMovement.raycastRadius);
    }
}
