using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace towerGame2
{
    public class Player : GameCharacter
    {
        CollisionManager mycollisionManager;
        
        public Animation PlayerAnimation;

        public Vector2 playerPosition;


        int health;
        public int Health
        {
            set { health = value; }
            get { return health;}
        }

        int score; 
        public int Score
        {
            set { score = value; }
            get { return score; }
        }

        public float playerMoveSpeed;
        public Vector2 playerPrevPos;

        ePlayerState currentPlayerState
        {
            get { return CurrentPlayerState; }
        }
        public ePlayerState CurrentPlayerState;

        ePlayerState prevPlayerState;

        public void LoadContent(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("knight iso char - Copy");
            PlayerAnimation = new Animation(texture, 5, 8, true);
        }

        public void Initialise(Vector2 position, CollisionManager collisionManager)
        {
            playerPosition = position;
            Active = true;
            Health = 100;
            playerMoveSpeed = 2.0f;
            CurrentPlayerState = ePlayerState.IDLE;
            PlayerAnimation.setCurrentFrame(0, 4);
            

            mycollisionManager = collisionManager;

            boundingRect = new Rectangle((int)(this.playerPosition.X),  (int)(this.playerPosition.Y), (int)this.PlayerAnimation.Texture.Width / PlayerAnimation.Columns, (int)this.PlayerAnimation.Texture.Height / PlayerAnimation.Rows);
        }

        //sets the current min and max frames based on the current player state.
        public void setAnimation()
        {
           
                switch (currentPlayerState)
                {
                    case ePlayerState.ATTACKDOWN:
                        PlayerAnimation.setCurrentFrame(26, 29);
                        break;
                    case ePlayerState.ATTACKLEFT:
                        PlayerAnimation.setCurrentFrame(35, 38);
                        break;
                    case ePlayerState.ATTACKRIGHT:
                        PlayerAnimation.setCurrentFrame(32, 35);
                        break;
                    case ePlayerState.ATTACKUP:
                        PlayerAnimation.setCurrentFrame(29, 32);
                        break;         
                    case ePlayerState.DOWN:
                        PlayerAnimation.setCurrentFrame(4,9);
                        break;
                    case ePlayerState.LEFT:
                        PlayerAnimation.setCurrentFrame(20, 26);
                        break;
                    case ePlayerState.RIGHT:
                        PlayerAnimation.setCurrentFrame(14, 20);
                        break;
                    case ePlayerState.UP:
                        PlayerAnimation.setCurrentFrame(9, 14);
                        break;
                    default:
                        PlayerAnimation.setCurrentFrame(0, 4);
                        break;
                }
            }
        
      
        public void MoveLeft(eButtonState buttonState, Vector2 amount)
        {
            if(buttonState == eButtonState.DOWN)
            {
                //change the player state so that the left animation is used
                changePlayerState(ePlayerState.LEFT);
               
                //update the new position of the player
                playerPrevPos = playerPosition;
                playerPosition.X -= playerMoveSpeed;
            }
            else
            {
                //if the left key is released, return to an idle state.
                changePlayerState(ePlayerState.IDLE);
            }

        }

        public void MoveRight(eButtonState buttonState, Vector2 amount)
        {
            if(buttonState == eButtonState.DOWN)
            {
                //change the player state so that the right animation is used 
                changePlayerState(ePlayerState.RIGHT);

                //update the new position of the player
                playerPrevPos = playerPosition;
                playerPosition.X += playerMoveSpeed;
            }else
            {
                //when the key is released, return to idle state
                changePlayerState(ePlayerState.IDLE);
                
            }
        }

        public void AttackLeft(eButtonState buttonState, Vector2 amount)
        {
            if (buttonState == eButtonState.DOWN)
            {
                //change the player state so that the attack left animation is used
                changePlayerState(ePlayerState.ATTACKLEFT);
                
            } else
            {
                //when the attack animation has looped once return to the left state.
                changePlayerState(ePlayerState.LEFT);
            }
        }

        public void Attack(eButtonState buttonState, Vector2 amount)
        {
            //since oonly one button is used to attack, check the current state of the player
            //to determine which direction to attack in.   
            if (buttonState == eButtonState.DOWN)
            {
                
                if (currentPlayerState == ePlayerState.LEFT)
                {
                   
                    AttackLeft(buttonState, amount);
                }
                else if (currentPlayerState == ePlayerState.RIGHT)
                {
                    
                    AttackRight(buttonState, amount);
                }
                else if (currentPlayerState == ePlayerState.DOWN)
                {
                    
                    AttackDown(buttonState, amount);
                }
                else if (currentPlayerState == ePlayerState.UP)
                {
                    
                    AttackUp(buttonState, amount);
                }
            }
        }

        public void AttackRight(eButtonState buttonState, Vector2 amount)
        {
            //change the player state so that the attack right animation is used
            changePlayerState(ePlayerState.ATTACKRIGHT);
            
            if(PlayerAnimation.CurrentFrame == 34)
            {
                //when the attack animation has looped once return to the right state.
                changePlayerState(ePlayerState.RIGHT);
            }
            
        }

        public void AttackDown(eButtonState buttonState, Vector2 amount)
        {
            //change the player state so that the attack down animation is used
            changePlayerState(ePlayerState.ATTACKDOWN);
            
            if (PlayerAnimation.CurrentFrame == 29)
            {
                //when the attack animation has looped once return to the down state.
                changePlayerState(ePlayerState.DOWN);
            }
        }

        public void AttackUp(eButtonState buttonState, Vector2 amount)
        {
            //change the player state so that the attck up aniamtion is used.
            changePlayerState(ePlayerState.ATTACKUP);
            
            if (PlayerAnimation.CurrentFrame == 32)
            {
                //when the attack animation has looped once return to the up state.
                changePlayerState(ePlayerState.UP);

            }

        }

        public void MoveUp(eButtonState buttonState, Vector2 amount)
        {
            if (buttonState == eButtonState.DOWN)
            {
                //change the player state so that the up animation is used
                changePlayerState(ePlayerState.UP);
                
                playerPrevPos = playerPosition;
                playerPosition.Y -= playerMoveSpeed;
            }
            else
            {
                //when the key is released, return to idle state
                changePlayerState(ePlayerState.IDLE);
                
            }
        }

        public void MoveDown(eButtonState buttonState, Vector2 amount)
        {
            if (buttonState == eButtonState.DOWN)
            {
                //change the player state so that the down animation is used
                changePlayerState(ePlayerState.DOWN);
                
                playerPrevPos = playerPosition;
                playerPosition.Y += playerMoveSpeed;
            }
            else
            {
                //when the key is released, return to idle state
                changePlayerState(ePlayerState.IDLE);
                
            }
        }


        //changes player state
        public void changePlayerState(ePlayerState newstate){
            prevPlayerState = currentPlayerState;
            CurrentPlayerState = newstate;
        }


        //update animation, bounding rectanlfe 
        public override void Update(GameTime gameTime)
        {
            PlayerAnimation.Update(gameTime);
            setAnimation();
            boundingRect = new Rectangle((int)(playerPosition.X), (int)(playerPosition.Y), PlayerAnimation.Texture.Width / PlayerAnimation.Columns, PlayerAnimation.Texture.Height / PlayerAnimation.Rows);
            base.Update(gameTime);
            if(Health < 0)
            {
                Active = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            PlayerAnimation.Draw(spriteBatch, playerPosition);
        }

       public override void OnCollision(Collidable obj)
       {
            obj.OnCollision(this, mycollisionManager);
        }
    }
}
