// Double wishbone geometry using Motion Ratio (MR)

using WrcTelemetry.Internals;

namespace WrcTelemetry.Geometries
{
    public class DoubleWishboneSuspension : ISuspensionSystem
    {
        public string Name => "Double Wishbone";
        public double MotionRatio { get; }
        private readonly IDamperCartridge _damper;

        public DoubleWishboneSuspension(IDamperCartridge damper, double motionRatio = 0.75)
        {
            _damper = damper;
            MotionRatio = motionRatio;
        }

        public double CalculateWheelTravelMm(double lateralWeightTransferNewtons)
        {
            double verticalForcePerWheel = lateralWeightTransferNewtons / 2.0;
            double shockForce = verticalForcePerWheel / MotionRatio;
            double shockTravelMm = _damper.CalculateDeflectionMm(shockForce);

            return shockTravelMm / MotionRatio;
        }
    }
}