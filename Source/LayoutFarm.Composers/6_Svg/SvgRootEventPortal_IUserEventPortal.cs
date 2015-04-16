﻿//2014,2015, WinterDev
//MS-PL, Apache2 some parts derived from github.com/vvvv/svg 

using System;
using System.Collections.Generic;
using PixelFarm.Drawing;
using LayoutFarm.HtmlBoxes;
using LayoutFarm.Css;
using LayoutFarm.Composers;

using LayoutFarm.WebDom;
using LayoutFarm.UI;
using LayoutFarm.Svg;

namespace LayoutFarm.Svg
{

    partial class SvgRootEventPortal : IUserEventPortal
    {


        //------------------------------------------------------------
        void IUserEventPortal.PortalMouseDown(UIMouseEventArgs e)
        {
             
            //find hit svg graphics....
            SvgHitChain hitChain = GetFreeHitChain();
            hitChain.SetRootGlobalPosition(e.X, e.Y);
            //1. hit test
            HitTestCore(this.SvgRoot.SvgSpec, hitChain, e.X, e.Y);
            SetEventOrigin(e, hitChain);
            //2. propagate event  portal
            ForEachOnlyEventPortalBubbleUp(e, hitChain, (portal) =>
            {
                portal.PortalMouseDown(e);
                return true;
            });

            if (!e.CancelBubbling)
            {
                //2. propagate events
                ForEachSvgElementBubbleUp(e, hitChain, () =>
                {
                    //-------
                    //temp test only
                    //-------
                    var svgElement = e.SourceHitElement as SvgElement;
                    if (svgElement is SvgRect)
                    {
                        ((SvgRect)svgElement).FillColor = Color.White;
                    }
                    return true;

                });
                //ForEachEventListenerBubbleUp(e, hitChain, () =>
                //{
                //    //-------
                //    //temp test only
                //    var currentSvg = e.CurrentContextElement as SvgElement; 
                //    //-------
                //    e.CurrentContextElement.ListenMouseDown(e);

                //    return true;
                //});
            }

            e.CancelBubbling = true;

            ReleaseHitChain(hitChain);

        }
        void IUserEventPortal.PortalMouseUp(UIMouseEventArgs e)
        {
             

            //find hit svg graphics....
            SvgHitChain hitChain = GetFreeHitChain();
            hitChain.SetRootGlobalPosition(e.X, e.Y);
            //1. hit test
            HitTestCore(this.SvgRoot.SvgSpec, hitChain, e.X, e.Y);
            SetEventOrigin(e, hitChain);
            //2. propagate event  portal
            ForEachOnlyEventPortalBubbleUp(e, hitChain, (portal) =>
            {
                portal.PortalMouseUp(e);
                return true;
            });

            if (!e.CancelBubbling)
            {
                //2. propagate events
                ForEachEventListenerBubbleUp(e, hitChain, () =>
                {
                    e.CurrentContextElement.ListenMouseUp(e);
                    return true;
                });
            }

            e.CancelBubbling = true;

            ReleaseHitChain(hitChain);
        }
        void IUserEventPortal.PortalMouseMove(UIMouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

            if (e.IsDragging)
            {
                //dragging *** , if changed

                //handle mouse drag
                SvgHitChain hitChain = GetFreeHitChain();
                hitChain.SetRootGlobalPosition(x, y);
                HitTestCore(this.SvgRoot.SvgSpec, hitChain, e.X, e.Y);

                SetEventOrigin(e, hitChain);
                //---------------------------------------------------------
                //propagate mouse drag 
                ForEachOnlyEventPortalBubbleUp(e, hitChain, (portal) =>
                {
                    portal.PortalMouseMove(e);
                    return true;
                });
                //---------------------------------------------------------  
                if (!e.CancelBubbling)
                {
                    //clear previous svg selection 
                    ClearPreviousSelection();

                    //if (hitChain.Count > 0)
                    //{
                    //    //create selection range 
                    //    this._htmlContainer.SetSelection(new SelectionRange(
                    //        _latestMouseDownChain,
                    //        hitChain,
                    //        this.ifonts));
                    //}
                    //else
                    //{
                    //    this._htmlContainer.SetSelection(null);
                    //}


                    ForEachEventListenerBubbleUp(e, hitChain, () =>
                    {

                        e.CurrentContextElement.ListenMouseMove(e);
                        return true;
                    });
                }


                //---------------------------------------------------------
                ReleaseHitChain(hitChain);

            }
            else
            {
                //mouse move  
                //---------------------------------------------------------
                SvgHitChain hitChain = GetFreeHitChain();
                hitChain.SetRootGlobalPosition(x, y);
                HitTestCore(this.SvgRoot.SvgSpec, hitChain, e.X, e.Y);
                SetEventOrigin(e, hitChain);
                //---------------------------------------------------------

                ForEachOnlyEventPortalBubbleUp(e, hitChain, (portal) =>
                {
                    portal.PortalMouseMove(e);
                    return true;
                });

                //---------------------------------------------------------
                if (!e.CancelBubbling)
                {
                    ForEachEventListenerBubbleUp(e, hitChain, () =>
                    {
                        e.CurrentContextElement.ListenMouseMove(e);
                        return true;
                    });
                }
                ReleaseHitChain(hitChain);
            }
        }
        void IUserEventPortal.PortalMouseWheel(UIMouseEventArgs e)
        {
            //this.OnMouseWheel(e);
        }

        //------------------------------------------------------------
        void IUserEventPortal.PortalKeyUp(UIKeyEventArgs e)
        {
            //this.OnKeyUp(e);
        }
        void IUserEventPortal.PortalKeyDown(UIKeyEventArgs e)
        {
            //this.OnKeyDown(e);
        }
        void IUserEventPortal.PortalKeyPress(UIKeyEventArgs e)
        {
            // this.OnKeyPress(e);
        }
        bool IUserEventPortal.PortalProcessDialogKey(UIKeyEventArgs e)
        {
            return false;
            //return this.OnProcessDialogKey(e);
        }
        //------------------------------------------------------------
        void IUserEventPortal.PortalGotFocus(UIFocusEventArgs e)
        {
            //this.OnGotFocus(e);
        }
        void IUserEventPortal.PortalLostFocus(UIFocusEventArgs e)
        {
            //this.OnLostFocus(e);
        }



    }
}