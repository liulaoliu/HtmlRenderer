﻿// "Therefore those skilled at the unorthodox
// are infinite as heaven and earth,
// inexhaustible as the great rivers.
// When they come to an end,
// they begin again,
// like the days and months;
// they die and are reborn,
// like the four seasons."
// 
// - Sun Tsu,
// "The Art of War"

using System.Drawing;
using HtmlRenderer.Entities;
using HtmlRenderer.Handlers;
using HtmlRenderer.Parse;
using HtmlRenderer.Utils;

namespace HtmlRenderer.Dom
{
    /// <summary>
    /// CSS box for hr element.
    /// </summary>
    sealed class CssBoxHr : CssBox
    {
        /// <summary>
        /// Init.
        /// </summary>
        /// <param name="parent">the parent box of this box</param>
        /// <param name="tag">the html tag data of this box</param>
        public CssBoxHr(CssBox parent, IHtmlElement tag)
            : base(parent, tag)
        {
            //Display = CssConstants.Block;
            this.CssDisplay = CssDisplay.Block;
        }

        /// <summary>
        /// Measures the bounds of box and children, recursively.<br/>
        /// Performs layout of the DOM structure creating lines by set bounds restrictions.
        /// </summary>
        /// <param name="g">Device context to use</param>
        protected override void PerformContentLayout(LayoutVisitor lay)
        {

            if (this.CssDisplay == CssDisplay.None)
            {
                return;
            }

            var prevSibling = lay.LatestSiblingBox;
            var myContainingBlock = lay.LatestContainingBlock;

            //float globalLeft = myContainingBlock.GlobalX + myContainingBlock.LocalClientLeft + ActualMarginLeft;
            //float globalTop = 0;
            //if (prevSibling == null)
            //{
            //    if (this.ParentBox != null)
            //    {
            //        globalTop = this.ParentBox.GlobalClientTop;
            //    }
            //}
            //else
            //{
            //    if (this.ParentBox == null)
            //    {

            //        globalTop = this.GlobalY;
            //    }
            //    globalTop += prevSibling.GlobalActualBottom + prevSibling.ActualBorderBottomWidth;
            //}

            //// fix for hr tag 
            //var maringTopCollapse = MarginTopCollapse(prevSibling);
            float localLeft = myContainingBlock.ClientLeft + this.ActualMarginLeft;
            float localTop = 0;


            if (prevSibling == null)
            {
                if (this.ParentBox != null)
                {
                    localTop = myContainingBlock.ClientTop;
                }
            }
            else
            {
                localTop = prevSibling.LocalBottom + prevSibling.ActualBorderBottomWidth;
            }

            float maringTopCollapse = MarginTopCollapse(prevSibling);

            if (maringTopCollapse < 0.1)
            {
                maringTopCollapse = this.GetEmHeight() * 1.1f;
            }
            localTop += maringTopCollapse;


            this.SetLocation(localLeft, localTop);

            this.SetHeightToZero();

            //width at 100% (or auto)
            float minwidth = CalculateMinimumWidth();

            float width = myContainingBlock.SizeWidth
                          - myContainingBlock.ActualPaddingLeft - myContainingBlock.ActualPaddingRight
                          - myContainingBlock.ActualBorderLeftWidth - myContainingBlock.ActualBorderRightWidth
                          - ActualMarginLeft - ActualMarginRight - ActualBorderLeftWidth - ActualBorderRightWidth;


            //Check width if not auto
            if (!this.Width.IsAuto && !this.Width.IsEmpty)
            {
                width = CssValueParser.ParseLength(Width, width, this);
            }


            if (width < minwidth || width >= 9999)
            {
                width = minwidth;
            }

            float height = ExpectedHeight;
            if (height < 1)
            {
                height = this.SizeHeight + ActualBorderTopWidth + ActualBorderBottomWidth;
            }
            if (height < 1)
            {
                height = 2;
            }
            if (height <= 2 && ActualBorderTopWidth < 1 && ActualBorderBottomWidth < 1)
            {
                BorderTopStyle = BorderBottomStyle = CssBorderStyle.Solid; //CssConstants.Solid;
                BorderTopWidth = CssLength.MakePixelLength(1); //"1px";
                BorderBottomWidth = CssLength.MakePixelLength(1);
            }

            this.SetSize(width, height);
            this.SetHeight(ActualPaddingTop + ActualPaddingBottom + height);
        }

        /// <summary>
        /// Paints the fragment
        /// </summary>
        /// <param name="g">the device to draw to</param>
        protected override void PaintImp(IGraphics g, PaintVisitor p)
        {

            var rect = new RectangleF(0, 0, this.SizeWidth, this.SizeHeight);

            if (rect.Height > 2 && RenderUtils.IsColorVisible(ActualBackgroundColor))
            {
                g.FillRectangle(RenderUtils.GetSolidBrush(ActualBackgroundColor), rect.X, rect.Y, rect.Width, rect.Height);
            }

            if (rect.Height > 1)
            {
                p.PaintBorders(this, rect);
                
            }
            else
            {
                p.PaintBorder(this, Border.Top, this.ActualBorderTopColor, rect);
               
            }
        }
    }
}