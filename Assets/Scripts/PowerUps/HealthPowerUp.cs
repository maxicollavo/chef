using System;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    private int extraHealthToPlayer = 100;
    [SerializeField] private AudioSource healingSource;

    private void Awake()
    {
        EventManager.Instance.Register(GameEventTypes.OnConf, OnConf);
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unregister(GameEventTypes.OnConf, OnConf);
    }

    private void OnConf(object sender, EventArgs e)
    {
        healingSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            PlayerBehaviour _pb = other.transform.parent.GetComponent<PlayerBehaviour>();

            if (_pb.health >= 100)
            {
                Debug.Log("Se tiene toda la vida");
            }
            else
            {
                EventManager.Instance.Dispatch(GameEventTypes.OnGainHealth, this, EventArgs.Empty);
                healingSource.Play();
                Destroy(gameObject);
            }
        }
    }
}
