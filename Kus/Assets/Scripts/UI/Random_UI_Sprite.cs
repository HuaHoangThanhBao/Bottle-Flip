using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Random_UI_Sprite : MonoBehaviour {

    public GameObject item_Panel;

    public IEnumerable<Image> food_Colection;

    public Image[] food_Images;

    public List<Image> image_Actives;

    public List<int> image_Pos;

    public Color orign_Color;

    public bool change;

    public bool start_Change;

    public bool chosen;

    public bool saved;

    int i = 0;

    int random_Stop_Order;

    float color_Duration = 1;

    float speed_Lerp = 2;

    Informaion_Handler informaion_Handler;

    void Awake()
    {
        informaion_Handler = FindObjectOfType<Informaion_Handler>();
    }

    void Start()
    {
        food_Colection = item_Panel.GetComponentsInChildren<Image>().Where(m => m.transform.tag == "Image").OrderByDescending(m => m.transform.name);

        food_Images = food_Colection.ToArray();

        SaveSystem.LoadPlayer(food_Images);

        Find_And_Add_Active_Image();
    }

    void Find_And_Add_Active_Image()
    {
        for (int j = 0; j < food_Images.Length; j++)
        {
            if(food_Images[j].transform.GetChild(0).gameObject.activeSelf)
            {
                image_Actives.Add(food_Images[j]);

                image_Pos.Add(j);
            }
        }
    }

    bool Check_If_Exist_Active(Image item)
    {
        bool found = false;

        foreach (Image ac in image_Actives)
        {
            if(item == ac)
            {
                found = true;
            }
        }

        if (!found)
            return false;
        else return true;
    }

    bool Check_If_Exist_Pos(int pos)//Kiem tra co ton tai vi tri da active image chua ?
    {
        bool found = false;

        foreach (int ac in image_Pos)
        {
            if (pos == ac)
            {
                found = true;
            }
        }

        if (!found)
            return false;
        else return true;
    }

    void Update()
    {
        if (start_Change)
        {
            Random_Sprite();
        }
    }

    public void Active_Random()
    {
        if (Informaion_Handler.diamond >= 1000)
        {
            if (image_Pos.Count < food_Images.Length)
            {
                if (!saved)
                {
                    start_Change = false;

                    saved = true;

                    while (Check_If_Exist_Pos(random_Stop_Order))
                    {
                        random_Stop_Order = Random.Range(0, food_Images.Length);
                    }

                    start_Change = true;

                    i = 0;
                }
            }
        }
    }


    void Random_Sprite()
    {
        if (!change && i <= food_Images.Length)
        {
            if (i != random_Stop_Order)
            {
                if (!Check_If_Exist_Active(food_Images[i]))
                    StartCoroutine(Change_Color(food_Images[i]));
                else i++;
            }
            else
            {
                if (!chosen)
                {
                    food_Images[random_Stop_Order].color = orign_Color;

                    image_Pos.Add(random_Stop_Order);

                    image_Actives.Add(food_Images[random_Stop_Order]);

                    informaion_Handler.Decrease_Diamond(1000);

                    Save_Data();

                    StartCoroutine(Color_Select_Effect(food_Images[random_Stop_Order]));
                }
                else
                    Active_UI_Sprite(food_Images[random_Stop_Order]);
            }
        }

        if (i >= food_Images.Length)
        {
            start_Change = false;

            change = false;
        }
    }

    IEnumerator Color_Select_Effect(Image item)
    {
        chosen = true;

        for (float t = 0; t < color_Duration; t+= Time.deltaTime)
        {
            item.color = Color.Lerp(orign_Color, Color.white, Mathf.PingPong(Time.time * speed_Lerp, 0.5f));

            yield return null;
        }

        saved = false;

        start_Change = false;

        chosen = false;

        Return_Color(item);
    }

    void Save_Data()
    {
        SaveSystem.Save_Data(image_Pos, Informaion_Handler.diamond);
    }

    void Active_UI_Sprite(Image item)
    {
        item.transform.GetChild(0).gameObject.SetActive(true);
    }

    IEnumerator Change_Color(Image item)
    {
        change = true;

        chosen = false;

        Update_Sprite_Color(item);

        yield return new WaitForSeconds(0.1f);

        Return_Color(item);

        change = false;

        i++;
    }

    void Update_Sprite_Color(Image current)
    {
        current.color = Color.white;
    }

    void Return_Color(Image prev)
    {
        prev.color = orign_Color;
    }
}
