using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    public Transform HandleWaypoint(int waypointIndex)
    {
        return transform.GetChild(waypointIndex);
    }

    public int HandleNextWaypointIndex(int currentWaypointIndex)
    {
        int nextWaypointIndex = currentWaypointIndex + 1;

        if (nextWaypointIndex == transform.childCount)
        {
            nextWaypointIndex = 0;
        }

        return nextWaypointIndex;
    }
}
