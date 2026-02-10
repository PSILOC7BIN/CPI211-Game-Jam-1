using UnityEngine;

public class MoverPingPong : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Vector3 moveDirection = Vector3.right; // left-right
    [SerializeField] private float distance = 3f;
    [SerializeField] private float speed = 2f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        moveDirection = moveDirection.normalized;
    }

    private void Update()
    {
        float t = Mathf.PingPong(Time.time * speed, 1f);
        // smooth it so it doesn't snap at ends
        t = t * t * (3f - 2f * t);

        transform.position = startPos + moveDirection * (t * distance);
    }
}
