using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.eNums;

namespace Ex03.GarageLogic
{
    public enum eVehicleType
    {
        Car = 1,
        ElectricCar,
        Motorcycle,
        ElectricMotorcycle,
        Truck,
        Return //Only for the menu options
        
    }


    public class VehicleCreator
    {
        public new static string ToString() {
            return string.Format(
                @"1. Car
2. Electric Car 
3. Motorcycle
4. Electric Motorcycle
5. Truck
6. Return
");

        }
        public static Vehicle CreateVehicle(eVehicleType i_VehicleType)
        {
            switch(i_VehicleType)
            {
                case eVehicleType.Car:
                    return new Car();
                case eVehicleType.ElectricCar:
                    return new ElectricCar();
                case eVehicleType.Motorcycle:
                    return new Motorcycle();
                case eVehicleType.ElectricMotorcycle:
                    return new ElectricMotorcycle();
                case eVehicleType.Truck:
                    return new Truck();
                default:
                    return null;
            }

        }
    }
}
