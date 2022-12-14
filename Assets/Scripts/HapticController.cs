using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticController : MonoBehaviour
{
    public static HapticController Instance;

    public XRBaseController leftController, rightController;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    public void SendHaptics()
    {
        leftController.SendHapticImpulse(1f, 0.25f);
        rightController.SendHapticImpulse(1f, 0.25f);
    }
}
