using UnityEngine;

public class GetCheckpoint : MonoBehaviour
{
    public Transform spawn;
    GameObject newSpawn;
    GameObject oldSpawn;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            spawn = collision.gameObject.transform;
            oldSpawn = newSpawn;
            if (oldSpawn != null) oldSpawn.SetActive(true);
            newSpawn = collision.transform.GetChild(0).gameObject;
            newSpawn.SetActive(false);
        }
    }
}
