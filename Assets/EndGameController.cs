using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : MonoBehaviour
{
    public GameObject endGameBackground;
    public GameObject escaped;
    public GameObject caught;
    public float activeTime;
    public float spinScaleTime;
    public float extraScaleTime;
    public float scaleDownTime;

    private PlayerStates playerStates;
    private GameObject thisPlayer;
    public Alteruna.Avatar avatar;

    private bool escapedAnimationPlayed = false;
    private bool caughtAnimationPlayed = false;

    void Start()
    {
        thisPlayer = transform.root.gameObject;

        playerStates = FindObjectOfType<PlayerStates>();
    }

    void Update()
    {
      
        if (!avatar.IsMe)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        
        if (playerStates.escapedPlayers.Contains(thisPlayer) && !escapedAnimationPlayed)
        {
            StartCoroutine(ActivateAndAnimateObjectsForTime(endGameBackground, escaped, activeTime, spinScaleTime, extraScaleTime, scaleDownTime));
            escapedAnimationPlayed = true;
        }

        if (playerStates.taggedPlayers.Contains(thisPlayer) && !caughtAnimationPlayed)
        {
            StartCoroutine(ActivateAndAnimateObjectsForTime(endGameBackground, caught, activeTime, spinScaleTime, extraScaleTime, scaleDownTime));
            caughtAnimationPlayed = true;
        }
    }

    private IEnumerator ActivateAndAnimateObjectsForTime(GameObject obj1, GameObject obj2, float totalTime, float spinScaleTime, float extraScaleTime, float scaleDownTime)
    {
        obj1.SetActive(true);
        obj2.SetActive(true);

        yield return AnimateObject(obj2, 360, spinScaleTime, 0.001f, 6.61f);

        float maxScale = 6.61f * 2f;
        yield return AnimateObject(obj2, 0, extraScaleTime, 6.61f, maxScale);

        yield return AnimateObject(obj2, 0, scaleDownTime, maxScale, 6.61f);

        yield return new WaitForSeconds(totalTime - spinScaleTime - extraScaleTime - scaleDownTime);

        obj1.SetActive(false);
        obj2.SetActive(false);
    }

    private IEnumerator AnimateObject(GameObject obj, float angle, float duration, float startScale, float endScale)
    {
        float startRotation = obj.transform.eulerAngles.z;
        float endRotation = startRotation + angle;
        float t = 0.0f;

        Vector3 initialScale = new Vector3(startScale, startScale, startScale);
        Vector3 finalScale = new Vector3(endScale, endScale, endScale);

        while (t < duration)
        {
            t += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, zRotation);

            obj.transform.localScale = Vector3.Lerp(initialScale, finalScale, t / duration);

            yield return null;
        }

        obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, endRotation % 360.0f);
        obj.transform.localScale = finalScale;
    }
}
