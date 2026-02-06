using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody body;
    //private GameObject ball;
    [SerializeField] private float maxSpeed = 3.5f;
    public float jumpPower = 10;
    [SerializeField] private float moveForce = 35f;
    public float brakeStrength = 0.85f;
    private Transform camTf;

    public Vector3 facing;
    public Vector3 perpendicular;

    [SerializeField]
    private bool isJumping = false;

    private float xInput;
    private float zInput;
    private bool braking;

    void Start()
    {
        //ball = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody>();

        camTf = GetComponentInChildren<UnityEngine.Camera>()?.transform;
        if (camTf == null && UnityEngine.Camera.main != null)
            camTf = UnityEngine.Camera.main.transform;

        facing = transform.forward;
        perpendicular = GetPerpendicular(facing);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            print("jump");
            isJumping = true;
            body.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");
        braking = Input.GetKey(KeyCode.B);
    }

    void FixedUpdate()
    {
        // Camera-relative directions on the XZ plane
        Vector3 forward = Vector3.forward;
        Vector3 right = Vector3.right;

        if (camTf != null)
        {
            forward = Vector3.ProjectOnPlane(camTf.forward, Vector3.up).normalized;
            right   = Vector3.ProjectOnPlane(camTf.right, Vector3.up).normalized;
        }

        Vector3 inputDir = (forward * zInput) + (right * xInput);

        if (braking)
        {
            Vector3 v0 = body.linearVelocity;
            Vector3 lateral0 = new Vector3(v0.x, 0f, v0.z);
            lateral0 *= (1f - brakeStrength);
            body.linearVelocity = new Vector3(lateral0.x, v0.y, lateral0.z);
        }

        body.AddForce(inputDir * moveForce, ForceMode.Force);

        Vector3 v = body.linearVelocity;
        Vector3 lateral = new Vector3(v.x, 0f, v.z);
        lateral = Vector3.ClampMagnitude(lateral, maxSpeed);
        body.linearVelocity = new Vector3(lateral.x, v.y, lateral.z);
    }

    //void FixedUpdate_base()
    //{
    //    float xInput = Input.GetAxis("Horizontal");
    //    float zInput = Input.GetAxis("Vertical");

    //    Vector3 movement = new Vector3(xInput, 0, zInput);
    //    movement*=speed;
    //    movement = Vector3.ClampMagnitude(movement,speed);
    //    body.AddForce(movement);
    //}

    
    private Vector3 GetPerpendicular(Vector3 inVec)
    {
        return new Vector3(inVec.z, 0, -inVec.x);
    }

    public void OnCollisionEnter(Collision col)
    {
        Vector3 delta = Vector3.zero;
        List<ContactPoint> list = new List<ContactPoint>();
        col.GetContacts(list);
       // print("Landing: " + col.contactCount);
        for(int i = 0; i < col.contactCount; i++)
        {
            delta += transform.position - list[i].point;
            //print(transform.position + " - " + list[i].point + " " + delta);
        }
        delta /= col.contactCount;
        //Debug.Log("Landing: Done " + delta + " --- " + Mathf.Abs(delta.y));
        if(Mathf.Abs(delta.y)>0.25)
            isJumping = false;
    }
}
