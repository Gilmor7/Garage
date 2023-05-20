using System;
namespace Ex03.ConsoleUI
{
	public class GarageInterface
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

		private static string[] i_Messages =
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

		private static eUserInput m_CurrInput = eUserInput.None;

		public static void RunMenu()
		{
			while(m_CurrInput != eUserInput.ExitTheSystem)
			{
				displayMenu();
				m_CurrInput = getInputFromUser();
				activateMenuOption(m_CurrInput);
			}
		}

		private static eUserInput getInputFromUser()
		{
			string userInput = Console.ReadLine();
		}

		private static void displayMenu()
		{
			foreach(string msg in i_Messages)
			{
				Console.WriteLine(msg);
			}
		}
	}
}

