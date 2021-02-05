using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2ndManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float speed = 0.2f;

    private Vector3 playerOffset;
    private Vector3 cameraPos;
    private Vector3 cameraVec;
    

    private void Start()
    {
        playerOffset = player.transform.position + transform.right * 3.4f;
        cameraPos = this.transform.position;
    }

    private void Update()
    {
        // 要編集
        if (player.transform.position.x > -2.6f && player.transform.position.x < 100.0f) {
            cameraVec = (player.transform.position - playerOffset) * speed;
            cameraVec.y = 0f;
            this.transform.position = cameraPos + cameraVec;
        }
    }
}