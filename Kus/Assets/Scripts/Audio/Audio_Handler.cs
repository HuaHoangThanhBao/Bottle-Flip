using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Audio_Handler : MonoBehaviour {

    public Sound[] sounds;

    Change_Sprite_Component change_Sprite_Component;

    void Awake()
    {
        change_Sprite_Component = FindObjectOfType<Change_Sprite_Component>();

        SoundData sound = SaveSystem.Load_Sound_Selection();

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.pitch = s.pitch;

            s.source.volume = s.volumn;

            s.enabled = sound.enabled;
        }

        if (sound.enabled)
        {
            change_Sprite_Component.transform.GetComponent<Image>().sprite = change_Sprite_Component.sound_Sprites[0];
        }
        else
        {
            change_Sprite_Component.transform.GetComponent<Image>().sprite = change_Sprite_Component.sound_Sprites[1];
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s.enabled)
            s.source.Play();
    }

    public void Disable_Audio()
    {
        foreach (Sound item in sounds)
        {
            item.enabled = false;
        }
    }

    public void Enable_Audio()
    {
        foreach (Sound item in sounds)
        {
            item.enabled = true;
        }
    }
}
