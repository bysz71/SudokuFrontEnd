using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SudokuLibrary;

namespace SudokuFrontEnd
{
    public partial class Form1 : Form
    {
        TextBox[,] _map = new TextBox[9, 9];
        Game newGame;

        //
        //_map initiation
        //
        void InitMap()
        {
            //use loop to create 9*9 TextBox matrix
            for (int row = 0; row < 9; row++ )
            {
                for (int col = 0; col < 9; col++ )
                {
                    var temp = new TextBox();
                    temp.Text = "";
                    temp.Size = new Size(24, 24);
                    temp.Location = new Point(row * 30 + 30 , col * 30 + 30);
                    _map[row, col] = temp;
                    this.Controls.Add(temp);
                }
            }
        }
        //
        //form1 initiation
        //
        public Form1()
        {
            InitMap();
            InitializeComponent();
        }
        //
        //loadGame click event handler
        //
        private void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int gameSize = 0;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                string line;
                StreamReader file = new StreamReader(fileName);
                try
                {
                    //read game size and read game template into a list
                    List<string> dataList = new List<string>();
                    line = file.ReadLine();
                    string temp = line.Substring(0, 1);
                    gameSize = int.Parse(temp);

                    while ((line = file.ReadLine()) != null)
                    {
                        dataList.Add(line);
                    }

                    //initiate the game with gameSize and template known
                    loadGameInit(gameSize, dataList);
                }
                catch (IOException)
                {
                }
                file.Close();
            }
        }
        //
        //game initiation
        //
        private void loadGameInit(int gameSize, List<string> dataList)
        {
            //tells user the game size
            labelGameSize.Text = "This is a " + gameSize + "*" + gameSize + " game.";

            newGame = new Game(gameSize, dataList);

            //initiate the TextBox array property settings
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (i < gameSize && j < gameSize)
                    {
                        _map[i, j].Visible = true;
                    }
                    else
                    {
                        _map[i, j].Visible = false;
                        _map[i, j].Enabled = false;
                    }
                    
                }
            }

            //present the game template to the System monitor label
            // and initiate the TextBox value and enability
            foreach (string item in dataList)
            {
                labelSysMonitor.Text = labelSysMonitor.Text + item + "\n";
                string[] tempArray = item.Split(' ');
                int row = int.Parse(tempArray[0]);
                int col = int.Parse(tempArray[1]);
                int val = int.Parse(tempArray[2]);
                _map[row, col].Text = val.ToString();
                _map[row, col].Enabled = false;

            }
        }

    }
}
