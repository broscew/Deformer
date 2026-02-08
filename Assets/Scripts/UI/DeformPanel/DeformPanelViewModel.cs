using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class DeformPanelViewModel : ViewModel
{
    private bool m_DeformMode;
    private string m_DeformModeLabel;
    private string m_DeformPanelInfoLabel;
    
    private float m_Strength;
    private float m_Radius;

    private DeformerModel m_DeformerModel;

    #region Bind Properties
    public bool DeformMode
    {
        get => m_DeformMode;
        set
        {
            if (m_DeformMode != value)
            {
                m_DeformMode = value;
                UpdateDeformModeLabel(value);
                OnPropertyChanged();
            }
        }
    }

    public string DeformModeLabel
    {
        get => m_DeformModeLabel;
        set 
        {
            if (m_DeformModeLabel != value)
            {
                m_DeformModeLabel = value;
                UpdateDeformPanelInfoLabel();
                OnPropertyChanged();
            }
        }
    }

    public float Strength
    {
        get => m_Strength;
        set
        {
            if (m_Strength != value)
            {
                m_Strength = value;
                UpdateDeformPanelInfoLabel();
                OnPropertyChanged();
            }
        }
    }
    public float Radius
    {
        get => m_Radius;
        set
        {
            if (m_Radius != value)
            {
                m_Radius = value;
                UpdateDeformPanelInfoLabel();
                OnPropertyChanged();
            }
        }
    }
    public string DeformPanelInfoLabel
    {
        get => m_DeformPanelInfoLabel;
        set
        {
            if (m_DeformPanelInfoLabel != value)
            {
                m_DeformPanelInfoLabel = value;
                OnPropertyChanged();
            }
        }
    }
    #endregion //Properties

    public DeformPanelViewModel()
    {
        GameObject deformerGameObject = GameObject.Find("Deformer");

        if (deformerGameObject == null)
        {
            Debug.LogError("Unable to find Deformer game object!");
            return;
        }

        m_DeformerModel = deformerGameObject.GetComponent<DeformerModel>();

        if (m_DeformerModel == null)
        {
            Debug.LogError("Deformer object has no DeformerModel component!");
        }

        m_DeformerModel.ConnectViewModel(this);
    }
    public override void Init()
    {
        if (m_DeformerModel == null)
        {
            return;
        }

        Strength = m_DeformerModel.m_Strength;
        Radius = m_DeformerModel.m_Radius;
        DeformMode = (m_DeformerModel.m_Mode == DeformerMode.Raise);
    }

    public void OnResetClicked()
    {
        m_DeformerModel.ResetMeshs();
    }

    private void UpdateDeformModeLabel(bool newValue)
    {
        DeformModeLabel = newValue ? "Raise" : "Lower";
    }

    private void UpdateDeformPanelInfoLabel()
    {
        DeformPanelInfoLabel = DeformModeLabel + "| Strength: " + 
            m_Strength.ToString("F2", CultureInfo.InvariantCulture) + 
            "| Radius: " + m_Radius.ToString("F2", CultureInfo.InvariantCulture);
    }
}
