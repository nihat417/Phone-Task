﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Phone_Task;



public partial class MainWindow : Window
{
    public ObservableCollection<string> AvailableTexts { get; set; }
    public string path = @"..\..\..\json1.json";
    private List<string> allwords = new List<string>();

    private string? lastKey;
    private int keyCount = 0;
    private DispatcherTimer timer = new DispatcherTimer();
    StringBuilder text = new StringBuilder();

    //----------------------------------------



    public MainWindow()
    {
        InitializeComponent();
        AddButtonClickHandlers();

        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
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
                textblock.Text=text.ToString();
            }
            if(keyCount >= letters.Length)
            {
                keyCount = 0;
            }
        }

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


    private void AddButtonClickHandlers()
    {
        button0.Click += (sender, e) => KeyPressed("0");
        button1.Click += (sender, e) => KeyPressed("1");
        button2.Click += (sender, e) => KeyPressed("2");
        button3.Click += (sender, e) => KeyPressed("3");
        button4.Click += (sender, e) => KeyPressed("4");
        button5.Click += (sender, e) => KeyPressed("5");
        button6.Click += (sender, e) => KeyPressed("6");
        button7.Click += (sender, e) => KeyPressed("7");
        button8.Click += (sender, e) => KeyPressed("8");
        button9.Click += (sender, e) => KeyPressed("9");
    }

    private void Buttondies_Click(object sender, RoutedEventArgs e)
    {
        textblock.Text += "#";
    }

    private void Buttonstar_Click(object sender, RoutedEventArgs e)
    {
        textblock.Text += "*";
    }

    private void Button_Click1(object sender, RoutedEventArgs e)
    {
        allwords.Add(textblock.Text);

        Task.Run(() =>
        {
            var jsonStr = JsonSerializer.Serialize(allwords);
            File.WriteAllText(path, jsonStr);

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


