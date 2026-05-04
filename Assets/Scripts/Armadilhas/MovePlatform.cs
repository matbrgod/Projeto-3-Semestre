using UnityEngine;
using System.Collections.Generic;

public class MovePlatform : MonoBehaviour
{
    [Header("Waypoints")]
    [SerializeField] private List<Transform> waypoints;

    [Header("Velocidade")]
    [SerializeField] private float speed;

    Rigidbody rb;
    private int currentWaypoint = 0;
    public bool movingBack;

    private Vector3 currentTarget => waypoints[currentWaypoint].position;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

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
