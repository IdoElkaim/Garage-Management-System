using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex03.GarageLogic.eNums;


namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {

        //properties
        private List<Tire> m_Tires;
        public List<Tire> Tires
        {
            get
            {
                return m_Tires;
            }
        }
        
        private string m_Manufacturer;
        public string Manufacturer
        {
            get
            {
                return m_Manufacturer;
            }
            set
            {
                m_Manufacturer = value;
            }
        }

        private float m_energyPercentage;
        public float Energy
        {
            get
            {
                return m_energyPercentage;
            }
            set
            {
                m_energyPercentage = value;
            }
        }

        private string m_plateNumber;
        public string PlateNumber
        {
            get
            {
                return m_plateNumber;
            }
            set
            {
                m_plateNumber = value;
            }
        }


        //methods
        //ctor 
        public Vehicle(int i_NumberOfTires, int i_MaxPressure)
        {
            m_Tires = new List<Tire>();
            for(int i = 0; i < i_NumberOfTires; i++)
            {
                m_Tires.Add(new Tire(i_MaxPressure));
            }
            foreach(Tire tire in m_Tires)
            {
                tire.MaxTirePressure = i_MaxPressure;
            }
        }
        public void FillAir(float i_howMuchToFill)
        {
            foreach(Tire tire in m_Tires)
            {
                tire.FillAir(i_howMuchToFill);
            }
        }

        public void FillAirToMaximum()
        {
            foreach (Tire tire in m_Tires)
            {
                tire.FillAir(tire.MaxTirePressure - tire.CurrentTirePressure);
            }
        }

        public abstract StringBuilder GetInfo();

        public StringBuilder GetVehicleInfo()
        {
            StringBuilder info = new StringBuilder();
            string moreInfo = string.Format("Car manufacturer is: {0}", m_Manufacturer);
            info.Append(moreInfo);
            info.AppendLine();
            moreInfo = string.Format("Energy left in percentages is: {0}", m_energyPercentage);
            info.Append(moreInfo);
            info.AppendLine();

            info.Append(m_Tires.ElementAt(0).GetInfo());

            int tireCount = 1;
            foreach (Tire tire in m_Tires)
            {
                moreInfo = string.Format("Pressure on tire number {0} is: {1} psi", tireCount++,  tire.CurrentTirePressure);
                info.Append(moreInfo);
                info.AppendLine();
            }

            return info;
        }


        public abstract StringBuilder[] GetParametersForVehicleType(out int o_HowMuchParams);

        public abstract bool SetParametersForVehicle(string i_UserInput, int i_ParamNumber);

        public bool IsInputValid(string i_UserInput, float i_MinValue, float i_MaxValue,
                                 out float o_ParamValue, bool i_TypeOfParam) //TypeOfParam == true if need to integer
        {
            
            bool IsNumber;
            int ParamInteger;
            float ParamFloat;
            if(i_TypeOfParam)
            {
                IsNumber = int.TryParse(i_UserInput, out ParamInteger);
                o_ParamValue = ParamInteger;
            }
            else
            {
                IsNumber = float.TryParse(i_UserInput, out ParamFloat);
                o_ParamValue = ParamFloat;
            }
            if (!IsNumber)
            {
                throw new FormatException();
            }
            
            else if (IsOutOfRange(o_ParamValue, i_MinValue, i_MaxValue))
            {
                //if true than its out of range
                throw new ValueOutOfRangeException(i_MinValue, i_MaxValue);
            }

            return IsNumber;
        }
        public bool IsOutOfRange(float i_userInput, float i_MinValue, float i_MaxValue)
        {
            return (i_userInput > i_MaxValue || i_userInput < i_MinValue);
        }

        public float GetMaxPressure()
        {
            return m_Tires[0].MaxTirePressure;
        }

        public void SetTireManufacturer(string i_Manufacturer)
        {
            foreach(Tire tire in m_Tires)
            {
                tire.Manufacturer = i_Manufacturer;
            }
        }

        public void SetTiresPressure(float i_Pressure)
        {
            foreach(Tire tire in m_Tires)
            {
                tire.CurrentTirePressure = i_Pressure;
            }
        }
    }
}
