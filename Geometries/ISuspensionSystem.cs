// Basically defining contract for overall suspension geometry

namespace WrcSuspensionCalculator.Geometries
{
    public interface ISuspensionSystem
    {
        string Name { get; }
        /// <summary>
        /// Computes wheel displacement (in mm) based on total lateral load transfer force.
        /// </summary>
        double CalculateWheelTravelMm(double lateralWeightTransferNewtons);
    }
}