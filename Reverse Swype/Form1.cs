using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reverse_Swype
{
    public partial class Form1 : Form
    {
        string directoryName;
        Dictionary<char, Point> keyboardDictionary = new Dictionary<char, Point>();
        int horizontalNumber=0, verticalNumber=0;
        int heightChar = 0, widthChar = 0;
        int verticalIdentation = 0, horizontalIdentation = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            //swypeImage.Image = new Bitmap(swypeImage.Width, swypeImage.Height);
            char[] word = wordBox.Text.ToUpper().ToArray<char>();
            List<Point> pointsWord = new List<Point>();
            foreach (char charWord in word)
            {
                pointsWord.Add(keyboardDictionary[charWord]);                
            }

            Point[] pointsWordArray = pointsWord.ToArray();
            foreach (Point pointArray in pointsWordArray)
            {
                Console.WriteLine(pointArray.ToString());
            }
            using (var g = Graphics.FromImage(swypeImage.Image))
            {
                g.FillEllipse(Brushes.Red, pointsWordArray[0].X-5, pointsWordArray[0].Y-5,10,10);
                g.DrawLines(Pens.Blue, pointsWordArray);
                swypeImage.Refresh();
            }
            
        }

        public Form1()
        {
            InitializeComponent();
            //swypeImage.Image = new Bitmap(swypeImage.Width,swypeImage.Height);
        }

        private void listKeyboards_SelectedIndexChanged(object sender, EventArgs e)
        {
            string layoutFileName = listKeyboards.SelectedItem.ToString();
            string fullLayoutPath = directoryName+"\\"+layoutFileName+".txt";
            swypeImage.ImageLocation = directoryName + "\\" + layoutFileName + ".png";
            string[] lines = System.IO.File.ReadAllLines(fullLayoutPath);
            foreach (string line in lines)
            {
                horizontalNumber = Math.Max(line.Length, horizontalNumber);
            }
            verticalNumber = lines.Length;
            heightChar = swypeImage.Height/verticalNumber;
            widthChar = swypeImage.Width / horizontalNumber;
            verticalIdentation = heightChar / 2;
            horizontalIdentation = widthChar / 2;
            int horizontalPosition = horizontalIdentation;
            int verticalPosition = verticalIdentation;
            foreach (string line in lines)
            {
                horizontalPosition = horizontalIdentation;
                foreach (char character in line)
                {
                    if (character == '_')
                    {
                        horizontalPosition -= horizontalIdentation;
                    }
                    else
                    {                    
                        if (character != '$')
                        {
                            keyboardDictionary[character] = new Point(horizontalPosition,verticalPosition);
                        }
                        else
                        {
                            horizontalPosition += horizontalIdentation;
                        }
                    }
                    horizontalPosition += widthChar;
                }
                verticalPosition += heightChar;
            }
            //foreach(KeyValuePair<char,Point> dictionaryEntry in keyboardDictionary)
            //{
            //    Console.WriteLine("Key = {0}, Value = {1}", dictionaryEntry.Key.ToString(), dictionaryEntry.Value.ToString());
            //}
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {           
            directoryName = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Application.ExecutablePath)))+"\\layouts";
            //Console.WriteLine(directoryName);
            string[] fileNames = System.IO.Directory.GetFiles(directoryName);
            List<string> listFileNames = new List<string>();
            foreach (string fileName in fileNames)
            {
                string fileNameClear = fileName.Replace(directoryName + "\\", "").Replace(".txt", "").Replace(".png", "");
                if (!listFileNames.Contains(fileNameClear))
                    listFileNames.Add(fileNameClear);
            }
            string[] fileNamesClear = listFileNames.ToArray();
            this.listKeyboards.Items.AddRange(fileNamesClear);
            listKeyboards.SelectedItem = 0;
        }
    }
}
