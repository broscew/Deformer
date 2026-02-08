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
        GameObject frameCounterObject = GameObject.Find("FrameCounter");

        if (frameCounterObject == null)
        {
            Debug.LogError("Unable to find FrameCounter game object!");
            return;
        }

        m_Model = frameCounterObject.GetComponent<FrameRateCounterModel>();

        if (m_Model == null)
        {
            Debug.LogError("Deformer object has no DeformerModel component!");
        }

        m_Model.OnFrameRateChanged += OnFrameRateChanged;
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
