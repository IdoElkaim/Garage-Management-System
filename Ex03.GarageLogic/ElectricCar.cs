using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.eNums;

namespace Ex03.GarageLogic
{
    public class ElectricCar : ElectricVehicle
    {
        //properties
        private eColor m_Color;
        public eColor Color
        {
            get
            {
                return m_Color;
            }
            set
            {
                m_Color = value;
            }
        }


        private eDoorsNumber m_numberOfDoors;
        public eDoorsNumber NumberOfDoors
        {
            get
            {
                return m_numberOfDoors;
            }
            set
            {
                m_numberOfDoors = value;
            }
        }


        //methods
        //ctor
        public ElectricCar() : base(4, 32)
        {
            m_Color = eColor.NoColorYet;
            m_numberOfDoors = eDoorsNumber.NoDoorsYet;
            base.CurrentBatteryTimeLeft = -1;
            base.MaxBatteryTime = 3.2f;
        }

        public override StringBuilder GetInfo()
        {
            StringBuilder info = base.GetElectricVehicleInfo();
            info.AppendLine();
            String moreInfo = string.Format("There are {0} doors", m_numberOfDoors);
            info.Append(moreInfo);
            info.AppendLine();
            moreInfo = string.Format("The color of the car is {0}", m_Color);
            info.Append(moreInfo);
            info.AppendLine();

            return info;
        }

        public override StringBuilder[] GetParametersForVehicleType(out int o_HowMuchParams)
        {
            o_HowMuchParams = 3;
            StringBuilder[] parameters = new StringBuilder[o_HowMuchParams];

            parameters[0] = new StringBuilder();
            parameters[0].Append("Please enter number of doors, choose from the following: ");
            parameters[0].AppendLine();
            parameters[0].Append("Number of doors available: 2, 3, 4, or 5");
            parameters[0].AppendLine();
            
            parameters[1] = new StringBuilder();
            parameters[1].Append("What is the color of your car ? choose from the following: ");
            parameters[1].AppendLine();
            parameters[1].Append("Colors available: 1 - Red, 2 - Silver, 3 - White, 4 - Black");
            parameters[1].AppendLine();
            
            parameters[2] = new StringBuilder();
            parameters[2].Append("Enter current battery level (in percentage): ");
            parameters[2].AppendLine();

            return parameters;
        }

        public override bool SetParametersForVehicle(string i_UserInput, int i_ParamNumber)
        {
            float ParameterInput;
            bool ValidInput = false;
            bool v_IsInteger = true;
            switch (i_ParamNumber)
            {
                case 0:
                    ValidInput = IsInputValid(i_UserInput, 2, 5, out ParameterInput, v_IsInteger);
                    m_numberOfDoors = (eDoorsNumber)ParameterInput;
                    break;
                case 1:
                    ValidInput = IsInputValid(i_UserInput, 1, 4, out ParameterInput, v_IsInteger);
                    m_Color = (eColor)ParameterInput;
                    break;
                case 2:
                    ValidInput = IsInputValid(i_UserInput, 0, 100, out ParameterInput, !v_IsInteger);
                    Energy = ParameterInput;
                    CurrentBatteryTimeLeft = (ParameterInput / 100) * MaxBatteryTime;
                    break;
            }
            return ValidInput;
        }
    }
}
