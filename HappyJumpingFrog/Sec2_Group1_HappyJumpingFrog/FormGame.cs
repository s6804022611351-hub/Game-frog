using HappyJumpingFrog;
using Sec2_Group1_HappyJumpingFrog.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sec2_Group1_HappyJumpingFrog
{
    public partial class FormGame : Form
    {
        

        private PlayerFrog _frog;
        private Image _background;
        private Road _road;
        private const int GameWidth = 750;
        private const int GameHeight = 650; 



        public FormGame()
        {
            InitializeComponent();

            DoubleBuffered = true;
            ResizeRedraw = true;  // กันภาพกะพริบ
            KeyPreview = true;                   // ให้ฟอร์มรับปุ่มคีย์บอร์ด
            ClientSize = new Size(750, 650);

            _background = Properties.Resources.background;
            _frog = new PlayerFrog(Properties.Resources.frog, 350, 600);


            // เพิ่มมาใหม่
            _road = new Road(7, 3,
            new Image[] { Properties.Resources.enemy1, Properties.Resources.enemy2 },
            2f, ClientSize.Width);
            //////////////////////
           

            timerGame.Interval = 16;             // ~60 เฟรมต่อวินาที
            timerGame.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            float scale = Math.Min((float)ClientSize.Width / GameWidth,
                           (float)ClientSize.Height / GameHeight);
            float offsetX = (ClientSize.Width - GameWidth * scale) / 2;
            float offsetY = (ClientSize.Height - GameHeight * scale) / 2;


            g.Clear(Color.Black);
            g.TranslateTransform(offsetX, offsetY);
            g.ScaleTransform(scale, scale);


            e.Graphics.DrawImage(_background, 0, 0, ClientSize.Width, ClientSize.Height);
            _road.Draw(g);
            _frog.Draw(g);

            _frog.Draw(e.Graphics);
            ///////
            _road.Draw(e.Graphics);
            e.Graphics.DrawString("Lives: " + _frog.Lives, Font, Brushes.White, 10, 10);



            for (int row = 0; row < 13; row++)
            {
                g.DrawLine(Pens.White, 0, row * 50, GameWidth, row * 50);
                g.DrawString(row.ToString(), Font, Brushes.Yellow, 2, row * 50 + 2);
            }

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            _frog.Hop(e.KeyCode, GameWidth, GameHeight);
        }

        private void timerGame_Tick(object sender, EventArgs e)
        {
            Text = DateTime.Now.Millisecond.ToString();

            _frog.Update();
            _road.Update();

            Invalidate();

            foreach (EnemyMonster enemy in _road.Enemies)
            {
                if (_frog.Intersects(enemy))
                {
                    _frog.Die();       // หัวใจลด 1 ดวง และกลับจุดเริ่มต้น
                    break;
                }
            }

            if (_frog.IsGameOver)
            {
                timerGame.Stop();
                MessageBox.Show("Game Over");   // ชั่วคราว เดี๋ยวเราทำเป็นฟอร์มจริงทีหลัง
                Close();
            }

            Invalidate();
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            //อยากเย็ดหี
            //คนสวย
            // yihjghjhgjhj
            // sdfdfdfd
        }
    }
}
