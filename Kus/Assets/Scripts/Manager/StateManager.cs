using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour {

    public GameObject menu;

    public GameObject item_Panel;

    public GameObject confeti_Effect;

    public GameObject congratulation;

    public GameObject lose_Panel;

    public GameObject sound_Bar;

    PlayerControl playerControl;

    Informaion_Handler informaion_Handler;

    Random_UI_Sprite random_UI_Sprite;

    UI_Component ui;

    private Vector3 spawn_Offset = new Vector3(1, -0.5f, 0);

    bool spawn_Effect;

    bool ui_Menu_Done_Loading;

    public bool sound_On;

    public IEnumerable<MeshRenderer> food_Models_Collection;

    public MeshRenderer[] food_Models;

    Audio_Handler audio_Handler;

    void Awake()
    {
        informaion_Handler = FindObjectOfType<Informaion_Handler>();

        playerControl = FindObjectOfType<PlayerControl>();

        audio_Handler = FindObjectOfType<Audio_Handler>();

        random_UI_Sprite = FindObjectOfType<Random_UI_Sprite>();
    }

    void Start()
    {
        Enable_Menu();
        Enable_Menu_Animator();

        Disable_Congra_Panel();
        Disable_Congra_Animator();

        Disable_Lose_Panel();
        Disable_Lose_Animator();

        Disable_Item_Panel();
        Disable_Item_Animator();

        Disable_Sound_Bar();

        ////

        ui = SaveSystem.Load_Food_Selection();

        Add_Models(playerControl.transform.GetChild(0));
    }

    public void Start_Game()
    {
        if (!playerControl.p_variables.win)
            playerControl.p_variables.game_Begin = true;
        else
        {
            Random_Level();
        }
    }

    void Add_Models(Transform parent)
    {
        food_Models_Collection = parent.GetComponentsInChildren<MeshRenderer>();

        food_Models = food_Models_Collection.ToArray();

        ////

        foreach (MeshRenderer item in food_Models)
        {
            if (item.gameObject.transform.tag != ui.food_Currrent_Selection_Name)
                item.gameObject.SetActive(false);
        }
    }

    public void Change_Food_Models(string name)
    {
        foreach (MeshRenderer item in food_Models)
        {
            if (item.transform.tag == name)
            {
                item.transform.gameObject.SetActive(true);
            }
            else item.transform.gameObject.SetActive(false);
        }
    }

    #region Menu
    public void Enable_Menu()
    {
        menu.SetActive(true);
    }

    public void Enable_Menu_Animator()
    {
        foreach (Animator item in menu.GetComponentsInChildren<Animator>())
        {
            item.enabled = true;
        }
    }

    public void Disable_Menu()
    {
        menu.SetActive(false);
    }

    public void Disable_Menu_Animator()
    {
        foreach (Animator item in menu.GetComponentsInChildren<Animator>())
        {
            item.enabled = false;
        }
    }
    #endregion


    #region Item_Panel
    public void Enable_Item_Panel()
    {
        item_Panel.SetActive(true);
    }

    public void Enable_Item_Animator()
    {
        foreach (Animator item in item_Panel.GetComponentsInChildren<Animator>())
        {
            item.enabled = true;
        }
    }

    public void Disable_Item_Panel()
    {
        item_Panel.SetActive(false);
    }

    public void Disable_Item_Animator()
    {
        foreach (Animator item in item_Panel.GetComponentsInChildren<Animator>())
        {
            item.enabled = false;
        }
    }
    #endregion


    #region Congratulation
    public void Enable_Congra()
    {
        congratulation.SetActive(true);
    }

    public void Enable_Congra_Animator()
    {
        foreach (Animator item in congratulation.GetComponentsInChildren<Animator>())
        {
            item.enabled = true;
        }
    }

    public void Disable_Congra_Panel()
    {
        congratulation.SetActive(false);
    }

    public void Disable_Congra_Animator()
    {
        foreach (Animator item in congratulation.GetComponentsInChildren<Animator>())
        {
            item.enabled = false;
        }
    }
    #endregion


    #region LosePanel
    public void Enable_Lose_Panel()
    {
        lose_Panel.SetActive(true);
    }

    public void Enable_Lose_Animator()
    {
        foreach (Animator item in lose_Panel.GetComponentsInChildren<Animator>())
        {
            item.enabled = true;
        }
    }

    public void Disable_Lose_Panel()
    {
        lose_Panel.SetActive(false);
    }

    public void Disable_Lose_Animator()
    {
        foreach (Animator item in lose_Panel.GetComponentsInChildren<Animator>())
        {
            item.enabled = false;
        }
    }

    #endregion

    public void Active_Refercene_Process()
    {
        ui_Menu_Done_Loading = true;
    }

    public void Disable_Refercene_Process()
    {
        ui_Menu_Done_Loading = false;
    }


    #region SoundBar
    public void Enable_Sound_Bar()
    {
        if (ui_Menu_Done_Loading)
        {
            sound_On = !sound_On;

            sound_Bar.GetComponent<Animator>().enabled = true;

            if (sound_On)
            {
                sound_Bar.SetActive(true);

                sound_Bar.GetComponent<Animator>().SetBool("on", true);

                sound_Bar.GetComponent<Animator>().SetBool("off", false);
            }
            else
            {
                sound_Bar.GetComponent<Animator>().SetBool("on", false);

                sound_Bar.GetComponent<Animator>().SetBool("off", true);
            }
        }
    }

    public void Disable_Sound_Bar()
    {
        if (ui_Menu_Done_Loading)
            sound_On = false;

        sound_Bar.SetActive(false);

        sound_Bar.GetComponent<Animator>().enabled = false;
    }
    #endregion


    void Update()
    {
        Game_Handler();
    }

    public void Random_Level()
    {
        int index = Random.Range(1, 10);

        SceneManager.LoadScene(index);
    }

    void Game_Handler()
    {
        if (playerControl.p_variables.win)
        {
            playerControl.p_variables.game_Begin = false;

            if (!spawn_Effect)
            {
                spawn_Effect = !spawn_Effect;

                audio_Handler.Play("Win");

                Enable_Menu();

                Enable_Menu_Animator();

                Enable_Congra();

                Enable_Congra_Animator();

                informaion_Handler.Increase_Diamond(100);

                SaveSystem.Save_Data(random_UI_Sprite.image_Pos, Informaion_Handler.diamond);

                GameObject spawn = Instantiate(confeti_Effect, playerControl.transform.position + spawn_Offset, confeti_Effect.transform.rotation);

                Destroy(spawn, 2.5f);
            }
        }

        if (playerControl.p_variables.lose)
        {
            if (playerControl.p_variables.game_Begin)
            {
                audio_Handler.Play("Error");

                playerControl.p_variables.game_Begin = false;
            }

            Enable_Lose_Panel();

            Enable_Lose_Animator();

            if (Input.GetMouseButtonDown(0))
            {
                Random_Level();
            }
        }
    }
}
