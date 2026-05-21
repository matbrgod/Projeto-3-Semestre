using UnityEngine;
using System.Collections.Generic;

public class MovePlatform : MonoBehaviour
{
    [Header("Waypoints")]
    [SerializeField] public List<Transform> waypoints;

    [Header("Velocidade")]
    [SerializeField] private float speed;

    private int currentWaypoint = 0;
    public bool movingBack;

    public Vector3 currentTarget => waypoints[currentWaypoint].position;

    private void OnEnable()
    {
        currentWaypoint = 0;
        transform.position = waypoints[0].position;
    }

    void FixedUpdate()
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
        int nextWaypoint = currentWaypoint + 1;

        if (nextWaypoint >= waypoints.Count)
        {
            movingBack = true;
        }
        else if (currentWaypoint <= 0)
        {
            movingBack = false;
        }

        if (movingBack) currentWaypoint--;
        else currentWaypoint++;
    }

    private void OnTriggerEnter(Collider collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider collision)
    {
        collision.transform.SetParent(null);
    }
}
