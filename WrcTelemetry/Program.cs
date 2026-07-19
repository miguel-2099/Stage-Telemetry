// Basic user interface, config. menus, and output engine

using System;
using WrcTelemetry.Geometries;
using WrcTelemetry.Internals;
using WrcTelemetry.VehicleComponents;

namespace WrcTelemetry
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("====================================================");
                Console.WriteLine("    WRC SUSPENSION TRAVEL & G-FORCE CALCULATOR     ");
                Console.WriteLine("====================================================\n");

                // 1. Pace Note Selection
                Console.WriteLine("Select Turn Type (Pace Note):");
                Console.WriteLine("1. Hairpin (R = 6m,  V = 32 km/h)");
                Console.WriteLine("2. Square  (R = 12m, V = 50 km/h)");
                Console.WriteLine("3. Turn 1  (R = 20m, V = 65 km/h)");
                Console.WriteLine("4. Turn 3  (R = 50m, V = 100 km/h)");
                Console.WriteLine("5. Turn 6  (R = 140m, V = 165 km/h)");
                Console.Write("Choice (1-5): ");
                
                if (!int.TryParse(Console.ReadLine(), out int turnChoice)) turnChoice = 2;
                GetTurnData(turnChoice, out double radiusMeters, out double speedKmh);

                // 2. Select Damper Cartridge
                Console.WriteLine("\nSelect Internal Damper Configuration:");
                Console.WriteLine("1. Linear Rate Spring (Standard)");
                Console.WriteLine("2. Dual-Stage Spring (Main + Helper)");
                Console.WriteLine("3. Hydraulic Bump Stop (HBS) Damper");
                Console.Write("Choice (1-3): ");

                if (!int.TryParse(Console.ReadLine(), out int damperChoice)) damperChoice = 1;
                IDamperCartridge damper = damperChoice switch
                {
                    2 => new DualStageSpring(),
                    3 => new HydraulicHbsDamper(),
                    _ => new LinearSpring()
                };

                // 3. Select Suspension Geometry
                Console.WriteLine("\nSelect Suspension Geometry:");
                Console.WriteLine("1. MacPherson Strut (WRC Standard)");
                Console.WriteLine("2. Double Wishbone");
                Console.WriteLine("3. Multi-Link");
                Console.Write("Choice (1-3): ");

                if (!int.TryParse(Console.ReadLine(), out int geometryChoice)) geometryChoice = 1;
                ISuspensionSystem suspension = geometryChoice switch
                {
                    2 => new DoubleWishboneSuspension(damper),
                    3 => new MultiLinkSuspension(damper),
                    _ => new MacPhersonSuspension(damper)
                };

                // 4. Vehicle Profile setup
                WrcCar car = new WrcCar(); 

                // 5. Run Calculations
                double velocityMs = PhysicsEngine.KmHourToMetersSecond(speedKmh);
                double centripetalAccel = PhysicsEngine.CalculateCentripetalAcceleration(velocityMs, radiusMeters);
                double gForce = PhysicsEngine.CalculateGForce(centripetalAccel);
                double weightTransferN = PhysicsEngine.CalculateLateralWeightTransferNewtons(car, centripetalAccel);
                double wheelTravelMm = suspension.CalculateWheelTravelMm(weightTransferN);

                // 6. Display Output
                Console.WriteLine("\n====================================================");
                Console.WriteLine("                SIMULATION RESULTS                  ");
                Console.WriteLine("====================================================");
                Console.WriteLine($"Selected Geometry  : {suspension.Name}");
                Console.WriteLine($"Selected Cartridge : {damper.Name}");
                Console.WriteLine($"Corner Radius      : {radiusMeters:F1} m");
                Console.WriteLine($"Entry Speed        : {speedKmh:F1} km/h ({velocityMs:F1} m/s)");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine($"Lateral G-Force    : {gForce:F2} G");
                Console.WriteLine($"Net Weight Transfer: {weightTransferN:F1} N ({weightTransferN / 9.81:F1} kg side-to-side)");
                Console.WriteLine($"Outside Wheel Travel: {wheelTravelMm:F2} mm");
                Console.WriteLine("====================================================\n");

                // 7. Loop / Exit Control Flow
                Console.Write("Perform another simulation run? (Y/N): ");
                string input = Console.ReadLine()?.Trim().ToUpper() ?? "N";
                
                if (input == "N" || input == "NO")
                {
                    running = false;
                    Console.WriteLine("\nExiting Telemetry System. Ride safe!");
                }
            }
        }

        private static void GetTurnData(int choice, out double radius, out double speedKmh)
        {
            switch (choice)
            {
                case 1:
                    radius = 6.0;
                    speedKmh = 32.0;
                    break;
                case 3:
                    radius = 20.0;
                    speedKmh = 65.0;
                    break;
                case 4:
                    radius = 50.0;
                    speedKmh = 100.0;
                    break;
                case 5:
                    radius = 140.0;
                    speedKmh = 165.0;
                    break;
                case 2:
                default:
                    radius = 12.0;
                    speedKmh = 50.0;
                    break;
            }
        }
    }
}