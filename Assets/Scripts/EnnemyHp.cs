using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnnemyHp : MonoBehaviour
{
    public TextMeshProUGUI HeroPVText;
    public static int maxHp;
    public static int staticHp;
    public int hp;
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

