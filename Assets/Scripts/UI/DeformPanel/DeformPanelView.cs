using System.ComponentModel;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class DeformPanelView : MonoBehaviour
{
    [SerializeField] public Toggle m_DeformModeToggle;
    [SerializeField] public Slider m_StrengthSlider;
    [SerializeField] public Slider m_RadiusSlider;
    [SerializeField] public Button m_ResetButton;
    [SerializeField] public TextMeshProUGUI m_PanelInfoLabel;

    private float stepSize = 0.1f;

    private DeformPanelViewModel m_ViewModel;

    private void Start()
    {
        m_ViewModel = new DeformPanelViewModel();

        m_ViewModel.PropertyChanged += OnViewModelPropertyChanged;
        m_ViewModel.Init();

        m_DeformModeToggle.onValueChanged.AddListener(OnDeformModeToggleChanged);
        m_StrengthSlider.onValueChanged.AddListener(OnStrengthSliderChanged);
        m_RadiusSlider.onValueChanged.AddListener(OnRadiusSliderChanged);
        m_ResetButton.onClick.AddListener(OnResetClick);
    }

    private void OnDestroy()
    {
        m_ResetButton.onClick.RemoveListener(OnResetClick);
        m_RadiusSlider.onValueChanged.RemoveListener(OnRadiusSliderChanged);
        m_StrengthSlider.onValueChanged.RemoveListener(OnStrengthSliderChanged);
        m_DeformModeToggle.onValueChanged.RemoveListener(OnDeformModeToggleChanged);

        m_ViewModel.PropertyChanged -= OnViewModelPropertyChanged;
    }

    private void OnDeformModeToggleChanged(bool newValue)
    {
        m_ViewModel.DeformMode = newValue;
    }

    private void OnStrengthSliderChanged(float newValue)
    {
        m_ViewModel.Strength = GetSteppedValue(newValue);
    }

    private void OnRadiusSliderChanged(float newValue)
    {
        m_ViewModel.Radius = GetSteppedValue(newValue);
    }

    private void OnResetClick()
    {
        m_ViewModel.OnResetClicked();
    }
    private float GetSteppedValue(float value)
    {
        return Mathf.Round(value / stepSize) * stepSize;
    }

    private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "DeformModeLabel")
        {
            m_DeformModeToggle.GetComponentInChildren<TextMeshProUGUI>().text = m_ViewModel.DeformModeLabel;
        }
        else if (e.PropertyName == "DeformPanelInfoLabel")
        {
            m_PanelInfoLabel.text = m_ViewModel.DeformPanelInfoLabel;
        }
        else if (e.PropertyName == "Strength")
        {
            m_StrengthSlider.value = m_ViewModel.Strength;
        }
        else if (e.PropertyName == "Radius")
        {
            m_RadiusSlider.value = m_ViewModel.Radius;
        }
    }
}
