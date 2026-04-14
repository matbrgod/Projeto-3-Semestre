using UnityEngine;

public class ChuvaPersonagem : MonoBehaviour
{
    public Transform targetTransform;
    [SerializeField] private float followSpeed;
    private Vector3 FollowVel = Vector3.zero;
    void Start()
    {
        targetTransform = FindFirstObjectByType<PlayerManager>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 ChuvaVector = new Vector3(targetTransform.position.x, this.gameObject.transform.position.y, targetTransform.position.z);
        Vector3 targetPos = Vector3.SmoothDamp(transform.position, ChuvaVector, ref FollowVel, followSpeed);
        transform.position = targetPos;
    }
}
