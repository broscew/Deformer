using UnityEngine;

public class FrameRateCounterModel : MonoBehaviour
{
    public event System.Action OnFrameRateChanged;

    private float m_RefreshRate = 0.5f;
    private float m_TimeSinceLastUpdate = 0.0f;
    private int m_FrameCounter = 0;
    private float m_FrameRate = 0;

    public float FrameRate
    {
        get => m_FrameRate;

        set 
        {
            if (false == Mathf.Approximately(m_FrameRate, value))
            {
                m_FrameRate = value;
                OnFrameRateChanged?.Invoke();
            }
        }
    }

    public static FrameRateCounterModel Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        ++m_FrameCounter;
        m_TimeSinceLastUpdate += Time.unscaledDeltaTime;

        if (m_TimeSinceLastUpdate >= m_RefreshRate)
        {
            FrameRate = (m_FrameCounter / m_TimeSinceLastUpdate);

            m_FrameCounter = 0;
            m_TimeSinceLastUpdate = 0;
        }
    }
}
