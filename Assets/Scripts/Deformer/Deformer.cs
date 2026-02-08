using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Deformer : MonoBehaviour
{
    private Mesh m_Mesh;

    private NativeArray<Vector3> m_OriginalVertices;
    private NativeArray<Vector3> m_CurrentVertices;
    private bool m_Scheduled;
    private DeformerJob m_Job;
    private JobHandle m_Handle;

    private void Awake()
    {
        m_Mesh = GetComponent<MeshFilter>().mesh;

        m_OriginalVertices = new NativeArray<Vector3>(m_Mesh.vertices, Allocator.Persistent);
        m_CurrentVertices = new NativeArray<Vector3>(m_Mesh.vertices, Allocator.Persistent);
    }

    private void LateUpdate()
    {
        CompleteJob();
    }

    private void OnDestroy()
    {
        m_OriginalVertices.Dispose();
        m_CurrentVertices.Dispose();
    }
    public bool TryScheduleJob(ref DeformerParams param)
    {
        if (true == m_Scheduled)
        {
            return false;
        }

        m_Scheduled = true;
        m_Job = new DeformerJob(ref param, m_CurrentVertices);
        m_Handle = m_Job.Schedule(m_CurrentVertices.Length, 64);

        return true;
    }

    private void CompleteJob()
    {
        if (false == m_Scheduled)
        {
            return;
        }

        m_Handle.Complete();

        m_Mesh.MarkDynamic();
        m_Mesh.SetVertices(m_CurrentVertices);
        m_Mesh.RecalculateNormals();

        m_Scheduled = false;
    }
    public void ResetMesh()
    {
        for (int i = 0; i < m_OriginalVertices.Length; ++i)
        {
            m_CurrentVertices[i] = m_OriginalVertices[i];
            
        }

        m_Mesh.MarkDynamic();
        m_Mesh.SetVertices(m_OriginalVertices);
        m_Mesh.RecalculateNormals();
    }
}
