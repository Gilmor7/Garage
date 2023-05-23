namespace Ex03.ConsoleUI
{
    public static class GarageMessages
    {
        public const string k_LicensePlateMsg = "Enter license plate: ";
        public const string k_ElectricCarChargeMsg = "Enter num of minutes to charge the car: ";
        public const string k_FuelTypeMsg = "Enter type of fuel: ";
        public const string k_FuelAmountMsg = "Enter amount of fuel: ";
        public const string k_VehicleTypeMsg = "Enter type of vehicle: ";
        public const string k_OwnerNameMsg = "Enter owner's name: ";
        public const string k_OwnerPhoneMsg = "Enter owner's Phone: ";
        public const string k_ExistingVehicleStatusChangeToRepair = "Updating vehicle's status to repair.";
        public const string k_StatusChangeMsg = "Enter new status for the vehicle: ";
        public const string k_FilterByStatusMsg = "Enter status to filter by: ";
        public const string k_GetAllOrFilterByStatus = "Do you want to filter by status? (true/false): ";
        public const string k_NoVehiclesFound = "No vehicles found";

        public static string[] s_MenuMessages =
        {
            "1. Add a new vehicle",
            "2. Show vehicles in the garage (all/filtered by status)",
            "3. Change a vehicle status",
            "4. Fill a vehicle's tires pressure to the max",
            "5. Fuel a vehicle",
            "6. Charge an electric vehicle",
            "7. Get full information about a vehicle",
            "8. Exit the system",
            "Pick an option: "
        };

        public static class ErrorMessages
        {
            public const string k_InvalidMenuOption = "Invalid menu option!";
            public const string k_InvalidInput = "Invalid input. Please enter a numeric value";
            public const string k_ValueOutOfRange = "Input not in range of ";
            public const string k_InvalidVehicleType = "Invalid vehicle type!";
            public const string k_InvalidStatus = "Invalid status!";
            public const string k_InvalidAmountOfMinutes = "Invalid amount of minutes!";
            public const string k_InvalidFuelType = "Invalid Fuel type!";
            public const string k_InvalidFuelAmount = "Invalid Fuel Amount!";
            public const string k_InvalidBoolInput = "Invalid input. Please enter true or false";
            
            public static string GenerateValueOutOfRangeMsg(int i_minBound, int i_maxBound)
            {
                return string.Format("{0}{1} to {2}", k_ValueOutOfRange, i_minBound, i_maxBound);
            }
        }
    }
}