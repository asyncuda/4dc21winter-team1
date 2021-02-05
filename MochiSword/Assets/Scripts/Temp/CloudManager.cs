using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    [SerializeField] GameObject cloudL;
    [SerializeField] GameObject cloudR;

    private bool fullPower = false;

    public IEnumerator FullPower()
    {
        var wait = new WaitForSeconds(0.02f);   // 0.02f * 100
        if (!fullPower) {       // クールタイムが必要
            fullPower = true;
            for (int i = 0; i < 100; i++) {
                cloudL.transform.position += transform.right * -0.035f;
                cloudR.transform.position += transform.right * 0.035f;
                yield return wait;
            }
        } else {
            fullPower = false;
            for (int i = 0; i < 100; i++) {
                cloudL.transform.position += transform.right * 0.035f;
                cloudR.transform.position += transform.right * -0.035f;
                yield return wait;
            }
        }
    }
}