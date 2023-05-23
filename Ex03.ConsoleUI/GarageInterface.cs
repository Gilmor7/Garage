using System;
using Ex03.GarageLogic;
using System.Collections.Generic;
using System.Text;

namespace Ex03.ConsoleUI
{
	public sealed class GarageInterface
    {
		private enum eUserInput
		{
			None,
			AddNewVehicle,
			ShowFilteredVehicleList,
			ChangeVehicleStatus,
			FillVehicleTiresToMax,
			FuelVehicle,
			ChargeVehicle,
			GetFullVehicleInfo,
			ExitTheSystem
		}

        private const int k_MinMenuOption = 1;
        private const int k_MaxMenuOption = 8;

		private eUserInput m_CurrInput;
		private readonly Garage r_Garage = null;

		public GarageInterface()
		{
			m_CurrInput = eUserInput.None;
            r_Garage = new Garage();
		}

		public void RunMenu()
		{
			while(m_CurrInput != eUserInput.ExitTheSystem)
			{
				displayMenu();
                try
                {
                    m_CurrInput = getMenuInputFromUser();
                    activateMenuOption();
                }
                catch(Exception e)
                {
                    Console.WriteLine("An error has occured: " + e.Message);
                }
                finally
                {
                    if(m_CurrInput != eUserInput.ExitTheSystem)
                    {
                        Console.WriteLine("Going back to menu...\n");
                    }
                }
			}
            Console.WriteLine("Exiting the system...");
		}

        private eUserInput getMenuInputFromUser()
        {
            eUserInput inputAsEnum = eUserInput.None;
            bool isValidInput = false;

            while (!isValidInput)
            {
                string userInput = Console.ReadLine();

                if (!Enum.TryParse(userInput, out inputAsEnum))
                {
                    throw new FormatException(GarageMessages.ErrorMessages.k_InvalidInput);
                }
                
                if (!Enum.IsDefined(typeof(eUserInput), inputAsEnum) || inputAsEnum == eUserInput.None)
                {
                    throw new ValueOutOfRangeException(k_MinMenuOption,
                        k_MaxMenuOption, GarageMessages.ErrorMessages.k_ValueOutOfRange);
                }
                
                isValidInput = true;
            }

            return inputAsEnum;
        }

        private void activateMenuOption()
        {
            switch (m_CurrInput)
            {
                case eUserInput.AddNewVehicle:
                    addNewVehicle();
                    break;
                case eUserInput.ShowFilteredVehicleList:
                    showFilteredVehicleList();
                    break;
                case eUserInput.ChangeVehicleStatus:
                    changeVehicleStatus();
                    break;
                case eUserInput.FillVehicleTiresToMax:
                    fillVehicleTiresToMax();
                    break;
                case eUserInput.FuelVehicle:
                    fuelVehicle();
                    break;
                case eUserInput.ChargeVehicle:
                    chargeVehicle();
                    break;
                case eUserInput.GetFullVehicleInfo:
                    getFullVehicleInfo();
                    break;
                case eUserInput.ExitTheSystem:
                    break;
                default:
                    throw new ArgumentException(GarageMessages.ErrorMessages.k_InvalidMenuOption);
            }
        }

        private void addNewVehicle()
        {
            string licensePlate = askForInputAfterMsg(GarageMessages.k_LicensePlateMsg);
            bool isVehicleInGarage = r_Garage.IsVehicleInGarage(licensePlate);

            if (isVehicleInGarage)
            {
                switchVehicleToRepairModeAndInformCustomer(licensePlate);
            }
            else
            {
                string carTypes = constructEnumValuesMsg<VehicleFactory.eVehicleTypes>();
                Console.WriteLine(carTypes);
                string vehicleTypeStr = askForInputAfterMsg(GarageMessages.k_VehicleTypeMsg);
                int numOfVehicleTypes = Enum.GetNames(typeof(VehicleFactory.eVehicleTypes)).Length;
                if(!Enum.TryParse(vehicleTypeStr, out VehicleFactory.eVehicleTypes vehicleType))
                {
                    throw new FormatException(GarageMessages.ErrorMessages.k_InvalidVehicleType);
                }
                if(!Enum.IsDefined(typeof(VehicleFactory.eVehicleTypes), vehicleType))
                {
                    throw new ValueOutOfRangeException(1, numOfVehicleTypes, GarageMessages.ErrorMessages.k_ValueOutOfRange + "1 to " + numOfVehicleTypes);
                }

                Vehicle vehicleToInsertToGarage = VehicleFactory.CreateVehicle(vehicleType, licensePlate);
                vehicleToInsertToGarage.SetMyRequirements();
                string ownerName = askForInputAfterMsg(GarageMessages.k_OwnerNameMsg);
                string ownerPhone = askForInputAfterMsg(GarageMessages.k_OwnerPhoneMsg);
                updateVehicleStateBasedOnRequirements(vehicleToInsertToGarage);
                r_Garage.AddVehicle(vehicleToInsertToGarage, ownerName, ownerPhone);
                r_Garage.ChangeVehicleStatus(licensePlate, GarageVehicle.eStatus.InRepair);
                Console.WriteLine("Vehicle was added successfully!\n");
            }
        }

        private void switchVehicleToRepairModeAndInformCustomer(string i_LicensePlate)
        {
            r_Garage.ChangeVehicleStatus(i_LicensePlate, GarageVehicle.eStatus.InRepair);
            Console.WriteLine(GarageMessages.k_ExistingVehicleStatusChangeToRepair);
        }

