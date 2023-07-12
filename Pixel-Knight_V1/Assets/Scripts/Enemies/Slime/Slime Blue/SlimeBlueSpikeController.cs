using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBlueSpikeController : MonoBehaviour
{
    public void IceSpikeUp()
    {
        AudioController.instance.PlaySFX(25);                                             /////// SFX //
    }

    public void IceSpikeDown()
    {
        AudioController.instance.PlaySFX(24);                                             /////// SFX //
    }
}
