using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class PlayerData : MonoBehaviour
{
    GameObject player;
  public void PlayerSave()
    {
       // Debug.Log(Application.dataPath);
        GameObject playerData = GameObject.FindWithTag("Player");
       // Debug.Log(playerData);
       // JsonUtility.ToJson(playerData);
        string jdata = JsonConvert.SerializeObject(JsonUtility.ToJson(playerData));
        File.WriteAllText(Application.dataPath + "/Resources/Playerdata.json", jdata);
    }

    public GameObject PlayerLoad()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/Resources/PlayerText.txt");
        return player = JsonConvert.DeserializeObject<GameObject>(jdata);
    }
}
