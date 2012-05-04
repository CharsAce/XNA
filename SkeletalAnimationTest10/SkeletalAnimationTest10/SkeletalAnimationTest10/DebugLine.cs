using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletalAnimationTest10
{
    public class DebugLine
    {
        BasicEffect _basicEffect;
        VertexPositionColor[] _vertices;

        float _x;
        float _y;

        public float X
        {
            get { return _x; }
            set { _x = value; }
        }

        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public DebugLine(BasicEffect be, float x, float y, float length)
        {
            _x = x;
            _y = y;            
            _basicEffect = be;

            _vertices = new VertexPositionColor[2];

            _vertices[0].Position = new Vector3(_x, _y, 0);
            _vertices[0].Color = Color.Black;
            _vertices[1].Position = new Vector3(_x+length, _y, 0);
            _vertices[1].Color = Color.Black;

        }

        public void MoveDebugRec(Vector2 movement)
        {
            for (int i = 0; i < 2; i++)
            {
                Vector3 m = _vertices[i].Position;
                _vertices[i].Position = new Vector3(m.X + movement.X, m.Y + movement.Y, 0);
            }
        }

        //public void ChangePosition(Vector2 movement)
        //{
        //    _vertices[0].Position = new Vector3(movement.X, movement.Y, 0);
        //    _vertices[0].Color = Color.Black;
        //    _vertices[1].Position = new Vector3(movement.X + _width, movement.Y, 0);
        //    _vertices[1].Color = Color.Black;
        //    _vertices[2].Position = new Vector3(movement.X + _width, movement.Y + _height, 0);
        //    _vertices[2].Color = Color.Black;
        //    _vertices[3].Position = new Vector3(movement.X, movement.Y + _height, 0);
        //    _vertices[3].Color = Color.Black;
        //    _vertices[4].Position = new Vector3(movement.X, movement.Y, 0);
        //    _vertices[4].Color = Color.Black;
        //}

        public void DebugDraw(GraphicsDevice gd)
        {
            _basicEffect.CurrentTechnique.Passes[0].Apply();
            gd.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, _vertices, 0, 1);
        }

        //public void DebugDraw(GraphicsDevice gd, Camera2d c)
        //{
        //    _basicEffect.View = c.GetTransFormation(1);
        //    _basicEffect.CurrentTechnique.Passes[0].Apply();
        //    gd.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, _vertices, 0, 4);
        //}

    }
}
