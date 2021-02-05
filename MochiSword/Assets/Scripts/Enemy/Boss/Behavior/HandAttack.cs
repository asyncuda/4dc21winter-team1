using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UniRx.Diagnostics;
using UniRx.Triggers;

public class HandAttack : MonoBehaviour
{
    [SerializeField] private Transform LeftHand;

    [SerializeField] private Transform RightHand;

    private Vector3 from;

    private Transform target;

    private Sequence Idle;

    private Sequence Chop;

    private Sequence Rocket;


    private float elapse = 0.0f;

    private void Awake()
    {
        target = gameObject.GetComponent<Transform>();

        from = target.position;

        Idle = DOTween.Sequence();

        Chop = DOTween.Sequence();

        Rocket = DOTween.Sequence();
    }
    // Start is called before the first frame update
    void Start()
    {
        var rnd = new System.Random();

        Observable
            .Interval(TimeSpan.FromSeconds(5))
            .Where(_ => Idle.IsPlaying())
            .Subscribe(_ =>
            {
                if(rnd.Next(0, 2) > 0)
                {
                    Rocket.Restart();
                }
                else
                {
                    Chop.Restart();
                }
            })
            ;

        Idle
            .SetEase(Ease.Linear)
            .Append(target.DOMoveY(from.y + 0.1f, 0.5f))
            .Append(target.DOMoveY(from.y, 0.5f))
            .SetLoops(-1)
            .SetAutoKill(false)
            .SetLink(gameObject)
            .Play()
            ;

        target = LeftHand; from = target.position;

        Chop
            .SetEase(Ease.Linear)
            .AppendCallback(() => Idle.Pause())
            .Append(target.DOMove(from, 0.1f))
            .Append(target.DOMove(from + Vector3.left * 12 + Vector3.up * 5, 0.5f))
            .Join(target.DORotate(from + Vector3.forward * 45, 0.5f))
            .AppendInterval(0.5f)
            .Append(target.DOMove(from + Vector3.left * 12 + Vector3.down* 2, 0.1f))
            .Join(target.DORotate(from + Vector3.forward * 90, 0.1f))
            .AppendInterval(0.5f)
            .Append(target.DOMove(from, 0.2f))
            .Join(target.DORotate(from, 0.1f))
            .AppendCallback(() => Idle.Restart())
            .Pause()
            .SetAutoKill(false)
            .SetLink(gameObject)
            ;

        target = RightHand; from = target.position;

        Rocket 
            .SetEase(Ease.Linear)
            .AppendCallback(() => Idle.Pause())
            .Append(target.DOMove(from, 0.1f))
            .AppendInterval(0.4f)
            .Join(target.DORotate(from + Vector3.forward * 90, 0.1f))
            .Append(target.DOMoveX(from.x - 5f, 0.2f))
            .Append(target.DOMoveX(from.x, 0.1f))
            .Join(target.DORotate(from, 0.1f))
            .AppendCallback(() => Idle.Restart())
            .Pause()
            .SetAutoKill(false)
            .SetLink(gameObject)
            ;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        elapse += Time.deltaTime;
        if(elapse > 10 && !Chop.IsPlaying())
        {
            Chop.Restart();
            elapse = 0f;
        }
        else if(elapse > 5 && !Rocket.IsPlaying())
        {
            Rocket.Restart();
        }
        */
    }
}
