﻿using System;
using Ex03.GarageLogic;
namespace Ex03.ConsoleUI
{
	public class GarageInterface
        //TODO: 1. impliment rest of the menu
        //TODO: 2. add error handeling in all of those options
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

		private eUserInput m_CurrInput;
		private Garage m_Garage = null;

		GarageInterface()
		{
			m_CurrInput = eUserInput.None;
			m_Garage = new Garage();
		}

		public void RunMenu()
		{
			while(m_CurrInput != eUserInput.ExitTheSystem)
			{
				displayMenu();
				m_CurrInput = getInputFromUser();
				activateMenuOption();
			}
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
                    Console.WriteLine("Invalid input. Please enter a numeric value.");
                }
                else if (!Enum.IsDefined(typeof(eUserInput), inputAsEnum) || inputAsEnum == eUserInput.None)
                {
                    Console.WriteLine("Invalid option. Please enter a number from 1 to 8.");
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
                    Console.WriteLine("Exiting the system...");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please select a valid option from the menu.");
                    break;
            }
        }

        private void addNewVehicle()
        {
            throw new NotImplementedException();
        }

        private void showFilteredVehicleList()
        {
            throw new NotImplementedException();
        }

        private void changeVehicleStatus()
        {
            throw new NotImplementedException();
        }

        private void fillVehicleTiresToMax()
        {
            throw new NotImplementedException();
        }

        private void getFullVehicleInfo()
        {
            string licensePlate = askForInputAfterMsg(k_LicensePlateMsg);
            m_Garage.GetVehicleInfo(licensePlate);
        }

        private void chargeVehicle()
        {
            string licensePlate = askForInputAfterMsg(k_LicensePlateMsg);
            string numOfMinutesToCharge = askForInputAfterMsg(k_ElectricCarChargeMsg);
            m_Garage.ChargeElectricVehicle(licensePlate, float.Parse(numOfMinutesToCharge));
        }

        private void fuelVehicle()
        {
            string licensePlate = askForInputAfterMsg(k_LicensePlateMsg);
            string fuelTypeInput = askForInputAfterMsg(k_FuelTypeMsg);
            string fuelAmountInput = askForInputAfterMsg(k_FuelAmountMsg);

            if (!Enum.TryParse(fuelTypeInput, out Fuel.eFuelType parsedFuelType))
            {
                Console.WriteLine("Invalid fuel type. Please enter a valid fuel type.");
                return;
            }

            if (!float.TryParse(fuelAmountInput, out float parsedFuelAmount))
            {
                Console.WriteLine("Invalid fuel amount. Please enter a valid number.");
                return;
            }

            try
            {
                m_Garage.RefuelVehicle(licensePlate, (Fuel.eFuelType)parsedFuelType, parsedFuelAmount);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("An error occurred while refueling the vehicle: " + ex.Message);
            }
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
