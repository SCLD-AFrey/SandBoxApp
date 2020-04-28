using System;
using System.Collections.Generic;
using System.Globalization;
using System.ServiceModel.Channels;
using System.Text;
using System.Windows.Data;
using SandBoxApp.Enumerations;

namespace SandBoxApp.ViewModels.Converters
{
    public class PersonTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is enumPersonType personType)
            {
                return personType switch
                {
                    enumPersonType.EM => "Employee",
                    enumPersonType.GC => "General Contractor",
                    enumPersonType.IN => "Individual",
                    enumPersonType.SC => "SC",
                    enumPersonType.SP => "SP",
                    enumPersonType.VC => "VC",
                    _ => throw new NotImplementedException(
                        "Unknown person type")
                };
            }

            throw new Exception(
                "Attempted to convert a type that was not 'enumPersonType' in PersonTypeConverter.Convert. User should never see this message.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string personTypeString)
            {
                return personTypeString switch
                {
                    "Employee" => enumPersonType.EM,
                    "General Contractor" => enumPersonType.GC,
                    "Individual" => enumPersonType.IN,
                    "SC" => enumPersonType.SC,
                    "SP" => enumPersonType.SP,
                    "VC" => enumPersonType.VC,
                    _ => throw new NotImplementedException(
                        "Unknown person type encountered in PersonTypeConverter.ConvertBack. User should never see this message.")
                };

            }
            throw new Exception(
                "Attempted to convert a type that was not 'string' in PersonTypeConverter.ConvertBack. User should never see this message.");
        }
    }
}
