using System.ComponentModel;
using TMPro;
using UnityEngine;

public class FrameRateCounterView : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI m_FrameRateLabel;
    [SerializeField] public int m_LowFPSThreshold = 20;

    private FrameRateCounterViewModel m_ViewModel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_ViewModel = new FrameRateCounterViewModel();
        m_ViewModel.PropertyChanged += OnViewModelPropertyChanged;
        m_ViewModel.Init();
    }

    private void OnDestroy()
    {
        m_ViewModel.PropertyChanged -= OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "FrameRate")
        {
            UpdateFrameCounterText(m_ViewModel.FrameRate);
        }
    }
    private void UpdateFrameCounterText(int newFrameRate)
    {
        m_FrameRateLabel.text = newFrameRate.ToString();

        Color textColor = newFrameRate <= m_LowFPSThreshold ? Color.red : Color.green;
        m_FrameRateLabel.color = textColor;
    }
}
