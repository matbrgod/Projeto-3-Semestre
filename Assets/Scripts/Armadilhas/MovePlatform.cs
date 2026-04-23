using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [Header("Waypoints")]
    [SerializeField] private Transform waypoints;

    [Header("Velocidade")]
    [SerializeField] private float speed;

    private float timeToWaypoint;
    private float elapsedTime;

    private int targetWaypointIndex;

    WaypointPath waypointPath;
    private Transform previousWaypoint;
    private Transform targetWaypoint;

    private void Start()
    {
        TargetNextWaypoint();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        float elapsedPercentage = elapsedTime / timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
        transform.position = Vector3.Lerp(previousWaypoint.position, targetWaypoint.position, elapsedPercentage);
        //transform.rotation = Quaternion.Lerp(previousWaypoint.rotation, targetWaypoint.rotation, elapsedPercentage);

        if (elapsedPercentage >= 1)
        {
            TargetNextWaypoint();
        }
    }

    private void TargetNextWaypoint()
    {
        previousWaypoint = waypointPath.HandleWaypoint(targetWaypointIndex);
        targetWaypointIndex = waypointPath.HandleNextWaypointIndex(targetWaypointIndex);
        targetWaypoint = waypointPath.HandleWaypoint(targetWaypointIndex);

        elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(previousWaypoint.position, targetWaypoint.position);
        timeToWaypoint = distanceToWaypoint / speed;
    }
}
