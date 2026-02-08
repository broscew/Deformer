using System.ComponentModel;
using TMPro;
using UnityEngine;

public class DeformerModel : MonoBehaviour
{
    [SerializeField] private InputReader m_InputReader;

    [SerializeField, Range(1.0f, 10.0f)] public float m_Radius = 2.0f;
    [SerializeField, Range(0.1f, 1.0f)] public float m_Strength = 1f;
    [SerializeField] public DeformerMode m_Mode = DeformerMode.Raise;

    private DeformPanelViewModel m_ViewModel;

    public void ConnectViewModel(DeformPanelViewModel viewModel)
    {
        if (m_ViewModel != null)
        {
            m_ViewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }

        m_ViewModel = viewModel;
        m_ViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "Strength")
        {
            m_Strength = m_ViewModel.Strength;
        }
        else if (e.PropertyName == "Radius")
        {
            m_Radius = m_ViewModel.Radius;
        }
        else if (e.PropertyName == "DeformMode")
        {
            m_Mode = m_ViewModel.DeformMode ? DeformerMode.Raise : DeformerMode.Lower;
        }
    }

    private void OnEnable()
    {
        m_InputReader.DeformEvent += OnDeform;
    }

    private void OnDisable()
    {
        m_InputReader.DeformEvent -= OnDeform;
        m_ViewModel.PropertyChanged -= OnViewModelPropertyChanged;
    }

    private void OnDeform(Vector2 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        bool result = Physics.Raycast(ray, out hit);

        if(false == result)
        {
            return;
        }

        Collider collider = hit.collider;
        Deformer colliderDeformer = collider.GetComponent<Deformer>();
        Transform colliderTransform = collider.GetComponent<Transform>();

        if (colliderDeformer == null || colliderTransform == null)
        {
            Debug.LogError("Unable to find Derformer or the Transform Component!");
            return;
        }

        ProcessDeformation(hit, colliderDeformer, colliderTransform);
    }

    private void ProcessDeformation(RaycastHit hit, Deformer deformer, Transform transform)
    {
        DeformerParams deformerParams = new DeformerParams();
        deformerParams.m_OwnerTranformMatrix = transform.localToWorldMatrix;
        deformerParams.m_SelectedPosition = hit.point;
        deformerParams.m_SelectedNormal = hit.normal;
        deformerParams.m_Radius = m_Radius;
        deformerParams.m_Strength = m_Strength;
        deformerParams.m_Mode = (m_Mode == DeformerMode.Raise);

        deformer.TryScheduleJob(ref deformerParams);
    }

    public void ResetMeshs()
    {
        Deformer[] components = GameObject.FindObjectsByType<Deformer>(FindObjectsSortMode.None);

        foreach (Deformer component in components)
        {
            component.ResetMesh();
        }
    }
}
