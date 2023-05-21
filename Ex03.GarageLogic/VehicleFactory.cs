﻿using System;

namespace Ex03.GarageLogic
{
    public sealed class VehicleFactory
    {
        public enum eVehicleTypes
        {
            FueledCar,
            ElectricCar,
            FueledMotorcycle,
            ElectricMotorcycle,
            Truck
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
                    break;
                case eVehicleTypes.ElectricCar:
                    break;
                case eVehicleTypes.FueledCar:
                    break; 
                case eVehicleTypes.Truck:
                    break;
            }

            return newVehicle;
        }
    }
}
