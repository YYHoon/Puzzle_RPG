using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Unity.IO;
using System.IO;

public class Item
{
    public Item(string _type,string _name,string _explain, bool _isUsing,float _fireDamage, float _waterDamage, float _plantDamage)
    {
        type = _type; name = _name; explain = _explain; isUsing = _isUsing; fireDamage = _fireDamage; waterDamage = _waterDamage; plantDamage = _plantDamage;
    }
    string type, name, explain;
    float fireDamage, waterDamage, plantDamage;
    bool isUsing;
}
public class ItemData : MonoBehaviour
{
    public TextAsset ItemDataBase;
    public List<Item> AllItemList = new List<Item>() , MyItemList= new List<Item>();

    private void Start()
    {
        string[] line = ItemDataBase.text.Substring(0, ItemDataBase.text.Length - 1).Split('\n');
        for(int i=0;i<line.Length;++i)
        {
            string[] row = line[i].Split('\t');
            AllItemList.Add(new Item(row[0], row[1], row[2], row[3] == "TRUE", float.Parse(row[4]), float.Parse(row[5]), float.Parse(row[6])));
        }
    }

    void Save()
    {
        string jdata = JsonConvert.SerializeObject(AllItemList);
        File.WriteAllText(Application.dataPath + "/Resources/MyItemText.txt", jdata);
    }

    private void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/Resources/MyItemText.txt");
        MyItemList = JsonConvert.DeserializeObject<List<Item>>(jdata);
    }
}
