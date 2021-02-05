using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDummy : MonoBehaviour
{
    public int Health;

    public float SpecialPercentage;

    private float elapse = 0f ;

    // Start is called before the first frame update


    private void Awake()
    {
        if(Health == 0) Health = 2400;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        elapse += Time.deltaTime;

        if(elapse >= 3 &&  Health > 0)
        {
            Health -= 600;
            SpecialPercentage += 0.35f;
            elapse = 0f;
        }
    }
}
