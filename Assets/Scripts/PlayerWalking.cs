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
    // Start is called before the first frame update
    void Start()
    {
        AudioManager = GetComponent<AudioSource>();
        Footsteps = Resources.LoadAll<AudioClip>("Sounds/Footsteps/Footsteps_Grass/Walk");
        

    }

    // Update is called once per frame
    void Update()   
    {
        Debug.DrawRay(footTransform.position, -transform.up,Color.blue);
        
        floorRay = new Ray(footTransform.position, -transform.up);
        if (Physics.Raycast(floorRay, out hit, 1,~6))
        {
            
            if (hit.transform.gameObject.tag.StartsWith("ground") && hit.transform.gameObject.tag.EndsWith(floorName) == false)
            {
                Debug.Log("Changed WalkType to:" + hit.transform.gameObject.tag);
                floorName = hit.transform.gameObject.tag;
                floorName = floorName.Replace("ground", "");
                Footsteps = null;
                Footsteps = Resources.LoadAll<AudioClip>($"Sounds/Footsteps/Footsteps_{floorName}/Walk");
            }

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
