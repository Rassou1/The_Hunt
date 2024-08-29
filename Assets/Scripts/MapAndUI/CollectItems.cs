using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollectItems : MonoBehaviour
{
    [SerializeField] public Animator Dooranim;
    [SerializeField] public Animator Dooranim2;
    public GameObject Diamonds;
    private static int diamondsCollected = 0;
    private int requiredDiamonds = 1;
    private bool isCollected = false;
    public Text diamondCountText;
    AudioSource audioPlay;
    public Sounds pickupsounds;
    private Renderer _renderer;
    private BoxCollider _collider;

    void Start()
    {
        audioPlay = GetComponent<AudioSource>();
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<BoxCollider>();
        if (Diamonds != null)
        {
            Diamonds.SetActive(true);  // Ensure the diamond is visible and interactable at the start
        }
        UpdateDiamondText();
    }

    private void OnTriggerStay(Collider other)
    {
        int hunterLayer = LayerMask.NameToLayer("hunter");
        if (other.gameObject.layer == hunterLayer)
        {
            return;  // Ignore collisions with objects on the "hunter" layer
        }

        if (other.gameObject.layer == 7 && !isCollected)
        {
            CollectDiamond();
            if (other.GetComponentInChildren<P_StateManager>() != null)
            {
                other.GetComponentInChildren<P_StateManager>().DiamondsTaken++;
            }
        }
    }
    void Awake()
    {
        isCollected = false;
        Diamonds.SetActive(true);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        diamondsCollected = 0;  // Reset the diamonds count when the scene is loaded
        UpdateDiamondText();  // Update the diamond text to reflect the reset count
    }

    private void CollectDiamond()
    {
        if (!isCollected)
        {
            int amount = Random.Range(0, pickupsounds.sounds.Length);
            audioPlay.PlayOneShot(pickupsounds.sounds[amount]);
            _collider.enabled = false;
            _renderer.enabled = false;
            isCollected = true;  // Mark the diamond as collected
            diamondsCollected++;
            UpdateDiamondText();
            CheckDiamondsCollected();
        }
    }

    private void Update()
    {
        if (isCollected)
        {
            if (!audioPlay.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }

    private void CheckDiamondsCollected()
    {
        if (diamondsCollected >= requiredDiamonds)
        {
            DoorController doorController1 = Dooranim.GetComponent<DoorController>();
            DoorController doorController2 = Dooranim2.GetComponent<DoorController>();

            if (doorController1 != null)
            {
                doorController1.OpenDoor();
            }

            if (doorController2 != null)
            {
                doorController2.OpenDoor();
            }
        }
    }

    void UpdateDiamondText()
    {
        if (diamondCountText != null)
        {
            diamondCountText.text = "Diamonds: " + diamondsCollected;
        }
    }

    
}
