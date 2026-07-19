// Basically defining contraction for overall suspension geometry

namespace WrcTelemetry.Geometries
{
    public interface ISuspensionSystem
    {
        string Name { get; }
        double CalculateWheelTravelMm(double lateralWeightTransferNewtons);
    }
}