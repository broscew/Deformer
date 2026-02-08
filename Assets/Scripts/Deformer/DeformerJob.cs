using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

[BurstCompile]
public struct DeformerJob : IJobParallelFor
{
    private NativeArray<Vector3> m_VerticesPosition;
    [ReadOnly] private DeformerParams m_Param;

    public DeformerJob(ref DeformerParams param, NativeArray<Vector3> verticesPosition)
    {
        m_Param = param;
        m_VerticesPosition = verticesPosition;
    }

    public void Execute(int index)
    {
        Vector3 position = m_VerticesPosition[index];
        position = DeformerUtilities.CalculateDisplacement(position, ref m_Param);
        m_VerticesPosition[index] = position;
    }
}
