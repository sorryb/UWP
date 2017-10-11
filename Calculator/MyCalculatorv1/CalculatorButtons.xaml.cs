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
using System.Windows.Shapes;

namespace MyCalculatorv1
{
    /// <summary>
    /// Interaction logic for WindowAll.xaml
    /// </summary>
    public partial class CalculatorButtons : Window
    {
        public CalculatorButtons()
        {
            InitializeComponent();
            EventManager.RegisterClassHandler(typeof(Window), Keyboard.KeyUpEvent,new KeyEventHandler(keyUp), true);
        }

        

        /// <summary>
        /// http://stackoverflow.com/questions/20428526/c-sharp-how-to-handle-xaml-keyboard-in-mvvm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void keyUp(object sender, KeyEventArgs e)
        {
              if(e.Key == Key.OemComma)
                     MessageBox.Show("YAY!!! comma");
        }

    }
}
