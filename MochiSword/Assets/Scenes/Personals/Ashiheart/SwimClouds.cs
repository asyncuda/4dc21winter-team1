using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Library;
using DG.Tweening;

public class SwimClouds : MonoBehaviour
{
    [SerializeField] private GameObject DarkCloudLeft;

    [SerializeField] private GameObject DarkCloudRight;

    [SerializeField] private float SpecialAttackTime;

    [SerializeField] private float CloudBound;


    private Vector3 LeftInitialPosition; 

    private Vector3 RightInitialPosition; 

    private void Awake()
    {
        DOTween.Init();
        DOTween.defaultAutoPlay = AutoPlay.None;

        LeftInitialPosition = DarkCloudLeft.transform.position;

        RightInitialPosition = DarkCloudRight.transform.position;
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

        SpecialTime();
    }

    private void CloudMoving()
    {
    }

    private void SpecialTime()
    {
        var LeftSeq = DOTween.Sequence();

        LeftSeq
            .Append(DarkCloudLeft.transform.DOMove(LeftInitialPosition + Vector3.left * 5, 2)) //開く
            .AppendCallback(() =>
            {
                DOTween.Sequence()
                .Append(DarkCloudLeft.transform.DOScale(Vector3.one * CloudBound, 0.5f))
                .Append(DarkCloudLeft.transform.DOScale(Vector3.one * 1.0f, 0.5f))
                .SetEase(Ease.Linear)
                .SetLoops((int)SpecialAttackTime)
                .Play();
            })
            .AppendInterval(SpecialAttackTime) // 待機
            .Append(DarkCloudLeft.transform.DOMove(LeftInitialPosition, 3)) // 閉じる
            .Play()
            ;

        var RightSeq = DOTween.Sequence();

         RightSeq
            .Append(DarkCloudRight.transform.DOMove(RightInitialPosition + Vector3.right* 5, 2)) //開く
            .AppendCallback(() =>
            {
                DOTween.Sequence()
                .Append(DarkCloudRight.transform.DOScale(Vector3.one * CloudBound, 0.5f))
                .Append(DarkCloudRight.transform.DOScale(Vector3.one * 1.0f, 0.5f))
                .SetEase(Ease.Linear)
                .SetLoops((int)SpecialAttackTime)
                .Play();
            })
            .AppendInterval(SpecialAttackTime) // 待機
            .Append(DarkCloudRight.transform.DOMove(RightInitialPosition, 3)) // 閉じる
            .Play()
            ;
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
