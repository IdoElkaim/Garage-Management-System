using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.eNums;

namespace Ex03.GarageLogic
{
    public class Tire
    {
        //properties
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

        private float m_currentTirePressure;
        public float CurrentTirePressure
        {
            get
            {
                return m_currentTirePressure;
            }
            set
            {
                m_currentTirePressure = value;
            }
        }

        private float m_maxTirePressure;
        public float MaxTirePressure
        {
            get
            {
                return m_maxTirePressure;
            }
            set
            {
                m_maxTirePressure = value;
            }
        }

        //methods
        //ctor
        public Tire(int i_MaxPressure)
        {
            m_maxTirePressure = i_MaxPressure;
        }
        public void FillAir(float i_howMuchToAdd)
        {
            if(CurrentTirePressure + i_howMuchToAdd > MaxTirePressure)
            {
                //throw exception
            }
            else
            {
                CurrentTirePressure += i_howMuchToAdd;
            }
        }

        public StringBuilder GetInfo()
        {
            StringBuilder info = new StringBuilder();
            string moreInfo = string.Format("Tires manufacturer is: {0}", m_Manufacturer);
            info.Append(moreInfo);
            info.AppendLine();
            moreInfo = string.Format("Tires maximum pressure is: {0}", m_maxTirePressure);
            info.Append(moreInfo);
            info.AppendLine();
            return info;
        }

        
    }
}
