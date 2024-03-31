using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public static string TypeToString(CardType type)
    {
        string suite = type.suite.ToString();
        string value;
        switch (type.value)
        {
            case Value.Joker:
            return "Joker_Color";
            case Value.Ace:
            value = "01";
            break;
            default:
            value = string.Format("{0:00}", (int)type.value + 2);
            break;
        }
        return string.Concat(suite, value);
    }

    public static Sprite TypeToSprite(CardType type)
    {
        string str = TypeToString(type);
        Sprite s = Resources.Load<Sprite>("PlayingCards/" + str);
        return s;
    }

    public enum Value
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace,
        Joker
    }

    public enum Suite
    {
        Spade,
        Heart,
        Diamond,
        Club
    }

    [Serializable]
    public struct CardType
    {
        public CardType(Value v, Suite s)
        {
            value = v;
            suite = s;
        }

        public bool Compare(CardType other)
        {
            bool result = this.value < other.value;

            if (this.suite == other.suite)
            {
                result = !result;
            }

            return result;
        }

        public Value value;
        public Suite suite;
    }

    [SerializeField]
    public CardType type;

    private RectTransform rectTransform;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Transform cardHolder;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(spriteRenderer);

        rectTransform = GetComponent<RectTransform>();
        Assert.IsNotNull(rectTransform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayCard()
    {

    }

    void OnMouseDrag()
    {
        Vector3 newPos = new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0);
        Debug.Log("New position: " + newPos);
        // Camera.main.ScreenToWorldPoint(newPos);
        transform.position = newPos;
    }

    void OnMouseDown()
    {
        Debug.Log("Grabbed a card");
        transform.SetParent(null);
    }

    void OnMouseUp()
    {
        Debug.Log("Released a card");
        transform.SetParent(cardHolder);
        transform.position = new Vector3(0, 0, 0);
    }
}
