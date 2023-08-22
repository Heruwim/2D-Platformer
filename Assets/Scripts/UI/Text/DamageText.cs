using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private Vector3 _moveSpeed = new Vector3(0, 75, 0);
    [SerializeField] private float _timeToFade = 1f;

    private RectTransform _textTransform;
    private TextMeshProUGUI _textMeshPro;
    private Color _startColor;
    
    private float _timeElapsed = 0f;

    private void Awake()
    {
        _textTransform = GetComponent<RectTransform>();
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        _startColor = _textMeshPro.color;
    }

    private void Update()
    {
        FadeText();
    }

    private void FadeText()
    {
        _textTransform.position += _moveSpeed * Time.deltaTime;

        _timeElapsed += Time.deltaTime;

        if (_timeElapsed < _timeToFade)
        {
            float fadeAlpha = _startColor.a * (1 - _timeElapsed / _timeToFade);
            _textMeshPro.color = new Color(_startColor.r, _startColor.g, _startColor.b, fadeAlpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
