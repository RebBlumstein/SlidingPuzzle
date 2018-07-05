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

namespace SlidingTilePuzzle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private List<Button> buttonList = new List<Button>();
        private List<int> list = new List<int>();
        private int blankIndex;

        public MainWindow()
        {
            InitializeComponent();


            buttonList.Add(button00);
            buttonList.Add(button10);
            buttonList.Add(button20);
            buttonList.Add(button30);
            buttonList.Add(button40);
            buttonList.Add(button01);
            buttonList.Add(button11);
            buttonList.Add(button21);
            buttonList.Add(button31);
            buttonList.Add(button41);
            buttonList.Add(button02);
            buttonList.Add(button12);
            buttonList.Add(button22);
            buttonList.Add(button32);
            buttonList.Add(button42);
            buttonList.Add(button03);
            buttonList.Add(button13);
            buttonList.Add(button23);
            buttonList.Add(button33);
            buttonList.Add(button43);
            buttonList.Add(button04);
            buttonList.Add(button14);
            buttonList.Add(button24);
            buttonList.Add(button34);
            buttonList.Add(button44);

            textBox1.Text = "Instructions: Arrange the numbers in order starting from 1 in the top left."
                + " The first row will end on the right with 5, and the next row will begin on the left with 6 and end with 10 on the right."
                + " Continue in this way until the second-to-last item in the last row is 24, and the bottom-right square will be empty."
                + " When you accomplish this, you have won.";

            buttonReset.Content = "New Puzzle";

            ShuffleTiles();

            DisableAndEnableButtons();
            
        }

        private void ShuffleTiles()
        {
            // reset the list
            list = new List<int>();
            Random random = new Random();

            // get the list of 0-24 in a random order
            for (int i = 0; i < 25; i++)
            {
                bool done = false;
                
                // keep trying until we get a new number
                do
                {
                    int number = random.Next() % 25;

                    if (list.Contains(number))
                    {
                        // do nothing
                    }
                    else
                    {
                        // add this number and record that we're done with this iteration
                        list.Add(number);
                        done = true;
                    }
                }
                while (!done);
            }

            // fill in the buttons
            for (int i = 0; i < 25; i++)
            {
                buttonList[i].Content = list[i];

                if (list[i] == 0)
                {
                    buttonList[i].Content = "";
                    blankIndex = i;
                }
            }
        }

        private void DisableAndEnableButtons()
        {
            // default to the case where we haven't won yet
            DisableAndEnableButtons(false);
        }

        private void DisableAndEnableButtons(bool won)
        {
            // default all buttons to disabled
            for (int i = 0; i < 25; i++)
            {
                buttonList[i].IsEnabled = false;
            }

            int x = blankIndex % 5;
            int y = (int)(blankIndex / 5);

            int index;

            if (!won)
            {
                // UP
                if (y + 1 < 5)
                {
                    index = 5 * (y + 1) + (x + 0);
                    buttonList[index].IsEnabled = true;
                }

                // RIGHT
                if (x + 1 < 5)
                {
                    index = 5 * (y + 0) + (x + 1);
                    buttonList[index].IsEnabled = true;
                }

                // DOWN
                if (y - 1 > -1)
                {
                    index = 5 * (y - 1) + (x + 0);
                    buttonList[index].IsEnabled = true;
                }

                // LEFT
                if (x - 1 > -1)
                {
                    index = 5 * (y + 0) + (x - 1);
                    buttonList[index].IsEnabled = true;
                }
            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e, int x, int y)
        {
            int index = y * 5 + x;

            string content = (sender as Button).Content.ToString();

            // swap the blank square with the moved square
            buttonList[blankIndex].Content = content;
            buttonList[index].Content = "";
            blankIndex = index;

            // check if we won
            if (CheckWin())
            {
                DisableAndEnableButtons(true);

                textBox1.Text = "You win!";
            }
            else
            {
                // Reset the enabled and disabled buttons
                DisableAndEnableButtons(false);
            }
        }

        private bool CheckWin()
        {
            bool lost = false;

            // start with the 1st (instead of 0th) element
            for (int i = 1; i < 25; i++)
            {
                int previous;
                int current;

                // try to parse the previous button in the list
                bool bWorked = int.TryParse(buttonList[i - 1].Content.ToString(), out previous);
                if (!bWorked)
                {
                    // this is for sure a loss
                    lost = true;
                    continue;
                }
                
                // try to parse the current button in the list
                bWorked = int.TryParse(buttonList[i].Content.ToString(), out current);
                if (!bWorked)
                {
                    if (i == 24)
                    {
                        // the empty spot is in the right place
                        continue;
                    }
                    else
                    {
                        // this is for sure a loss
                        lost = true;
                        continue;
                    }
                }

                bool winCondition;

                if (current == previous + 1)
                {
                    winCondition = true;
                }
                else
                {
                    winCondition = false;
                }

                // we lost if we've already lost or if the current comparison failed
                lost = lost || !winCondition;
            }

            // if we didn't lose, then we won
            return !lost;
        }

        private void button00_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 0, 0);
        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 1, 0);
        }

        private void button20_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 2, 0);
        }

        private void button30_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 3, 0);
        }

        private void button40_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 4, 0);
        }

        private void button01_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 0, 1);
        }

        private void button11_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 1, 1);
        }

        private void button21_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 2, 1);
        }

        private void button31_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 3, 1);
        }

        private void button41_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 4, 1);
        }

        private void button02_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 0, 2);
        }

        private void button12_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 1, 2);
        }

        private void button22_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 2, 2);
        }

        private void button32_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 3, 2);
        }

        private void button42_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 4, 2);
        }

        private void button03_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 0, 3);
        }

        private void button13_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 1, 3);
        }

        private void button23_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 2, 3);
        }

        private void button33_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 3, 3);
        }

        private void button43_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 4, 3);
        }

        private void button04_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 0, 4);
        }

        private void button14_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 1, 4);
        }

        private void button24_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 2, 4);
        }

        private void button34_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 3, 4);
        }

        private void button44_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick(sender, e, 4, 4);
        }

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            ShuffleTiles();
            DisableAndEnableButtons();
        }
    }
}
