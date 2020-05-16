using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Informaion_Handler : MonoBehaviour {

    public List<Bottle> food_Obj;

    public static float diamond;

    public Image model;

    public Text food_Name;

    public Text [] diamond_Text;

    public Image[] play_Icon;

    PlayerControl playerControl;

    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
    }
    
    void Update()
    {
        foreach (Text item in diamond_Text)
        {
            item.text = diamond.ToString();
        }

        if(!playerControl.p_variables.win)
        {
            play_Icon[0].enabled = true;

            play_Icon[1].enabled = false;
        }
        else
        {
            play_Icon[0].enabled = false;

            play_Icon[1].enabled = true;
        }
    }

    public void Load_Diamond(float amount)
    {
        diamond = amount;
    }

    public void Change_Model_Sprite(Sprite new_Sprite, string name)
    {
        model.sprite = new_Sprite;

        food_Name.text = name;
    }

    public void Increase_Diamond(float amount)
    {
        diamond += amount;
    }

    public void Decrease_Diamond(float amount)
    {
        diamond -= amount;
    }
}
