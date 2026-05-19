using System.Collections.Generic;
using UnityEngine;

public class MoveSpiralPlatform : MonoBehaviour
{
    WaypointPath waypointPath;

    [Header("Velocidade")]
    [SerializeField] private float speed;

    private int currentWaypoint = 0;

    //public Vector3 currentTarget => waypointPath.HandleWaypoint(currentWaypoint).position;

    private void Awake()
    {
        waypointPath = FindFirstObjectByType<WaypointPath>();
    }

    private void OnEnable()
    {
        currentWaypoint = 0;
        //transform.position = waypointPath.HandleWaypoint(0).position;
    }

    void FixedUpdate()
    {
        //if(waypointPath.destroyPlatform) Destroy(gameObject);
        Move();
    }

    void Move()
    {
        float time = Time.deltaTime * speed;
        time = Mathf.SmoothStep(0, 1, time);
        //transform.position = Vector3.MoveTowards(transform.position, currentTarget, time);
        //if (transform.position == currentTarget) waypointPath.HandleNextWaypointIndex(currentWaypoint);
    }

    //private void TargetNextWaypoint()
    //{
    //    int nextWaypoint = currentWaypoint + 1;

    //    if (nextWaypoint >= waypoints.Count)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        currentWaypoint++;
    //    }
    //}

    private void OnTriggerEnter(Collider collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider collision)
    {
        collision.transform.SetParent(null);
    }

    //private void Update()
    //{
    //    if (transform.position == movePlatform.waypoints[waypointCount].position)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
