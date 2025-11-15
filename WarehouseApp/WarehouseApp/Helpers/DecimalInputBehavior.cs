using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace WarehouseApp.Helpers
{
    public class DecimalInputBehavior : Behavior<TextBox>
    {
        // Регулярка для нормалізованого числа (з крапкою)
        private static readonly Regex _regex = new Regex(@"^\d*\.?\d*$");

        protected override void OnAttached()
        {
            AssociatedObject.PreviewTextInput += OnPreviewTextInput;
            DataObject.AddPastingHandler(AssociatedObject, OnPaste);
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
            DataObject.RemovePastingHandler(AssociatedObject, OnPaste);
            base.OnDetaching();
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string newText = GetNewText(e.Text);
            e.Handled = !IsTextValidDecimal(newText);
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string pasteText = e.DataObject.GetData(DataFormats.Text)!.ToString();
                string newText = GetNewText(pasteText);
                if (!IsTextValidDecimal(newText))
                    e.CancelCommand();
            }
            else
                e.CancelCommand();
        }

        private string GetNewText(string input)
        {
            var tb = AssociatedObject;
            string text = tb.Text ?? "";

            if (tb.SelectionLength > 0)
                text = text.Remove(tb.SelectionStart, tb.SelectionLength);

            return text.Insert(tb.CaretIndex, input);
        }

        private bool IsTextValidDecimal(string text)
        {
            if (string.IsNullOrEmpty(text)) return true;

            // Замінюємо кому на крапку
            string normalized = text.Replace(',', '.');

            // Дозволяємо тільки одну крапку
            if (normalized.Split('.').Length > 2) return false;

            return _regex.IsMatch(normalized);
        }
    }
}
