using System;
using Ex03.GarageLogic;
using System.Collections.Generic;
using System.Text;

namespace Ex03.ConsoleUI
{
	public sealed class GarageInterface
        //TODO: 2. add error handeling in all of those options
        //TODO: 3. change ALL of the strings to const strings (now we have only half)
        //TODO: 4. maybe put messages in a different class
        //TODO: 5. add option to vehicle choosing
        //TODO: 6. add print that operation was successful
        //TODO: 7. make the class sealed
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

        private const string k_LicensePlateMsg = "Enter license plate: ";
        private const string k_ElectricCarChargeMsg = "Enter num of minutes to charge the car: ";
        private const string k_FuelTypeMsg = "Enter type of fuel: ";
        private const string k_FuelAmountMsg = "Enter amount of fuel: ";
        private const string k_VehicleTypeMsg = "Enter type of vehicle: ";
        private const string k_OwnerNameMsg = "Enter owner's name: ";
        private const string k_OwnerPhoneMsg = "Enter owner's Phone: ";
        private const string k_ExistingVehicleStatusChangeToRepair = "Updating vehicle's status to repair.";
        private const string k_StatusChangeMsg = "Enter new status for the vehicle: ";
        private const string k_FilterByStatusMsg = "Enter status to filter by: ";

        private static string[] s_Messages =
		{
			"1. Add a new vehicle",
			"2. Show all vehicles filtered by status",
			"3. Change a vehicle status",
			"4. Fill a vehicle's tires pressure to the max",
			"5. Fuel a vehicle",
			"6. Charge an electric vehicle",
			"7. Get full information about a vehicle",
			"8. Exit the system",
			"Pick an option: "
		};

        private const int k_minMenuOption = 1;
        private const int k_maxMenuOption = 8;

		private eUserInput m_CurrInput;
		private Garage m_Garage = null;

		public GarageInterface()
		{
			m_CurrInput = eUserInput.None;
			m_Garage = new Garage();
		}

