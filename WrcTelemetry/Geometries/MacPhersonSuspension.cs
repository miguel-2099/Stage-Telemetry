// MacPherson geometry factoring in strut inclination angle

using System;
using WrcTelemetry.Internals;

namespace WrcTelemetry.Geometries
{
    public class MacPhersonSuspension : ISuspensionSystem
    {
        public string Name => "MacPherson Strut";
        public double InclinationAngleDegrees { get; }
        private readonly IDamperCartridge _damper;

        public MacPhersonSuspension(IDamperCartridge damper, double inclinationAngleDegrees = 12.0)
        {
            _damper = damper;
            InclinationAngleDegrees = inclinationAngleDegrees;
        }

        public double CalculateWheelTravelMm(double lateralWeightTransferNewtons)
        {
            // Applies total calculated load shift onto the outside suspension track
            double angleRadians = InclinationAngleDegrees * (Math.PI / 180.0);
            double axialStrutForce = lateralWeightTransferNewtons * Math.Cos(angleRadians);

            double damperStrokeMm = _damper.CalculateDeflectionMm(axialStrutForce);
            return damperStrokeMm / Math.Cos(angleRadians);
        }
    }
}