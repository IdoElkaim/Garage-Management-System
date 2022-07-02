using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.eNums;



namespace Ex03.GarageLogic
{
    //properties
    public class Truck : FuelVehicle
    {
        private bool m_isMovingToxins;
        public bool IsMovingToxins
        {
            get
            {
                return m_isMovingToxins;
            }
            set
            {
                m_isMovingToxins = value;
            }
        }

        private float m_maxWeightToCarry;
        public float MaxWeightToCarry
        {
            get
            {
                return m_maxWeightToCarry;
            }
            set
            {
                m_maxWeightToCarry = value;
            }
        }


        //methods
        //ctor
        public Truck()
            : base(16, 26)
        {
            m_isMovingToxins = false;
            m_maxWeightToCarry = 0;
            base.CurrentFuel = 0;
            base.FuelType = eFuelType.Soler;
            base.MaxFuel = 120;
        }

        public override StringBuilder GetInfo()
        {
            StringBuilder info = base.GetFuelVehicleInfo();
            info.AppendLine();
            String moreInfo = string.Format("The truck does{0}carry toxins.", m_isMovingToxins ? " ":" not ");
            info.Append(moreInfo);
            info.AppendLine();
            moreInfo = string.Format("Maximum carry capabillity of the truck is : {0}", m_maxWeightToCarry);
            info.Append(moreInfo);
            info.AppendLine();

            return info;
        }

        public override StringBuilder[] GetParametersForVehicleType(out int o_HowMuchParams)
        {
            o_HowMuchParams = 3;
            StringBuilder[] parameters = new StringBuilder[o_HowMuchParams];

            parameters[0] = new StringBuilder();
            parameters[0].Append("Please enter maximum weight carrying capability (in tons, maximum is 15): ");
            parameters[0].AppendLine();

            parameters[1] = new StringBuilder();
            parameters[1].Append("Is the truck carrying any toxins ? (1 - yes, 2 - no)");
            parameters[1].AppendLine();

            parameters[2] = new StringBuilder();
            parameters[2].Append("Enter current fuel level (in percentage): ");
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
                    ValidInput = IsInputValid(i_UserInput, 0, 15, out ParameterInput, !v_IsInteger);
                    m_maxWeightToCarry = ParameterInput;
                    break;
                case 1:
                    ValidInput = IsInputValid(i_UserInput, 1, 2, out ParameterInput, v_IsInteger);
                    m_isMovingToxins = ((int)ParameterInput == 1) ? true : false ;
                    break;
                case 2:
                    ValidInput = IsInputValid(i_UserInput, 0, 100, out ParameterInput, !v_IsInteger);
                    Energy = ParameterInput;
                    CurrentFuel = (ParameterInput / 100) * MaxFuel;
                    break;
            }
            return ValidInput;
        }
    }
}



