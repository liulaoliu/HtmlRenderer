﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace HtmlRenderer.Dom
{
    partial class CssBox
    {
        float _globalX;
        
        float _localX;
        float _localY;

        public bool IsAbsolutePosition
        {
            get
            {
                return this.Position == CssPosition.Absolute;
            }
        }
        public float GlobalX
        {
            get { return this._globalX; }
        }
        
        public float LocalX
        {
            get { return this._localX; }
        }
        public float LocalY
        {
            get { return this._localY; }
        }

        public void SetGlobalLocation(float localX, float localY, float container_globalX, float container_globalY)
        {
            this._localX = localX;
            this._localY = localY;

            this._globalX = localX + container_globalX;
            
            this._boxCompactFlags |= CssBoxFlagsConst.HAS_ASSIGNED_LOCATION;
        }

       
        public RectangleF LocalBound
        {
            get { return new RectangleF(new PointF(this.LocalX, this.LocalY), Size); }
        }
        /// <summary>
        /// Gets the width available on the box, counting padding and margin.
        /// </summary>
        public float AvailableWidth
        {
            get { return this.SizeWidth - ActualBorderLeftWidth - ActualPaddingLeft - ActualPaddingRight - ActualBorderRightWidth; }
        }

        /// <summary>
        /// Gets the right of the box. When setting, it will affect only the width of the box.
        /// </summary>
        public float GlobalActualRight
        {
            get { return GlobalX + this.SizeWidth; }
        }
        public float LocalActualRight
        {
            get { return this.LocalX + this.SizeWidth; }
        }
        public void SetGlobalActualRight(float value)
        {
            this.SetSize(value - GlobalX, this.SizeHeight);
        }
        
     
        public float LocalActualBottom
        {
            get { return this.LocalY + this.SizeHeight; }
        }


        /// <summary>
        /// Gets the left of the client rectangle (Where content starts rendering)
        /// </summary>
        public float GlobalClientLeft
        {
            get { return this.GlobalX + this.LocalClientLeft; }
        }
        public float LocalClientLeft
        {
            get { return ActualBorderLeftWidth + ActualPaddingLeft; }
        }
        /// <summary>
        /// Gets the right of the client rectangle
        /// </summary>
        public float GlobalClientRight
        {
            get { return GlobalActualRight - ActualPaddingRight - ActualBorderRightWidth; }
        }
        public float LocalClientRight
        {
            get { return this.LocalActualRight - ActualPaddingRight - ActualBorderRightWidth; }
        }


        public float LocalClientTop
        {
            get { return ActualBorderTopWidth + ActualPaddingTop; }
        }

        public float LocalClientBottom
        {
            get { return this.LocalActualBottom - ActualPaddingBottom - ActualBorderBottomWidth; }
        }


        public RectangleF LocalClientRectangle
        {
            get { return RectangleF.FromLTRB(this.LocalClientLeft, LocalClientTop, LocalClientRight, LocalClientBottom); }
        }

        public float ClientWidth
        {
            get { return this.SizeWidth - (ActualPaddingLeft + ActualBorderLeftWidth + ActualPaddingRight + ActualBorderRightWidth); }
        }
        public float ClientHeight
        {
            get { return this.SizeHeight - (ActualPaddingTop + ActualBorderTopWidth + ActualPaddingBottom + ActualBorderBottomWidth); }
        }
    }
}
