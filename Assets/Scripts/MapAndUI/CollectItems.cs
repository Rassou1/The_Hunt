using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI diamondCountText;
    AudioSource audioPlay;
    public Sounds pickupsounds;
    private Renderer _renderer;
    private BoxCollider _collider;

    public PlayerStates playerStates;

    GameObject diamondCount;

    // Floating and rotation variables
    public float floatSpeed = 1.0f;     // Speed of floating up and down
    float floatHeight = 0.2f;    // Maximum height of the float
    public float rotationSpeed = 45.0f; // Speed of rotation in degrees per second

    private Vector3 initialPosition;

    void Start()
    {
        GameObject roomMenu = GameObject.Find("Room Menu");
        GameObject diamondCount = roomMenu.transform.Find("dimond counter").gameObject;
        diamondCountText = diamondCount.GetComponent<TextMeshProUGUI>();

        audioPlay = GetComponent<AudioSource>();
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<BoxCollider>();
        
        
        initialPosition = Diamonds.transform.position; // Store the initial position for floating effect

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
                playerStates.PlayerForceSync();
            }
        }
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
          // Reset the diamonds count when the scene is loaded
        // Update the diamond text to reflect the reset count
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
            playerStates.PlayerForceSync();

        }
    }

    private void Update()
    {
        playerStates = FindAnyObjectByType<PlayerStates>();

        // Animate the diamond with floating and rotation
        if (!isCollected)
        {
            AnimateDiamond();
        }

        if (isCollected)
        {
            if (!audioPlay.isPlaying)
            {
               
            }
        }
        if (!playerStates.gameEnded && !playerStates.gameStarted)
        {
            isCollected = false;
            _collider.enabled = true;
            _renderer.enabled = true;
            Debug.Log("DiamondReset");
            UpdateDiamondText();
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
            diamondCountText.text = "Diamonds: " + diamondsCollected + "/20";
        }
    }

    private void AnimateDiamond()
    {
        // Floating effect
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight + initialPosition.y;
        Diamonds.transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);

        // Rotation effect
        Diamonds.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
