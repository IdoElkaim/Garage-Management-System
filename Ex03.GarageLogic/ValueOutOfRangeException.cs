using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        //properties
        private float m_MinValue;
        

        private float m_MaxValue;
        public float MaxValue
        {
            get
            {
                return MaxValue;
            }
            set
            {
                MaxValue = value;
            }
        }

        //methods
        //ctor
        public ValueOutOfRangeException()
        {

        }

        public ValueOutOfRangeException(string msg) 
            : base(msg)
        {

        }

        public ValueOutOfRangeException(string msg, Exception innerException) 
            : base(msg, innerException)
        {

        }

        public ValueOutOfRangeException(float i_Min, float i_Max) 
            : base(string.Format("Input must be between {0} and {1}.", i_Min, i_Max))
        {
            m_MinValue = i_Min;
            m_MaxValue = i_Max;
        }
    }
}
