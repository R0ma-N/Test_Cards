using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image cardArt;
    [SerializeField] private Text attackText, healthText, manaText;
    [SerializeField] float countDuration = 1;
    private string url = "https://picsum.photos/200";
    private Sprite _image;
    private int _attackValue, _healthValue, _manaValue;

    public delegate void Handler();
    public event Handler NoHP;

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
        int randomValue = Random.Range(-2, 10);
        StartCoroutine(CountToTarget(randomParametr, randomValue));
    }

    public void MoveCard(Transform target, float time)
    {
        StartCoroutine(smoothLerpMove(target, time));
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

                for (float timer = 0; timer < countDuration; timer += Time.deltaTime)
                {
                    float progress = timer / countDuration;
                    Attack = (int)Mathf.Lerp(start, target, progress);
                    yield return null;
                }
                break;
            case 1:
                start = Health;
                target = Health + valueToAdd;

                for (float timer = 0; timer < countDuration; timer += Time.deltaTime)
                {
                    float progress = timer / countDuration;
                    Health = (int)Mathf.Lerp(start, target, progress);
                    yield return null;

                    if (Health < 1)
                    {
                        NoHP?.Invoke();
                    }
                }
                break;
            case 2:
                start = Mana;
                target = Mana + valueToAdd;

                for (float timer = 0; timer < countDuration; timer += Time.deltaTime)
                {
                    float progress = timer / countDuration;
                    Mana = (int)Mathf.Lerp(start, target, progress);
                    yield return null;
                }
                break;
        }
    }

    private IEnumerator smoothLerpMove(Transform target, float time)
    {
        Vector3 startingPos = transform.position;
        Vector3 finalPos = target.position;
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}

