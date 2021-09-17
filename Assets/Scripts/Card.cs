using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image cardArt;
    [SerializeField] private Text attackText, healthText, manaText;
    private int _attackValue, _healthValue, _manaValue;

    private string url = "https://picsum.photos/200";
    private Sprite _image;

    [SerializeField] float duration = 1;

    public delegate void Handler();
    public event Handler Death;

    public int Attack
    {
        get { return _attackValue; }
        set 
        { 
            _attackValue = value;
            attackText.text = Attack.ToString();
        }
    }

    public int Health
    {
        get { return _healthValue; }
        set
        {
            _healthValue = value;
            healthText.text = Health.ToString();
        }
    }

    public int Mana
    {
        get { return _manaValue; }
        set
        {
            _manaValue = value;
            manaText.text = Mana.ToString();
        }
    }

    void Start()
    {
        StartCoroutine(LoadImageFromWebCoroutine());
    }

    public void ChangeValue()
    {
        int randomParametr = Random.Range(0, 3);
        int randomValue = Random.Range(-2, 0);
        StartCoroutine(CountToTarget(randomParametr, randomValue));
    }

    private IEnumerator LoadImageFromWebCoroutine()
    {
        WWW wwwLoader = new WWW(url);
        yield return wwwLoader;

        Texture2D loadedTexture = wwwLoader.texture;
        _image = Sprite.Create(loadedTexture, new Rect(0, 0, loadedTexture.width, loadedTexture.height), new Vector2(0.5f, 0.5f));
        cardArt.sprite = _image;
    }

    private IEnumerator CountToTarget(int parametrNumber, int valueToAdd)
    {
        int start;
        int target;

        switch (parametrNumber)
        {
            case 0:
                start = Attack;
                target = Attack + valueToAdd;

                for (float timer = 0; timer < duration; timer += Time.deltaTime)
                {
                    float progress = timer / duration;
                    Attack = (int)Mathf.Lerp(start, target, progress);
                    yield return null;
                }
                break;
            case 1:
                start = Health;
                target = Health + valueToAdd;

                for (float timer = 0; timer < duration; timer += Time.deltaTime)
                {
                    float progress = timer / duration;
                    Health = (int)Mathf.Lerp(start, target, progress);

                    if (Health <= 0)
                    {
                        Death?.Invoke();
                    }
                    yield return null;
                }
                break;
            case 2:
                start = Mana;
                target = Mana + valueToAdd;

                for (float timer = 0; timer < duration; timer += Time.deltaTime)
                {
                    float progress = timer / duration;
                    Mana = (int)Mathf.Lerp(start, target, progress);
                    yield return null;
                }
                break;
        }
    }
}

