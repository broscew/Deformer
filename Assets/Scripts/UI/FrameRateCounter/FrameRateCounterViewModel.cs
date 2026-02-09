using UnityEngine;

public class FrameRateCounterViewModel : ViewModel
{
    private int m_FrameRate = 0;

    private readonly FrameRateCounterModel m_Model;

    #region Bind Properties
    public int FrameRate
    {
        get => m_FrameRate;
        set
        {
            if (m_FrameRate != value)
            {
                m_FrameRate = value;
                OnPropertyChanged();
            }
        }
    }

    #endregion // Bind Properties

    public FrameRateCounterViewModel()
    {
        m_Model = FrameRateCounterModel.Instance;

        if (m_Model == null)
        {
            Debug.LogError("Unable to get Framer Rate Counter Model!");
        }

        m_Model.OnFrameRateChanged += OnFrameRateChanged;
    }
    ~FrameRateCounterViewModel()
    {
        m_Model.OnFrameRateChanged -= OnFrameRateChanged;
    }

    public override void Init()
    {
        OnFrameRateChanged();
    }

    private void OnFrameRateChanged()
    {
        FrameRate = Mathf.RoundToInt(m_Model.FrameRate);
    }
}
