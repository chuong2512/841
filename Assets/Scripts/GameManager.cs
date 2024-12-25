using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("All required scripts by GM are here")]
    public CharacterParent characterParent;
    public Player player;


    [Header("Drag gameobjects of scene here")]
    public GameObject characterParentGroup;
    public GameObject mainCamera;
    public GameObject bullet;
    public GameObject GeneralUiElements;

    public Animator castleAnimator;
    public Animator dianoAnim;

    public Transform castleVanishingPoint;
    public Transform castleCameraPoint;
    public Transform cannonShootPoint;
    public Transform characterEndReachPoint;


    [Header("Lists goes here")]
    public List<Material> characterColors;
    public List<GameObject> playersList;
    public List<Material> trailEffects;


    [Header("All Booleans are here")]
    public bool characterMove;
    public bool characterMoveDead;
    public bool dragControl;
    public bool cameraFollow;
    public bool moveCamera;
    public bool fire;
    public bool startCannonRotation;

    [Header("All ints and floats are here")]
    public float totalEnemies;
    public float enemiesDied;
    public int bullets;
    public float time;

    [Header("All UI elements goes here")]
    public Image playerProgressBar;
    public Image enemyProgressBar;
    public TextMeshProUGUI playerNumber;
    public TextMeshProUGUI enemyNumber;
    public GameObject levelComplete;
    public GameObject levelFail;
    public GameObject swipe;

    [Header("VFX goes here")]
    public GameObject addEffect;
    public GameObject dieEffext;
    public GameObject popParticle1;
    public GameObject popParticle2;

    [Header("Sounds goes here")]
    public AudioClip addPlayer;
    public AudioClip diePlayer;
    public AudioClip canonShot;
    public AudioClip win;
    public AudioClip loose;
    public AudioClip reachCastle;

    public Animator canonAnim;

    AudioSource audioSource;
    float timer;
    int counter = 0;
    int endMusicCounter = 0;
    int touchedFinishlineInteger = 0;

    bool touchedFinishLine;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        AddPlayersInList();
        dragControl = true;
        cameraFollow = true;
        timer = time;
        PlayerCounter();
        audioSource = this.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !characterMove && !characterMoveDead)
        {
            characterMove = true;
            characterMoveDead = true;
            player.Run();
        }


        PlayerProgress();
        EnemyProgress();

        if (fire && Input.GetMouseButton(0))
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                if (counter < playersList.Count)
                {
                    GameObject bulletObj = Instantiate(bullet, cannonShootPoint.position, cannonShootPoint.rotation);
                    bulletObj.transform.rotation = Quaternion.Euler(58, 0, 0);
                    canonAnim.Play("Canon fire");                    
                    CanonShot();                   
                    counter++;
                    bullets--;
                    playerNumber.text = bullets.ToString();
                }
                timer = time;

                if(bullets == 0 && (totalEnemies == enemiesDied))
                {
                    if(endMusicCounter == 0)
                    {
                        StartCoroutine(LevelCompleteDelay());
                        Win();
                        endMusicCounter++;
                    }
                    
                }
                else if(bullets == 0 && (totalEnemies != enemiesDied))
                {
                    if (endMusicCounter == 0)
                    {
                        StartCoroutine(LevelFailDelay());
                        endMusicCounter++;
                    }
                    
                }
            }
        }

        if (characterMove)
        {
            characterParent.Move();
        }

        if(touchedFinishLine)
        {
            InToTheCastle();
        }

        if(moveCamera)
        {
            MoveCamera();
        }

    }

    public void CharacterParentCheck()
    {
        if(playersList.Count == 0)
        {
            characterMove = false;
            characterMoveDead = true;
            characterParent.Stop();
            StartCoroutine(LevelFailDelay());
        }
    }

    public void AddPlayersInList()
    {
        for (int i = 0; i < characterParentGroup.gameObject.transform.childCount; i++)
        {
            playersList.Add(characterParentGroup.gameObject.transform.GetChild(i).gameObject);
        }
    }

    public void InToTheCastle()
    {
        if(Vector3.Distance(characterParentGroup.transform.position, castleVanishingPoint.position) > 0.2f)
        {
            characterParentGroup.transform.position = Vector3.MoveTowards(characterParentGroup.transform.position, castleVanishingPoint.position, 15 * Time.deltaTime);
        }
        else
        {
            for (int i = 0; i < playersList.Count; i++)
            {
                playersList[i].gameObject.GetComponent<Animator>().SetInteger("Player", 0);
            }
            touchedFinishLine = false;
        }
    }

    public void MoveCamera()
    {
        playerProgressBar.transform.parent.gameObject.SetActive(false);

        if (Vector3.Distance(mainCamera.transform.position, castleCameraPoint.position) > 0.2f)
        {

            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, castleCameraPoint.position, 50 * Time.deltaTime);                        
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, Quaternion.Euler(8.244f, 0, 0), 2 * Time.deltaTime);
        }
        else
        {
            dianoAnim.SetInteger("Diano", 1);
            moveCamera = false;
            fire = true;
            bullets = playersList.Count;
            startCannonRotation = true;
            swipe.SetActive(true);
            for (int i = 0; i < playersList.Count; i++)
            {
                playersList[i].gameObject.SetActive(false);
            }
            enemyProgressBar.transform.parent.gameObject.SetActive(true);
        }

    }

    void EnemyProgress()
    {
        enemyNumber.text = enemiesDied.ToString();
        enemyProgressBar.fillAmount = enemiesDied / totalEnemies;
    }

    void PlayerProgress()
    {
        playerProgressBar.fillAmount = characterParentGroup.transform.position.z / characterEndReachPoint.position.z;
    }

    public void PlayerCounter()
    {
        bullets = playersList.Count;
        playerNumber.text = bullets.ToString();
    }

    public void TouchedFinishline()
    {
        if(touchedFinishlineInteger == 0)
        {
            ReachCastle();
            touchedFinishlineInteger++;
        }
        castleAnimator.SetInteger("Door", 1);
        characterMove = false;
        dragControl = false;
        touchedFinishLine = true;
        cameraFollow = false; 
        StartCoroutine(CloseTheDoor());
    }

    public void EnemyDiedIncrementer()
    {
        enemiesDied++;
    }

    //---------------------------Sound Effexts---------------------------
    public void AddPlayer()
    {
        audioSource.PlayOneShot(addPlayer);
    }

    public void DiePlayer()
    {
        audioSource.PlayOneShot(diePlayer);
    }

    public void CanonShot()
    {
        audioSource.PlayOneShot(canonShot);
    }

    public void Win()
    {
        audioSource.PlayOneShot(win);
    }

    public void Loose()
    {
        audioSource.PlayOneShot(loose);
    }

    public void ReachCastle()
    {
        audioSource.PlayOneShot(reachCastle);
    }
    //---------------------------Sound Effexts---------------------------

    //---------------------------Delay---------------------------
    IEnumerator CloseTheDoor()
    {
        yield return new WaitForSeconds(2.5f);
        castleAnimator.SetInteger("Door", 2);

        yield return new WaitForSeconds(1f);
        moveCamera = true;
    }

    IEnumerator LevelFailDelay()
    {
        GeneralUiElements.SetActive(false);
        yield return new WaitForSeconds(2);
        Loose();
        levelFail.SetActive(true);
        Canvas.instance.Activate_Privacy_Policy();
    }

    IEnumerator LevelCompleteDelay()
    {
        GeneralUiElements.SetActive(false);
        yield return new WaitForSeconds(2);
        popParticle1.SetActive(true);
        popParticle2.SetActive(true);
        levelComplete.SetActive(true);
        Canvas.instance.Activate_Privacy_Policy();

    }
    //---------------------------Delay---------------------------
}
