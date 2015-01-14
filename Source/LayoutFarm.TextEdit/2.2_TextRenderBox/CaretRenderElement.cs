﻿// 2015,2014 ,Apache2, WinterDev
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PixelFarm.Drawing;



namespace LayoutFarm.Text
{

    class CaretRenderElement : RenderElement
    {
        //implement caret for text edit
        public CaretRenderElement(RootGraphic g, int w, int h)
            : base(g, w, h)
        {
        }
        public override void CustomDrawToThisCanvas(Canvas canvas, Rectangle updateArea)
        {
        }
        internal void DrawCaret(Canvas canvas, int x, int y)
        {
            canvas.FillRectangle(Color.Black, x, y, this.Width, this.Height);
        }
    }
}