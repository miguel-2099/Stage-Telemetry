// Defines the contract for damper internals (springs, bump stops)

namespace WrcTelemetry.Internals
{
    public interface IDamperCartridge
    {
        string Name { get; }
        double CalculateDeflectionMm(double forceNewtons);
    }
}