using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
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
using System.Windows.Threading;

namespace Phone_Task;



public partial class MainWindow : Window
{
    public ObservableCollection<string> AvailableTexts { get; set; }
    public string path = "@\"..\\..\\..\\json1.json\")";
    private List<string> allwords = new List<string>();

    private string lastKey = null;
    private int keyCount = 0;
    private DispatcherTimer timer = new DispatcherTimer();
    StringBuilder text = new StringBuilder();

    //----------------------------------------




    public MainWindow()
    {
        InitializeComponent();

        if (File.Exists("C:\\Users\\akrem\\source\\repos\\Phone Task\\Phone Task\\json1.json"))
        {
            var json = File.ReadAllText("C:\\Users\\akrem\\source\\repos\\Phone Task\\Phone Task\\json1.json");
            allwords = JsonSerializer.Deserialize<List<string>>(json)!;
        }

        timer.Interval = TimeSpan.FromSeconds(5);
        timer.Tick += Timer_Tick;


    }

    private void KeyPressed(string key)
    {
        if (key == lastKey)
        {
            keyCount++;
            text.Append(textblock.Text);
            if(text.Length > 0)
            {
                text.Remove(text.Length - 1,1);
            }
        }
        else
        {
            text.Append(textblock.Text);
            lastKey = key;
            keyCount = 1;
        }

        
        if (int.TryParse(key, out int keyNumber))
        {
            string[] letters = new string[] { "", "", "" };
            switch (keyNumber)
            {
                case 1:
                    letters = new string[] { "a", "b", "c" };
                    break;
                case 2:
                    letters = new string[] { "d", "e", "f" };
                    break;
                case 3:
                    letters = new string[] { "g", "h", "j" };
                    break;
                case 4:
                    letters = new string[] { "k", "l", "m" };
                    break;
                case 5:
                    letters = new string[] { "n", "o", "p" };
                    break;
                case 6:
                    letters = new string[] { "f", "r", "s", };
                    break;
                case 7:
                    letters = new string[] { "t", "u", "v" };
                    break;
                case 8:
                    letters = new string[] { "w", "x", "y", "z" };
                    break;
                case 9:
                    letters = new string[] { "?","!","+" };
                    break;
                case 0:
                    letters = new string[] { " " };
                    break;
            }

            if (keyCount <= letters.Length)
            {
                text.Append(letters[keyCount - 1]);
                //textblock.Text = letters[keyCount - 1];
                textblock.Text=text.ToString();
                //MessageBox.Show(text.ToString());
            }
            if(keyCount >= letters.Length)
            {
                keyCount = 0;
            }
        }

        // restart timer
        text.Clear();
        timer.Stop();
        timer.Start();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        lastKey = null;
        keyCount = 0;

        timer.Stop();
    }

    private void Button1_Click(object sender, RoutedEventArgs e)
    {
        KeyPressed("1");
    }

    private void Button2_Click(object sender, RoutedEventArgs e)
    {
        KeyPressed("2");
    }

    private void Button3_Click(object sender, RoutedEventArgs e)
    {
        KeyPressed("3");
    }

    private void Button4_Click(object sender, RoutedEventArgs e)
    {
        KeyPressed("4");
    }

    private void Button5_Click(object sender, RoutedEventArgs e)
    {
        KeyPressed("5");
    }

    private void Button6_Click(object sender, RoutedEventArgs e)
    {
        KeyPressed("6");
    }

    private void Button7_Click(object sender, RoutedEventArgs e)
    {
        KeyPressed("7");
    }

    private void Button8_Click(object sender, RoutedEventArgs e)
    {
        KeyPressed("8");
    }

    private void Button9_Click(object sender, RoutedEventArgs e)
    {
        KeyPressed("9");
    }

    private void Button0_Click(object sender, RoutedEventArgs e)
    {
        KeyPressed("0");
    }

    private void Buttondies_Click(object sender, RoutedEventArgs e)
    {
        textblock.Text += "#";
    }

    private void Buttonstar_Click(object sender, RoutedEventArgs e)
    {
        textblock.Text += "*";
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        allwords.Add(textblock.Text);

        Task.Run(() =>
        {
            var jsonStr = JsonSerializer.Serialize(allwords);
            File.WriteAllText("C:\\Users\\akrem\\source\\repos\\Phone Task\\Phone Task\\json1.json", jsonStr);

            MessageBox.Show("Add process is successfully.", "", MessageBoxButton.OK, MessageBoxImage.Information);
        });
    }



    private void Textblock_TextChanged(object sender, TextChangedEventArgs e)
    {
        Task.Run(() =>
        {
            Dispatcher.Invoke(() =>
            {
                AvailableTexts.Clear();

                foreach (var word in allwords)
                    if (word.StartsWith(textblock.Text.ToLower()))
                        AvailableTexts.Add(word);
            });
        });
    }
}


