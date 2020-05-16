using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundData {

    public bool enabled;

    public SoundData(bool mute)
    {
        enabled = mute;
    }
}
