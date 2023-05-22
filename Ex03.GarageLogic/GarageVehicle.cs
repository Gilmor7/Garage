using System;

namespace Ex03.GarageLogic
{
    public class GarageVehicle
    {
        public enum eStatus
        {
            InRepair,
            Repaired,
            Paid,
        }

        private readonly Vehicle r_Vehicle;
        private string m_OwnerName;
        private string m_OwnerCellphone;
        private eStatus m_Status = eStatus.InRepair;

        public GarageVehicle(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerCellphone)
        {
            r_Vehicle = i_Vehicle;
            m_OwnerName = i_OwnerName;
            m_OwnerCellphone = i_OwnerCellphone;
        }

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }

            set
            {
                m_OwnerName = value;
            }
        }

        public string OwnerCellphone
        {
            get
            {
                return m_OwnerCellphone;
            }

            set
            {
                m_OwnerCellphone = value;
            }
        }

        public eStatus Status
        {
            get
            {
                return m_Status;
            }
            set
            {
                m_Status = value;
            }
        }

        public void InflateWheelsToMax()
        {
            r_Vehicle.InflateWheelsToMax();
        }

        public void FillEnergySource(float i_AmountToFill)
        {
            r_Vehicle.FillEnergySource(i_AmountToFill);
        }

        public VehicleEnergySource GetEnergySource()
        {
            return r_Vehicle.EnergySource;
        }

        public override string ToString()
        {
            string fullVehicleInfo = string.Format(
                @"Owner name: {0}
Owner Cellphone number: {1}
Vehicle status: {2}
{3}",
                m_OwnerName,
                m_OwnerCellphone,
                m_Status.ToString(),
                r_Vehicle.ToString());

            return fullVehicleInfo;
        }
    }
}
