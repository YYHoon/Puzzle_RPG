using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Unity.IO;
using System.IO;

public class Item
{
    public Item(string _type,string _name,string _explain, bool _isUsing,float _fireDamage, float _waterDamage, float _plantDamage,int _idx,string _path)
    {
        type = _type; name = _name; explain = _explain; isUsing = _isUsing; fireDamage = _fireDamage; waterDamage = _waterDamage; plantDamage = _plantDamage; idx = _idx; path = _path; 
    }
    public string type, name, explain,path;
    public int idx;
    public float fireDamage, waterDamage, plantDamage;
    public bool isUsing;
}
public class ItemData : MonoBehaviour
{
    public TextAsset ItemDataBase;
    List<Item> AllItemList = new List<Item>();
    Item[] MyItemList = new Item[9];
    public Item[] CurrentItme
    {
        get { return MyItemList; }
        set { MyItemList = value; }
    }
    public Item[] PlayerItemList()
    {
        Load();
        return MyItemList;
    }
    private void Start()
    {
        string[] line = ItemDataBase.text.Substring(0, ItemDataBase.text.Length - 1).Split('\n');
        for(int i=0;i<line.Length;++i)
        {
            string[] row = line[i].Split('\t');
            AllItemList.Add(new Item(row[0], row[1], row[2], row[3] == "TRUE", float.Parse(row[4]), float.Parse(row[5]), float.Parse(row[6]),int.Parse(row[7]),row[8]));
        }

        MyItemList[0]=AllItemList[0];
        MyItemList[0].isUsing = true;
        MyItemList[1] = AllItemList[6];
        MyItemList[1].isUsing = true;
        MyItemList[2] = AllItemList[2];
        MyItemList[4] = AllItemList[8];
        Save();
    }

    void Save()
    {
        string jdata = JsonConvert.SerializeObject(MyItemList);
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
        string format = System.Convert.ToBase64String(bytes);
        File.WriteAllText(Application.dataPath + "/Resources/MyItemText.json", format);
    }

    private void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/Resources/MyItemText.json");
        byte[] bytes = System.Convert.FromBase64String(jdata);
        string format = System.Text.Encoding.UTF8.GetString(bytes);
        MyItemList = JsonConvert.DeserializeObject<Item[]>(format);
    }
}
