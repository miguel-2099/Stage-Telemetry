// Multi-link gemometry utilizing a dynamic linkage ratio

using WrcTelemetry.Internals;

namespace WrcTelemetry.Geometries
{
    public class MultiLinkSuspension : ISuspensionSystem
    {
        public string Name => "Multi-Link";
        public double BaseMotionRatio { get; }
        private readonly IDamperCartridge _damper;

        public MultiLinkSuspension(IDamperCartridge damper, double baseMotionRatio = 0.82)
        {
            _damper = damper;
            BaseMotionRatio = baseMotionRatio;
        }

        public double CalculateWheelTravelMm(double lateralWeightTransferNewtons)
        {
            double shockForce = lateralWeightTransferNewtons / BaseMotionRatio;
            double baseTravelMm = _damper.CalculateDeflectionMm(shockForce);

            double dynamicLeverageModifier = 0.95;
            return (baseTravelMm / BaseMotionRatio) * dynamicLeverageModifier;
        }
    }
}