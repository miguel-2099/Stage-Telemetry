// Implementation featuring an extra hydraulic bump stop (HBS) near end-of-stroke

using System;

namespace WrcTelemetry.Internals
{
    public class HydraulicHbsDamper : IDamperCartridge
    {
        public string Name => "Hydraulic Bump Stop (HBS) Damper";
        public double MainSpringRateNm { get; }
        public double HbsThresholdMm { get; }
        public double MaxHbsTravelMm { get; } // Hard mechanical travel boundary limit

        public HydraulicHbsDamper(double mainRate = 50000.0, double hbsThresholdMm = 120.0, double maxHbsTravelMm = 30.0)
        {
            MainSpringRateNm = mainRate;
            HbsThresholdMm = hbsThresholdMm;
            MaxHbsTravelMm = maxHbsTravelMm;
        }

        public double CalculateDeflectionMm(double forceNewtons)
        {
            double linearDeflectionMm = (forceNewtons / MainSpringRateNm) * 1000.0;

            if (linearDeflectionMm <= HbsThresholdMm)
            {
                return linearDeflectionMm;
            }

            double excessForce = forceNewtons - ((HbsThresholdMm / 1000.0) * MainSpringRateNm);
            
            // Asymptotic exponential resistance profile
            // Additional travel approaches MaxHbsTravelMm as force approaches infinity
            double stiffnessFactor = 8500.0; // Tuning parameter for compression curve speed
            double additionalTravelMm = MaxHbsTravelMm * (1.0 - Math.Exp(-excessForce / stiffnessFactor));

            return HbsThresholdMm + additionalTravelMm;
        }
    }
}