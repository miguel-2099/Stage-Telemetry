// Implementation of a dual-rate setup (tender/helper spring + main spring)

namespace WrcSuspensionCalculator.Internals
{
    public class DualStageSpring : IDamperCartridge
    {
        public string Name => "Dual-Stage Spring (Helper + Main)";
        public double HelperSpringRateNm { get; }
        public double MainSpringRateNm { get; }
        public double HelperTravelLimitMm { get; }

        public DualStageSpring(double helperRate = 20000.0, double mainRate = 50000.0, double helperLimitMm = 25.0)
        {
            HelperSpringRateNm = helperRate;
            MainSpringRateNm = mainRate;
            HelperTravelLimitMm = helperLimitMm;
        }

        public double CalculateDeflectionMm(double forceNewtons)
        {
            // Combined rate while helper spring is active: 1/k_eff = 1/k_helper + 1/k_main
            double combinedRate = (HelperSpringRateNm * MainSpringRateNm) / (HelperSpringRateNm + MainSpringRateNm);
            double initialDeflectionMm = (forceNewtons / combinedRate) * 1000.0;

            if (initialDeflectionMm <= HelperTravelLimitMm)
            {
                return initialDeflectionMm;
            }

            // Helper spring bound completely; remaining force acts solely on main spring
            double forceToBindHelper = (HelperTravelLimitMm / 1000.0) * combinedRate;
            double remainingForce = forceNewtons - forceToBindHelper;
            double mainDeflectionMeters = remainingForce / MainSpringRateNm;

            return HelperTravelLimitMm + (mainDeflectionMeters * 1000.0);
        }
    }
}