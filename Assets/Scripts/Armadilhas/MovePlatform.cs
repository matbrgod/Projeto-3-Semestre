using UnityEngine;
using System.Collections.Generic;

public class MovePlatform : MonoBehaviour
{
    [Header("Waypoints")]
    [SerializeField] private List<Transform> waypoints;

    [Header("Velocidade")]
    [SerializeField] private float speed;

    private int currentWaypoint = 0;

    private Vector3 currentTarget => waypoints[currentWaypoint].position;

    private void OnEnable()
    {
        currentWaypoint = 0;
        transform.position = waypoints[0].position;
    }

    void Update()
    {
        Move();
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
        currentWaypoint++;
        if(currentWaypoint >= waypoints.Count)
            currentWaypoint = 0;
    }
}
