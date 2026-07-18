// Defines the contract for damper internals (springs, bump stops)

namespace WrcSuspensionCalculator.Internals
{
    public interface IDamperCartridge
    {
        string Name { get; }
        /// <summary>
        /// Calculates spring/damper deflection in millimeters given an axial force in Newtons.
        /// </summary>
        double CalculateDeflectionMm(double forceNewtons);
    }
}