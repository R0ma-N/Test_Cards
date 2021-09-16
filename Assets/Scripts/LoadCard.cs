using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadCard : MonoBehaviour
{
    [SerializeField] GameObject card;
    Transform[] places;
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

        //parent = cards[0].transform.parent;
        //cards[0].transform.SetParent(canvas.transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cards[0].transform.SetParent(parent);
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            cards[0].transform.SetParent(canvas.transform);
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
        print($"cardNumber {activeCardNumber}");
        print($"cardsCount {cardsCount}");

        if (activeCardNumber == 0)
        {
            SelectCard(cards[activeCardNumber]);
            activeCardNumber++;
        }
        else if (activeCardNumber > 0)
        {
            DeselectCard(cards[activeCardNumber - 1]);
            SelectCard(cards[activeCardNumber]);

            if (activeCardNumber < cardsCount-1)
            {
                activeCardNumber++;
            }
            else
            {
                DeselectCard(cards[cardsCount - 1]);
                activeCardNumber = 0;
            }
        }

        cards[activeCardNumber].ChangeValue();
    }
}
