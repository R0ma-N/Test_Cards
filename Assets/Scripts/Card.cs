using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image cardArt;
    [SerializeField] private Text attackText, healthText, manaText;
    private int _attackValue, _healthValue, _manaValue;

    private Sprite _image;
    private string url = "https://picsum.photos/200";

    public int AttackValue
    {
        get { return _attackValue; }
        set 
        { 
            _attackValue = value;
            attackText.text = AttackValue.ToString();
        }
    }

    public int Health
    {
        get { return _healthValue; }
        set
        {
            _healthValue = value;
            attackText.text = Health.ToString();
        }
    }

    public int Mana
    {
        get { return _manaValue; }
        set
        {
            _manaValue = value;
            attackText.text = Mana.ToString();
        }
    }

    void Start()
    {
        StartCoroutine(LoadFromWebCoroutine());

       // parametr = new Text[] { attackText, healthText, manaText };
        AttackValue = Random.Range(1, 10);
        _healthValue = Random.Range(1, 10);
        _manaValue = Random.Range(1, 10);

        healthText.text = _healthValue.ToString();
        manaText.text = _manaValue.ToString();
    }

    public void ChangeValue()
    {
        // parametr[Random.Range(1, 4)].text = Random.Range(-2, 10).ToString();
    }

    private IEnumerator LoadFromWebCoroutine()
    {
        WWW wwwLoader = new WWW(url);
        yield return wwwLoader;

        Texture2D loadedTexture = wwwLoader.texture;
        _image = Sprite.Create(loadedTexture, new Rect(0, 0, loadedTexture.width, loadedTexture.height), new Vector2(0.5f, 0.5f));
        cardArt.sprite = _image;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ChangeValue();
        }
    }

    private void Counter()
    {

    }
}

