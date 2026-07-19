// Implementation of a standard single-rate spring (F=k*x)

namespace WrcTelemetry.Internals
{
    public class LinearSpring : IDamperCartridge
    {
        public string Name => "Linear Rate Spring";
        public double SpringRateNm { get; }

        public LinearSpring(double springRateNm = 45000.0)
        {
            SpringRateNm = springRateNm;
        }

        public double CalculateDeflectionMm(double forceNewtons)
        {
            double deflectionMeters = forceNewtons / SpringRateNm;
            return deflectionMeters * 1000.0;
        }
    }
}