using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWalking : MonoBehaviour
{
    //This script is in charge of playing all of the sounds the player does and was worked on by me -Jonathan
    [HideInInspector]
    public AudioSource AudioManager;
    public AudioClip[] Sounds;
    public Transform footTransform;
    public SoundFolder grass;
    public SoundFolder gravel;
    public SoundFolder wood;
    public SoundFolder tile;
    public SoundFolder rock;
    public SoundFolder mud;
    public SoundFolder dirt;
    public SoundFolder metal;
    public Sounds dash;
    public SoundFolder Empty;
    
    Dictionary<string, SoundFolder> sFolders = new Dictionary<string, SoundFolder>();

    Ray floorRay;
    RaycastHit hit;
    string secondaryFloorName = "DefaultName";
    string floorName = "DefaultName";
    // Start is called before the first frame update
    void Start()
    {
        //This adds all diffrent types of surfaces so they can get refrenced by name
        AudioManager = GetComponent<AudioSource>();
        sFolders.Add("Gravel", gravel);
        sFolders.Add("Grass", grass);
        sFolders.Add("Wood", wood);
        sFolders.Add("Tile", tile);
        sFolders.Add("Rock", rock);
        sFolders.Add("Mud", mud);
        sFolders.Add("Dirt", dirt);
        sFolders.Add("Metal", metal);
        
    }

    /// <summary>
    /// This Method Checks what surface the player is currently on
    /// </summary>
    /// <remarks> If no surface is found the method assigns the default as a metal surface</remarks>
    /// <param name="type"> The type of surface the player is on</param>
    void UpdatePlayerSound(SoundType type)
    {
        Sounds = Empty.walkClips;
        floorRay = new Ray(footTransform.position, -transform.up);
        if (Physics.Raycast(floorRay, out hit, 1, ~6))
        {
            if (hit.transform.gameObject.GetComponent<MeshFilter>() != null)
            {
                secondaryFloorName = hit.transform.gameObject.GetComponent<MeshFilter>().mesh.name;
                foreach (KeyValuePair<string, SoundFolder> kvp in sFolders)
                {
                    if (secondaryFloorName.Contains(kvp.Key, System.StringComparison.OrdinalIgnoreCase))
                    {
                        if (type == SoundType.Walking)
                        {
                            Sounds = kvp.Value.walkClips;
                        }
                        else if (type == SoundType.Running)
                        {
                            Sounds = kvp.Value.runClips;
                        }
                        else if (type == SoundType.JumpingStart)
                        {
                            Sounds = kvp.Value.JumpStartClips;
                        }
                        else if (type == SoundType.JumpingEnd)
                        {
                            Sounds = kvp.Value.JumpEndClips;
                        }
                        else if (type == SoundType.Sliding)
                        {
                            Sounds = kvp.Value.slideclips;
                        }
                    }
                }
                
            }

            if (Sounds.Length == 0)
            {
                floorName = hit.transform.gameObject.tag;
                foreach (KeyValuePair<string, SoundFolder> kvp in sFolders)
                {
                    if (floorName.Contains(kvp.Key, System.StringComparison.OrdinalIgnoreCase))
                    {
                        if (type == SoundType.Walking)
                        {
                            Sounds = kvp.Value.walkClips;
                        }
                        else if (type == SoundType.Running)
                        {
                            Sounds = kvp.Value.runClips;
                        }
                        else if (type == SoundType.JumpingStart)
                        {
                            Sounds = kvp.Value.JumpStartClips;
                        }
                        else if (type == SoundType.JumpingEnd)
                        {
                            Sounds = kvp.Value.JumpEndClips;
                        }
                        else if (type == SoundType.Sliding)
                        {
                            Sounds = kvp.Value.slideclips;
                        }
                    }
                }
            }

            if (Sounds.Length == 0 && metal.walkClips != null)
            {
                Sounds = metal.walkClips;
            }
        }
    }

    void Update()
    {
        //Debug.DrawRay(footTransform.position, -transform.up, Color.blue);

    }
   
    
    public void PlayWalkSound()
    {
        UpdatePlayerSound(SoundType.Walking);
        int amount = Random.Range(0, Sounds.Length);
        if (!AudioManager.isPlaying && Sounds.Length > 0)
        {
            AudioManager.PlayOneShot(Sounds[amount]);
        }
    }

    
    public void PlayRunSound()
    {
        UpdatePlayerSound(SoundType.Running);
        int amount = Random.Range(0, Sounds.Length);
        if (!AudioManager.isPlaying && Sounds.Length > 0)
        {
            AudioManager.PlayOneShot(Sounds[amount]);
        }
    }

    public void PlayJumpStartSound()
    {
        UpdatePlayerSound(SoundType.JumpingStart);
        int amount = Random.Range(0, Sounds.Length);
        if (Sounds.Length > 0)
        {
            AudioManager.PlayOneShot(Sounds[amount]);
        }
    }

    public void PlayJumpEndSound()
    {
        UpdatePlayerSound(SoundType.JumpingEnd);
        int amount = Random.Range(0, Sounds.Length);
        if (Sounds.Length > 0)
        {
            AudioManager.PlayOneShot(Sounds[amount]);
        }
    }

    public void PlaySlidingSound()
    {
        UpdatePlayerSound(SoundType.Sliding);
        int amount = Random.Range(0, Sounds.Length);
        AudioManager.Stop();
        if (Sounds.Length > 0)
        {
            AudioManager.PlayOneShot(Sounds[amount]);
        }
    }

    public void PlayDashSound()
    {
        AudioManager.PlayOneShot(dash.sounds[0]);
    }

    enum SoundType
    {
        Walking,
        Running,
        JumpingStart,
        JumpingEnd,
        Sliding
    }
}
