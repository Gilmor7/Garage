using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public sealed class Garage
    {
        private Dictionary<string, GarageVehicle> m_Vehicles = new Dictionary<string, GarageVehicle>();

        public bool IsVehicleInGarage(string i_LicenseNumber)
        {
            return m_Vehicles.ContainsKey(i_LicenseNumber);
        }

        public void AddVehicle(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerCellphone)
        {
            GarageVehicle garageVehicle = new GarageVehicle(i_Vehicle, i_OwnerName, i_OwnerCellphone);

            m_Vehicles.Add(i_Vehicle.LicenseNumber, garageVehicle);
        }

        public List<string> GetLicenseNumberListFilteredByStatus(GarageVehicle.eStatus? i_Status)
        {
            List<string> licenseNumbersList;

            if(i_Status.HasValue)
            {
                licenseNumbersList = filterLicenseNumberListByStatus((GarageVehicle.eStatus)i_Status);
            }
            else
            {
                licenseNumbersList = new List<string>(m_Vehicles.Keys);
            }

            return licenseNumbersList;
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, GarageVehicle.eStatus i_NewStatus)
        {
            if (IsVehicleInGarage(i_LicenseNumber))
            {
                m_Vehicles[i_LicenseNumber].Status = i_NewStatus;
            }
            else
            {
                throw new ArgumentException(getInvalidLicenseNumberMessage(i_LicenseNumber));
            }
        }

        public void InflateVehicleWheelsToMax(string i_LicenseNumber)
        {
            if (IsVehicleInGarage(i_LicenseNumber))
            {
                m_Vehicles[i_LicenseNumber].InflateWheelsToMax();
            }
            else
            {
                throw new ArgumentException(getInvalidLicenseNumberMessage(i_LicenseNumber));
            }
        }

        public void RefuelVehicle(string i_LicenseNumber, Fuel.eFuelType i_FuelType, float i_AmountToFill)
        {
            if (IsVehicleInGarage(i_LicenseNumber))
            {
                bool isFuelValid = true;
                
                if (isFuelValid)
                {
                }
            }
            else
            {
                throw new ArgumentException(getInvalidLicenseNumberMessage(i_LicenseNumber));
            }
        }

        public void ChargeElectricVehicle(string i_LicenseNumber, float i_MinutesToCharge)
        {
            if (IsVehicleInGarage(i_LicenseNumber))
            {
            }
            else
            {
                throw new ArgumentException(getInvalidLicenseNumberMessage(i_LicenseNumber));
            }
        }

        public string GetVehicleInfo(string i_LicenseNumber)
        {
            string vehicleInfo;

            if (IsVehicleInGarage(i_LicenseNumber))
            {
                vehicleInfo = m_Vehicles[i_LicenseNumber].ToString();
            }
            else
            {
                throw new ArgumentException(getInvalidLicenseNumberMessage(i_LicenseNumber));
            }

            return vehicleInfo;
        }

        private List<string> filterLicenseNumberListByStatus(GarageVehicle.eStatus i_Status)
        {
            List<string> filteredList = new List<string>();

            foreach(KeyValuePair<string, GarageVehicle> garageVehicle in m_Vehicles)
            {
                if(garageVehicle.Value.Status == i_Status)
                {
                    filteredList.Add(garageVehicle.Key);
                }
            }

            return filteredList;
        }

        private string getInvalidLicenseNumberMessage(string i_LicenseNumber)
        {
            string msg = $"Couldn't find Vehicle with the license number: {i_LicenseNumber}.";
            return msg;
        }
    }
}
