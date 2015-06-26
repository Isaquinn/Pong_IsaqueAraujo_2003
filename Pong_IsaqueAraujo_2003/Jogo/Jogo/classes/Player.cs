using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jogo.classes
{
    public class Player : GameObject
    {
        public int score;

        public void ScoreUp()
        { this.score++; }
        public Vector2 Direction;
       // public float velocity = 100f;
        public void Update(GameTime gameTime)
        {
           
            //Position += Position *velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
      
  
        
    }
}
