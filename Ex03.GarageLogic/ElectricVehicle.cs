using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.eNums;


namespace Ex03.GarageLogic
{
    public abstract class ElectricVehicle : Vehicle
    {
        //properties
        private float m_batteryTimeLeft;
        public float CurrentBatteryTimeLeft
        {
            get
            {
                return m_batteryTimeLeft;
            }
            set
            {
                m_batteryTimeLeft = value;
            }
        }

        private float m_maxBatteryTime;
        public float MaxBatteryTime
        {
            get
            {
                return m_maxBatteryTime;
            }
            set
            {
                m_maxBatteryTime = value;
            }
        }

        //methods
        //ctor
        public ElectricVehicle(int i_NumberOfTires, int i_MaxPressure)
            : base(i_NumberOfTires, i_MaxPressure)
        {
            //No need
        }
        public void ChargeBattery(float i_minutesToAdd)
        {
            if (CurrentBatteryTimeLeft + i_minutesToAdd/60f > MaxBatteryTime || i_minutesToAdd < 0)
            {
                throw new ValueOutOfRangeException(0, MaxBatteryTime);
            }
            else
            {
                CurrentBatteryTimeLeft += i_minutesToAdd/60f;
            }
        }

        public StringBuilder GetElectricVehicleInfo()
        {
            StringBuilder info = base.GetVehicleInfo();
            string moreInfo = string.Format("The current battery time left: {0} hours", m_batteryTimeLeft);
            info.Append(moreInfo);
            info.AppendLine();
            moreInfo = string.Format("The maximum battery time: {0} hours", m_maxBatteryTime);
            info.Append(moreInfo);
            info.AppendLine();

            return info;
        }
    }
}
