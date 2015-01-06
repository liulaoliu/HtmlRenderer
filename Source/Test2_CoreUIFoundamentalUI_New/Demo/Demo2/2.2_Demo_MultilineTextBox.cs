﻿//2014,2015 Apache2, WinterDev
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using LayoutFarm.Drawing;  
using LayoutFarm.UI;

namespace LayoutFarm
{
    [DemoNote("2.2 MultiLineTextBox")]
    class Demo_MultiLineTextBox : DemoBase
    {
        protected override void OnStartDemo(SampleViewport viewport)
        {
            {
                var textbox = new LayoutFarm.CustomWidgets.UITextBox(400, 500, true);
                viewport.AddContent(textbox);
            }

            {
                var textbox = new LayoutFarm.CustomWidgets.UITextBox(400, 500, true);
                textbox.SetLocation(0, 120);
                viewport.AddContent(textbox);
            }
        }
    }
}