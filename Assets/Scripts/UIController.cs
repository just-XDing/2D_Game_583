using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{

    public HumanBase hb;
    public Button B_Tier1Summon;
    // Start is called before the first frame update
    void Awake()
    {
        loadResources();
        setupUserControls();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void loadResources()
    {
        Debug.Log(hb);
        hb = Resources.Load<HumanBase>("Prefabs/HumanBasePrefab");
    }

    void setupUserControls()
    {
        B_Tier1Summon = GameObject.Find("B_Tier1").GetComponent<Button>();
        B_Tier1Summon.onClick.AddListener(OnClickSummonTier1);
    }


    void OnClickSummonTier1()
    {
        hb = Resources.Load<HumanBase>("Prefabs/HumanBasePrefab");
        hb = Instantiate(hb) as HumanBase;
        hb.transform.position = new Vector3(-7, -2, 0);
        if (hb == null)
        {
            Debug.Log("prefab null");
        }
        else
        {
            Debug.Log("prefab not null");
        }
    }
}

