using UnityEngine;

public class SawSpinner : MonoBehaviour
{
    [SerializeField] private Vector3 spinAxis = Vector3.forward; // change if needed
    [SerializeField] private float spinSpeed = 360f; // degrees per second

    private void Update()
    {
        transform.Rotate(spinAxis.normalized, spinSpeed * Time.deltaTime, Space.Self);
    }
}
