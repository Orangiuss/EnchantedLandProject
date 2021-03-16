using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHp : MonoBehaviour
{
    public TextMeshProUGUI HeroPVText;
    public static float maxHp;
    public static float staticHp;
    public float hp;
    // Start is called before the first frame update
    void Start()
    {
        maxHp = 30;
        staticHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        hp = staticHp;
        HeroPVText.text = "" + hp; 
    }
}
