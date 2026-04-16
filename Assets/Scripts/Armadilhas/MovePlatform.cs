using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    public float moveVel; // velocidade da plataforma de se mexer
    float step;

    private void Start()
    {
        step = moveVel * 0.5f;
    }

    void Update()
    {
        MoveToPointA();
    }

    private void MoveToPointA()
    {
        transform.position = Vector3.MoveTowards(transform.position, pointA.position, step);
    }

    private void MoveToPointB()
    {
        transform.position = Vector3.MoveTowards(transform.position, pointB.position, step);
    }
}
