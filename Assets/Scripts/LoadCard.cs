using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadCard : MonoBehaviour
{
    [SerializeField] GameObject card;
    Transform[] places;
    [SerializeField] HorizontalLayoutGroup horizontalLayout;
    public float spacing;
    float timeElapsed;
    [SerializeField] float lerpDuration = 3;

    void Start()
    {
        places = GetComponentsInChildren<Transform>();
        spacing = horizontalLayout.spacing;

        for (int i = 1; i < Random.Range(5,7); i++)
        {
            GameObject shadowObject = Instantiate(card);
            shadowObject.transform.SetParent(places[i].transform, false);
        }
    }

    private void Update()
    {
        if (timeElapsed < lerpDuration)
        {
            horizontalLayout.spacing = Mathf.Lerp(0, 74, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
        }
    }
}
