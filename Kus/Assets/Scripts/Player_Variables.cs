using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player_Variables {

    [System.NonSerialized]
    public float force_Up = 6f;

    [System.NonSerialized]
    public float force_Right = 1.2f;

    [System.NonSerialized]
    public float distance = 0.05f;

    [System.NonSerialized]
    public float distance_Left = 1;

    [System.NonSerialized]
    public float fan_Speed = 1;

    [System.NonSerialized]
    public float force_Acceleration = 50;

    public bool can_Press;

    public bool is_Grounded;

    public bool rotate;

    public bool jump_Twice;

    public bool follow_Obj;

    public bool jump;

    public bool end_Game;

    public bool win;

    public bool lose;

    public bool game_Begin;

    [System.NonSerialized]
    public string jump_Str = "jump";

    [System.NonSerialized]
    public string jumpTwice_Str = "jump_twice";
}
