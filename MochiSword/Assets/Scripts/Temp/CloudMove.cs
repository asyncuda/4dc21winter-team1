using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    [SerializeField] CloudManager cloudManager = default;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) {
            cloudManager.StartCoroutine("FullPower");
        }
    }
}
