using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Dish currentDish;
    public List<CardBehaviour> allCards;
    [SerializeField] GameObject RerollButton,secondRerollButton,thirdButton;
    [SerializeField] Transform boxSpawn,cupBOardSpawn;
    [SerializeField] UnityEvent onFirstTurnDealt, onSecondTurnDealt, onThirdTurnDealt, onDeath;
    [SerializeField] Hand ing;
    bool rerolling = false,rolledTooMuch = false, rerolling2 = false, rolled2Much = false, rerolling3 = false, rolled3Much = false;
    [SerializeField]CustomerStuff customer;
    [SerializeField] List<GameObject> lives;
    [SerializeField] TextMeshProUGUI textFeedback,dishStats;
    int livesCount = 3;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        allCards = FindObjectsByType<CardBehaviour>(FindObjectsInactive.Include,FindObjectsSortMode.None).ToList();
    }
    [ContextMenu("ShuffleRQ")]
    public void FirstDealCards()
    {
        allCards = ShuffleList(allCards);
        StartCoroutine(DealCardsFirstTurn());
    }
    public void ProperReset()
    {
        rerolling = false;
        rerolling2 = false;
        rerolling3 = false;
        rolledTooMuch = false;
        rolled2Much = false;
        rolled3Much = false;
    }
    IEnumerator DealCardsFirstTurn()
    {
        if (!rolledTooMuch)
        {
            if (rerolling)
            {
                ing.Discardhand(CardBehaviour.CardCategory.Ingredient, true);
                ing.Discardhand(CardBehaviour.CardCategory.Flavor, true);
            }      
            int ingredients = 0;
            int flavor = 0;
            int cardAmount;
            if (rerolling) cardAmount = 2;
            else cardAmount = 3;
            for (int i = 0; i < allCards.Count; i++)
            {
                if (allCards[i].category == CardBehaviour.CardCategory.Ingredient && ingredients < cardAmount)
                {                    
                    ing.PutCardIn(allCards[i]);
                    allCards[i].transform.position = boxSpawn.position;
                    allCards[i].gameObject.SetActive(true);
                    ingredients++;
                    yield return new WaitForSeconds(0.25f);
                }
                else if (allCards[i].category == CardBehaviour.CardCategory.Flavor && flavor < cardAmount)
                { 
                    ing.PutCardIn(allCards[i]);
                    allCards[i].lerping = true;
                    allCards[i].transform.position = boxSpawn.position;
                    allCards[i].gameObject.SetActive(true);
                    flavor++;
                    yield return new WaitForSeconds(0.25f);
                }
            }
            if (!rerolling)
            {
                
                rerolling = true;
            }
            else RerollButton.SetActive(false);
            onFirstTurnDealt.Invoke();
        }
    }
    [ContextMenu("SécondTurn")]
    public void SecondtDealCards()
    {
        allCards = ShuffleList(allCards);
        StartCoroutine(DealCardsSecondTurn());
    }
    IEnumerator DealCardsSecondTurn()
    {
        if (!rolled2Much)
        {
            if (ing.cards.Count > 0) foreach (var card in ing.cards)
            {
                 if(card.category == CardBehaviour.CardCategory.Equipment)card.gameObject.SetActive(false);
            }
            for (int i = 0; i < ing.transform.childCount; i++)
            {
                if(ing.transform.GetChild(i).childCount < 1) ing.transform.GetChild(i).gameObject.SetActive(false);
            }
            int equipment = 0;
            int cardAmount;
            cardAmount = 1;
            for (int i = 0; i < allCards.Count; i++)
            {
                if (allCards[i].category == CardBehaviour.CardCategory.Equipment && equipment < cardAmount)
                {
                    ing.PutCardIn(allCards[i]);
                    allCards[i].transform.position = boxSpawn.position;
                    allCards[i].gameObject.SetActive(true);
                    equipment++;
                    yield return new WaitForSeconds(0.25f);
                }
            }
            if (!rerolling2)
            {

                rerolling2 = true;
            }
            else secondRerollButton.SetActive(false);
            onSecondTurnDealt.Invoke();
        }
    }
    public void ThirdDealCards()
    {
        allCards = ShuffleList(allCards);
        StartCoroutine(DealCardsThirdTurn());
    }
    IEnumerator DealCardsThirdTurn()
    {
        if (!rolled3Much)
        {
            if (ing.cards.Count > 0) foreach (var card in ing.cards)
                {
                    if (card.category == CardBehaviour.CardCategory.Technique) card.gameObject.SetActive(false);
                }
            for (int i = 0; i < ing.transform.childCount; i++)
            {
                if (ing.transform.GetChild(i).childCount < 1) ing.transform.GetChild(i).gameObject.SetActive(false);
            }
            int tech = 0;
            int cardAmount;
            if(rerolling3) cardAmount = 1;
            else cardAmount = 2;
            for (int i = 0; i < allCards.Count; i++)
            {
                if (allCards[i].category == CardBehaviour.CardCategory.Technique && tech < cardAmount)
                {
                    ing.PutCardIn(allCards[i]);
                    allCards[i].transform.position = boxSpawn.position;
                    allCards[i].gameObject.SetActive(true);
                    tech++;
                    yield return new WaitForSeconds(0.25f);
                }
            }
            if (!rerolling3)
            {

                rerolling3 = true;
            }
            else secondRerollButton.SetActive(false);
            onThirdTurnDealt.Invoke();
        }
    }
    public void OrderFit()
    {
        currentDish.Taste();
        if (currentDish.TastePref == customer.tastepreferance)
        {
            switch (customer.tastepreferance)
            {
                case CustomerStuff.TastePref.Salty:
                    if (Mathf.Abs(currentDish.salty - customer.salty) > 5) textFeedback.text = "Yeah it is correct taste, but not the way I like it.(1 out of 3)";
                    else if (Mathf.Abs(currentDish.salty - customer.salty) < 5) textFeedback.text = "Almost exactly as I wanted!(2 out of 3)";
                    else textFeedback.text = "OH MY GOD, EXACTLY AS I WANTED THANK YOU!!!(3 out of 3)";
                        break;
                case CustomerStuff.TastePref.Sweet:
                    if (Mathf.Abs(currentDish.sweety - customer.sweet) > 5) textFeedback.text = "Yeah it is correct taste, but not the way I like it.(1 out of 3)";
                    else if (Mathf.Abs(currentDish.sweety- customer.sweet) < 5) textFeedback.text = "Almost exactly as I wanted!(2 out of 3)";
                    else textFeedback.text = "OH MY GOD, EXACTLY AS I WANTED THANK YOU!!!(3 out of 3)";
                    break;
                case CustomerStuff.TastePref.Sour:
                    if (Mathf.Abs(currentDish.soury - customer.soury) > 5) textFeedback.text = "Yeah it is correct taste, but not the way I like it.(1 out of 3)";
                    else if (Mathf.Abs(currentDish.soury - customer.soury) < 5) textFeedback.text = "Almost exactly as I wanted!(2 out of 3)";
                    else textFeedback.text = "OH MY GOD, EXACTLY AS I WANTED THANK YOU!!!(3 out of 3)";
                    break;
                case CustomerStuff.TastePref.Bitter:
                    if (Mathf.Abs(currentDish.bittery- customer.bitter) > 5) textFeedback.text = "Yeah it is correct taste, but not the way I like it.(1 out of 3)";
                    else if (Mathf.Abs(currentDish.bittery - customer.bitter) < 5) textFeedback.text = "Almost exactly as I wanted!(2 out of 3)";
                    else textFeedback.text = "OH MY GOD, EXACTLY AS I WANTED THANK YOU!!!(3 out of 3)";
                    break;
            }
        }
        else
        {
            
            lives[livesCount-1].SetActive(false);
            livesCount--;
            if (livesCount == 0)
            {
                onDeath.Invoke();
            }
            textFeedback.text = "Bleugh, what the hell is this? And you call yourself a chef?(0 out of 3)";
        }
    }
    public void BasicCardUse(CardBehaviour card)
    {
        if (currentDish != null)
        {
           currentDish.AddUp(card); 
        }
    }

    public List<CardBehaviour> ShuffleList(List<CardBehaviour> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            CardBehaviour temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        return list;
    }
    public void Update()
    {
        if(currentDish.salty > 20)currentDish.salty = 20;
        if(currentDish.sweety > 20)currentDish.sweety = 20;
        if(currentDish.soury > 20) currentDish.soury = 20;
        if(currentDish.bittery > 20)currentDish.bittery = 20;
        dishStats.text = "Saltyness:" + currentDish.salty +"\nSweetness:" + currentDish.sweety +"\nBitterness:" + currentDish.bittery + "\nSourness:" + currentDish.soury;
    }
}
[Serializable]
public class Dish 
{
    public float salty, sweety, bittery, soury, tolerance;
    public CustomerStuff.TastePref TastePref;
    public void AddUp(CardBehaviour card)
    {
        salty += card.addSalt;
        sweety += card.addSweet;
        bittery += card.addBitter;
        soury += card.addSour;
        salty *= card.saltyMult;
        sweety *= card.sweetMult;
        bittery *= card.bitterMult;
        soury *= card.sourMult;
    }
    public CustomerStuff.TastePref Taste()
    {
        List<float> list = new List<float> {salty,sweety,bittery,soury };
        float min = list.Min();
        float max = list.Max();
        if (max - min < tolerance) TastePref = CustomerStuff.TastePref.PourBalance;
        else if (salty > 10) TastePref = CustomerStuff.TastePref.Salty;
        else if (sweety > 10) TastePref = CustomerStuff.TastePref.Sweet;
        else if (bittery > 10) TastePref = CustomerStuff.TastePref.Bitter;
        else if (soury > 10) TastePref = CustomerStuff.TastePref.Sour;
        return TastePref;
    }
}
