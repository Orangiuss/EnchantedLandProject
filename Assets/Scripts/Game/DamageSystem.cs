using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DamageSystem : MonoBehaviour, IPointerClickHandler
{
    public GameObject targetImage;
    public GameObject Ennemy;
    public Serviteur Target;
    public EnnemyHp TargetEnnemy;

    public GameHandler gameHandler;

    public static bool damage;
    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        Ennemy = GameObject.Find("HeroEnnemi");
        damage = false;
    }

    // Update is called once per frame
    void Update()
    { 
    }

    public void StartDamage(int n)
	{
        damage = true;
        targetImage.SetActive(true);
        StartCoroutine(DamageTarget());
    }

    public string DamageTarget()
	{
        Vector3 vector3;
        while (damage)
        {
            vector3 = Input.mousePosition;
            targetImage.transform.position = vector3;
        }
        targetImage.SetActive(false);
        return null;
    }
         
	public void OnPointerClick(PointerEventData eventData)
	{
        Debug.Log("SALUT");
	}
}
