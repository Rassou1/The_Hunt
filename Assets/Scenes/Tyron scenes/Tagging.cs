using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;

public class InteractablePlayer : AttributesSync, IInteractable
{
    private bool tagged = false;

    public Vector3 syncedPosition = new Vector3(63.7f, 10.58f, -17.28f);

    [SynchronizableField] public Alteruna.TransformSynchronizable _transform;


    public void Interact(GameObject interactor)
    {
        StartCoroutine(EmprisonCoroutine());
    }

    private IEnumerator EmprisonCoroutine()
    {
        yield return new WaitForEndOfFrame();
        Emprison();
    }

    public void Update()
    {
        Debug.Log("" + transform.position);
        if (tagged)
            Debug.Log("Prey has been tagged");
    }
    private void Emprison()
    {
        //if (gameObject.layer == LayerMask.NameToLayer("Prey") && interactor.layer == LayerMask.NameToLayer("Hunter"))
        //{

        _transform.transform.position = syncedPosition;

        tagged = true;
        //}
    }


}
