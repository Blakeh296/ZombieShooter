using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ZombieShooter
{
    class bullet
    {
        // start the variable
        public string direction; // creating a public string called direction
        public int speed = 20; // creating a integer called speed and assigning a value of 20
        PictureBox Bullet = new PictureBox(); // create a picture box
        Timer tm = new Timer(); // create a new timer called tm.

        public int bulletLeft; // create a new public integer
        public int bulletTop; // create a new public integer
        // end of variables

        public void mkBullet(Form form)
        {
            // this function will add the bullet to the game play
            // it is required to be called from the main class

            Bullet.BackColor = System.Drawing.Color.White; // set the color white for the bullet
            Bullet.Size = new Size(5, 5);
            Bullet.Tag = "bullet"; // set the tag to bullet
            Bullet.Left = bulletLeft; // set bullet left
            Bullet.Top = bulletTop; // set the bullet right
            Bullet.BringToFront(); // bring the bullet to front of other objects
            form.Controls.Add(Bullet); // add the bullet to the screen

            tm.Interval = speed; // set the timer interval to speed
            tm.Tick += new EventHandler(tm_tick); // assignment the timer 
            tm.Start(); //
        }

        public void tm_tick (object sender, EventArgs e)
        {
            // if direction equals to left
            if (direction == "left")
            {
                Bullet.Left -= speed; // move bullet6 towards the left of screen
            }

            // if direction equals right
            if (direction == "right")
            {
                Bullet.Left += speed; // move the bullet towards the right of the screen
            }

            // if direction is up
            if (direction == "up")
            {
                Bullet.Top -= speed; // move the bullet towards the top of screen
            }

            // if direction is down
            if (direction == "down")
            {
                Bullet.Top += speed; // move bullet towards the bottom of the screen
            }

            // if the bullet is less than 16 pixel to the left OR
            // if the bullet is more than 860 pixels to the right OR
            // if the bullet is 10 pixels from the top OR
            // if the bullet is 616 pixels to the bottom OR
            // IF ANY ONE OF THE CONDITIONS ARE MET THEN THE FOLLOWING CODE WILL BE EXECUTED

            if (Bullet.Left < 16 || Bullet.Left > 860 || Bullet.Top < 10 || Bullet.Top > 616)
            {
                tm.Stop(); // stop the timer
                tm.Dispose(); // get rid of the timer from the program
                Bullet.Dispose(); //dispose the bullet
                tm = null; // nullify the timer object
                Bullet = null; // nullify the bullet object
            }
        }
    }

    
}
