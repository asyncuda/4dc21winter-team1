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


    private Vector3 LeftInitialPosition; 

    private Vector3 RightInitialPosition; 

    private void Awake()
    {
        DOTween.Init();
        DOTween.defaultAutoPlay = AutoPlay.None;

        LeftInitialPosition = new Vector3
            (
            DarkCloudLeft.transform.position.x,
            DarkCloudLeft.transform.position.y,
            DarkCloudLeft.transform.position.z
            );

        RightInitialPosition = new Vector3
            (
            DarkCloudRight.transform.position.x,
            DarkCloudRight.transform.position.y,
            DarkCloudRight.transform.position.z
            );
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
        var leftseq1 = DOTween.Sequence();

        var leftseq2 = DOTween.Sequence();

        leftseq2
            .Append(DarkCloudLeft.transform.DOScale(Vector3.one * 1.05f, 0.5f))
            .Append(DarkCloudLeft.transform.DOScale(Vector3.one * 1.0f, 0.5f))
            .SetEase(Ease.Linear)
            .SetLoops((int)SpecialAttackTime);

        leftseq1
            .Append(DarkCloudLeft.transform.DOMove(LeftInitialPosition + Vector3.left * 5, 2))
            .AppendCallback(() => leftseq2.Play())
            .AppendInterval(SpecialAttackTime)
            .Append(DarkCloudLeft.transform.DOMove(LeftInitialPosition, 3))
            .Play()
            ;

        var rightseq1 = DOTween.Sequence();

        rightseq1
            .Append(DarkCloudRight.transform.DOMove(RightInitialPosition + Vector3.right * 5, 2))
            .AppendInterval(SpecialAttackTime)
            .Append(DarkCloudRight.transform.DOMove(RightInitialPosition, 3));

        var rightseq2 = DOTween.Sequence();

        rightseq2
            .Append(DarkCloudRight.transform.DOScale(Vector3.one * 1.2f, 0.5f))
            .Append(DarkCloudRight.transform.DOScale(Vector3.one * 1.0f, 0.5f))
            .SetEase(Ease.Linear)
            .SetLoops((int)SpecialAttackTime);
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
