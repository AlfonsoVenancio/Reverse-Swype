﻿using System;
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

        private void swypeImage_Paint(object sender, PaintEventArgs e)
        {
            //Graphics strokeSwype = e.Graphics;
            //Pen penSwype = new Pen(Color.AliceBlue);
            //Brush brushSwype = new SolidBrush(Color.Red);
            //strokeSwype.DrawLine(penSwype, 2, 2, 400, 450);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            char[] word = wordBox.Text.ToUpper().ToArray<char>();
            List<Point> pointsWord = new List<Point>();
            foreach (char charWord in word)
            {
                pointsWord.Add(keyboardDictionary[charWord]);                
            }
            //swypeImage.Paint += new System.Windows.Forms.PaintEventHandler(this.swypeImage_Paint);
            //this.Controls.Add(swypeImage);
            Point[] pointsWordArray = pointsWord.ToArray();
            foreach (Point pointArray in pointsWordArray)
            {
                Console.WriteLine(pointArray.ToString());
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void listKeyboards_SelectedIndexChanged(object sender, EventArgs e)
        {
            string layoutFileName = listKeyboards.SelectedItem.ToString();
            string fullLayoutPath = directoryName+"\\"+layoutFileName+".txt";
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
                //Console.WriteLine(verticalPosition.ToString());
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
                listFileNames.Add(fileName.Replace(directoryName+"\\", "").Replace(".txt",""));
            }
            string[] fileNamesClear = listFileNames.ToArray();
            this.listKeyboards.Items.AddRange(fileNamesClear);
            listKeyboards.SelectedItem = 0;
        }
    }
}
