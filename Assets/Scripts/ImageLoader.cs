using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    public string url = "https://picsum.photos/200";
    public Image CardArt;
    [SerializeField] private Rect rect;
    private Sprite _image;

    void Start()
    {
        CardArt = GetComponent<Image>();
        StartCoroutine(LoadFromWebCoroutine());
    }

    private IEnumerator LoadFromWebCoroutine()
    {
        Debug.Log("Loading...");
        WWW wwwLoader = new WWW(url);
        yield return wwwLoader;

        Debug.Log("Loaded");
        Texture2D loadedTexture = wwwLoader.texture;
        _image = Sprite.Create(loadedTexture, new Rect(0, 0, loadedTexture.width, loadedTexture.height), new Vector2(0.5f, 0.5f));
        CardArt.sprite = _image;
    }
}
