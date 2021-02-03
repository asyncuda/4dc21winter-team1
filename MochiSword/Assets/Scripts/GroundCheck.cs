using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public string GroundTag = "Ground";

    private bool IsGround = false;
    private bool GroundEnter = false;
    private bool GroundStay = false;
    private bool GroundExit = false;

    public bool isGrounded()
    {
        if(GroundEnter && GroundStay)
        {
            IsGround = true;
        }else if (GroundExit)
        {
            IsGround = false;
        }

        GroundEnter = false;
        GroundStay = false;
        GroundExit = false;

        return IsGround;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == GroundTag)
        {
            GroundEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == GroundTag)
        {
            GroundStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == GroundTag)
        {
            GroundExit = true;
        }
    }
}
