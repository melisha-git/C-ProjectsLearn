using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextEditor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _currentFileName = "Undefined";
        private string _currentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFileButtonHandler(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Выбрать файл";
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Все файлы (*.*)|*.*|Текстовые файлы (*.txt)|*.txt";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (openFileDialog.ShowDialog() == true)
            {
                _currentFileName = openFileDialog.FileName;
                _currentPath = _currentFileName.Substring(0, _currentFileName.LastIndexOf('\\') + 1);
                _currentFileName = _currentFileName.Substring(_currentFileName.LastIndexOf('\\') + 1);
                var content = openFileDialog.OpenFile();
                byte[] line = new byte[1024];
                string allText = "";
                int bytes = 0;
                while ((bytes = content.Read(line, 0, 1024)) > 0)
                {
                    allText += Encoding.UTF8.GetString(line, 0, bytes);
                }

                MainTextBox.Text = allText;
            }
            MessageBox.Show($"Файл {_currentFileName}, путь {_currentPath}");
        }

        private void SaveFileButtonHandler(object sender, RoutedEventArgs e)
        {
            string allText = MainTextBox.Text;
            File.WriteAllText(_currentPath + _currentFileName, allText);
            MessageBox.Show($"Текст успешно сохранен");
        }

        private void KeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control) {
                SaveFileButtonHandler(sender, null);
                e.Handled = true;
            } else if (e.Key == Key.O && Keyboard.Modifiers == ModifierKeys.Control)
            {
                OpenFileButtonHandler(sender, null);
                e.Handled = true;
            }
        }
    }
}
