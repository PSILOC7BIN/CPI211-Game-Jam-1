using UnityEngine;

public class ShowNearPlayer : MonoBehaviour
{
    public float showDistance = 25f;

    private Transform playerTf;
    private Renderer[] rends;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) playerTf = p.transform;

        rends = GetComponentsInChildren<Renderer>(true);
    }

    void Update()
    {
        if (playerTf == null) return;

        float d = Vector3.Distance(transform.position, playerTf.position);
        bool visible = d <= showDistance;

        for (int i = 0; i < rends.Length; i++)
            if (rends[i] != null) rends[i].enabled = visible;
    }
}
