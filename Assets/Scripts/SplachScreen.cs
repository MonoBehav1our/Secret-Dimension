using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections;

public class SplachScreen : MonoBehaviour
{
    public static SplachScreen Instance;

    [SerializeField] private float _animTime;
    [SerializeField] private float _timeToLoadScene;

    private TextMeshProUGUI _text;
    private Image _image;
    
    private void Start()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);

        _text = GetComponentInChildren<TextMeshProUGUI>();
        _image = GetComponentInChildren<Image>();
        StartCoroutine(OpenAnimation());
    }

    public void LoadScene(int SceneNumber, string Message)
    {
        StartCoroutine(ExitAnimation(SceneNumber, Message));
    }

    private IEnumerator ExitAnimation(int SceneNumber, string Message)
    {
        _text.text = Message;
        float time = 0;
        while (time <= _animTime)
        {
            float colorA = time / _animTime;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, colorA);
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, colorA);
            time += Time.deltaTime;
            yield return null;
        }
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1);
        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 1);

        yield return new WaitForSeconds(_timeToLoadScene);
        SceneManager.LoadScene(SceneNumber);
    }

    private IEnumerator OpenAnimation()
    {
        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 0);
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1);

        float time = _animTime;
        while (time >= 0)
        {
            float colorA = time / _animTime;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, colorA);
            time -= Time.deltaTime;
            yield return null;
        }

        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0);
    }
}
