using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;
    public Rigidbody playerBody;

    [Header("Base Offset")]
    public float followDistance = 10f;
    public float height = 7f;

    [Header("Downhill Arc Offset")]
    public float downhillExtraHeight = 6f;
    public float downhillForwardBoost = 4f;

    [Header("Downhill Conditions")]
    public float groundCheckDistance = 2.0f;   // how close ground must be to count grounded
    public float minSlope01 = 0.10f;           // ignore tiny bumps (0.05-0.15 good)
    public float minDownhillSpeed = 0.4f;      // small dips won't trigger

    [Header("Look")]
    public float lookHeight = 1.5f;

    [Header("Slope Tilt")]
    public float maxDownTilt = 40f;

    [Header("Smoothing")]
    public float posSmoothTime = 0.12f;
    public float tiltSmooth = 8f;
    public float downhillBlendSmooth = 8f;

    private Vector3 posVel;
    private float currentTilt;
    private float downhillBlend;

    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        if (playerBody == null && player != null)
            playerBody = player.GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Ground + slope check
        bool grounded = false;
        float slope01 = 0f;

        Ray ray = new Ray(player.position + Vector3.up * 0.2f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, groundCheckDistance))
        {
            grounded = true;

            float flatness = Vector3.Dot(hit.normal, Vector3.up); // 1 flat, smaller = steeper
            slope01 = Mathf.Clamp01(1f - flatness);
        }

        // Downhill speed check (only while grounded)
        float downVel = 0f;
        if (playerBody != null) downVel = -playerBody.linearVelocity.y;

        bool onRealSlope = slope01 >= minSlope01;
        bool trulyDownhill = grounded && onRealSlope && downVel >= minDownhillSpeed;

        float targetBlend = trulyDownhill ? 1f : 0f;
        downhillBlend = Mathf.Lerp(downhillBlend, targetBlend, downhillBlendSmooth * Time.deltaTime);

        // Blend offsets (arc)
        float d = followDistance - downhillForwardBoost * downhillBlend;
        float h = height + downhillExtraHeight * downhillBlend;

        Vector3 desiredPos = player.position + new Vector3(0f, h, -d);
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref posVel, posSmoothTime);

        // Tilt: based on slope, plus a little extra when downhill
        float targetTilt = slope01 * maxDownTilt;
        targetTilt += 10f * downhillBlend;

        currentTilt = Mathf.Lerp(currentTilt, targetTilt, tiltSmooth * Time.deltaTime);

        Vector3 lookTarget = player.position + Vector3.up * lookHeight;
        Quaternion baseRot = Quaternion.LookRotation(lookTarget - transform.position);
        transform.rotation = baseRot * Quaternion.Euler(currentTilt, 0f, 0f);
    }
}
