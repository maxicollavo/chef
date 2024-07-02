using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private Camera mixCam;
    [SerializeField]
    private Camera respawnCam;
    [SerializeField]
    private Camera mainCam;
    private Vector3 initialPos;
    private bool isTransitioning;
    public bool checkpoint;
    public bool cintas;
    public Animator anim;
    private Coroutine fireCoroutine;

    [SerializeField]
    Transform startSpawn;

    public Transform PostitionCamMiniGame;
    public Transform PostitionCamRespawn;

    [SerializeField]
    Transform checkpointSpawn;
    [SerializeField]
    Transform cintasSpawn;

    public bool onFire;
    int maxHealth;
    public int health;
    public int maxLives;
    public int currentLives;
    public AudioSource DamageSound;
    public AudioSource SpeedBoostSound;
    public AudioSource lava;
    public AudioSource respawnSound;
    public AudioSource burnedSound;

    [SerializeField]
    GameObject firePanel;

    [SerializeField]
    GameObject miniGameText;

    [SerializeField]
    GameObject menu;

    [SerializeField]
    GameObject subtitleText;

    [SerializeField]
    GameObject movementText;

    [SerializeField]
    TextMeshProUGUI lives;

    [SerializeField]
    GameObject goodChefText;

    [SerializeField]
    GameObject badChefText;

    [SerializeField]
    GameObject playerHurtText;

    [SerializeField]
    GameObject crosshair;
    private WaitForSeconds time = new WaitForSeconds(0.5f);
    public static PlayerBehaviour Instance { get; private set; }

    [SerializeField]
    GameObject redPanel;

    [SerializeField]
    GameObject greenPanel;

    float boostTime = 0;

    private void Awake()
    {
        Instance = this;
        mainCam = Camera.main;
        EventManager.Instance.Register(GameEventTypes.OnReceiveDamage, OnReceiveDamage);
        EventManager.Instance.Register(GameEventTypes.OnGainHealth, OnGainHealth);
        EventManager.Instance.Register(GameEventTypes.OnAbility, OnAbility);
        EventManager.Instance.Register(GameEventTypes.OnConf, OnConf);
        EventManager.Instance.Register(GameEventTypes.OnRestart, OnRestart);
    }

    void Update()
    {
        lives.text = currentLives.ToString();

        if (GameManager.Instance.speedBoost)
        {
            boostTime += Time.deltaTime;
        }

        if (boostTime >= 1.5f)
        {
            boostTime = 0;
            GameManager.Instance.speedBoost = false;
        }

        if (health <= 0)
        {
            Death();
        }

        if (!onFire)
        {
            if (fireCoroutine != null)
            {
                firePanel.SetActive(false);
                burnedSound.Pause();
                StopCoroutine(fireCoroutine);
                fireCoroutine = null;
            }
        }
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unregister(GameEventTypes.OnReceiveDamage, OnReceiveDamage);
        EventManager.Instance.Unregister(GameEventTypes.OnGainHealth, OnGainHealth);
        EventManager.Instance.Unregister(GameEventTypes.OnAbility, OnAbility);
        EventManager.Instance.Unregister(GameEventTypes.OnConf, OnConf);
        EventManager.Instance.Unregister(GameEventTypes.OnRestart, OnRestart);
    }

    void Start()
    {
        maxHealth = 100;
        maxLives = 3;
        currentLives = maxLives;
        health = 80;
    }

    private void OnGainHealth(object sender, EventArgs e)
    {
        AddHealth(50);
    }

    private void OnRestart(object sender, EventArgs e)
    {
        currentLives = maxLives;
    }

    private void OnConf(object sender, EventArgs e)
    {
        AddHealth(40);
        StartCoroutine(ConfCoroutine());
    }

    private void OnAbility(object sender, EventArgs e)
    {
        StartCoroutine(AbilityCoroutine());
    }

    private void OnReceiveDamage(object sender, EventArgs e)
    {
        StartCoroutine(ReceiveDamageCoroutine());
    }

    private IEnumerator ReceiveDamageCoroutine()
    {
        if (badChefText.activeInHierarchy || goodChefText.activeInHierarchy)
        {
            badChefText.SetActive(false);
            goodChefText.SetActive(false);
        }

        DamageSound.Play();
        greenPanel.SetActive(false);
        playerHurtText.SetActive(true);
        ScaleChefText(playerHurtText);
        redPanel.SetActive(true);
        yield return time;
        redPanel.SetActive(false);
        yield return time;
    }

    private IEnumerator AbilityCoroutine()
    {
        if (badChefText.activeInHierarchy || playerHurtText.activeInHierarchy)
        {
            badChefText.SetActive(false);
            playerHurtText.SetActive(false);
        }

        redPanel.SetActive(false);
        goodChefText.SetActive(true);
        ScaleChefText(goodChefText);
        greenPanel.SetActive(true);
        yield return time;
        greenPanel.SetActive(false);
        yield return time;
    }

    private IEnumerator ConfCoroutine()
    {
        if (goodChefText.activeInHierarchy || playerHurtText.activeInHierarchy)
        {
            badChefText.SetActive(false);
            goodChefText.SetActive(false);
        }

        greenPanel.SetActive(false);
        badChefText.SetActive(true);
        ScaleChefText(badChefText);
        redPanel.SetActive(true);
        yield return time;
        redPanel.SetActive(false);
        yield return time;
    }

    private void ScaleChefText(GameObject text)
    {
        ScaleBigAnim(text, text.transform.localScale * 1.5f, text.transform.localScale, 0.5f);
        MovePosY(text, text.transform.localPosition.y + 1f, 0.5f);
    }

    private void ScaleBadChefText()
    {
        ScaleBigAnim(
            badChefText,
            badChefText.transform.localScale * 1.5f,
            badChefText.transform.localScale,
            0.5f
        );
    }

    void ScaleBigAnim(GameObject text, Vector3 maxScale, Vector3 originalScale, float time)
    {
        LeanTween
            .scale(text, maxScale, time)
            .setOnComplete(() => ScaleBack(text, originalScale, time));
    }

    void ScaleBack(GameObject text, Vector3 originalScale, float time)
    {
        LeanTween.scale(text, originalScale, time).setOnComplete(() => text.SetActive(false));
    }

    void MovePosY(GameObject text, float targetY, float duration)
    {
        LeanTween
            .moveLocalY(text, targetY, duration)
            .setEaseInBack()
            .setOnComplete(() => MovePosYBack(text, text.transform.localPosition.y, duration));
    }

    void MovePosYBack(GameObject text, float originalY, float duration)
    {
        LeanTween.moveLocalY(text, originalY - 1f, duration).setEaseInBack();
    }

    public void Suicide()
    {
        menu.SetActive(false);
        RelocatePlayer();
        TopDownCameraChange.changeCam = false;
        GameManager.Instance.canAttack = !GameManager.Instance.canAttack;
        GameManager.Instance.menuPressed = !GameManager.Instance.menuPressed;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void Death()
    {
        //GameManager.Instance.canAttack = true;
        currentLives--;
        if (currentLives <= 0)
        {
            EventManager.Instance.Dispatch(GameEventTypes.OnRestart, this, EventArgs.Empty);
            TopDownCameraChange.changeCam = false;
            return;
        }
        health = maxHealth;
        RelocatePlayer();
        //ResetEnemiesStats();
        TopDownCameraChange.changeCam = false;
    }

    private void ResetEnemiesStats()
    {
        foreach (var enemy in GameManager.Instance.enemies)
        { 
            var beh = enemy.GetComponent<EnemyBehaviour>();
            beh.health = beh.maxHealth;
            beh.LifeBar.value = beh.health;

            var recipe = enemy.GetComponent<EnemyRecipe>();
            recipe.hitNumber = UnityEngine.Random.Range(1, 4);
            recipe.gunType = recipe.GetGun();

            //Hacerle get component a requires para actualizar la receta, pero como es hijo de hijos del enemigo no se como hacerlo
        }
    }

    public void RelocatePlayer()
    {
        anim.SetTrigger("OnDie");

        if (checkpoint)
        {
            transform.position = checkpointSpawn.position;
        }
        else if (cintas)
        {
            transform.position = cintasSpawn.position;
        }
        else
        {
            transform.position = startSpawn.position;
        }
        respawnSound.Play();
    }

    public void TakeDamage(int dmg)
    {
        EventManager.Instance.Dispatch(GameEventTypes.OnReceiveDamage, this, EventArgs.Empty);
        health -= dmg;
    }
    
    public void AddHealth(int extraHealth)
    {
        health += extraHealth;
        if (health > 100)
        {
            health = maxHealth;
        }
    }

    private void DisableCamera(Camera cam)
    {
        cam.gameObject.SetActive(false);
        Camera.main.gameObject.SetActive(true);
    }

    private void TransitionMixCam(Camera cam)
    {
        if (GameManager.Instance.onMinigame == false && isTransitioning == false)
        {
            MixCamTrans.Instance.TransitionActive(PostitionCamMiniGame, mixCam.gameObject, mainCam.gameObject);
            Debug.LogError("Active");

            GameManager.Instance.onMinigame = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            StartCoroutine(Delay());
        }
     
    }

    public IEnumerator Delay()
    {
        isTransitioning = true;
        yield return new WaitForSeconds(1);
        isTransitioning = false;
    }

    public IEnumerator FireDamage()
    {
        while (onFire)
        {
            firePanel.SetActive(true);
            health -= 10;
            burnedSound.Play();
            yield return new WaitForSeconds(1);
        }
    }

    private void TransitionCamBack(Camera cam)
    {
        if (GameManager.Instance.onMinigame == true && isTransitioning == false)
        {
            MixCamTrans.Instance.TransitionActive(mainCam.transform, mainCam.gameObject, mixCam.gameObject);

            Debug.LogError("Back");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameManager.Instance.onMinigame = false;
            StartCoroutine (Delay());
        }
    }

    private void SpeedBoost()
    {
        if (GameManager.Instance.speedBoost)
            boostTime = 0;
        else
            GameManager.Instance.speedBoost = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dead"))
        {
            lava.Play();
            Death();
        }
       
        if (other.CompareTag("TriggerWin"))
        {
            SceneManager.LoadScene("WinScene");
        }

        if (other.CompareTag("SpeedBoost"))
        {
            SpeedBoost();
            SpeedBoostSound.Play();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("CheckPoint"))
        {
            checkpoint = true;
        }

        if (other.CompareTag("CintasCheckpoint"))
        {
            cintas = true;
        }

        if (other.CompareTag("OnDamage"))
        {
            onFire = true;
            if (fireCoroutine == null)
            {
                fireCoroutine = StartCoroutine(FireDamage());
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MixStation"))
        {
            subtitleText.SetActive(true);
            movementText.SetActive(false);
            miniGameText.SetActive(true);

            if (Input.GetKey(KeyCode.F))
            {
                if (!GameManager.Instance.onMinigame)
                {
                    TransitionMixCam(mixCam);
                    crosshair.SetActive(false);
                    GameManager.Instance.onMinigame = true;
                }
                else
                {
                    TransitionCamBack(mixCam);
                    crosshair.SetActive(true);
                    GameManager.Instance.onMinigame = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MixStation"))
        {
            subtitleText.SetActive(false);
            miniGameText.SetActive(false);
        }
        if (other.CompareTag("OnDamage"))
        {
            if(fireCoroutine != null)
            {
                firePanel.SetActive(false);
                burnedSound.Pause();
                StopCoroutine(fireCoroutine);
                fireCoroutine = null;
            }
        }
    }
}
