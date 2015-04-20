using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using PolishLibrary;

namespace PolishCalculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            textBox1.Focus();
        }

     

       


        /*bool CheckBrakes(string str)
        {
            str.Replace(" ","");

            Regex reg = new Regex(@"^((\w|\s)*(\((\w|\s)*|\))*(\w|\s)*)*$");
            bool res = false;
            res = reg.IsMatch(str);
            return res;
        }*/


        bool CheckBrakes(string str)
        {
            int balance = 0;
            foreach (char ch in str)
            { 
                if(ch ==')') balance--;
                else if (ch == '(') balance++;

                if (balance < 0) return false;

            }

            if (balance != 0) return false;
            else return true;
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool res = CheckBrakes(textBox1.Text);
            if (res)
            {
                textBox1.Foreground = Brushes.Black;
            }
            else
            {
                textBox1.Foreground = Brushes.Red;

            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Parser prs = new Parser(textBox1.Text);
                List<string> strings = prs.Parse();
                label1.Content = "";
                foreach (string stroke in strings)
                {
                    label1.Content += stroke + " ";
                }
                label2.Content = Calculator.Calculate(strings);

            }
            catch (MyParserException ex)
            {
                MessageBox.Show(ex.Message);
                label1.Content = "";
                label2.Content = "";
            }

            catch (MyCalculateException cex)
            {
                MessageBox.Show(cex.Message);
                label2.Content = "Ошибка вычисления";
            }
        }
    }
}
