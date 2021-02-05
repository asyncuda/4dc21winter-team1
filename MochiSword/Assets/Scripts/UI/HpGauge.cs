using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;
using Players;
using Library;

public class HpGauge : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private PlayerMediator playerMediator;

    [SerializeField] private Image hpGauge;

    [SerializeField] private Image specialGauge;

    [SerializeField] private List<GameObject> SpecialGaugeIcon;

    private IReadOnlyReactiveProperty<float> hpPercentage;

    private IReadOnlyReactiveProperty<float> specialPercentage;

    private void Awake()
    {
        hpGauge.fillAmount = 1.0f;
        specialGauge.fillAmount = 0.0f;
    }

    void Start()
    {
        int HealthMax = 0;

        playerMediator
            .ObserveEveryValueChanged(c => c.Health)
            .Where(x => 0 < x).Take(1)
            .Subscribe(x => { HealthMax = x; Debugger.Log("Health Max is " + x); });

        hpPercentage = playerMediator
            .ObserveEveryValueChanged(c => (float)c.Health / HealthMax)
            .Where(_ => 0 < HealthMax)
            .ToReactiveProperty<float>();

        specialPercentage = playerMediator.OnSpecialPercentageChanged.ToReactiveProperty<float>();

        SetHpGauge();

        SetSpecialGauge();
    }

    private void SetHpGauge()
    {
        hpPercentage.Subscribe(
                    x =>
                    {
                        DOTween.To(
                            () => hpGauge.fillAmount,
                            num => hpGauge.fillAmount = num,
                            x,
                            1.0f
                            )
                            .SetLink(gameObject)
                            .Play()
                        ;
                    })
                    .AddTo(this);

        hpGauge
            .ObserveEveryValueChanged(c => c.fillAmount)
            .Where(x => 0f <= x && x < 0.34)
            .Subscribe(_ => hpGauge.color = Color.red)
            ;

        hpGauge
            .ObserveEveryValueChanged(c => c.fillAmount)
            .Where(x => 0.34 <= x && x < 0.67)
            .Subscribe(_ => hpGauge.color = Color.yellow)
            ;

        hpGauge
            .ObserveEveryValueChanged(c => c.fillAmount)
            .Where(x => 0.67 <= x)
            .Subscribe(_ => hpGauge.color = Color.green)
            ;
    }

    private void SetSpecialGauge()
    {
        specialPercentage.Subscribe(
            x =>
            {
                DOTween.To(
                    () => specialGauge.fillAmount,
                    num => specialGauge.fillAmount = num,
                    x,
                    1.0f
                    )
                    .SetLink(gameObject)
                    .Play()
                ;
                specialGauge.color = new Color(255, 255, 0, specialPercentage.Value + 0.1f);
            })
            .AddTo(this);

        specialGauge
            .ObserveEveryValueChanged(c => c.fillAmount)
            .Where(x => 0f <= x && x < 0.25f)
            .Subscribe(_ => SetIconUnique(-1)) 
            ;

        specialGauge
            .ObserveEveryValueChanged(c => c.fillAmount)
            .Where(x => 0.25f <= x && x < 0.5f)
            .Subscribe(_ => SetIconUnique(0))
            ;

        specialGauge
            .ObserveEveryValueChanged(c => c.fillAmount)
            .Where(x => 0.5f <= x)
            .Subscribe(_ => SetIconUnique(1))
            ;

        specialGauge
            .ObserveEveryValueChanged(c => c.fillAmount)
            .Where(x => 0.99f <= x)
            .Subscribe(_ => SetIconUnique(2))
            ;
    }

    private void SetIconUnique(int index)
    {
        foreach (var s in SpecialGaugeIcon)
        {
            s.SetActive(false);
        }

        if(index != -1) SpecialGaugeIcon[index].SetActive(true);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
