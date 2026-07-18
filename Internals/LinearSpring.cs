// Implementation of a standard single-rate spring (F=k*x)

namespace WrcSuspensionCalculator.Internals
{
    public class LinearSpring : IDamperCartridge
    {
        public string Name => "Linear Rate Spring";
        public double SpringRateNm { get; } // Newtons per meter

        public LinearSpring(double springRateNm = 45000.0)
        {
            SpringRateNm = springRateNm;
        }

        public double CalculateDeflectionMm(double forceNewtons)
        {
            // x (meters) = F / k
            double deflectionMeters = forceNewtons / SpringRateNm;
            return deflectionMeters * 1000.0; // convert to mm
        }
    }
}