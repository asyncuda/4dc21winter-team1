using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Library;
using DG.Tweening;

public class SwimClouds : MonoBehaviour
{
    [SerializeField] private GameObject DarkCloud;


    private void Awake()
    {

        
    }

    // Start is called before the first frame update
    void Start()
    {
        Debugger.Log("start");

        /*
        GameObject dc1 = Instantiate(DarkCloud) as GameObject;

        GameObject dc2 = Instantiate(DarkCloud) as GameObject;

        dc1.transform.position = new Vector3(0, 0, 0);

        dc2.transform.position = new Vector3(19, 0, 0);
        */

        CloudMoving();
    }

    private void CloudMoving()
    {
        GameObject dc = Instantiate(DarkCloud);

        dc.transform.position = new Vector3(0, 0, 0);

        dc.transform
            .DOMove(Vector3.left * 10, 3)
            .OnComplete(() => CloudMoving());

        Debugger.Log("作成！");



        dc.transform
            .DOMove(Vector3.left * 20, 3)
            .OnComplete(() => Destroy(dc));

        Debugger.Log("完了！");
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
