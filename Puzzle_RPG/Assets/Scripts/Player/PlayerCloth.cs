using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloth : MonoBehaviour
{
    [SerializeField]
    GameObject dataManager;
    [SerializeField]
    DataManager dm;
    public Transform cloth;
    List<Transform> clothChild = new List<Transform>();
    public Transform hair;
    List<Transform> hairChild = new List<Transform>();
    public Transform helm;
    List<Transform> helmChild = new List<Transform>();
    public Transform glove;
    List<Transform> gloveChild = new List<Transform>();
    public Transform shoes;
    List<Transform> shoesChild = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        dataManager = GameObject.Find("DataManager");
        dm = dataManager.GetComponent<DataManager>();
        
        clothChild.AddRange(cloth.GetComponentsInChildren<Transform>(true));
        clothChild.RemoveAt(0);
        clothChild[dm.SaveIdx.clothIndex].gameObject.SetActive(true);

        hairChild.AddRange(hair.GetComponentsInChildren<Transform>(true));
        hairChild.RemoveAt(0);
        hairChild[dm.SaveIdx.hairIndex].gameObject.SetActive(true);

        helmChild.AddRange(helm.GetComponentsInChildren<Transform>(true));
        helmChild.RemoveAt(0);
        helmChild[dm.SaveIdx.helmIndex].gameObject.SetActive(true);

        gloveChild.AddRange(glove.GetComponentsInChildren<Transform>(true));
        gloveChild.RemoveAt(0);
        gloveChild[dm.SaveIdx.gloveIndex].gameObject.SetActive(true);

        shoesChild.AddRange(shoes.GetComponentsInChildren<Transform>(true));
        shoesChild.RemoveAt(0);
        shoesChild[dm.SaveIdx.shoesIndex].gameObject.SetActive(true);
    }

}
