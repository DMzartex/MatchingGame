using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Windows.Threading;

namespace MatchingGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int timeEslaped = 0;
        int countPairFind = 0;
        TextBlock derniereTBClique; // on va l’utiliser pour faire une référence à  la TextBlock sur laquelle on vient de cliquer
        bool trouvePaire = false;
        public MainWindow()
        {  
            InitializeComponent();
            SetUpGame();
            timer.Tick += new EventHandler(Timer_Tick);
        }

        public void SetUpGame()
        {
            int index;
            string nextEmoji;
            Random nbAlea = new Random();
           
            List<String> animalEmoji = new List<String>()
            {
                "🐈","🐈",
                "🦍","🦍",
                "🦁","🦁",
                "🐕","🐕",
                "🐎","🐎",
                "🐖","🐖",
                "🐄","🐄",
                "🦒","🦒",
            };

            foreach (TextBlock textBlock in grdMain.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "txtTime")
                {

                    index = nbAlea.Next(animalEmoji.Count); // index est de type int
                                                            // nbalea est un objet de type Random()
                    nextEmoji = animalEmoji[index]; // nextEmoji est de type string
                    textBlock.Text = nextEmoji;
                    textBlock.Visibility = Visibility.Visible;
                    animalEmoji.RemoveAt(index); // on retire un animal de la liste pour ne pas l’attribuer à nouveau.
                }


            }

            timer.Interval = TimeSpan.FromSeconds(.1);
            timeEslaped = 0;
            countPairFind = 0;
            timer.Start();
        }

        
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlockActif = sender as TextBlock;
            if (!trouvePaire)
            {
                textBlockActif.Visibility = Visibility.Hidden;
                derniereTBClique = textBlockActif;
                trouvePaire = true;
                countPairFind = countPairFind + 1;
            }
            else if (textBlockActif.Text == derniereTBClique.Text)
            {
                textBlockActif.Visibility = Visibility.Hidden;
                trouvePaire = false;
                
            }
            else
            {
                derniereTBClique.Visibility = Visibility.Visible;
                trouvePaire = false;
            }
        }

        private void txtTime_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (countPairFind == 8)
            {
                countPairFind = 0;
                SetUpGame();
                
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeEslaped++;
            txtTime.Text = (timeEslaped / 10F).ToString("0.0s");
            if (countPairFind == 8)
            {
                timer.Stop();
                txtTime.Text = txtTime.Text + " - Rejouer ? ";
            }
        }

    }
}
