using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{   
    [SerializeField] int pcurrentHP, pTotalHP;
    [SerializeField] Text currentHP, TotalHP;
    [SerializeField] Image HPBar;
    // Start is called before the first frame update
    void Start()
    {
        pcurrentHP = Player.currentHP;
        pTotalHP = Player.totalHP;
    }

    // Update is called once per frame
    void Update()
    {
        HPBar.fillAmount = (float)(Player.currentHP)/(float)(Player.totalHP);
        currentHP.text = Player.currentHP.ToString();
        TotalHP.text = Player.totalHP.ToString();
    }
}
