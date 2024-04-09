using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalking : MonoBehaviour
{
    AudioSource AudioManager;
    public AudioClip[] Footsteps;
    Ray floorRay;
    RaycastHit hit;
    string floorName = string.Empty;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager = GetComponent<AudioSource>();
        
        Footsteps = Resources.LoadAll<AudioClip>("Sounds/Footsteps/Footsteps_Grass/Walk");
        floorRay = new Ray(transform.position, transform.up * -1);

    }

    // Update is called once per frame
    void Update()   
    {
        if (Physics.Raycast(floorRay,out hit,100))
        {
            floorName = hit.transform.gameObject.tag;
            floorName = floorName.Replace("ground", "");
            Footsteps = Resources.LoadAll<AudioClip>($"Sounds/Footsteps/Footsteps_{floorName}/Walk");
        }
    }

    public void PlayWalkSound()
    {
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

    public void PlayRunSound()
    {

    }
}
