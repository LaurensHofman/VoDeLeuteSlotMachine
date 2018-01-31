using SlotMachineLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VoDeLeuteSlotMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SlotMachine : INotifyPropertyChanged
    {
        int _credits = 0;
        int _input = 87;
        bool horizontal1Selected = false;
        bool horizontal2Selected = false;
        bool horizontal3Selected = false;
        bool diagonalAimDownSelected = false;
        bool diagonalAimUpSelected = false;

        Column column1;
        Column column2;
        Column column3;

        string[] column1AfterPlay;
        string[] column2AfterPlay;
        string[] column3AfterPlay;

        Random random = new Random();

        public int Credits
        {
            get
            {
                return _credits;
            }
            
            set
            {
                _credits = value;
                NotifyPropertyChanged("Credits");
            }
        }

        public SlotMachine()
        {
            InitializeComponent();
            this.DataContext = this;

            column1 = BL_SlotMachine.GetColumn1();
            column2 = BL_SlotMachine.GetColumn2();
            column3 = BL_SlotMachine.GetColumn3();

            SetMultiplierLabels();
        }

        private void SetMultiplierLabels()
        {
            lbl7.Content = BL_SlotMachine.SetLabel(lbl7.Name);
            lblBAR.Content = BL_SlotMachine.SetLabel(lblBAR.Name);
            lblBerry.Content = BL_SlotMachine.SetLabel(lblBerry.Name);
            lblCherry.Content = BL_SlotMachine.SetLabel(lblCherry.Name);
            lblMelon.Content = BL_SlotMachine.SetLabel(lblMelon.Name);
            lblClock.Content = BL_SlotMachine.SetLabel(lblClock.Name);
        }

        private void btnBuyCredits_Click(object sender, RoutedEventArgs e)
        {
            Credits += 1000;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void btnStartMachine_Click(object sender, RoutedEventArgs e)
        {
            if (GetMultiplierForInput() == 0)
            {
                prtxtWhatsHappening.Text = "Select at least one row or diagonal to play.\r\n \r\n" + prtxtWhatsHappening.Text;
            }
            else
            {
                _input = int.Parse(tabInputCredits.SelectedValue.ToString());
                int _insertedCredits = GetMultiplierForInput() * _input;

                if (Credits >= _insertedCredits)
                {
                    Credits -= _insertedCredits;

                    prtxtWhatsHappening.Text = $"You inserted {_insertedCredits} credits.\r\n \r\n" + prtxtWhatsHappening.Text;

                    ClearColumnImages();

                    #region Disable Buttons
                    btnStartMachine.IsEnabled = false;
                    btnDiagonalArrowAimDown.IsEnabled = false;
                    btnDiagonalArrowAimUp.IsEnabled = false;
                    btnHorizontalArrow1.IsEnabled = false;
                    btnHorizontalArrow2.IsEnabled = false;
                    btnHorizontalArrow3.IsEnabled = false;
                    #endregion

                    SetImagesColumn1();
                    SetImagesColumn2();
                    SetImagesColumn3();
                }
                else
                {
                    prtxtWhatsHappening.Text = "You don't have enough credits. \r\n \r\n" + prtxtWhatsHappening.Text;
                }                
            }
        }

        private int GetMultiplierForInput()
        {
            int multiplier = 0;

            if (horizontal1Selected)
                multiplier += 1;

            if (horizontal2Selected)
                multiplier += 1;

            if (horizontal3Selected)
                multiplier += 1;

            if (diagonalAimUpSelected)
                multiplier += 1;

            if (diagonalAimDownSelected)
                multiplier += 1;

            return multiplier;
        }

        private void ClearColumnImages()
        {
            slot11.Source = null;
            slot21.Source = null;
            slot31.Source = null;
            slot12.Source = null;
            slot22.Source = null;
            slot32.Source = null;
            slot13.Source = null;
            slot23.Source = null;
            slot33.Source = null;
        }

        private async void SetImagesColumn1()
        {
            column1AfterPlay = column1.GetPictures(random.Next(0, column1.ColumnArray.Length - 1));

            await Task.Delay(800);

            slot11.Source = (ImageSource)Resources[column1AfterPlay[0]];
            slot21.Source = (ImageSource)Resources[column1AfterPlay[1]];
            slot31.Source = (ImageSource)Resources[column1AfterPlay[2]];
        }

        private async void SetImagesColumn2()
        {
            column2AfterPlay = column2.GetPictures(random.Next(0, column2.ColumnArray.Length - 1));

            await Task.Delay(1600);

            slot12.Source = (ImageSource)Resources[column2AfterPlay[0]];
            slot22.Source = (ImageSource)Resources[column2AfterPlay[1]];
            slot32.Source = (ImageSource)Resources[column2AfterPlay[2]];
        }

        private async void SetImagesColumn3()
        {
            column3AfterPlay = column3.GetPictures(random.Next(0, column3.ColumnArray.Length - 1));

            await Task.Delay(2400);

            slot13.Source = (ImageSource)Resources[column3AfterPlay[0]];
            slot23.Source = (ImageSource)Resources[column3AfterPlay[1]];
            slot33.Source = (ImageSource)Resources[column3AfterPlay[2]];

            ValidateWinnings();

            #region Enable Buttons
            btnStartMachine.IsEnabled = true;
            btnDiagonalArrowAimDown.IsEnabled = true;
            btnDiagonalArrowAimUp.IsEnabled = true;
            btnHorizontalArrow1.IsEnabled = true;
            btnHorizontalArrow2.IsEnabled = true;
            btnHorizontalArrow3.IsEnabled = true;
            #endregion
        }

        private void ValidateWinnings()
        {
            bool nothingWon = true;

            if (horizontal1Selected)
            {
                if (column1AfterPlay[0] == column2AfterPlay[0] && column1AfterPlay[0] == column3AfterPlay[0])
                {
                    int winnings = BL_SlotMachine.GetRewardMultiplier(column1AfterPlay[0]) * _input;
                    prtxtWhatsHappening.Text = $"Woohoo! Horizontal triple {column1AfterPlay[0]}. You won {winnings} credits! \r\n" + prtxtWhatsHappening.Text;
                    Credits += winnings;
                    nothingWon = false;
                } 
            }
            if (horizontal2Selected)
            {
                if (column1AfterPlay[1] == column2AfterPlay[1] && column1AfterPlay[1] == column3AfterPlay[1])
                {
                    int winnings = BL_SlotMachine.GetRewardMultiplier(column1AfterPlay[1]) * _input;
                    prtxtWhatsHappening.Text = $"Woohoo! Horizontal triple {column1AfterPlay[1]}. You won {winnings} credits! \r\n" + prtxtWhatsHappening.Text;
                    Credits += winnings;
                    nothingWon = false;
                }
            }
            if (horizontal3Selected)
            {
                if (column1AfterPlay[2] == column2AfterPlay[2] && column1AfterPlay[2] == column3AfterPlay[2])
                {
                    int winnings = BL_SlotMachine.GetRewardMultiplier(column1AfterPlay[2]) * _input;
                    prtxtWhatsHappening.Text = $"Woohoo! Horizontal triple {column1AfterPlay[2]}. You won {winnings} credits! \r\n" + prtxtWhatsHappening.Text;
                    Credits += winnings;
                    nothingWon = false;
                }
            }
            if (diagonalAimDownSelected)
            {
                if (column1AfterPlay[0] == column2AfterPlay[1] && column1AfterPlay[0] == column3AfterPlay[2])
                {
                    int winnings = BL_SlotMachine.GetRewardMultiplier(column1AfterPlay[0]) * _input;
                    prtxtWhatsHappening.Text = $"Woohoo! Diagonal triple {column1AfterPlay[0]}. You won {winnings} credits! \r\n" + prtxtWhatsHappening.Text;
                    Credits += winnings;
                    nothingWon = false;
                }
            }
            if (diagonalAimUpSelected)
            {
                if (column1AfterPlay[2] == column2AfterPlay[1] && column1AfterPlay[2] == column3AfterPlay[0])
                {
                    int winnings = BL_SlotMachine.GetRewardMultiplier(column1AfterPlay[2]) * _input;
                    prtxtWhatsHappening.Text = $"Woohoo! Diagonal triple {column1AfterPlay[2]}. You won {winnings} credits! \r\n" + prtxtWhatsHappening.Text;
                    Credits += winnings;
                    nothingWon = false;
                }
            }

            if (nothingWon)
            {
                prtxtWhatsHappening.Text = " :-( Better luck next time. \r\n" + prtxtWhatsHappening.Text;
            }
        }
        
        #region ArrowButtons
        private void btnDiagonalArrowAimDown_Click(object sender, RoutedEventArgs e)
        {
            if (!diagonalAimDownSelected)
            {
                diagonalAimDownSelected = true;
                btnDiagonalArrowAimDown.Opacity = 1;
                btnDiagonalArrowAimDown.BorderThickness = new Thickness(2);
            }
            else
            {
                diagonalAimDownSelected = false;
                btnDiagonalArrowAimDown.Opacity = 0.3;
                btnDiagonalArrowAimDown.BorderThickness = new Thickness(1);
            }
        }

        private void btnHorizontalArrow1_Click(object sender, RoutedEventArgs e)
        {
            if (!horizontal1Selected)
            {
                horizontal1Selected = true;
                btnHorizontalArrow1.Opacity = 1;
                btnHorizontalArrow1.BorderThickness = new Thickness(2);
            }
            else
            {
                horizontal1Selected = false;
                btnHorizontalArrow1.Opacity = 0.3;
                btnHorizontalArrow1.BorderThickness = new Thickness(1);
            }
        }

        private void btnHorizontalArrow2_Click(object sender, RoutedEventArgs e)
        {
            if (!horizontal2Selected)
            {
                horizontal2Selected = true;
                btnHorizontalArrow2.Opacity = 1;
                btnHorizontalArrow2.BorderThickness = new Thickness(2);
            }
            else
            {
                horizontal2Selected = false;
                btnHorizontalArrow2.Opacity = 0.3;
                btnHorizontalArrow2.BorderThickness = new Thickness(1);
            }
        }

        private void btnHorizontalArrow3_Click(object sender, RoutedEventArgs e)
        {
            if (!horizontal3Selected)
            {
                horizontal3Selected = true;
                btnHorizontalArrow3.Opacity = 1;
                btnHorizontalArrow3.BorderThickness = new Thickness(2);
            }
            else
            {
                horizontal3Selected = false;
                btnHorizontalArrow3.Opacity = 0.3;
                btnHorizontalArrow3.BorderThickness = new Thickness(1);
            }
        }

        private void btnDiagonalArrowAimUp_Click(object sender, RoutedEventArgs e)
        {
            if (!diagonalAimUpSelected)
            {
                diagonalAimUpSelected = true;
                btnDiagonalArrowAimUp.Opacity = 1;
                btnDiagonalArrowAimUp.BorderThickness = new Thickness(2);
            }
            else
            {
                diagonalAimUpSelected = false;
                btnDiagonalArrowAimUp.Opacity = 0.3;
                btnDiagonalArrowAimUp.BorderThickness = new Thickness(1);
            }
        }
        #endregion
        
    }
}
