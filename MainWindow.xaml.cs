using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Reflection;
using System.Windows;
using Microsoft.Win32;

namespace vImgEditor
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
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            myInkCanvas.Strokes.Clear();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Strokes format|*.str";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create);
                myInkCanvas.Strokes.Save(stream);
                stream.Close();
            } else
            {
                MessageBox.Show("Ошибка при сохранении!");
            }
                
            
         
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            /*OpenFileDialog openFileDialog = new OpenFileDialog();
            FileStream stream = new FileStream(openFileDialog.FileName, FileMode.Open);
            myInkCanvas.Strokes = new System.Windows.Ink.StrokeCollection(stream);
            stream.Close();*/

            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Strokes format|*.str";
            if (openFileDialog1.ShowDialog() == true)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            myInkCanvas.Strokes = new System.Windows.Ink.StrokeCollection(myStream);

                            myStream.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка, нельзя прочитать файл с компьютера: " + ex.Message);
                }
            }
        }

        private void cboBrushSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboBrushSize.Items.Count > 0 && ((ComboBoxItem)cboBrushSize.SelectedItem).Content != null)
            {
                myInkCanvas.DefaultDrawingAttributes.Width = Convert.ToDouble(((ComboBoxItem)cboBrushSize.SelectedItem).Content);
                myInkCanvas.DefaultDrawingAttributes.Height = Convert.ToDouble(((ComboBoxItem)cboBrushSize.SelectedItem).Content);
            }
        }

        private void cboColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboColor.Items.Count > 0 && ((ComboBoxItem)cboColor.SelectedItem).Content != null)
            {         
                if (((ComboBoxItem)cboColor.SelectedItem).Content.ToString() == "Black")
                    myInkCanvas.DefaultDrawingAttributes.Color = Colors.Black;
                else if (((ComboBoxItem)cboColor.SelectedItem).Content.ToString() == "Blue")
                    myInkCanvas.DefaultDrawingAttributes.Color = Colors.Blue;
                else if (((ComboBoxItem)cboColor.SelectedItem).Content.ToString() == "Green")
                    myInkCanvas.DefaultDrawingAttributes.Color = Colors.Green;
                else if (((ComboBoxItem)cboColor.SelectedItem).Content.ToString() == "Red")
                    myInkCanvas.DefaultDrawingAttributes.Color = Colors.Red;
                else if (((ComboBoxItem)cboColor.SelectedItem).Content.ToString() == "Yellow")
                    myInkCanvas.DefaultDrawingAttributes.Color = Colors.Yellow;
            }
        }

        private void RadMode_Checked(object sender, RoutedEventArgs e)
        {
            if (radDrawingMode.IsChecked.Value == true)
                myInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            else if (radSelectMode.IsChecked.Value == true)
                myInkCanvas.EditingMode = InkCanvasEditingMode.Select;
        }

    }
}
