using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public static Hand instance;
    public List<Transform> cardSlots;
    public List<CardBehaviour> cards;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }
    public void PutCardIn(CardBehaviour card)
    {
        Debug.Log(card);
        for (int i = 0; i < cardSlots.Count; i++)
        {
            if (!cardSlots[i].gameObject.activeSelf)
            {
                cardSlots[i].gameObject.SetActive(true);
                cards.Add(card);
                card.LerpTo(cardSlots[i].transform);                
                break;
            }
        }
    }
    public void CardOut(Transform slot,CardBehaviour card)
    {
        //cards.Remove(card); 
    }
    public void Discardhand(CardBehaviour.CardCategory cat,bool sort)
    {
        if (sort)
        {
            foreach (CardBehaviour card in cards)
            {
                if(card.category == cat)card.ProperDisableCard();
            }
        }
        else
        {
            foreach (CardBehaviour card in cards)
            {
                card.ProperDisableCard();
            }
        }
        
    }
    void Update()
    {
        
    }
}
