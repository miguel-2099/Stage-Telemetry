namespace WrcSuspensionCalculator.VehicleComponents
{
    public class WrcCar
    {
        public double MassKg { get; set; }
        public double TrackWidthMeters { get; set; }
        public double CenterOfGravityHeightMeters { get; set; }

        public WrcCar(double massKg = 1430.0, double trackWidthMeters = 1.6, double cgHeightMeters = 0.5)
        {
            MassKg = massKg;
            TrackWidthMeters = trackWidthMeters;
            CenterOfGravityHeightMeters = cgHeightMeters;
        }
    }
}