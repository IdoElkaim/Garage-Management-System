using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ex03.GarageLogic;
using Ex03.GarageLogic.eNums;

namespace Ex03.ConsoleUI
{
    public class ConsoleUI
    {

       
        private GarageManager m_Garage;
        private StringBuilder m_MainMenu, m_NewVehicleMenu, m_VehiclesListMenu, m_StatusMenu, m_FuelMenu;

        public ConsoleUI()
        {
            m_Garage = new GarageManager();
            BuildMainMenu();
            BuildNewVehicleMenu();
            BuildVehiclesListMenu();
            BuildStatusMenu();
            BuildFuelMenu();
        }
        
        //methods
        public void RunGarageSystem()
        {
            //need to implement exceptions managing

            bool systemRunning = true;

            while(systemRunning)
            {
                PrintMainMenu();
                try
                {
                    int userInput = GetInputForMenu(8);
                    eMainMenu choose = (eMainMenu)userInput;

                    switch(choose)
                    {
                        case eMainMenu.AddVehicle:
                            ChooseVehicleType();
                            break;
                        case eMainMenu.PrintList:
                            PrintPlates();
                            break;
                        case eMainMenu.ChangeStatus:
                            ChooseStatus();
                            break;
                        case eMainMenu.FillAir:
                            FillAirToMaximum();
                            break;
                        case eMainMenu.Fuel:
                            FillFuelToVehicle();
                            break;
                        case eMainMenu.Charge:
                            ChargeBatteryToVehicle();
                            break;
                        case eMainMenu.PrintVehicleInfo:
                            GetInfoByLicensePlate();
                            break;
                        case eMainMenu.Exit:
                            systemRunning = false;
                            break;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Thread.Sleep(3000);
                    PressAKeyToContinue();
                }

            }
        }

        private void ChooseVehicleType()
        {
            string licensePlate = GetLicensePlateNumber();
            Client? currentClient = m_Garage.FindClientInGarage(licensePlate);

            if (currentClient != null)
            {
                ChangeVehicleStatus(currentClient, GarageLogic.eNums.eStatus.OnWork);
                return;
            }
            else
            {
                PrintNewVehicleMenu();
                int vehicleTypeInput = GetInputForMenu(6);      //if exception thrown from GetInputForMenu --> Main Menu

                if(vehicleTypeInput != 6)
                {
                    AddVehicleToGarage(licensePlate, (eVehicleType)vehicleTypeInput);
                }
            }
        }

        private void BuildMainMenu()
        {
            m_MainMenu = new StringBuilder();
            m_MainMenu.Append("WELCOME TO THE GARAGE !");
            m_MainMenu.AppendLine();
            m_MainMenu.Append("Choose an action from the following options:");
            m_MainMenu.AppendLine();
            string menu = string.Format(
                @"1. Add new vehicle to garage
2. Print vehicles list
3. Change a vehicle's status
4. Fill air to a vehicle
5. Add fuel to a vehicle
6. Charge a vehicle's battery
7. Show a vehicle's info
8. Exit");
            m_MainMenu.Append(menu);
            m_MainMenu.AppendLine();

        }
        private void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine(m_MainMenu);
        }

        private void BuildNewVehicleMenu()
        {
            m_NewVehicleMenu = new StringBuilder();
            m_NewVehicleMenu.Append("Choose your vehicle type from the following: ");
            m_NewVehicleMenu.AppendLine();
            m_NewVehicleMenu.Append(VehicleCreator.ToString());
            m_NewVehicleMenu.AppendLine();
        }
        private void PrintNewVehicleMenu()
        {
            Console.Clear();
            Console.WriteLine(m_NewVehicleMenu);
        }

        private void BuildVehiclesListMenu()
        {
            m_VehiclesListMenu = new StringBuilder();
            m_VehiclesListMenu.Append("Choose status filter from the following: ");
            m_VehiclesListMenu.AppendLine();
            string menu = string.Format(
                @"1. On work
2. Work finished
3. Paid
4. No filter
5. Return");
            m_VehiclesListMenu.Append(menu);
            m_VehiclesListMenu.AppendLine();

        }
        private void PrintVehiclesListMenu()
        {
            Console.Clear();
            Console.WriteLine(m_VehiclesListMenu);
        }


        private void BuildStatusMenu()
        {
            m_StatusMenu = new StringBuilder();
            m_StatusMenu.Append("Choose a new status from the following options:");
            m_StatusMenu.AppendLine();
            string menu = string.Format(
                @"1. On work
2. Work finished
3. Paid
4. Return");
            m_StatusMenu.Append(menu);
            m_StatusMenu.AppendLine();
        }
        private void PrintStatusMenu()
        {
            Console.Clear();
            Console.WriteLine(m_StatusMenu);
        }


        private void BuildFuelMenu()
        {
            m_FuelMenu = new StringBuilder();
            m_FuelMenu.Append("Choose a fuel type from the following options:");
            m_FuelMenu.AppendLine();
            string menu = string.Format(
                @"1. Octan 95
2. Octan 96
3. Octan 98
4. Soler
5. Return");
            m_FuelMenu.Append(menu);
            m_FuelMenu.AppendLine();
        }
        private void PrintFuelMenu()
        {
            Console.Clear();
            Console.WriteLine(m_FuelMenu);
        }


        private void AddVehicleToGarage(string i_LicensePlate, eVehicleType i_TypeOfVehicle)
        {
            Client currentClient = GetNewClientInfo(i_LicensePlate, i_TypeOfVehicle);
            Vehicle tempVehicle = currentClient.GetVehicle;
            StringBuilder[] parametersMessages = tempVehicle.GetParametersForVehicleType(out int parametersNumber);
            GetVehicleInfo(tempVehicle);
            for(int i = 0; i < parametersNumber; i++)
            {
                bool isParameterValid = false;
                while(!isParameterValid)
                {
                    Console.WriteLine(parametersMessages[i]);
                    string userInput = Console.ReadLine(); 
                    try
                    {
                        isParameterValid = tempVehicle.SetParametersForVehicle(userInput, i);
                    }
                    catch (Exception ex)
                    { 
                        Console.WriteLine(ex.Message);
                        isParameterValid = false;
                    }
                }
            }

            m_Garage.AddNewClientToGarage(currentClient);
        }


        private Client GetNewClientInfo(string i_LicensePlate, eVehicleType i_TypeOfVehicle)
        {

            string phoneNumber = GetPhoneNumber();
            Console.WriteLine("Enter client's name: ");
            string ownerName = Console.ReadLine();

            return m_Garage.CreateNewClient(i_TypeOfVehicle, i_LicensePlate, phoneNumber, ownerName);
            
        }

        private void PrintPlates()
        {
            if(m_Garage.IsGarageEmpty())
            {
                Console.WriteLine("There are no vehicles in the garage right now.");
                PressAKeyToContinue();
                return;
            }
            PrintVehiclesListMenu();
            int userInput = GetInputForMenu(5);     //if exception thrown from GetInputForMenu --> Main Menu
            if(userInput == 5)
            {
                return;
            }

            if (userInput == 4)
            {
                PrintPlatesOfVehiclesInGarage();
            }
            else
            {
                PrintPlatesOfVehiclesInGarageByStatus((GarageLogic.eNums.eStatus)userInput);
            }

            PressAKeyToContinue();
        }

        private void PrintPlatesOfVehiclesInGarageByStatus(GarageLogic.eNums.eStatus i_Status)
        {
            StringBuilder vehiclesList = null;
            try
            {
                vehiclesList = m_Garage.GetVehiclesListByStatus(i_Status);
                if(vehiclesList==null)
                {
                    throw new NullReferenceException("There are no vehicle that are in this work status");
                }
                else
                {
                    Console.WriteLine(vehiclesList);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void PrintPlatesOfVehiclesInGarage()
        {
            StringBuilder vehiclesList = m_Garage.GetVehiclesList();
            Console.WriteLine(vehiclesList);
        }

        private void ChangeVehicleStatus(Client i_ClientToChange, GarageLogic.eNums.eStatus i_newStatus)
        {
            m_Garage.ChangeStatusTo(i_ClientToChange, i_newStatus);
        }

        private int GetInputForMenu(int i_Max)
        {
            int numberOfTriesLeft = 3;
            bool isNumber = false; 
            int intUserInput = 0;

            while (!isNumber)
            {
                string userInput = Console.ReadLine();
                isNumber = int.TryParse(userInput, out intUserInput);
                try
                {
                    if(!isNumber)
                    {
                        throw new FormatException("You did not enter a number. Please enter a valid input.");
                    }

                    else if(intUserInput < 1 || intUserInput > i_Max)
                    {
                        isNumber = false;
                        throw new ValueOutOfRangeException(1, i_Max);
                    }
                }
                catch(Exception ex)
                {
                    if(numberOfTriesLeft == 1)
                    {
                        Console.WriteLine(
                            "You have entered an invalid value 3 times. You are going back to the main menu.");
                        throw;
                    }
                    Console.WriteLine(ex.Message);
                    numberOfTriesLeft--;
                }
            }
            return intUserInput;
        }

        private string GetLicensePlateNumber()
        {
            Console.WriteLine("Please enter the vehicle's license plate:");
            return Console.ReadLine();
        }

        private void ChooseStatus()
        {
            string licensePlate = GetLicensePlateNumber();
            Client? currentClient = m_Garage.FindClientInGarage(licensePlate);
            if (currentClient == null)
            {
                throw new NullReferenceException("There is no vehicle with this license plate in the garage.");
            }
            PrintStatusMenu();
            int statusInput = GetInputForMenu(4);       //if exception thrown from GetInputForMenu --> Main Menu
            if (statusInput != 4)
            {
                ChangeVehicleStatus(currentClient, (eStatus)statusInput);
            }

        }

        private void FillAirToMaximum()
        {
            string licensePlate = GetLicensePlateNumber();
            Client toFill = m_Garage.FindClientInGarage(licensePlate);
            if (toFill == null)
            {
                throw new NullReferenceException("There is no vehicle with this license plate in the garage.");
            }
            m_Garage.FillAirToVehicle(toFill);
            Console.WriteLine("Air filled to maximum for vehicle: "+ licensePlate);
            PressAKeyToContinue();
        }

        private void FillFuelToVehicle()
        {
            string licensePlate = GetLicensePlateNumber();
            float littersToFill;
            PrintFuelMenu();
            int userInput = GetInputForMenu(5);     //if exception thrown from GetInputForMenu --> Main Menu

            if (userInput != 5)
            {
                string message = "Please enter amount of fuel to fill (in litters): ";
                eFuelType typeOfFuel = (eFuelType)userInput;

                littersToFill = GetFloatInput(message);     //if exception thrown from GetFloatInput --> Main Menu
                Client toFillFuelTo = m_Garage.FindClientInGarage(licensePlate);
                if (toFillFuelTo == null)
                {
                    throw new NullReferenceException("There is no vehicle with this license plate in the garage.");
                }
                m_Garage.PumpFuelToVehicle(toFillFuelTo, typeOfFuel, littersToFill);
                //if exception is thrown from PuumpFuelToVehicle --> Main Menu 
                //(Argument exception - electric car/wrong fuel type, out of range exception - too much to add)
            }
            Console.WriteLine("Requested fuel amount was filled for vehicle: " + licensePlate);
            PressAKeyToContinue();

        }

        private void ChargeBatteryToVehicle()
        {
            string licensePlate = GetLicensePlateNumber(); 
            string message = "Please enter amount of minutes to charge: ";
            float minutesToCharge = GetFloatInput(message);         //if exception thrown from GetFloatInput --> Main Menu
            Client toChargeBatteryTo = m_Garage.FindClientInGarage(licensePlate);
            if (toChargeBatteryTo == null)
            {
                throw new NullReferenceException("There is no vehicle with this license plate in the garage.");
            }
            m_Garage.ChargeVehicle(toChargeBatteryTo, minutesToCharge);
            //if exception is thrown from PuumpFuelToVehicle --> Main Menu 
            //(Argument exception - electric car/wrong fuel type, out of range exception - too much to add)

            Console.WriteLine("Requested battery amount was filled for vehicle: " + licensePlate);
            PressAKeyToContinue();
        }

        private void GetInfoByLicensePlate()
        {
            string licensePlate = GetLicensePlateNumber();
            Client toGetInfoAbout = m_Garage.FindClientInGarage(licensePlate);
            if(toGetInfoAbout == null)
            {
                throw new NullReferenceException("There is no vehicle with this license plate in the garage.");
            }
            Console.WriteLine(toGetInfoAbout.GetInfo());
            PressAKeyToContinue();
        }
        private float GetFloatInput(string i_Message)
        {
            float userInput = 0;
            int numberOfTriesLeft = 3;

            bool isParseSuccess = false;
            while(!isParseSuccess)
            {
                Console.WriteLine(i_Message);
                isParseSuccess = float.TryParse(Console.ReadLine(), out userInput);
                try
                {
                    if(!isParseSuccess)
                    {
                        throw new FormatException("You did not enter a number. Please enter a valid input.");
                    }
                }
                catch (Exception ex)
                {
                    if(numberOfTriesLeft == 1)
                    {
                        Console.WriteLine(
                            "You have entered an invalid value 3 times. You are going back to the main menu.");
                        throw;
                    }

                    Console.WriteLine(ex.Message);
                    numberOfTriesLeft--;
                }
            }

            return userInput;
        }

        private void PressAKeyToContinue()
        {
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }

        private void GetVehicleInfo(Vehicle i_Vehicle)
        {

            bool isInteger = false;
            bool isInputVal = false;
            Console.WriteLine("Please enter vehicle manufacturer name: ");
            string userInput = Console.ReadLine();
            i_Vehicle.Manufacturer = userInput;
            Console.WriteLine("Please enter tires manufacturer name: ");
            userInput = Console.ReadLine();
            float userFloatInput = 0;
            i_Vehicle.SetTireManufacturer(userInput);
            while(!isInputVal)
            {
                Console.WriteLine("Please enter current pressure on tires: ");
                userInput = Console.ReadLine();
                try
                {
                    isInputVal = i_Vehicle.IsInputValid(userInput, 0, i_Vehicle.GetMaxPressure(), out userFloatInput, isInteger);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                i_Vehicle.SetTiresPressure(userFloatInput);
            }


        }

        private String GetPhoneNumber()
        {

            const int k_MaxLength = 10;
            string number=null;
            bool phoneValid = false;
            while(!phoneValid)
            {
                Console.WriteLine("Please enter 10 digits phone number");
                number = Console.ReadLine();
                try
                {
                    if (number.Length != k_MaxLength)
                    {
                        throw new FormatException("Please enter 10 digit string");
                    }
                    else
                    {
                        for (int i = 0; i < k_MaxLength; i++)
                        {
                            if (number[i] > '9' || number[i] < '0')
                            {
                                throw new FormatException("Please enter numbers only");
                            }
                        }
                        phoneValid = true;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return number;
        }
    }
}
