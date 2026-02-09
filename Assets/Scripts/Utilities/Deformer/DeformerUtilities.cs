using Unity.Burst;
using UnityEngine;

public enum DeformerMode { Raise, Lower };

public struct DeformerParams
{
    public Matrix4x4 m_OwnerTranformMatrix;
    public Vector3 m_SelectedPosition;
    public Vector3 m_SelectedNormal;
    public float m_Radius;
    public float m_Strength;
    public bool m_Mode;
};

public static class DeformerUtilities
{
    [BurstCompile]
    public static Vector3 CalculateDisplacement(Vector3 vertexPosition, ref DeformerParams param)
    {
        Vector3 worldVertex = param.m_OwnerTranformMatrix.MultiplyPoint3x4(vertexPosition);

        Vector3 pointToVertex = worldVertex - param.m_SelectedPosition;
        float radialDiff = (param.m_Radius * param.m_Radius) - pointToVertex.sqrMagnitude;
        
        radialDiff = Mathf.Max(radialDiff, 0.0f);

        float radialRatio = radialDiff / param.m_Radius;
        float displacement = param.m_Strength * radialRatio;

        Vector3 vertexVelocity = param.m_SelectedNormal * displacement;

        if (false == param.m_Mode)
        {
            vertexVelocity *= -1;
        }

        Vector3 newPosition = vertexPosition + vertexVelocity;

        return newPosition;
    }
}
