using System;
using System.Collections.Generic;
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
using Microsoft.Win32;
using LibMas;
using Lib_12;

namespace pr_3_teselko_01._01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[,] _matrix;
        private Modules matrixModules = new Modules();
        private LogicModule matrixLogic = new LogicModule();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = null;
            _matrix = null;
            min.Clear();
            max.Clear();
            matrixN.Clear();
            matrixM.Clear();
            outMaxValues.Clear();
            outMin.Clear();
        }

        private void saveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                DefaultExt = ".txt",
                Filter = "Все файлы (*.*)|*.*|Текстовые файлы|*.txt",
                Title = "Сохранение таблицы"
        };
            if (dataGrid1.ItemsSource != null)
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    matrixModules.Save(saveFileDialog.FileName);
                }
            }
            else MessageBox.Show("Нечего сохранять", "Ошибка");
        }

        private void openFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".txt",
                Filter = "Все файлы (*.*)|*.*|Текстовые файлы|*.txt",
                Title = "Открытие таблицы"
        };

            if (openFileDialog.ShowDialog() == false)
            {
                return;
            }

            _matrix = matrixModules.Open(openFileDialog.FileName);
            matrixN.Text = Convert.ToString(_matrix.GetLength(0));
            matrixM.Text = Convert.ToString(_matrix.GetLength(1));
            dataGrid1.ItemsSource = _matrix.ToDataTable().DefaultView;
        }

        private void calculate_Click(object sender, RoutedEventArgs e)
        {
            if (_matrix != null)
            {
                var GetMin = matrixLogic.MinAmongMax(_matrix);
                int[] arrayMaxValues = GetMin.maxValues;
                int min = GetMin.min;
                outMaxValues.Text = string.Join(" ", arrayMaxValues);
                outMin.Text = min.ToString();
            }
            else MessageBox.Show("Пустая таблицы", "Ошибка");
        }

        private void fill_Click(object sender, RoutedEventArgs e)
        {
            if(_matrix != null)
            {
                bool isMinEmpty = Int32.TryParse(min.Text, out int minRange);
                bool isMaxEmpty = Int32.TryParse(max.Text, out int maxRange);
                if (isMinEmpty && isMaxEmpty && minRange < maxRange)
                {
                    _matrix = matrixModules.Generate(_matrix.GetLength(0), _matrix.GetLength(1), minRange, maxRange);
                    dataGrid1.ItemsSource = VisualMatrix.ToDataTable(_matrix).DefaultView;
                }
                else MessageBox.Show("Введите правильный диапазон", "Ошибка");
            }
            else MessageBox.Show("Сначала создайте таблицу", "Ошибка");
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            bool IsNEmpty = Int32.TryParse(matrixN.Text, out int rangeN);
            bool IsMEmpty = Int32.TryParse(matrixM.Text, out int rangeM);
            if (IsNEmpty && IsMEmpty)
            {
                _matrix = matrixModules.Generate(rangeN, rangeM);
                dataGrid1.ItemsSource = VisualMatrix.ToDataTable(_matrix).DefaultView;
            }
            else MessageBox.Show("Введите размеры", "Ошибка");
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Теселько Максим ИСП - 34\n" +
                "Практическая работа №3\n" +
                "Дана матрица размера M × N. Найти минимальный среди максимальных элементов ее столбцов.", "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
