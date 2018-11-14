using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZombieShooter
{
    public partial class EnduranceZ1 : Form
    {
        public EnduranceZ1()
        {
            InitializeComponent();
        }
        // start of variable list
        // Edit the variables to give you a different in game experience

        int roundCounter = 2;
        bool goup; //thid boolean will be used for the player to go up the screen
        bool godown; //this boolean will be used for the player to go down the screen
        bool goleft; //this bool will be used for the player to go left
        bool goright; //this will be used for the player to go right
        string facing = "up"; //this string is used to guide the bullets
        double playerHealth = 100; //this is player health
        int speed = 10; //this integer is for the speed of the player
        int ammo = 10; //this is start of game ammo
        int zombieSpeed = 3; //this is the zombie speed in game
        int score = 0; //this holds the score the player will achieved in game
        bool gameOver = false; //this bool is fales in the beginning and it will be used when the game is finished
        Random rnd = new Random(); //an instance of the random class we will use this to create a random number for this game

        // end of variable list
        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (gameOver) return; //if game over is ture then do nothing

            // if the left key is pressed then do the following
            if (e.KeyCode == Keys.Left)
            {
                goleft = true; //change go left to true
                facing = "left"; //change facing to left
                player.Image = Properties.Resources.left; //  change the player image to left
            }

            // end of left key selection

            // if the right key is pressed then do the following
            if (e.KeyCode == Keys.Right)
            {
                goright = true; //change go right to true
                facing = "right"; //change facing to right
                player.Image = Properties.Resources.right; // change the player image to right
            }

            // end of right key selection

            // if the up key is pressed then do the following
            if (e.KeyCode == Keys.Up)
            {
                facing = "up"; //change facing to up
                goup = true; //change go up to true
                player.Image = Properties.Resources.up; // change the player image to up
            }

            // end of up key selection

            // if the down key is pressed then do the following
            if (e.KeyCode == Keys.Down)
            {
                facing = "down"; // change facing to down
                godown = true; //change go down to true
                player.Image = Properties.Resources.down; // change the player image to down
            }
            // end of down key selection
        }
        private void keyisup(object sender, KeyEventArgs e)
            {
                if (gameOver) return; // if game is over then do nothing

                // below is the key up selection for left key
                if (e.KeyCode == Keys.Left)
                {
                    goleft = false; // change the go left bool to false
                }

                //below is the key up selection for the right key
                if (e.KeyCode == Keys.Right)
                {
                    goright = false; // change the go right bool to false
                }

                // below is the key up selection for up key
                if (e.KeyCode == Keys.Up)
                {
                    goup = false; //change the go up bool to false
                }

                // below is the keu up selection for the down key
                if (e.KeyCode == Keys.Down)
                {
                    godown = false; // change the go down bool to false
                }

                // below is the key up selection for the space key
                if (e.KeyCode == Keys.Space && ammo > 0)//checking if the space bare is up and ammo is more than 0
                {
                    ammo--; //reduce amo by 1 from the total number
                    Shoot(facing); //invoke the shoot function with the facing string inside
                    //facing will transfer up, down, left, right to the function and that will shoot the bullets the right direction

                    if (ammo < 1) //if ammo is less than one
                    {
                        DropAmmo(); // invoke drop ammo function
                    }

                }
            }
        private void gameengine(object sender, EventArgs e)
        {

            if (playerHealth > 1) // if player health is greater than 1
            {
                // assign the progress
                progressBar1.Value = Convert.ToInt32(playerHealth); 
            }
            else
            {
                // if player health is below 1
                player.Image = Properties.Resources.dead; // show dead player image
                timer1.Stop(); // stop the timer
                gameOver = true; // change game over to true
                this.Close();
            }

            label1.Text = "Ammo: " + ammo; //show the ammo amount on label 1
            label2.Text = "Kills: " + score; //show total kills on the score

            // if player health is less than 20
            if (playerHealth < 20)
            {
                // change progress bar color to red
                progressBar1.ForeColor = System.Drawing.Color.Red; 
            }

            if (goleft && player.Left > 0)
            {
                player.Left -= speed;
                // if moving left is true AND pacman is 1 pixel more from the left
                // then move the player to the left
            }

            if (goright && player.Left + player.Width < 930)
            {
                player.Left += speed;
                //if moving RIGHT is true and player left + player width is less than 930 pixels
                // then move the player to the right
            }

            if (goup && player.Top > 60)
            {
                player.Top -= speed;
                // if moving TOP is true AND player is 60 pixels more from the top
                // then move player up
            }

            if (godown && player.Top + player.Height < 700)
            {
                player.Top += speed;
                //if moving DOWN is ture AND player top + player height is less than 700 pixels
                // then move the player down
            }

            // run the first for each loop below
            // X is a control and we will seach for all controls in this loop

            foreach (Control x in this.Controls)
            {
                // if the X is a picture box and X has a tag AMMO

                if (x is PictureBox && x.Tag.ToString() == "ammo")
                {
                    // check is X in hitting the player picture box

                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        // once the player picks up the ammo
                        this.Controls.Remove(((PictureBox)x));

                        // remove ammo picture box completely from program
                        ((PictureBox)x).Dispose();

                        ammo += 5; // adds 5 ammo
                        
                    }
                }

                // if the bullets hits the 4 borders of the game
                // if x is a picture box and x has the tag of bullet
                if (x is PictureBox && x.Tag.ToString() == "bullet")
                {
                    //if the bullet is less than 1 pixel to the left
                    // if the bullet is more than 930 pixels to the right
                    // if the bullet is 10 pixels from the top
                    // if the bullet is 700 pixels to the bottom

                    if (((PictureBox)x).Left < 1 || ((PictureBox)x).Left > 930 || ((PictureBox)x).Top < 10
                        || ((PictureBox)x).Top > 700)
                    {
                        this.Controls.Remove(((PictureBox)x)); // remove the bullet from display
                        ((PictureBox)x).Dispose(); //dispose the bullet from the program
                    }
                }
                // below is the if statement which will be checking if the player hits a zombie
                if (x is PictureBox && x.Tag.ToString() == "zombie")
                {
                    // below is the if statement thats chekcing the bounds of the player and the zombie
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        playerHealth -= 1; //if the zombie hits the player then we decrease the health by 1
                    }

                    //move zombie towards the player pictuer box
                    if (((PictureBox)x).Left > player.Left)
                    {
                        ((PictureBox)x).Left -= zombieSpeed; // move zombie towards the left of the player
                        ((PictureBox)x).Image = Properties.Resources.zleft; // change the zombie image to left
                    }

                    if (((PictureBox)x).Top > player.Top)
                    {
                        ((PictureBox)x).Top -= zombieSpeed; // move the zombie upwards towards player
                        ((PictureBox)x).Image = Properties.Resources.zup; // Change the zombie Pic to facing up
                    }

                    if (((PictureBox)x).Left < player.Left)
                    {
                        ((PictureBox)x).Left += zombieSpeed; // move zombie towards the right of player
                        ((PictureBox)x).Image = Properties.Resources.zright; // change z image to facing right
                    }
                    if (((PictureBox)x).Top < player.Top)
                    {
                        ((PictureBox)x).Top += zombieSpeed; // move the zombie towards  the bottom of player
                        ((PictureBox)x).Image = Properties.Resources.zdown; // change the image to zombie facing down
                    }
                }

                // below is the second for loop, this is nested inside the first
                // the bullet and zombie needs to be different than each other
                // then we can use that to determine if they hit each other

                foreach (Control j in this.Controls)
                {
                    // below is the selection thats identifying the bullet and zombie
                    if ((j is PictureBox && j.Tag.ToString() == "bullet") && (x is PictureBox && x.Tag.ToString() == "zombie"))
                    {
                        // below is the if statement that s checkingif the bullet hits the zombie
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++; // increase score by 1
                            this.Controls.Remove(j); // this will remove the bullet from the screen
                            j.Dispose(); // this will dispose the bullet from the program all together
                            this.Controls.Remove(x); // This will remove the zombie from the screen
                            x.Dispose(); // this will remove the zombie from the program
                            MakeZombies(); // this function will invoke the make zombies function to add another
                        }
                    }
                }

                // ROUND COUNTER STILL SHOWING UP THE WRONG COLOR
               if ((score >= 3) && (score <=6) && (x is PictureBox && x.Tag.ToString() == "zombie"))
                    {

                    roundCounter = 2;
                    roundLabel.ForeColor = System.Drawing.Color.Yellow;
                    roundLabel.Text = "Round :" + roundCounter;
                    
                }
                // ROUND COUNTER STILL SHOWING UP THE WRONG COLOR
                else if ((score >= 6) && (score <= 11) && (x is PictureBox && x.Tag.ToString() == "zombie"))
                {
                    roundCounter = 3;
                    roundLabel.ForeColor = System.Drawing.Color.Yellow;
                    roundLabel.Text = "Round :" + roundCounter;
                    
                }
                // ROUND COUNTER STILL SHOWING UP THE WRONG COLOR
                else if ((score >= 11) && (score <= 16) && (x is PictureBox && x.Tag.ToString() == "zombie"))
                {
                    roundCounter = 4;
                    roundLabel.ForeColor = System.Drawing.Color.Yellow;
                    roundLabel.Text = "Round :" + roundCounter;

                }
                // ROUND COUNTER STILL SHOWING UP THE WRONG COLOR
                else if ((score >= 16) && (score <= 20) && (x is PictureBox && x.Tag.ToString() == "zombie"))
                {
                    roundCounter = 5;
                    roundLabel.ForeColor = System.Drawing.Color.Yellow;
                    roundLabel.Text = "Round :" + roundCounter;

                }
                // ROUND COUNTER STILL SHOWING UP THE WRONG COLOR
                else if ((score >= 20) && (x is PictureBox && x.Tag.ToString() == "zombie"))
                {

                    roundLabel.ForeColor = System.Drawing.Color.Yellow;
                    roundLabel.Text = "ENDURANCE MODE";
                    
                }

                // ENDURANCE MODE ZOMBIES are spawning on their own, in the begining, 
                // needs conditional statement to prevent this

                
            }
        }

        private void DropAmmo()
        {
            // this function will make a ammo image for this game

            PictureBox ammo = new PictureBox(); // Create a new instance of the picture box
            ammo.Image = Properties.Resources.ammo_Image; // assigns the ammo image to the picture box
            ammo.SizeMode = PictureBoxSizeMode.AutoSize; // set the size to auto size
            ammo.Left = rnd.Next(100, 890); // set spawn location to a random left
            ammo.Top = rnd.Next(50, 600); // set spawn locatioion to a random top
            ammo.Tag = "ammo"; // set the tag to ammo
            this.Controls.Add(ammo); // add the ammo picture to the screen
            ammo.BringToFront(); // bring it to front
            player.BringToFront(); // bring the player to the front
        }

        private void Shoot(string direct)
        {
            // this is the function that makes the new bullets in this game

            bullet shoot = new bullet(); // create a new instance of the bullet class
            shoot.direction = direct; // assignment the direction to the bullet
            shoot.bulletLeft = player.Left + (player.Width / 2); // place the bullet to the left half of the player
            shoot.bulletTop = player.Top + (player.Height / 2); // place the bullet on top half of the player
            shoot.mkBullet(this); // run the function mkBullet from the bullet class.
        }

        private void MakeZombies()
        {
            // when this function is called it will make zombies in the game

            PictureBox zombie = new PictureBox(); // create a new picture box called zombie
            zombie.Tag = "zombie"; // add a tag to it called zombie
            zombie.Image = Properties.Resources.zdown; // the default picture for the zombies
            zombie.Left = rnd.Next(0, 900); // generate a random number and assign it to zombie left
            zombie.Top = rnd.Next(0, 800); //   generate a random number and assign it to zombie top
            zombie.SizeMode = PictureBoxSizeMode.AutoSize; // set auto size for the new picture box
            this.Controls.Add(zombie); // add the picture box to the screen
            player.BringToFront(); // bring player to the front
        }
    }
}
