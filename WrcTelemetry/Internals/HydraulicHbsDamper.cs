// Implementation featuring an extra hydraulic bump stop (HBS) near end-of-stroke

using System;

namespace WrcTelemetry.Internals
{
    public class HydraulicHbsDamper : IDamperCartridge
    {
        public string Name => "Hydraulic Bump Stop (HBS) Damper";
        public double MainSpringRateNm { get; }
        public double HbsThresholdMm { get; }

        public HydraulicHbsDamper(double mainRate = 50000.0, double hbsThresholdMm = 120.0)
        {
            MainSpringRateNm = mainRate;
            HbsThresholdMm = hbsThresholdMm;
        }

        public double CalculateDeflectionMm(double forceNewtons)
        {
            double linearDeflectionMm = (forceNewtons / MainSpringRateNm) * 1000.0;

            if (linearDeflectionMm <= HbsThresholdMm)
            {
                return linearDeflectionMm;
            }

            double excessForce = forceNewtons - ((HbsThresholdMm / 1000.0) * MainSpringRateNm);
            double additionalTravelMm = Math.Log1p(excessForce / 1000.0) * 10.0;

            return HbsThresholdMm + additionalTravelMm;
        }
    }
}