using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    public bool destroyPlatform;
    public List<Transform> waypoints;

    //public Transform HandleWaypoint(int waypointIndex)
    //{
    //    return transform.GetChild(waypointIndex);
    //}

    public int HandleNextWaypointIndex(int currentWaypointIndex)
    {
        int nextWaypointIndex = currentWaypointIndex + 1;

        if (nextWaypointIndex == transform.childCount)
        {
            destroyPlatform = true;
        }

        return nextWaypointIndex;
    }
}
