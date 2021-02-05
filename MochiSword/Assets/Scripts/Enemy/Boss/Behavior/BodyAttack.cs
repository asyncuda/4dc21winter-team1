using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

public class BodyAttack : MonoBehaviour
{
    private Vector3 from;

    private Transform target;

    private void Awake()
    {
        target = this.gameObject.GetComponent<Transform>();

        from = target.position; 
    }

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Sequence()
            .SetEase(Ease.Linear)
            .Append(target.DOMoveX(from.x + 0.08f, 0.5f))
            .Append(target.DOMoveX(from.x, 0.5f))
            .SetLoops(-1)
        ;


        /*
        DOTween.To(
            () => target.RotateAround(
            )
        */
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
