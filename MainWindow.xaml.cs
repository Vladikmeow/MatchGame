using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MatchGame
{
    using System.Windows.Threading;
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthOfSecondsElapsed;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthOfSecondsElapsed++;
            timeTextBlock.Text = (tenthOfSecondsElapsed / 10f).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Еще раз?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()// 1❤❤2😁😁3😎😎4💋💋5💕💕6🐱‍👤🐱‍👤7😍😍8😉😉9🌹🌹10👀🎁👩‍🦰//Создает список из восьми пар эмодзи
            {
                "❤","❤",//2
                "😁","😁",//4
                "😎","😎",//6
                "💋","💋",//8
                "💕","💕",//10
                "🐱‍👤","🐱‍👤",//12
                "😍","😍",
                "😉","😉",
            };


            Random random = new Random();//Создает новый генератор случайных чисел


            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())//Находит каждый элемент TextBlock в сетке и повторяет следующие команды для каждого элемента
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility= Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);// Выбирает случайное число от 0 до коли-чества эмодзи в списке и назначает емуимя «index»
                    string nextEmoji = animalEmoji[index];//Использует случайное число с именем "index" для получения случайного эмодзи из списка
                    textBlock.Text = nextEmoji;//Обновляет TextBlock случайным эмодзи из списка
                    animalEmoji.RemoveAt(index); //Удаляет случайный эмодзи из списка
                }
            }
            timer.Start();
            tenthOfSecondsElapsed = 0; 
            matchesFound = 0;

        }


        TextBlock lastTextBlockClicked;
        bool findingMatch = false;//Этот признак определяет, щелкнул ли игрок на первом животном в паре, и теперь пытается найти для него пару.
        private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch==false)//Игрок только что щелкнул на первом животном в паре, поэтому это животное становится невидимым, а соответствующий элемент TextBlock сохраняется на случай,если его придется делать видимым снова.
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;

            }
            else if (textBlock.Text == lastTextBlockClicked.Text)//Игрок нашел пару! Второе животное в паре становится невидимым (а при щелчках на нем ничего не происходит), а признак findingMatch сбрасывается, чтобы следующее животное, на котором щелкнет игрок, снова считалось первым в паре.
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch= false;
            }
            else//Игрок щелкнул на животном, которое не совпадает с первым, поэтому первое выбранное животное снова становится видимым, а признак findingMatch сбрасывается
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch=false;
            }
        }
        private void TimeTextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (matchesFound==8)
            {
                SetUpGame();
            }

        }
    }
}
