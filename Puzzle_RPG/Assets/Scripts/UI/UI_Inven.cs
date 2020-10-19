using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.Assertions.Must;

public class UI_Inven : MonoBehaviour
{
    [SerializeField]
    GameObject stat, inven;
    [SerializeField]
    Image[] invenSlot,equipSlot;
    [SerializeField]
    GameObject[] usingImage;
    [SerializeField]
    Text playerStat;
    Item[] currentItemData;
    [SerializeField]
    Transform weapon;
    [SerializeField]
    Transform shield;
    [SerializeField]
    GameObject player;
    [SerializeField]
    List<Transform> weaponList=new List<Transform>();
    [SerializeField]
    List<Transform> shieldList = new List<Transform>();

    private void Start()
    {
        inven = transform.Find("INVEN/Inventory").gameObject;
        stat = transform.Find("INVEN/Stat").gameObject;
        stat.SetActive(false);
        currentItemData = GameObject.Find("DataManager").GetComponent<ItemData>().PlayerItemList();

        weaponList.AddRange(weapon.GetComponentsInChildren<Transform>(true));
        weaponList.RemoveAt(0);

        shieldList.AddRange(shield.GetComponentsInChildren<Transform>(true));
        shieldList.RemoveAt(0);

        InvenSetting();
        EquipmentSetting();
    }

    void InvenSetting()
    {
        for(int i=0;i< currentItemData.Length;++i)
        {
            if (currentItemData[i] == null) invenSlot[i].GetComponent<Image>().enabled = false;
            else
            {
                invenSlot[i].GetComponent<Image>().enabled=true;
                invenSlot[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(currentItemData[i].path);
                if (currentItemData[i].isUsing == true)
                    usingImage[i].SetActive(true);
            }
        }
        
    }

    void EquipmentSetting()
    {
        for(int i=0;i < currentItemData.Length; ++i)
        {
            if (currentItemData[i] == null) continue;
            if (currentItemData[i].isUsing)
            {
                if(currentItemData[i].type == "Weapon")
                    equipSlot[0].GetComponent<Image>().sprite = Resources.Load<Sprite>(currentItemData[i].path);
                else if(currentItemData[i].type == "Shield")
                    equipSlot[1].GetComponent<Image>().sprite = Resources.Load<Sprite>(currentItemData[i].path);
            }
        }
    }

    public void OnEquip(int idx)
    {
        if (currentItemData[idx] == null) return;
        if (currentItemData[idx].isUsing == true) return;
        if(currentItemData[idx].type=="Weapon")
        {
            OnWeaponEquip(idx);
        }
        else
        {
            OnShieldEquip(idx);
        }
    }

    public void OnWeaponEquip(int idx)
    {
        for (int i = 0; i < currentItemData.Length; ++i)
        {
            if (currentItemData[i] == null) continue;
            if (currentItemData[i].isUsing == true && currentItemData[i].type == "Weapon")
            {
                player.GetComponent<Player>().ChangeEquip();
                currentItemData[i].isUsing = false;
                weaponList[currentItemData[i].idx].gameObject.SetActive(false);
                usingImage[i].SetActive(false);
                break;
            }
        }
        weaponList[currentItemData[idx].idx].gameObject.SetActive(true);
        currentItemData[idx].isUsing = true;
        usingImage[idx].SetActive(true);
        invenSlot[idx].GetComponent<Image>().enabled = true;
        equipSlot[0].GetComponent<Image>().sprite = Resources.Load<Sprite>(currentItemData[idx].path);
        DataManager.Instance.savePlayerData.fireDamage = currentItemData[idx].fireDamage;
        DataManager.Instance.savePlayerData.waterDamage = currentItemData[idx].waterDamage;
        DataManager.Instance.savePlayerData.plantDamage = currentItemData[idx].plantDamage;
    }
    public void OnShieldEquip(int idx)
    {
        for (int i = 0; i < currentItemData.Length; ++i)
        {
            if (currentItemData[i]==null) continue;
            if (currentItemData[i].isUsing == true && currentItemData[i].type == "Shield")
            {
                player.GetComponent<Player>().ChangeEquip();
                currentItemData[i].isUsing = false;
                shieldList[currentItemData[i].idx].gameObject.SetActive(false);
                usingImage[i].SetActive(false);
                break;
            }
        }
        shieldList[currentItemData[idx].idx].gameObject.SetActive(true);
        currentItemData[idx].isUsing = true;
        usingImage[idx].SetActive(true);
        invenSlot[idx].GetComponent<Image>().enabled = true;
        equipSlot[1].GetComponent<Image>().sprite = Resources.Load<Sprite>(currentItemData[idx].path);
        DataManager.Instance.savePlayerData.fireDefense = currentItemData[idx].fireDamage;
        DataManager.Instance.savePlayerData.waterDefense = currentItemData[idx].waterDamage;
        DataManager.Instance.savePlayerData.plantDefense = currentItemData[idx].plantDamage;
    }


    public void OnButtonInventory()
    {
        stat.SetActive(false);
        inven.SetActive(true);
    }
    public void OnButtonStat()
    {
        stat.SetActive(true) ;
        inven.SetActive(false);
    }
    public void StatSetting()
    {
        playerStat.text = "Fire Damage = "+DataManager.Instance.savePlayerData.fireDamage+ "\nWater Damage = " + DataManager.Instance.savePlayerData.waterDamage + "\nPlant Damage = " + DataManager.Instance.savePlayerData.plantDamage + "\n" +
            "\nFire Defense = " + DataManager.Instance.savePlayerData.fireDefense + "\nWater Defense = " + DataManager.Instance.savePlayerData.waterDefense + "\nPlant Defense = " + DataManager.Instance.savePlayerData.plantDefense;
    }
    public void SaveInven()
    {
        GameObject.Find("DataManager").GetComponent<ItemData>().CurrentItme = currentItemData;
    }
}
