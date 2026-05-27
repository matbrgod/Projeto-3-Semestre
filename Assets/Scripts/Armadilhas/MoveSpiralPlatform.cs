using System.Collections.Generic;
using UnityEngine;

public class MoveSpiralPlatform : MonoBehaviour
{
    WaypointPath waypointPath;

    [Header("Velocidade")]
    [SerializeField] private float speed;

    GameObject waypoints;

    private int currentWaypoint = 0;

    public Vector3 currentTarget => waypointPath.waypoints[currentWaypoint].position;

    private void Awake()
    {
        waypoints = GameObject.FindGameObjectWithTag("WaypointPath");
        waypointPath = waypoints.GetComponent<WaypointPath>();
    }

    private void OnEnable()
    {
        currentWaypoint = 0;
        transform.position = waypointPath.waypoints[0].position;
    }

    void FixedUpdate()
    {
        Move();
        //if (waypointPath.destroyPlatform) Destroy(gameObject);
    }

    void Move()
    {
        float time = Time.deltaTime * speed;
        time = Mathf.SmoothStep(0, 1, time);
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, time);
        if (transform.position == currentTarget) TargetNextWaypoint();
    }

    private void TargetNextWaypoint()
    {
        int nextWaypoint = currentWaypoint + 1;

        if (nextWaypoint >= waypointPath.waypoints.Count)
        {
            Destroy(gameObject);
        }
        else
        {
            currentWaypoint++;
        }
    }

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
    //    if (waypointPath.destroyPlatform)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
