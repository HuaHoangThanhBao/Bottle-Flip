using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Change_Sprite_Component : MonoBehaviour {

    public Sprite[] sound_Sprites;

    public bool mute;

    Audio_Handler audio_Handler;

    void Start()
    {
        audio_Handler = FindObjectOfType<Audio_Handler>();
    }

    public void Change_Sprite()
    {
        mute = !mute;

        if (!mute)
        {
            transform.GetComponent<Image>().sprite = sound_Sprites[0];

            audio_Handler.Enable_Audio();
        }
        else
        {
            transform.GetComponent<Image>().sprite = sound_Sprites[1];

            audio_Handler.Disable_Audio();
        }

        SaveSystem.Save_Sound_Selection(!mute);
    }
}
