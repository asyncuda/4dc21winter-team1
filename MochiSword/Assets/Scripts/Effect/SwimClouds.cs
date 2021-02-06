using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Library;
using DG.Tweening;
using UniRx;
using Players;

public class SwimClouds : MonoBehaviour
{
    [SerializeField] private PlayerMediator playerMediator; 

    [SerializeField] private GameObject DarkCloudLeft;

    [SerializeField] private GameObject DarkCloudRight;

    [SerializeField] private float SpecialAttackTime;

    [SerializeField] private float CloudBound;

    private Sequence Bound = DOTween.Sequence();

    private Sequence Open   = DOTween.Sequence();

    private Sequence Close   = DOTween.Sequence();


    private Vector3 LeftInitialPosition; 

    private Vector3 RightInitialPosition; 

    private void Awake()
    {
        LeftInitialPosition = DarkCloudLeft.transform.position;

        RightInitialPosition = DarkCloudRight.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMediator
            .OnSpecialPercentageChanged
            .Where(x => playerMediator.isBuffing)
            .Subscribe(_ => Open.Restart());

        playerMediator.OnSpecialPercentageChanged
            .Where(_ => playerMediator.isBuffing)
            .Where(_ => !Open.IsPlaying())
            .Subscribe(_ => Bound.Restart())
            ;

        playerMediator.OnSpecialPercentageChanged
            .Where(_ => !playerMediator.isBuffing)
            .Where(_ => !Bound.IsPlaying())
            .Subscribe(_ => Close.Restart())
            ;

        Open
            .Append(DarkCloudRight.transform.DOLocalMove(RightInitialPosition + Vector3.right * 5, 2)) //開く
            .Append(DarkCloudLeft.transform.DOLocalMove(LeftInitialPosition + Vector3.left * 5, 2)) //開く
            .Pause()
            //.SetLoops(0)
            .SetAutoKill(false)
            .SetLink(gameObject)
            ;

        Bound
            .Append(DarkCloudLeft.transform.DOScale(Vector3.one * CloudBound, 0.5f))
            .Join(DarkCloudRight.transform.DOScale(Vector3.one * CloudBound, 0.5f))
            .Append(DarkCloudLeft.transform.DOScale(Vector3.one * 1.0f, 0.5f))
            .Join(DarkCloudRight.transform.DOScale(Vector3.one * 1.0f, 0.5f))
            .SetEase(Ease.Linear)
            .Pause()
            .SetLoops(-1)
            .SetLink(gameObject)
            ;

        Close
            .Append(DarkCloudLeft.transform.DOLocalMove(LeftInitialPosition, 3)) // 閉じる
            .Join(DarkCloudRight.transform.DOLocalMove(RightInitialPosition, 3)) // 閉じる
            .Pause()
            .SetLoops(0)
            .SetAutoKill(false)
            .SetLink(gameObject)
            ;
    }

    private void CloudMoving()
    {
        return;
    }

    private void SpecialTime()
    {
        var LeftSeq = DOTween.Sequence();

        LeftSeq
            .Append(DarkCloudLeft.transform.DOLocalMove(LeftInitialPosition + Vector3.left * 5, 2)) //開く
            .AppendCallback(() =>
            {
                DOTween.Sequence()
                .Append(DarkCloudLeft.transform.DOScale(Vector3.one * CloudBound, 0.5f))
                .Append(DarkCloudLeft.transform.DOScale(Vector3.one * 1.0f, 0.5f))
                .SetEase(Ease.Linear)
                .SetLoops((int)SpecialAttackTime)
                .SetLink(gameObject)
                .Play();
            })
            .AppendInterval(SpecialAttackTime) // 待機
            .Append(DarkCloudLeft.transform.DOLocalMove(LeftInitialPosition, 3)) // 閉じる
            .Play()
            ;

        var RightSeq = DOTween.Sequence();

         RightSeq
            .Append(DarkCloudRight.transform.DOLocalMove(RightInitialPosition + Vector3.right* 5, 2)) //開く
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
            .Append(DarkCloudRight.transform.DOLocalMove(RightInitialPosition, 3)) // 閉じる
            .Play()
            ;
    }
}
