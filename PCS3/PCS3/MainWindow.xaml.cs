using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace PCS3
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

        async private void submit(object sender, RoutedEventArgs e)
        {

            string firstname = firstnameEdit.Text;
            string lastname = lastnameEdit.Text;
            string email = emailEdit.Text;
            DateTime? birthdate = birthDatePicker.SelectedDate;

            try {
                DoWork(firstname, lastname, email, birthdate);
            } catch (EmptyParametersException epe) {
                MessageBox.Show("Empty parameters!");
            } catch (InvalidBirthDateException ibde) {
                MessageBox.Show("Invalid birthdate ("+ibde.Birthdate+")!");
            } catch (TooLongAgeException tlae) {
                MessageBox.Show("Too long age ("+tlae.Age+")!");
            } catch (InvalidEmailException iee) {
                MessageBox.Show("Invalid email ("+iee.Email+")!");
            }

            
        }

        private void DoWork(string firstname, string lastname, string email, DateTime? birthdate)
        {

            if (firstname == null || firstname.Length == 0 || lastname == null || lastname.Length == 0 || email == null || email.Length == 0 || birthdate == null)
            {
                throw new EmptyParametersException();
            }

            if (birthdate.Value > DateTime.Now)
            {
                throw new InvalidBirthDateException(birthdate.Value);
            }

            if (Service.GetAge(birthdate.Value) > 135)
            {
                throw new TooLongAgeException(Service.GetAge(birthdate.Value));
            }

            if (!email.EndsWith("@mydomain.com"))
            {
                throw new InvalidEmailException(email);
            }

            Person person = new Person(firstname, lastname, email, birthdate.Value);

            resultTextBlock.Text = "Firstname: " + person.Firstname +
                "\nLastname: " + person.Lastname +
                "\nEmail: " + person.Email +
                "\nBirthdate: " + person.Birthdate +
                "\nIs adult: " + person.IsAdult +
                "\nSun sign: " + person.SunSign +
                "\nChinese sign: " + person.ChineseSign +
                "\nIs birthday now: " + person.IsBirthday;
        }
    }




    public class Person
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }

        public Person(string firstname, string lastname, string email, DateTime birthdate)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Birthdate = birthdate;
        }

        public Person(string firstname, string lastname, string email)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Birthdate = DateTime.Today;
        }

        public Person(string firstname, string lastname, DateTime birthdate)
        {
            Firstname = firstname;
            Lastname = lastname;
            Birthdate = birthdate;
        }

        public bool IsAdult
        {
            get
            {
                return Service.GetAge(Birthdate) >= 18;
            }
        }

        public string SunSign
        {
            get
            {
                return Service.GetClassicalZodiac(Birthdate);
            }
        }

        public string ChineseSign
        {
            get
            {
                return Service.GetChineseZodiac(Birthdate);
            }
        }

        public bool IsBirthday
        {
            get
            {
                return Birthdate.Day == DateTime.Today.Day && Birthdate.Month == DateTime.Today.Month;
            }
        }
    }

    public class Service
    {
        private Service() { }

        public static string GetChineseZodiac(DateTime birthdate)
        {
            int year = birthdate.Year;
            int val = (year - 1780) % 12;

            switch (val)
            {
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


        public static string GetClassicalZodiac(DateTime birthdate)
        {
            int month = birthdate.Month;
            int day = birthdate.Day;

            if (month == 1)
            {
                if (day < 20) return "Capricorn"; else return "Aquarius";
            }
            else if (month == 2)
            {
                if (day < 19) return "Aquarius"; else return "Pisces";
            }
            else if (month == 3)
            {
                if (day < 21) return "Pisces"; else return "Aries";
            }
            else if (month == 4)
            {
                if (day < 20) return "Aries"; else return "Taurus";
            }
            else if (month == 5)
            {
                if (day < 21) return "Taurus"; else return "Gemini";
            }
            else if (month == 6)
            {
                if (day < 21) return "Gemini"; else return "Cancer";
            }
            else if (month == 7)
            {
                if (day < 23) return "Cancer"; else return "Leo";
            }
            else if (month == 8)
            {
                if (day < 23) return "Leo"; else return "Virgo";
            }
            else if (month == 9)
            {
                if (day < 23) return "Virgo"; else return "Libra";
            }
            else if (month == 10)
            {
                if (day < 23) return "Libra"; else return "Scorpio";
            }
            else if (month == 11)
            {
                if (day < 22) return "Scorpio"; else return "Sagittarius";
            }
            else
            {
                if (day < 22) return "Sagittarius"; else return "Capricorn";
            }
        }

        public static int GetAge(DateTime birthdate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthdate.Year;
            if (birthdate > today.AddYears(-age)) age--;
            return age;
        }
    }
}
