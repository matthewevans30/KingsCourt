using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DisableMoveOnGrab : MonoBehaviour
{
    public GameObject XROrigin;
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        XROrigin.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
    }

    public void OnSelectExited(SelectExitEventArgs args)
    {
        XROrigin.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
    }
}
