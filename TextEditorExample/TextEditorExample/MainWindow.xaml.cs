using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;

namespace TextEditorExample
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmbFontSize.ItemsSource = new List<double> {8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72};
        }

        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontFamily.SelectedItem != null)
                MyTextBox.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, cmbFontFamily.SelectedItem);
        }

        private void cmbFontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            MyTextBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, cmbFontSize.Text);
        }

        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MyTextBox.Document.Blocks.Clear();
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                var fileStream = new FileStream(dlg.FileName, FileMode.Open);
                var range = new TextRange(MyTextBox.Document.ContentStart, MyTextBox.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                var fileStream = new FileStream(dlg.FileName, FileMode.Create);
                var range = new TextRange(MyTextBox.Document.ContentStart, MyTextBox.Document.ContentEnd);
                range.Save(fileStream, DataFormats.Rtf);
            }
        }

        private void MyTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var temp = MyTextBox.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
            cmbFontFamily.SelectedItem = temp;

            temp = MyTextBox.Selection.GetPropertyValue(TextElement.FontSizeProperty);
            if (temp is double)
                cmbFontSize.Text = temp.ToString();

            temp = MyTextBox.Selection.GetPropertyValue(TextElement.FontWeightProperty);
            btnBold.IsChecked = temp != DependencyProperty.UnsetValue && temp.Equals(FontWeights.Bold);

            temp = MyTextBox.Selection.GetPropertyValue(TextElement.FontStyleProperty);
            btnItalic.IsChecked = temp != DependencyProperty.UnsetValue && temp.Equals(FontStyles.Italic);

            var info = string.Format($"{MyTextBox.Selection.Text}");
            StatusBlock.Text = info;
        }
    }
}