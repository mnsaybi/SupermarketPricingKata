using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketPricing.Common
{
    
    /// <summary>
    /// Represents the list of measurment units for products.
    /// </summary>
    public enum MeasureUnit { PIECE, LITER, MILLILITER, GALLON, POUND, OUNCE, KILOGRAM, GRAM }

    /// <summary>
    /// Helper class to convert measurement units.
    /// </summary>
    public static class UnitsConverter
    {
        /// <summary>
        /// Convert measure units beetween two compatible units
        /// </summary>
        /// <param name="fromUnit">The measure unit to be converted</param>
        /// <param name="toUnit">The target measure unit</param>
        /// <returns>The equivalent ratio between two measure units</returns>
        public static decimal GetEquivalentUnit(MeasureUnit fromUnit, MeasureUnit toUnit)
        {
            IList<MeasureUnit> massUnits = new List<MeasureUnit>() { MeasureUnit.KILOGRAM, MeasureUnit.GRAM, MeasureUnit.POUND, MeasureUnit.OUNCE };
            IList<MeasureUnit> volumeUnits = new List<MeasureUnit>() { MeasureUnit.LITER, MeasureUnit.MILLILITER, MeasureUnit.GALLON};

            if (fromUnit.Equals(toUnit))
            {
                return 1;
            }

            if (massUnits.Contains(fromUnit) && massUnits.Contains(toUnit))
            {
                // mass conversion
                return GetMassConversion(fromUnit, toUnit);
                
            }

            if (volumeUnits.Contains(fromUnit) && volumeUnits.Contains(toUnit))
            {
                // volume conversion
                return GetVolumeConversion(fromUnit, toUnit);
            }

            throw new Exception("Measurement units not compatible");

        }

        private static decimal GetMassConversion(MeasureUnit fromUnit, MeasureUnit toUnit)
        {
            decimal eq = 0;
            switch (fromUnit)
            {
                case MeasureUnit.POUND:
                    eq = GetPoundEquivalent(toUnit);
                    break;
                case MeasureUnit.OUNCE:
                    eq = GetOunceEquivalent(toUnit);
                    break;
                case MeasureUnit.KILOGRAM:
                    eq = GetKgEquivalent(toUnit);
                    break;
                case MeasureUnit.GRAM:
                    eq = GetGramEquivalent(toUnit);
                    break;
                default:
                    break;
            }

            return eq;
        }

        private static decimal GetVolumeConversion(MeasureUnit fromUnit, MeasureUnit toUnit)
        {
            decimal eq = 0;
            switch (fromUnit)
            {
                case MeasureUnit.LITER:
                    eq = GetLiterEquivalent(toUnit);
                    break;
                case MeasureUnit.MILLILITER:
                    eq = GetMilliliterEquivalent(toUnit);
                    break;
                case MeasureUnit.GALLON:
                    eq = GetGallonEquivalent(toUnit);
                    break;
                default:
                    break;
            }

            return eq;
        }

        private static decimal GetPoundEquivalent(MeasureUnit toUnit)
        {
            decimal eq = 0;

            switch (toUnit)
            {               
                case MeasureUnit.POUND:
                    eq = 1m;
                    break;
                case MeasureUnit.OUNCE:
                    eq = 16m;
                    break;
                case MeasureUnit.KILOGRAM:
                    eq = 0.453592m;
                    break;
                case MeasureUnit.GRAM:
                    eq = 453.592m;
                    break;
                default:
                    break;
            }

            return eq;
        }

        private static decimal GetOunceEquivalent(MeasureUnit toUnit)
        {
            decimal eq = 0;

            switch (toUnit)
            {
                case MeasureUnit.POUND:
                    eq = 0.0625m;
                    break;
                case MeasureUnit.OUNCE:
                    eq = 1;
                    break;
                case MeasureUnit.KILOGRAM:
                    eq = 0.0283495m;
                    break;
                case MeasureUnit.GRAM:
                    eq = 28.3495m;
                    break;
                default:
                    break;
            }

            return eq;
        }

        private static decimal GetKgEquivalent(MeasureUnit toUnit)
        {
            decimal eq = 0;

            switch (toUnit)
            {
                case MeasureUnit.POUND:
                    eq = 2.20462m;
                    break;
                case MeasureUnit.OUNCE:
                    eq = 35.274m;
                    break;
                case MeasureUnit.KILOGRAM:
                    eq = 1;
                    break;
                case MeasureUnit.GRAM:
                    eq = 1000;
                    break;
                default:
                    break;
            }

            return eq;
        }

        private static decimal GetGramEquivalent(MeasureUnit toUnit)
        {
            decimal eq = 0;

            switch (toUnit)
            {
                case MeasureUnit.POUND:
                    eq = 0.00220462m;
                    break;
                case MeasureUnit.OUNCE:
                    eq = 0.035274m;
                    break;
                case MeasureUnit.KILOGRAM:
                    eq = 0.001m;
                    break;
                case MeasureUnit.GRAM:
                    eq = 1;
                    break;
                default:
                    break;
            }

            return eq;
        }

        private static decimal GetLiterEquivalent(MeasureUnit toUnit)
        {
            decimal eq = 0;

            switch (toUnit)
            {
                case MeasureUnit.LITER:
                    eq = 1;
                    break;
                case MeasureUnit.MILLILITER:
                    eq = 1000;
                    break;
                case MeasureUnit.GALLON:
                    eq = 0.264172m;
                    break;                
                default:
                    break;
            }

            return eq;
        }

        private static decimal GetMilliliterEquivalent(MeasureUnit toUnit)
        {
            decimal eq = 0;

            switch (toUnit)
            {
                case MeasureUnit.LITER:
                    eq = 0.001m;
                    break;
                case MeasureUnit.MILLILITER:
                    eq = 1;
                    break;
                case MeasureUnit.GALLON:
                    eq = 0.453592m;
                    break;
                default:
                    break;
            }

            return eq;
        }

        private static decimal GetGallonEquivalent(MeasureUnit toUnit)
        {
            decimal eq = 0;

            switch (toUnit)
            {
                case MeasureUnit.LITER:
                    eq = 3.78541m;
                    break;
                case MeasureUnit.MILLILITER:
                    eq = 3785.41m;
                    break;
                case MeasureUnit.GALLON:
                    eq = 1;
                    break;
                default:
                    break;
            }

            return eq;
        }


    }
}
