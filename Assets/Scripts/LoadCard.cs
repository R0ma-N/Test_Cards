using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadCard : MonoBehaviour
{
    [SerializeField] GameObject card;
    Transform[] places;
    int[] parametrs;
    List<Card> cards = new List<Card>();
    
    [SerializeField] Canvas canvas;
    [SerializeField] HorizontalLayoutGroup horizontalLayout;
    public float spacing;
    float timeElapsed;
    [SerializeField] float lerpDuration = 3;

    Transform parent;
    int activeCardNumber = 0;
    int cardsCount;

    void Start()
    {
        places = GetComponentsInChildren<Transform>();
        cardsCount = Random.Range(4, 7);

        for (int i = 1; i < cardsCount + 1; i++)
        {
            GameObject tempObject = Instantiate(card);
            tempObject.transform.SetParent(places[i].transform, false);
            cards.Add(tempObject.GetComponent<Card>());
        }

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].Attack = Random.Range(1, 10);
            cards[i].Health = Random.Range(1, 10);
            cards[i].Mana = Random.Range(1, 10);

            cards[i].Death += onDeath;
        }
    }

    private void Update()
    {

    }

    public void SelectCard(Card card)
    {
        parent = card.transform.parent;
        card.transform.SetParent(canvas.transform);
    }

    private void DeselectCard(Card card)
    {
        card.transform.SetParent(parent);
    }

    public void NextCard()
    {
        if (cards[activeCardNumber])
        {
            cards[activeCardNumber].ChangeValue();
        }
        print("ActiveCard " + activeCardNumber);

        if (activeCardNumber == 0)
        {
            SelectCard(cards[activeCardNumber]);
            activeCardNumber++;
        }
        else if (activeCardNumber > 0)
        {
            if (cards[activeCardNumber - 1])
            {
                DeselectCard(cards[activeCardNumber - 1]);
            }
            SelectCard(cards[activeCardNumber]);

            if (activeCardNumber < cards.Count-1)
            {
                activeCardNumber++;
            }
            else
            {
                DeselectCard(cards[cards.Count - 1]);
                activeCardNumber = 0;
            }
        }
    }

    void onDeath()
    {
        Destroy(cards[activeCardNumber - 1].gameObject);
        cards.RemoveAt(activeCardNumber - 1);
        cards.Remove(null);
        print(cards.Count);
        //for (int i = activeCardNumber - 1; i < cards.Count - 2; i++)
        //{
        //    cards.RemoveAt(i);
        //    cards.Insert(i, cards[i + 1]);
        //}

        activeCardNumber = activeCardNumber - 1;

        foreach (var item in cards)
        {
            print(item);
        }
        print(cards);
    }
    
}
 