        private void updateVehicleStateBasedOnRequirements(Vehicle i_VehicleToUpdate)
        {
            Dictionary<string, string> requirements = i_VehicleToUpdate.Requirments;
            Dictionary<string, string> userValues = new Dictionary<string, string>();
            bool isUpdatingWasOk = false;
            
            while (!isUpdatingWasOk)
            {
                userValues.Clear();
                
                foreach (KeyValuePair<string, string> requirementPair in requirements)
                {
                    Console.WriteLine(requirementPair.Value);
                    string userInput = Console.ReadLine();
                    userValues.Add(requirementPair.Key, userInput);
                }
                try
                {
                    i_VehicleToUpdate.SetValuesFromRequirements(userValues);
                    isUpdatingWasOk = true;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Enter the values again!");
                }
            }
        }

        private void showFilteredVehicleList()
        {
            string statusList = constructEnumValuesMsg<GarageVehicle.eStatus>();
            Console.WriteLine(statusList);
            string statusStr = askForInputAfterMsg(GarageMessages.k_FilterByStatusMsg);
            if (!Enum.TryParse(statusStr, out GarageVehicle.eStatus status))
            {
                throw new ArgumentException(GarageMessages.ErrorMessages.k_InvalidStatus);
            }

            List<string> filteredVehicles = r_Garage.GetLicenseNumberListFilteredByStatus(status);

            if (filteredVehicles.Count == 0)
            {
                Console.WriteLine("No vehicles found with the specified status.");
            }
            else
            {
                Console.WriteLine("Vehicles with status {0}:", status);
                
                foreach (string licensePlate in filteredVehicles)
                {
                    Console.WriteLine("License Plate: " + licensePlate);
                }
            }
        }

        private void changeVehicleStatus()
        {
            string vehicleStatusesList = constructEnumValuesMsg<GarageVehicle.eStatus>();
            string licenseNumber = askForInputAfterMsg(GarageMessages.k_LicensePlateMsg);
            Console.WriteLine(vehicleStatusesList);
            string newStatusStr = askForInputAfterMsg(GarageMessages.k_StatusChangeMsg);

            if (!Enum.TryParse(newStatusStr, out GarageVehicle.eStatus newStatus))
            {
                throw new ArgumentException(GarageMessages.ErrorMessages.k_InvalidStatus);
            }

            r_Garage.ChangeVehicleStatus(licenseNumber, newStatus);
            Console.WriteLine("Vehicle status successfully changed.\n");
        }

        private void fillVehicleTiresToMax()
        {
            string licensePlate = askForInputAfterMsg(GarageMessages.k_LicensePlateMsg);
            r_Garage.InflateVehicleWheelsToMax(licensePlate);
        }

        private void getFullVehicleInfo()
        {
            string licensePlate = askForInputAfterMsg(GarageMessages.k_LicensePlateMsg);
            string vehicleInfo = r_Garage.GetVehicleInfo(licensePlate);
            Console.WriteLine(vehicleInfo);
        }

        private void chargeVehicle()
        {
            string licensePlate = askForInputAfterMsg(GarageMessages.k_LicensePlateMsg);
            string numOfMinutesToCharge = askForInputAfterMsg(GarageMessages.k_ElectricCarChargeMsg);
            
            if(!float.TryParse(numOfMinutesToCharge, out float parsedMinutesToCharge))
            {
                throw new ArgumentException(GarageMessages.ErrorMessages.k_InvalidAmountOfMinutes);
            }
            else
            {
                r_Garage.ChargeElectricVehicle(licensePlate, parsedMinutesToCharge);
                Console.WriteLine("Vehicle successfully charged.\n");
            }
        }

        private void fuelVehicle()
        {
            string fuelTypesList = constructEnumValuesMsg<Fuel.eFuelType>();
            string licensePlate = askForInputAfterMsg(GarageMessages.k_LicensePlateMsg);
            Console.WriteLine(fuelTypesList);
            string fuelTypeInput = askForInputAfterMsg(GarageMessages.k_FuelTypeMsg);
            string fuelAmountInput = askForInputAfterMsg(GarageMessages.k_FuelAmountMsg);

            if (!Enum.TryParse(fuelTypeInput, out Fuel.eFuelType parsedFuelType))
            {
                throw new ArgumentException(GarageMessages.ErrorMessages.k_InvalidFuelType);
            }

            if (!float.TryParse(fuelAmountInput, out float parsedFuelAmount))
            {
                throw new ArgumentException(GarageMessages.ErrorMessages.k_InvalidFuelAmount);
            }

            r_Garage.RefuelVehicle(licensePlate, parsedFuelType, parsedFuelAmount);
            Console.WriteLine("Vehicle successfully fueled.\n");
        }
        
        private string constructEnumValuesMsg<TEnum>() where TEnum : Enum
        {
            StringBuilder enumValuesMsg = new StringBuilder();
            int i = 1;

            foreach (TEnum enumValue in Enum.GetValues(typeof(TEnum)))
            {
                enumValuesMsg.AppendLine($"{i}. {enumValue}");
                i++;
            }

            return enumValuesMsg.ToString();
        }


        private string askForInputAfterMsg(string i_Msg)
        {
            Console.WriteLine(i_Msg);
            return Console.ReadLine();
        }



        private void displayMenu()
		{
			foreach(string msg in GarageMessages.s_MenuMessages)
			{
				Console.WriteLine(msg);
			}
		}
	}
}

