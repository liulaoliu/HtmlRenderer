﻿// 2015,2014 ,Apache2, WinterDev
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using LayoutFarm;
using LayoutFarm.UI;


namespace TestGraphicPackage
{
    public partial class Form1 : Form
    {
        UIPlatform uiPlatformWinForm;
        PixelFarm.Drawing.GraphicsPlatform gfxPlatform;
        public Form1(PixelFarm.Drawing.GraphicsPlatform p)
        {
            InitializeComponent();
            this.uiPlatformWinForm = new LayoutFarm.UI.UIPlatformWinForm();
            this.gfxPlatform = p;
        }

        static void ShowFormLayoutInspector(LayoutFarm.UI.UISurfaceViewportControl viewport)
        {

            var formLayoutInspector = new LayoutFarm.Dev.FormLayoutInspector();
            formLayoutInspector.Show();

            formLayoutInspector.FormClosed += (s, e2) =>
            {
                formLayoutInspector = null;
            };
            formLayoutInspector.Connect(viewport);
        }

        public class TopWindowEventPortal : ITopWindowEventPortal
        {
            UserEventPortal userEventPortal;
            public TopWindowEventPortal(UserEventPortal userEventPortal)
            {
                this.userEventPortal = userEventPortal;
            }
            void ITopWindowEventPortal.BindRenderElement(object topRenderElement)
            {
                this.userEventPortal.BindTopRenderElement((RenderElement)topRenderElement);
            }
            IEventListener ITopWindowEventPortal.CurrentKeyboardFocusedElement
            {
                get
                {
                    return this.userEventPortal.CurrentKeyboardFocusedElement;
                }
                set
                {
                    this.userEventPortal.CurrentKeyboardFocusedElement = value;
                }
            }
        }

        private void cmdShowBasicFormCanvas_Click(object sender, EventArgs e)
        {

            LayoutFarm.UI.UISurfaceViewportControl viewport;

            int w = 800;
            int h = 600;

            var userEventPortal = new UserEventPortal();
            var topWindowEventPortal = new TopWindowEventPortal(userEventPortal);
            MyRootGraphic rootgfx = new MyRootGraphic(
                this.uiPlatformWinForm,
                this.gfxPlatform,
                topWindowEventPortal,
                w,
                h);


            Form formCanvas = FormCanvasHelper.CreateNewFormCanvas(rootgfx,
               userEventPortal,
               InnerViewportKind.GdiPlus,
               out viewport);

            viewport.PaintMe();
            formCanvas.Show();
            ShowFormLayoutInspector(viewport);
        }

        private void cmdShowEmbededViewport_Click(object sender, EventArgs e)
        {
            Form simpleForm = new Form();
            simpleForm.Text = "SimpleForm2";
            simpleForm.WindowState = FormWindowState.Maximized;
            Rectangle screenClientAreaRect = Screen.PrimaryScreen.WorkingArea;
            var viewport = new LayoutFarm.UI.UISurfaceViewportControl();
            viewport.Bounds = new Rectangle(0, 0, screenClientAreaRect.Width, screenClientAreaRect.Height);
            simpleForm.Controls.Add(viewport);

            int w = 800;
            int h = 600;
            var userEventPortal = new UserEventPortal();
            var topWindowEventPortal = new TopWindowEventPortal(userEventPortal);
            var rootgfx = new MyRootGraphic(this.uiPlatformWinForm,
                this.gfxPlatform,
                topWindowEventPortal, w, h);

            viewport.InitRootGraphics(rootgfx, userEventPortal, InnerViewportKind.GdiPlus);
            viewport.PaintMe();

            simpleForm.Show();

            ShowFormLayoutInspector(viewport);
        }
    }
}
