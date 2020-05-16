using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SimpleJSON;
using UnityEngine.UI;

public static class SaveSystem {

    public static void Save_Sound_Selection(bool mute)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/sound.txt";

        FileStream stream = new FileStream(path, FileMode.Create);

        SoundData sound = new SoundData(mute);

        formatter.Serialize(stream, sound);

        stream.Close();
    }

    public static SoundData Load_Sound_Selection()
    {
        string path = Application.persistentDataPath + "/sound.txt";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            SoundData sound = formatter.Deserialize(stream) as SoundData;

            stream.Close();

            return sound;
        }
        else
        {
            SoundData s = new SoundData(true);

            Save_Sound_Selection(true);

            return s;
        }
    }

    public static void Save_Food_Selection(string name)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/selection.txt";

        FileStream stream = new FileStream(path, FileMode.Create);

        UI_Component ui = new UI_Component(name);

        formatter.Serialize(stream, ui);

        stream.Close();
    }

    public static UI_Component Load_Food_Selection()
    {
        string path = Application.persistentDataPath + "/selection.txt";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            UI_Component ui = formatter.Deserialize(stream) as UI_Component;

            stream.Close();

            return ui;
        }
        else
        {
            UI_Component ui = new UI_Component("Sauce");

            Save_Food_Selection("Sauce");

            return ui;
        }
    }

    public static void Save_Data(List<int> pos_ID, float diamond)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.txt";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(pos_ID, diamond);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static PlayerData LoadPlayer(Image [] food_Images)
    {
        string path = Application.persistentDataPath + "/player.txt";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            Informaion_Handler.diamond = data.coin;

            if (data.pos_List != null)
            {
                for (int i = 0; i < data.pos_List.Count; i++)
                {
                    for (int j = 0; j < food_Images.Length; j++)
                    {
                        if (data.pos_List[i] == j)
                        {
                            food_Images[j].transform.GetChild(0).gameObject.SetActive(true);
                        }
                    }
                }
            }

            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }
}
