using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private Slider _energySlider;
    [SerializeField] private TMP_Text _energyText;


    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TMP_Text _healthText;

    [SerializeField] private GameObject _pausePanel;

    void OnEnable()
    {
        Player.Instance.OnEnergyChanged += UpdateEnergySlider;
        Player.Instance.OnHealthChanged += UpdateHealthSlider;

        GameManager.Instance.OnGamePaused += TogglePausePanel;
    }
    void OnDisable()
    {
        Player.Instance.OnEnergyChanged -= UpdateEnergySlider;
        Player.Instance.OnHealthChanged -= UpdateHealthSlider;

        GameManager.Instance.OnGamePaused -= TogglePausePanel;
    }

    void Start()
    {
        UpdateEnergySlider();
        UpdateHealthSlider();
    }


    void UpdateEnergySlider()
    {
        _energySlider.value = Mathf.RoundToInt(Player.Instance.Energy);
        _energySlider.maxValue = Player.Instance.MaxEnergy;
        _energyText.text = _energySlider.value + "/" + _energySlider.maxValue;
    }

    void UpdateHealthSlider()
    {
        _healthSlider.value = Player.Instance.Health;
        _healthSlider.maxValue = Player.Instance.MaxHealth;
        _healthText.text = _healthSlider.value + "/" + _healthSlider.maxValue;
    }

    public void TogglePausePanel()
    {
        _pausePanel.SetActive(!_pausePanel.activeSelf);
        Time.timeScale = _pausePanel.activeSelf ? Time.timeScale = 0f : Time.timeScale = 1f;
    }
}
