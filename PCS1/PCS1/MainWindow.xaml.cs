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

namespace PCS1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Evaluate(object sender, RoutedEventArgs e)
        {
            DateTime? datetime = date.SelectedDate;
            if (datetime == null) {
                MessageBox.Show("Select date at first!");
                return;
            }
            DateTime birthdate = (DateTime)datetime;
            DateTime today = DateTime.Today;

            if (birthdate > today) {
                MessageBox.Show("You have not born yet!");
                return;
            }

            int age = today.Year - birthdate.Year;
            if (birthdate > today.AddYears(-age)) age--;

            if (age > 135) {
                MessageBox.Show("Bithdate is invalid!");
                return;
            }

            if (birthdate.Day == today.Day && birthdate.Month == today.Month) {
                MessageBox.Show("Happy Birthday!");
            }

            text.Text = "Age: " + age + "\nClassical zodiac: " + GetClassicalZodiac(birthdate) + "\nChinese zodiac: " + GetChineseZodiac(birthdate);
        }



        public static string GetChineseZodiac(DateTime birthdate) {
            int year = birthdate.Year;
            int val = (year - 1780) % 12;

            switch (val) {
                case 0: return "Rat";
                case 1: return "Ox";
                case 2: return "Tiger";
                case 3: return "Cat";
                case 4: return "Dragon";
                case 5: return "Snake";
                case 6: return "Horse";
                case 7: return "Goat";
                case 8: return "Monkey";
                case 9: return "Rooster";
                case 10: return "Dog";
                default: return "Boar";
            }
        }


        public static string GetClassicalZodiac(DateTime birthdate) {
            int month = birthdate.Month;
            int day = birthdate.Day;

            if (month == 1) { 
                if (day < 20) return "Capricorn"; else  return "Aquarius";
            } else if (month == 2) {
                if (day < 19) return "Aquarius"; else  return "Pisces";
            } else if (month == 3) {
                if (day < 21) return "Pisces"; else return "Aries";
            } else if (month == 4) {
                if (day < 20) return "Aries"; else return "Taurus";
            } else if (month == 5) {
                if (day < 21) return "Taurus"; else return "Gemini";
            } else if (month == 6) {
                if (day < 21) return "Gemini"; else return "Cancer";
            } else if (month == 7) {
                if (day < 23) return "Cancer"; else return "Leo";
            } else if (month == 8) {
                if (day < 23) return "Leo"; else return "Virgo";
            } else if (month == 9) {
                if (day < 23) return "Virgo"; else return "Libra";
            } else if (month == 10) {
                if (day < 23) return "Libra"; else return "Scorpio";
            } else if (month == 11) {
                if (day < 22) return "Scorpio"; else return "Sagittarius";
            } else {
                if (day < 22) return "Sagittarius"; else return "Capricorn";
            }
        }
    }


   
}
