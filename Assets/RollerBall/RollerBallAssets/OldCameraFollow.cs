using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldCameraFollow : MonoBehaviour
{
    private GameObject player;
    private Rigidbody playerBody;

    [Header("Follow Settings")]
    public float followDistance = 5f;
    public float height = 5f;
    public float lookHeight = 2f;

    [Header("Smoothing")]
    public float posSmoothTime = 0.15f;   // lower = snappier, higher = smoother
    public float rotSmoothSpeed = 10f;    // higher = faster rotation

    private Vector3 posVel;               // SmoothDamp helper

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerBody = player.GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        if (player == null || playerBody == null) return;

        // Choose direction: velocity when moving, otherwise player's forward
        Vector3 v = new Vector3(playerBody.linearVelocity.x, 0, playerBody.linearVelocity.z);
        Vector3 dir = (v.sqrMagnitude > 0.05f) ? v.normalized : player.transform.forward;

        // Desired camera position: behind direction + up
        Vector3 desiredPos = player.transform.position - dir * followDistance;
        desiredPos.y = player.transform.position.y + height;

        // Smooth position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref posVel, posSmoothTime);

        // Smooth look-at
        Vector3 lookTarget = player.transform.position + Vector3.up * lookHeight;
        Quaternion desiredRot = Quaternion.LookRotation(lookTarget - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, rotSmoothSpeed * Time.deltaTime);
    }
}
