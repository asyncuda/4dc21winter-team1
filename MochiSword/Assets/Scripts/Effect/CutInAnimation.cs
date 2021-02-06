using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CutInAnimation : MonoBehaviour
{
    [SerializeField] private GameObject BgWhite; 
    [SerializeField] private GameObject Human; 
    [SerializeField] private GameObject Letter; 
    [SerializeField] private GameObject Ink1; 
    [SerializeField] private GameObject Ink2;

    private Vector3 from;

    private Transform target;

    private Sequence SetUp;

    private Sequence CutIn;

    private void Awake()
    {
        target = Human.GetComponent<Transform>();

        from = target.position;

        SetUp = DOTween.Sequence();

        CutIn = DOTween.Sequence();
    }

    // Start is called before the first frame update
    void Start()
    {
        BgWhite.SetActive(false); 
        Human.SetActive(false);
        Letter.SetActive(false);
        Ink1.SetActive(false);
        Ink2.SetActive(false);

        Human.transform.position = new Vector3(Mathf.Sqrt(3.0f) * 20, -20f, 0f);
        //Human.transform.position = Vector3.zero;
        Letter.transform.localScale = Vector3.one * 0.8f;
        Ink1.transform.localScale = Vector3.one * 0.1f;

        CutIn
            .AppendCallback(() => { BgWhite.SetActive(true); Human.SetActive(true); })
            .Append(Human.transform.DOMove(Vector3.zero, 0.5f))
            .AppendCallback(() => Letter.SetActive(true))
            .Join(Letter.transform.DOScale(Vector3.one, 0.3f))
            .AppendInterval(0.3f)
            .AppendCallback(() => { Ink1.SetActive(true); Ink2.SetActive(true); })
            .Append(Ink1.transform.DOScale(Vector3.one, 0.5f))
            .Append(Ink2.transform.DOScale(Vector3.one, 0.5f))
            .AppendInterval(0.2f)
            .SetEase(Ease.Linear)
            .Append(this.transform.DORotate(Vector3.forward * -30, 0.2f))
            .Join(this.transform.DOScale(Vector3.zero, 0.2f))
            .Join(this.transform.DOMove(new Vector3(-160f, 9f, 0f), 0.2f))
            .SetAutoKill(false)
            .SetLink(gameObject)
            ;

        CutIn.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
