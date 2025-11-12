using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WarehouseApp.Helpers
{
    public static class NumericInputBehavior
    {
        public static readonly DependencyProperty AllowOnlyNumbersProperty =
            DependencyProperty.RegisterAttached(
                "AllowOnlyNumbers",
                typeof(bool),
                typeof(NumericInputBehavior),
                new PropertyMetadata(false, OnAllowOnlyNumbersChanged));

        public static bool GetAllowOnlyNumbers(DependencyObject obj) => (bool)obj.GetValue(AllowOnlyNumbersProperty);
        public static void SetAllowOnlyNumbers(DependencyObject obj, bool value) => obj.SetValue(AllowOnlyNumbersProperty, value);

        private static void OnAllowOnlyNumbersChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                if ((bool)e.NewValue)
                {
                    textBox.PreviewTextInput += TextBox_PreviewTextInput;
                    DataObject.AddPastingHandler(textBox, OnPaste);
                }
                else
                {
                    textBox.PreviewTextInput -= TextBox_PreviewTextInput;
                    DataObject.RemovePastingHandler(textBox, OnPaste);
                }
            }
        }

        private static void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[0-9]*(?:[.,][0-9]*)?$");
        }

        private static void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                var text = e.DataObject.GetData(DataFormats.Text) as string;
                if (!Regex.IsMatch(text ?? "", @"^[0-9]*(?:[.,][0-9]*)?$"))
                    e.CancelCommand();
            }
            else e.CancelCommand();
        }
    }
}
