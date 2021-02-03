using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HFollowCam : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;

    void Update()
    {
        transform.position = offset + new Vector3(target.transform.position.x, 0, 0);
    }
}
