using System;
using UnityEngine;

public class BarReactions : MonoBehaviour
{
    [SerializeField] private GameObject rightBar;
    private Vector3 rbScale;
    [SerializeField] private GameObject leftBar;
    private Vector3 lbScale;
    [SerializeField] private GameObject healthBowl;
    private Vector3 healthScale;
    private float time = 0.25f;
    private float scale = 1.25f;

    private void Awake()
    {
        rbScale = rightBar.transform.localScale;
        lbScale = leftBar.transform.localScale;
        healthScale = healthBowl.transform.localScale;

        EventManager.Instance.Register(GameEventTypes.OnReceiveDamage, OnReceiveDamage);
        EventManager.Instance.Register(GameEventTypes.OnGainHealth, OnGainHealth);
        EventManager.Instance.Register(GameEventTypes.OnAbility, OnAbility);
        EventManager.Instance.Register(GameEventTypes.OnConf, OnConf);
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unregister(GameEventTypes.OnReceiveDamage, OnReceiveDamage);
        EventManager.Instance.Unregister(GameEventTypes.OnGainHealth, OnGainHealth);
        EventManager.Instance.Unregister(GameEventTypes.OnAbility, OnAbility);
        EventManager.Instance.Unregister(GameEventTypes.OnConf, OnConf);    
    }

    private void OnGainHealth(object sender, EventArgs e)
    {
        ScaleBigAnim(healthBowl, healthScale * scale, healthScale, time);
    }

    private void OnReceiveDamage(object sender, EventArgs e)
    {
        ScaleSmallAnim(healthBowl, healthScale / scale, healthScale, time);
    }

    private void OnConf(object sender, EventArgs e)
    {
        ScaleBigAnim(rightBar, rbScale * scale, rbScale, time);

        ScaleSmallAnim(leftBar, lbScale / scale, lbScale, time);

        ScaleBigAnim(healthBowl, healthScale * scale, healthScale, time);
    }

    private void OnAbility(object sender, EventArgs e)
    {
        ScaleBigAnim(leftBar, lbScale * scale, lbScale, time);

        ScaleSmallAnim(rightBar, rbScale / scale, rbScale, time);
    }

    void ScaleBigAnim(GameObject bar, Vector3 maxScale, Vector3 scale, float time)
    {
        LeanTween.scale(bar, maxScale, time).setOnComplete(() => ScaleBack(bar, scale, time));
    }

    void ScaleSmallAnim(GameObject bar, Vector3 minScale, Vector3 scale, float time)
    {
        LeanTween.scale(bar, minScale, time).setOnComplete(() => ScaleBack(bar, scale, time));
    }

    void ScaleBack(GameObject bar, Vector3 scale, float time)
    {
        LeanTween.scale(bar, scale, time);
    }
}
