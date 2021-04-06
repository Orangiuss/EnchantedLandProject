using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Serviteur : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Card thisCard = new Card();
    public int thisId;
    public int Id;
    public int type;
    public int Cost;
    public int Power;
    public int PV;
    public int dealXdamage;
    public int healXhp;

    public Sprite sprite;
    public Image image;

    public TextMeshProUGUI typeText;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI PVText;

    public int index;

    public GameObject MinionDropArea;
    public bool ennemi;

    public GameObject attackBorder;
    public GameObject healBorder;
    public GameObject damageBorder;

    public GameObject targetImage;
    public GameObject Ennemy;
    public Serviteur Target;
    public EnnemyHp TargetEnnemy;

    public bool canAttack;
    public bool healEffect;
    public bool damageEffect;

    public bool effectGo;

    public bool summonedAttack;

    public GameHandler gameHandler;
    // Start is called before the first frame update
    void Start()
    {
        // Initialisation du serviteur 
        effectGo = true;
        healEffect = false;
        damageEffect = false;
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        attackBorder.SetActive(false);
        healBorder.SetActive(false);
        damageBorder.SetActive(false);
        if (!ennemi)
        {
            Ennemy = GameObject.Find("HeroEnnemi");
        }

        if (!ennemi)
        { 
            MinionDropArea = GameObject.Find("MinionDropArea");
            this.transform.SetParent(MinionDropArea.transform);
        }
        thisCard = CardDataBase.cardList[thisId];
    }

	// Update is called once per frame
	void Update()
    {
        if(healEffect && !SystemeDeTour.isYourTurn)
		{
            effectGo = false;
            healEffect = false;
        }
        if (damageEffect && !SystemeDeTour.isYourTurn)
        {
            effectGo = false;
            damageEffect = false;
        }
        if (summonedAttack && SystemeDeTour.isYourTurn && !ennemi)
        {
            canAttack = true;
            attackBorder.SetActive(summonedAttack);
        }
        else if (!summonedAttack && !SystemeDeTour.isYourTurn && !ennemi)
        {
            summonedAttack = true;
            canAttack = false;
        }
        else
        {
            canAttack = false;
            attackBorder.SetActive(false);
        }
        if (healXhp > 0 && !ennemi && SystemeDeTour.isYourTurn && effectGo) { healEffect = true; }
        if (dealXdamage > 0 && !ennemi && SystemeDeTour.isYourTurn && effectGo) { damageEffect = true; }
        healBorder.SetActive(healEffect);
        damageBorder.SetActive(damageEffect);
    }

    public void Initialize()
	{
        this.gameObject.SetActive(true);
        thisCard = CardDataBase.cardList[thisId];
        summonedAttack = thisCard.directAttack;
        Id = thisCard.Id;
        type = thisCard.Type;
        Cost = thisCard.Cost;
        healXhp = thisCard.healXhp;
        if(healXhp > 0 ) { healEffect = true; }
        dealXdamage = thisCard.dealXdamage;

        // Amie des dragons 
        if (Id == 22)
        {
            if(Aerien())
			{
                dealXdamage = 3;
            } 
            else
			{
                dealXdamage = 0;
			}
        }

        //Radluir double-hache
        if (Id == 29)
		{
            if (Nains())
            {
                dealXdamage = 5;
            }
            else
            {
                dealXdamage = 0;
            }
        }

        if (dealXdamage > 0) { damageEffect = true; }

        sprite = thisCard.Image;

        powerText.text = "" + Power;
        PVText.text = "" + PV;

        switch (type)
        {
            case 7:
                typeText.text = "Aérien";
                break;
            case 6:
                typeText.text = "Aquatique";
                break;
            case 5:
                typeText.text = "Orc";
                break;
            case 4:
                typeText.text = "Bête";
                break;
            case 3:
                typeText.text = "Nain";
                break;
            case 2:
                typeText.text = "Elfe";
                break;
            case 1:
                typeText.text = "Humain";
                break;
            default:
                Debug.Log("Error : Incorrect type of card");
                break;
        }

        image.sprite = sprite;
    }

    public void UpdateServiteur()
	{
        if (PV <= 0)
        {
            PV = 0;
            Power = 0;
            Id = 0;
        }
        if (ennemi)
		{
            PlateauEnnemi.serviteurEnnemiStatic[index].id = Id;
            PlateauEnnemi.serviteurEnnemiStatic[index].hp = PV;
            PlateauEnnemi.serviteurEnnemiStatic[index].pa = Power;
        }
        else
		{
            Plateau.serviteurStatic[index].id = Id;
            Plateau.serviteurStatic[index].hp = PV;
            Plateau.serviteurStatic[index].pa = Power;
        }
        powerText.text = "" + Power;
        PVText.text = "" + PV;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && SystemeDeTour.isYourTurn && (canAttack || damageEffect || healEffect ))
        {
            targetImage.SetActive(true);
            targetImage.transform.position = new Vector3(eventData.position.x, eventData.position.y);
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && SystemeDeTour.isYourTurn && (canAttack || damageEffect || healEffect))
        {
            targetImage.transform.position = new Vector3(eventData.position.x, eventData.position.y);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && SystemeDeTour.isYourTurn && (canAttack || damageEffect || healEffect))
        {
            targetImage.transform.position = new Vector3(eventData.position.x, eventData.position.y);
            Target = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Serviteur>();
            TargetEnnemy = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<EnnemyHp>();
            if (Target != null)
            {
                if (Target.ennemi)
                {
                    if (canAttack)
                    {
                        Target.PV = Target.PV - this.Power;
                        this.PV = this.PV - Target.Power;
                        this.UpdateServiteur();
                        Target.UpdateServiteur();
                        canAttack = false;
                        summonedAttack = false;
                    }
                    else if (damageEffect)
					{
                        damageEffect = false;
                        Target.PV = Target.PV - dealXdamage;
                        Target.UpdateServiteur();
                        dealXdamage = 0;
                    }
                }
                if (healEffect)
                {
                    healEffect = false;
                    Target.PV = Target.PV + healXhp;
                    Target.UpdateServiteur();
                    healXhp = 0;
                }
            }
            if(TargetEnnemy != null)
			{
                if (canAttack)
                {
                    EnnemyHp.staticHp = EnnemyHp.staticHp - this.Power;
                    canAttack = false;
                    summonedAttack = false;
                }
                else if(damageEffect)
				{
                    EnnemyHp.staticHp = EnnemyHp.staticHp - dealXdamage;
                    dealXdamage = 0;
                    damageEffect = false;
                }
                else if (healEffect)
                {
                    EnnemyHp.staticHp = EnnemyHp.staticHp + healXhp;
                    healXhp = 0;
                    healEffect = false;
                }
            }
            gameHandler.UpdatePlateaus();
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            targetImage.SetActive(false);
        }
    }

    public bool Aerien()
    {
        bool ok = false;
        foreach (ServiteurDB serviteur in Plateau.serviteurStatic)
        {
            if (CardDataBase.cardList[serviteur.id].Type == 7)
            {
                ok = true;
            }
        }
        return ok;
    }

    public bool Nains()
	{
        int ok = 0;
        foreach (ServiteurDB serviteur in Plateau.serviteurStatic)
        {
            if (CardDataBase.cardList[serviteur.id].Type == 3)
            {
                ok++;
            }
        }
        if (ok >= 2)
        {
            return true;
        }
        else
		{
            return false;
		}
    }
}
