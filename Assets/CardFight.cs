using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Card;

public class CardFight : MonoBehaviour
{

    static System.Random rand = new System.Random();
    static List<CardType> standardDeck = new List<CardType>();

    static CardFight()
    {
        foreach (var val in Enum.GetValues(typeof(Value)))
        {
            Value v = (Value)val;
            if (v == Value.Joker)
            {
                standardDeck.Add(new CardType());
            }
            else
            {
                foreach (var suite in Enum.GetValues(typeof(Suite)))
                {
                    Suite s = (Suite) suite;
                    standardDeck.Add(new CardType(v, s));
                }
            }
        }
        Debug.Log("Generated cards: " + standardDeck.Count);

        foreach (var c in standardDeck)
        {
            Debug.Log(Card.TypeToString(c));
        }
    }

    [SerializeField]
    private int cardNumber = 6;
    [SerializeField]
    private Transform myHandObj;

    private List<CardType> cardPool;
    private List<CardType> myHand;
    private List<CardType> enemyHand;

    CardType GetRandomFromPool()
    {
        int idx = rand.Next(cardPool.Count);
        CardType ret = cardPool[idx];
        cardPool.Remove(ret);
        return ret;
    }

    List<CardType> GenerateHand()
    {
        List<CardType> hand = new List<CardType>();
        for (int i = 0; i < cardNumber; ++i)
        {
            hand.Add(GetRandomFromPool());
        }

        return hand;
    }

    void StartFight()
    {
        cardPool = new List<CardType>(standardDeck);
        myHand = GenerateHand();
        enemyHand = GenerateHand();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