		public void RunMenu()
		{
			while(m_CurrInput != eUserInput.ExitTheSystem)
			{
				displayMenu();
                try
                {
                    m_CurrInput = getInputFromUser();
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
                        Console.WriteLine("Going back to menu...");
                    }
                }
			}
            Console.WriteLine("Exiting the system...");
		}

        private eUserInput getInputFromUser()
        {
            eUserInput inputAsEnum = eUserInput.None;
            bool isValidInput = false;

            while (!isValidInput)
            {
                string userInput = Console.ReadLine();

                if (!Enum.TryParse(userInput, out inputAsEnum))
                {
                    throw new FormatException("Invalid input. Please enter a numeric value");
                }
                else if (!Enum.IsDefined(typeof(eUserInput), inputAsEnum) || inputAsEnum == eUserInput.None)
                {
                    throw new ValueOutOfRangeException(k_minMenuOption,
                        k_maxMenuOption, $"Input not in range of {k_minMenuOption} - {k_maxMenuOption}");
                }
                else
                {
                    isValidInput = true;
                }
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
                    throw new ArgumentException($"Invalid input option: {m_CurrInput}. Please select a valid option from the menu.");
            }
        }

        private void addNewVehicle()
        {
            string licensePlate = askForInputAfterMsg(k_LicensePlateMsg);
            bool isVehicleInGarage = m_Garage.IsVehicleInGarage(licensePlate);

            if (isVehicleInGarage)
            {
                switchVehicleToRepairModeAndInformCustomer(licensePlate);
            }
            else
            {
                string carTypes = constructEnumValuesMsg<VehicleFactory.eVehicleTypes>();
                Console.WriteLine(carTypes);
                string vehicleTypeStr = askForInputAfterMsg(k_VehicleTypeMsg);
                if(!Enum.TryParse(vehicleTypeStr, out VehicleFactory.eVehicleTypes vehicleType))
                {
                    throw new ArgumentException("Invalid vehicle type!");
                }

                Vehicle vehicleToInsertToGarage = VehicleFactory.CreateVehicle(vehicleType, licensePlate);
                string ownerName = askForInputAfterMsg(k_OwnerNameMsg);
                string ownerPhone = askForInputAfterMsg(k_OwnerPhoneMsg);
                updateVehicleStateBasedOnRequirements(vehicleToInsertToGarage);
                m_Garage.AddVehicle(vehicleToInsertToGarage, ownerName, ownerPhone);
                m_Garage.ChangeVehicleStatus(licensePlate, GarageVehicle.eStatus.InRepair);
                Console.WriteLine("Vehicle was added successfully!");
            }
        }

        private void switchVehicleToRepairModeAndInformCustomer(string i_LicensePlate)
        {
            m_Garage.ChangeVehicleStatus(i_LicensePlate, GarageVehicle.eStatus.InRepair);
            Console.WriteLine(k_ExistingVehicleStatusChangeToRepair);
        }

        private void updateVehicleStateBasedOnRequirements(Vehicle i_VehicleToUpdate)
        {
            Dictionary<string, string> requirments = i_VehicleToUpdate.Requirments;
            Dictionary<string, string> userValues = new Dictionary<string, string>();
            string userInput = null;
            bool isUpdatingWasOk = false;
            
            while (!isUpdatingWasOk)
            {
                userValues.Clear();
                
                foreach (KeyValuePair<string, string> requirementPair in requirments)
                {
                    Console.WriteLine(requirementPair.Value);
                    userInput = Console.ReadLine();
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
            string statusStr = askForInputAfterMsg(k_FilterByStatusMsg);
            if (!Enum.TryParse(statusStr, out GarageVehicle.eStatus status))
            {
                Console.WriteLine("Invalid status!");
                return;
            }

            List<string> filteredVehicles = m_Garage.GetLicenseNumberListFilteredByStatus(status);

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
            string licenseNumber = askForInputAfterMsg(k_LicensePlateMsg);
            Console.WriteLine(vehicleStatusesList);
            string newStatusStr = askForInputAfterMsg(k_StatusChangeMsg);

            if (!Enum.TryParse(newStatusStr, out GarageVehicle.eStatus newStatus))
            {
                throw new ArgumentException("Invalid vehicle type!");
            }

            m_Garage.ChangeVehicleStatus(licenseNumber, newStatus);
            Console.WriteLine("Vehicle status successfully changed.");
        }

        private void fillVehicleTiresToMax()
        {
            string licensePlate = askForInputAfterMsg(k_LicensePlateMsg);
            m_Garage.InflateVehicleWheelsToMax(licensePlate);
        }

        private void getFullVehicleInfo()
        {
            string licensePlate = askForInputAfterMsg(k_LicensePlateMsg);
            string vehicleInfo = m_Garage.GetVehicleInfo(licensePlate);
            Console.WriteLine(vehicleInfo);
        }

        private void chargeVehicle()
        {
            string licensePlate = askForInputAfterMsg(k_LicensePlateMsg);
            string numOfMinutesToCharge = askForInputAfterMsg(k_ElectricCarChargeMsg);
            if(!float.TryParse(numOfMinutesToCharge, out float parsedMinutesToCharge))
            {
                throw new ArgumentException("Invalid amount of minutes!");
            }
            else
            {
                m_Garage.ChargeElectricVehicle(licensePlate, parsedMinutesToCharge);
                Console.WriteLine("Vehicle successfully charged.");
            }
        }

        private void fuelVehicle()
        {
            string fuelTypesList = constructEnumValuesMsg<Fuel.eFuelType>();
            string licensePlate = askForInputAfterMsg(k_LicensePlateMsg);
            Console.WriteLine(fuelTypesList);
            string fuelTypeInput = askForInputAfterMsg(k_FuelTypeMsg);
            string fuelAmountInput = askForInputAfterMsg(k_FuelAmountMsg);

            if (!Enum.TryParse(fuelTypeInput, out Fuel.eFuelType parsedFuelType))
            {
                throw new ArgumentException("Invalid Fuel type!");
            }

            if (!float.TryParse(fuelAmountInput, out float parsedFuelAmount))
            {
                throw new ArgumentException("Invalid Fuel Amount!");
            }

            m_Garage.RefuelVehicle(licensePlate, parsedFuelType, parsedFuelAmount);
            Console.WriteLine("Vehicle successfully fueled.");
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
			foreach(string msg in s_Messages)
			{
				Console.WriteLine(msg);
			}
		}
	}
}

