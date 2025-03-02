using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MatchGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            SetUpGame();
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()// 1❤❤2😁😁3😎😎4💋💋5💕💕6🐱‍👤🐱‍👤7😍😍8😉😉9🌹🌹10👀🎁👩‍🦰//Создает список из восьми пар эмодзи
            {
                "❤","❤",
                "😁","😁",
                "😎","😎",
                "💋","💋",
                "💕","💕",
                "🐱‍👤","🐱‍👤",
                "😍","😍",
                "😉","😉",
            };
            Random random = new Random();//Создает новый генератор случайных чисел

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())//Находит каждый элемент TextBlock в сетке и повторяет следующие команды для каждого элемента
            {
                int index = random.Next(animalEmoji.Count);// Выбирает случайное число от 0 до коли-чества эмодзи в списке и назначает емуимя «index»
                string nextEmoji = animalEmoji[index];//Использует случайное число с именем "index" для получения случайного эмодзи из списка
                textBlock.Text = nextEmoji;//Обновляет TextBlock случайным эмодзи из списка
                animalEmoji.RemoveAt(index); //Удаляет случайный эмодзи из списка
            }
        }
    }
}
