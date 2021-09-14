using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadCard : MonoBehaviour
{
    [SerializeField] GameObject card;
    [SerializeField] Canvas canvas;

    void Start()
    {
        Instantiate(card);
        card.transform.SetParent(canvas.transform, false);
        //card.transform.parent = canvas.transform;
    }
}
