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

    void Update()
    {
        Move();
    }

    void Move()
    {
        float time = Time.deltaTime * speed;
        time = Mathf.SmoothStep(0, 1, time);
        //Vector3 movePoint = currentTarget * time;
        //rb.MovePosition(transform.position + movePoint);
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, time);
        if (transform.position == currentTarget) TargetNextWaypoint();
    }

    private void TargetNextWaypoint()
    {
        currentWaypoint++;
        if(currentWaypoint >= waypoints.Count)
            currentWaypoint = 0;
    }

    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.transform.CompareTag("Player"))
    //    {
    //        collision.transform.SetParent(transform, true);
    //    }
    //}

    //private void OnTriggerStay(Collider collision)
    //{
    //    if (collision.transform.CompareTag("Player"))
    //    {
    //        collision.transform.SetParent(transform, true);
    //    }
    //}

    //private void OnTriggerExit(Collider collision)
    //{
    //    if (collision.transform.CompareTag("Player"))
    //    {
    //        collision.transform.SetParent(transform, false);
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.CompareTag("Player"))
    //    {
    //        collision.transform.parent = transform;
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.transform.CompareTag("Player"))
    //    {
    //        collision.transform.parent = null;
    //    }
    //}
}
