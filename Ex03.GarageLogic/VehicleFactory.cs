using System;

namespace Ex03.GarageLogic
{
    public sealed class VehicleFactory
    {
        public enum eVehicleTypes
        {
            FueledCar = 1,
            ElectricCar = 2,
            FueledMotorcycle = 3,
            ElectricMotorcycle = 4,
            Truck = 5
        }

        public static Vehicle CreateVehicle(eVehicleTypes i_VehicleChoose, string i_LicenseNumber)
        {
            Vehicle newVehicle = null;

            switch (i_VehicleChoose)
            {
                case eVehicleTypes.FueledMotorcycle:
                    newVehicle = new FueledMotorcycle(i_LicenseNumber);
                    break;
                case eVehicleTypes.ElectricMotorcycle:
                    newVehicle = new ElectricMotorcycle(i_LicenseNumber);
                    break;
                case eVehicleTypes.ElectricCar:
                    newVehicle = new ElectricCar(i_LicenseNumber);
                    break;
                case eVehicleTypes.FueledCar:
                    newVehicle = new FueledCar(i_LicenseNumber);
                    break; 
                case eVehicleTypes.Truck:
                    newVehicle = new FueledTruck(i_LicenseNumber);
                    break;
            }

            return newVehicle;
        }
    }
}
