using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.eNums;

namespace Ex03.GarageLogic
{
    public abstract class FuelVehicle : Vehicle
    {

        //properties
        private eFuelType m_fuelType;
        public eFuelType FuelType
        {
            get
            {
                return m_fuelType;
            }
            set
            {
                m_fuelType = value;
            }
        }

        private float m_currentFuel;
        public float CurrentFuel
        {
            get
            {
                return m_currentFuel;
            }
            set
            {
                m_currentFuel = value;
            }
        }

        private float m_maxFuel;
        public float MaxFuel
        {
            get
            {
                return m_maxFuel;
            }
            set
            {
                m_maxFuel = value;
            }
        }

        //methods
        //ctor
        public FuelVehicle(int i_NumberOfTires, int i_MaxPressure)
            : base(i_NumberOfTires, i_MaxPressure)
        {
            //No need
        }

        public void PumpFuel(float m_litersToAdd, eFuelType i_fuelType)
        {
            if (i_fuelType != FuelType)
            {
                throw new ArgumentException("Wrong fuel type for the specific vehicle.");

            }

            else if (CurrentFuel + m_litersToAdd > MaxFuel || m_litersToAdd < 0)
            {
                throw new ValueOutOfRangeException(0, MaxFuel);
            }

            else
            {
                CurrentFuel += m_litersToAdd;
            }
        }

        public StringBuilder GetFuelVehicleInfo()
        {
            StringBuilder info = base.GetVehicleInfo();
            string moreInfo = string.Format("The current fuel amount: {0} liters", m_currentFuel);
            info.Append(moreInfo);
            info.AppendLine();
            moreInfo = string.Format("The maximum fuel amount: {0} liters", m_maxFuel);
            info.Append(moreInfo);
            info.AppendLine();

            return info;
        }
    }
}
