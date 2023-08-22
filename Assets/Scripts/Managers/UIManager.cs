using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _damageTextPrefab;
    [SerializeField] private GameObject _healthTextPrefab;
    [SerializeField] private Canvas _gameCanvas;

    private void Awake()
    {
        _gameCanvas = FindObjectOfType<Canvas>();
    }

    private void OnEnable()
    {
        CharacterEvents.CharacterDamaged += CharacterTookDamage;
        CharacterEvents.CharacterHealed += CharacterHealed;        
    }

    private void OnDisable()
    {
        CharacterEvents.CharacterDamaged -= CharacterTookDamage;
        CharacterEvents.CharacterHealed -= CharacterHealed;
    }

    public void CharacterTookDamage(GameObject character, int damageReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmp_Text = Instantiate(_damageTextPrefab, spawnPosition, Quaternion.identity, 
            _gameCanvas.transform).GetComponent<TMP_Text>();
        tmp_Text.text = damageReceived.ToString();
    }

    public void CharacterHealed(GameObject character, int healthRestored)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmp_Text = Instantiate(_healthTextPrefab, spawnPosition, Quaternion.identity,
            _gameCanvas.transform).GetComponent<TMP_Text>();
        tmp_Text.text = healthRestored.ToString();
    } 
}
