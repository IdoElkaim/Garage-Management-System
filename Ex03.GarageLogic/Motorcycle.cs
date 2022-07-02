using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.eNums;


namespace Ex03.GarageLogic
{
    public class Motorcycle : FuelVehicle
    {
        //properties
        private  eLicenseType m_licenseType;
        public eLicenseType LicenseType
        {
            get
            {
                return m_licenseType;
            }
            set
            {
                m_licenseType = value;
            }
        }

        private  int m_engineSize;
        public int EngineSize
        {
            get
            {
                return m_engineSize;
            }
            set
            {
                m_engineSize = value;
            }
        }

        //methods
        //ctor
        public Motorcycle() : base(2, 30)
        {
            LicenseType = eLicenseType.NoLicenseType;
            m_engineSize = 0;
            base.CurrentFuel = 0;
            base.FuelType = eFuelType.Octan98;
            base.MaxFuel = 6;
        }

        public override StringBuilder GetInfo()
        {
            StringBuilder info = base.GetFuelVehicleInfo();
            string moreInfo = string.Format("Engine size is: {0} cc", m_engineSize);
            info.Append(moreInfo);
            info.AppendLine();
            moreInfo = string.Format("License type is: {0}", m_licenseType);
            info.Append(moreInfo);
            info.AppendLine();

            return info;
        }

        public override StringBuilder[] GetParametersForVehicleType(out int o_HowMuchParams)
        {
            o_HowMuchParams = 3;
            StringBuilder[] parameters = new StringBuilder[o_HowMuchParams];

            parameters[0] = new StringBuilder();
            parameters[0].Append("Please enter license type, choose from the following: ");
            parameters[0].AppendLine();
            parameters[0].Append("License levels: 1 - A, 2 - B1, 3 - AA, 4 - BB");
            parameters[0].AppendLine();

            parameters[1] = new StringBuilder();
            parameters[1].Append("What is the size of the engine in cc ? (Between 50 and 2000)");
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
                    ValidInput = IsInputValid(i_UserInput, 1, 4, out ParameterInput, v_IsInteger);
                    m_licenseType = (eLicenseType)ParameterInput;
                    break;
                case 1:
                    ValidInput = IsInputValid(i_UserInput, 50, 2000, out ParameterInput, v_IsInteger);
                    m_engineSize = (int)ParameterInput;
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
