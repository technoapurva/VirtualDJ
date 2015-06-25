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

namespace MetroWindows
{
    /// <summary>
    /// Interaction logic for BlueDisc.xaml
    /// </summary>
    public partial class BlueDisc : UserControl
    {
        // Using a DependencyProperty backing store for Angle.
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle1", typeof(double), typeof(BlueDisc), new UIPropertyMetadata(0.0));

        public double Angle1
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        public BlueDisc()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += new MouseButtonEventHandler(OnMouseLeftButtonDown);
            this.MouseUp += new MouseButtonEventHandler(OnMouseUp);
            this.MouseMove += new MouseEventHandler(OnMouseMove);
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(this);
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.Captured == this)
            {
                // Get the current mouse position relative to the volume control
                Point currentLocation = Mouse.GetPosition(this);

                // We want to rotate around the center of the knob, not the top corner
                Point knobCenter = new Point(this.ActualHeight / 2, this.ActualWidth / 2);

                // Calculate an angle
                double radians = Math.Atan((currentLocation.Y - knobCenter.Y) /
                                           (currentLocation.X - knobCenter.X));
                this.Angle1 = radians * 180 / Math.PI;

                // Apply a 180 degree shift when X is negative so that we can rotate
                // all of the way around
                if (currentLocation.X - knobCenter.X < 0)
                {
                    this.Angle1 += 180;
                }
            }
        }
    }
}
