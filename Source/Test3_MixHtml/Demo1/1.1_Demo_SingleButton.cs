﻿//2014,2015 Apache2, WinterDev
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using LayoutFarm.Drawing;   
using LayoutFarm.UI;

namespace LayoutFarm
{
    [DemoNote("1.1 SingleButton")]
    class Demo_SingleButton : DemoBase
    {
        protected override void OnStartDemo(SampleViewport viewport)
        {


            var sampleButton = new LayoutFarm.CustomWidgets.EaseBox(30, 30);
            viewport.AddContent(sampleButton);

            int count = 0;
            sampleButton.MouseDown += new EventHandler<UIMouseEventArgs>((s, e2) =>
            {
                Console.WriteLine("click :" + (count++));
            });

        }
    }
}