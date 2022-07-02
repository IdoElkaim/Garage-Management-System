using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.eNums;

namespace Ex03.GarageLogic
{
    public class Client
    {
        //properties
        private Vehicle m_Vehicle;
        public Vehicle GetVehicle
        {
            get
            {
                return m_Vehicle;
            }
        }
        public string VehicleLicensePlate
        {
            get
            {
                return m_Vehicle.PlateNumber;
            }
        }


        private string m_ownerName;
        public string VehicleOwner
        {
            get
            {
                return m_ownerName;
            }
        }


        private string m_ownerPhone;
        public string VehicleOwnerPhone
        {
            get
            {
                return m_ownerPhone;
            }
        }


        private eStatus m_Status;
        public eStatus VehicleStatus
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

        //methods
        //ctor
        public Client(Vehicle i_Vehicle, string i_LicensePlate, string i_PhoneNumber, string i_OwnerName)
        {
            m_Vehicle = i_Vehicle;
            m_Vehicle.PlateNumber = i_LicensePlate;
            m_ownerPhone = i_PhoneNumber;
            m_ownerName = i_OwnerName;
            m_Status = eStatus.OnWork;
        }

        public StringBuilder GetInfo()
        {
            StringBuilder info = new StringBuilder();
            string moreInfo = string.Format("License plate Number is: {0}", m_Vehicle.PlateNumber);
            info.Append(moreInfo);
            info.AppendLine();
            moreInfo = string.Format("Vehicle's owner name: {0}", m_ownerName);
            info.Append(moreInfo);
            info.AppendLine();
            moreInfo = string.Format("Vehicle's status in garage: {0}", m_Status);
            info.Append(moreInfo);
            info.AppendLine();
            info.Append(m_Vehicle.GetInfo());
            return info;
        }
    }
}
