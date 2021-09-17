using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject card;
    [SerializeField] Canvas canvas;
    [SerializeField] float moveSpeed = 0.6f;
    [SerializeField] Transform[] places;

    List<Card> cards = new List<Card>();
    Transform parent;

    int activeCardNumber = 0;

    void Start()
    {
        int cardsCount = Random.Range(4, 7);

        for (int i = 0; i < cardsCount; i++)
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

            cards[i].NoHP += remove;
            cards[i].NoHP += moveCard;
        }
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
        cards[activeCardNumber].ChangeValue();

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

            if (activeCardNumber < cards.Count - 1)
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

    void remove()
    {
        if (activeCardNumber == 0)
        {
            Destroy(cards[cards.Count - 1].gameObject);
            cards.RemoveAt(cards.Count - 1);
            activeCardNumber = 0;
        }
        else
        {
            Destroy(cards[activeCardNumber - 1].gameObject);
            cards.RemoveAt(activeCardNumber - 1);
            activeCardNumber = activeCardNumber - 1;
        }
    }

    void moveCard()
    {
        if (activeCardNumber < cards.Count)
        {
            print("moveCard");
            for (int i = activeCardNumber; i < cards.Count; i++)
            {
                print(i);
                cards[i].MoveCard(places[i + 1], moveSpeed);
            }
        }
    }
}
