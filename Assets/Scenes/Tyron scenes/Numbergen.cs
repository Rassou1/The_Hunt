using UnityEngine;
using Alteruna;

public class RoleGiver : AttributesSync, IInteractable
{
    public GameObject hunterCanvas;
    public GameObject preyCanvas;

    private bool hunterCanvasActive = false;
    private float timer = 5f;

    public GameObject GiveObject()
    {
        throw new System.NotImplementedException();
    }

    public void Interact(GameObject interactor, Alteruna.Avatar interactorAvatar)
    {
        gameObject.SetActive(false);

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 0)
        {
            int hunterIndex = Random.Range(0, players.Length);

            for (int i = 0; i < players.Length; i++)
            {

                if (i == hunterIndex)
                {
                    players[i].layer = LayerMask.NameToLayer("Hunter");
                    hunterCanvasActive = true; // Set hunterCanvasActive to true

                    if (hunterCanvasActive)
                    {
                        timer -= Time.deltaTime;
                        if (timer <= 0f)
                        {
                            hunterCanvas.SetActive(false);
                            hunterCanvasActive = false; // Reset hunterCanvasActive
                            timer = 5f; // Reset the timer
                        }
                        else
                        {
                            hunterCanvas.SetActive(true);
                        }
                    }



                }
                else
                {
                    players[i].layer = LayerMask.NameToLayer("Prey");
                    preyCanvas.SetActive(true);
                }
            }
        }
    }

    void Update()
    {
       
    }
}
