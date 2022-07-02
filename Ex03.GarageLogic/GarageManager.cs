using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.eNums;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private List<Client> m_Clients;


        //methods
        //ctor
        public GarageManager()
        {
            m_Clients = new List<Client>();
        }


        public bool IsGarageEmpty()
        {
            return (m_Clients.Count == 0) ? true : false;
        }

        public void AddNewClientToGarage(Client i_toEnter)
        {
            m_Clients.Add(i_toEnter);
        }

        public StringBuilder GetVehiclesListByStatus(eStatus i_statusToShow)
        {
            StringBuilder vehiclesList = null;
            foreach (Client client in m_Clients)
            {
                if(client.VehicleStatus == i_statusToShow)
                {
                    vehiclesList = new StringBuilder();
                    vehiclesList.Append(client.VehicleLicensePlate);
                    vehiclesList.AppendLine();
                }
            }

            return vehiclesList;
        }

        public StringBuilder GetVehiclesList()
        {
            StringBuilder vehiclesList = new StringBuilder();
            foreach(Client client in m_Clients)
            {
                vehiclesList.Append(client.VehicleLicensePlate);
                vehiclesList.AppendLine();
            }

            return vehiclesList;
        }

        public void ChangeStatusTo(Client i_Client, eStatus i_wantedStatus)
        {
            i_Client.VehicleStatus = i_wantedStatus;
        }

        public void FillAirToVehicle(Client i_Client)
        {
            i_Client.GetVehicle.FillAirToMaximum();
        }

        public void PumpFuelToVehicle(Client i_Client, eFuelType i_fuelType, float i_howMuchToAdd)
        {
            if(i_Client.GetVehicle is FuelVehicle)
            {
                (i_Client.GetVehicle as FuelVehicle).PumpFuel(i_howMuchToAdd, (eFuelType)i_fuelType);
            }
            else
            {
                throw new ArgumentException(
                    "The vehicle plate you entered belong to an Electric Vehicle. Cannot add fuel.");
            }
        }

        public void ChargeVehicle(Client i_Client, float i_minutesToCharge)
        {
            if(i_Client.GetVehicle is ElectricCar)
            {
                (i_Client.GetVehicle as ElectricCar).ChargeBattery(i_minutesToCharge);
            }
            else
            {
                throw new ArgumentException(
                    "The vehicle plate you entered belong to a Fuel Vehicle. Cannot charge battery.");
            }
        }

        public Client? FindClientInGarage(string i_licensePlate)
        {
            foreach(Client currentClient in m_Clients)
            {
                if(currentClient.VehicleLicensePlate.Equals(i_licensePlate))
                {
                    return currentClient;
                }
            }
            return null;
        }

        public Client CreateNewClient(eVehicleType i_TypeOfVehicle, string i_LicensePlate, string i_PhoneNumber
                                      , string i_OwnerName)
        {
            Vehicle newVehicle = VehicleCreator.CreateVehicle(i_TypeOfVehicle);
            return new Client(newVehicle, i_LicensePlate, i_PhoneNumber, i_OwnerName);
        }
    }
}
