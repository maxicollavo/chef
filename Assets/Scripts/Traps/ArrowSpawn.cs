using System;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawn : MonoBehaviour
{

    [SerializeField]
    GameObject fruitPrefab;
    Pool<GameObject> poolFruit;
    private List<GameObject> fruitObjects = new List<GameObject>();

    bool isSpawned;
    public int maxFruits;
    public float timeToRestart;
    int fruitCount;
    private float fruitTimeCount;
    private float timeToStart;

    private void Awake()
    {
        fruitTimeCount = 0;
        timeToStart = UnityEngine.Random.Range(0.0f, 3.0f);
    }

    private void Start()
    {
        poolFruit = new Pool<GameObject>(CreateFruit, (gameObject) => gameObject.SetActive(true), (gameObject) => gameObject.SetActive(false), maxFruits);
        fruitCount = 0;


        EventManager.Instance.Register(GameEventTypes.OnRestart, Restart);
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unregister(GameEventTypes.OnRestart, Restart);
    }

    private void Restart(object sender, EventArgs e)
    {
        ResetPool();
    }

    private void Update()
    {
        if (timeToStart > 0)
        {
            timeToStart -= Time.deltaTime;
            return;
        }

        TimeCount();

        if (!isSpawned)
        {
            InstantiateFruit();
            isSpawned = true;
            fruitTimeCount = 0;
        }
    }

    void TimeCount()
    {
        fruitTimeCount += Time.deltaTime;

        if (fruitTimeCount >= timeToRestart)
        {
            isSpawned = false;
            ResetPool();
        }
    }

    private GameObject CreateFruit()
    {
        return Instantiate(fruitPrefab);
    }

    public void ResetPool()
    {
        fruitObjects.ForEach((gameObject) => poolFruit.ReturnObject(gameObject));
        fruitObjects.Clear();

        fruitCount = 0;
    }

    void InstantiateFruit()
    {
        GameObject fruitInstance = poolFruit.GetObject();
        fruitInstance.transform.position = transform.position;
        fruitObjects.Add(fruitInstance);

        var rb = fruitInstance.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;

        fruitCount++;
    }
}