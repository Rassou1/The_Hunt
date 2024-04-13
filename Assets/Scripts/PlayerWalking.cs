using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWalking : MonoBehaviour
{
    AudioSource AudioManager;
    public AudioClip[] Footsteps;
    public Transform footTransform;

    Ray floorRay;
    RaycastHit hit;
    string floorName = "grg";
    string soundType = "alk";
    // Start is called before the first frame update
    void Start()
    {
        AudioManager = GetComponent<AudioSource>();
        Footsteps = Resources.LoadAll<AudioClip>("Sounds/Footsteps/Footsteps_Grass/Walk");
    }

   
    void ChangeSoundType(string type)
    {
        Debug.Log("Changed WalkType to:" + hit.transform.gameObject.tag);
        floorName = hit.transform.gameObject.tag;
        floorName = floorName.Replace("ground", "");
        Footsteps = null;
        Footsteps = Resources.LoadAll<AudioClip>($"Sounds/Footsteps/Footsteps_{floorName}/{type}");
    }

    // Update is called once per frame
    void Update()   
    {
        Debug.DrawRay(footTransform.position, -transform.up, Color.blue);

        floorRay = new Ray(footTransform.position, -transform.up);
        if (Physics.Raycast(floorRay, out hit, 1, ~6))
        {
            if (hit.transform.gameObject.tag.StartsWith("ground") && hit.transform.gameObject.tag.EndsWith(floorName) == false)
            {
                ChangeSoundType(soundType);
            }
        }
        
    }

    public void PlayWalkSound()
    {
        if (soundType != "Walk")
        {
            soundType = "Walk";
            ChangeSoundType(soundType);
        }

        int footstep = Random.Range(0, Footsteps.Length);
        Debug.Log($"Played Walking sound number {footstep}");
        if (!AudioManager.isPlaying)
        {
            AudioManager.PlayOneShot(Footsteps[footstep]);
        }
    }

    public void PlayRunSound()
    {
        if (soundType != "Run")
        {
            soundType = "Run";
            ChangeSoundType(soundType);
        }
        int footstep = Random.Range(0, Footsteps.Length);
        Debug.Log($"Played Walking sound number {footstep}");
        if (!AudioManager.isPlaying)
        {
            AudioManager.PlayOneShot(Footsteps[footstep]);
        }
    }

    public void PlayJumpSound()
    {

    }
}
