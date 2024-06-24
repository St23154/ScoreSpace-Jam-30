using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public PlayerMovement playerMovement;

    // Cette méthode sera appelée par l'événement d'animation
    public void Action1()
    {
        playerMovement.Action1End();
    }

    public void Action2()
    {
        playerMovement.Action2End();
    }

    public void Action3()
    {
        playerMovement.Action3End();
    }

    public void Action4()
    {
        playerMovement.Action4End();
    }

    public void Mort()
    {
        playerMovement.ReloadScene();
    }
}
