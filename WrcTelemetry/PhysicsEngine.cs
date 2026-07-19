// Calculates turning physics, centripetal acceleration, and lateral weight trasnfer

using System;
using WrcTelemetry.VehicleComponents;

namespace WrcTelemetry
{
    public static class PhysicsEngine
    {
        private const double Gravity = 9.81; // m/s^2

        public static double KmHourToMetersSecond(double kmh) => kmh / 3.6;

        public static double CalculateCentripetalAcceleration(double velocityMs, double radiusMeters)
        {
            if (radiusMeters <= 0) throw new ArgumentException("Radius must be greater than zero.");
            return (velocityMs * velocityMs) / radiusMeters;
        }

        public static double CalculateGForce(double centripetalAccel)
        {
            return centripetalAccel / Gravity;
        }

        public static double CalculateLateralWeightTransferNewtons(WrcCar car, double centripetalAccel)
        {
            // Weight Transfer (N) = (Mass * Centripetal Acceleration * CG Height) / Track Width
            return (car.MassKg * centripetalAccel * car.CenterOfGravityHeightMeters) / car.TrackWidthMeters;
        }
    }
}