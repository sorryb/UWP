
using System;
using Windows.UI.Xaml.Data;

namespace StepsCounter
{
    /// <summary>
    /// Returns the half value of the step graph
    /// </summary>
    public class Half : IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        public object Convert( object value, Type targetType, object parameter, string language )
        {
            return (double)value / 2;
        }

        /// <summary>
        /// Converts back to the initial value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        public object ConvertBack( object value, Type targetType, object parameter, string language )
        {
            return "";
        }
    }

    /// <summary>
    /// Returns the margin o the step graph.
    /// </summary>
    public class Margin : IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        public object Convert( object value, Type targetType, object parameter, string language )
        {
            return (double)value - 6;
        }

        /// <summary>
        /// Converts back to the initial value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        public object ConvertBack( object value, Type targetType, object parameter, string language )
        {
            return "";
        }
    }
}
