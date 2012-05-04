using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SkeletalAnimationTest10
{
    public class DebugRectangle
    {
        BasicEffect _basicEffect;
        VertexPositionColor[] _vertices;
        
        int _x;
        int _y;

        int _width;
        int _height;

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public DebugRectangle(BasicEffect be, int x, int y, int width, int height)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _basicEffect = be;

            _vertices = new VertexPositionColor[5];

            _vertices[0].Position = new Vector3(_x, _y, 0);
            _vertices[0].Color = Color.Black;
            _vertices[1].Position = new Vector3(_x + _width, _y, 0);
            _vertices[1].Color = Color.Black;
            _vertices[2].Position = new Vector3(_x + _width, _y + _height, 0);
            _vertices[2].Color = Color.Black;
            _vertices[3].Position = new Vector3(_x, _y + _height, 0);
            _vertices[3].Color = Color.Black;
            _vertices[4].Position = new Vector3(_x, _y, 0);
            _vertices[4].Color = Color.Black;

        }

        public void MoveDebugRec(Vector2 movement)
        {
            for (int i = 0; i < 4; i++)
            {
                Vector3 m = _vertices[i].Position;
                _vertices[i].Position = new Vector3(m.X + movement.X, m.Y + movement.Y, 0);
            }
        }

        public void ChangePosition(Vector2 movement)
        {
            _vertices[0].Position = new Vector3(movement.X, movement.Y, 0);
            _vertices[0].Color = Color.Black;
            _vertices[1].Position = new Vector3(movement.X + _width, movement.Y, 0);
            _vertices[1].Color = Color.Black;
            _vertices[2].Position = new Vector3(movement.X + _width, movement.Y + _height, 0);
            _vertices[2].Color = Color.Black;
            _vertices[3].Position = new Vector3(movement.X, movement.Y + _height, 0);
            _vertices[3].Color = Color.Black;
            _vertices[4].Position = new Vector3(movement.X, movement.Y, 0);
            _vertices[4].Color = Color.Black;
        }

        public void DebugDraw(GraphicsDevice gd)
        {
            _basicEffect.CurrentTechnique.Passes[0].Apply();
            gd.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, _vertices, 0, 4);
        }

        //public void DebugDraw(GraphicsDevice gd, Camera2d c)
        //{
        //    _basicEffect.View = c.GetTransFormation(1);
        //    _basicEffect.CurrentTechnique.Passes[0].Apply();
        //    gd.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, _vertices, 0, 4);
        //}

    }
}
