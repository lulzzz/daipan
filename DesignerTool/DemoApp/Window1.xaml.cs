﻿//-----------------------------------------------------------------------

// <copyright file="Window1.xaml.cs" company="Breanos GmbH">
// Copyright Notice:
// DAIPAN - This file, program or part of a program is considered part of the DAIPAN framework by Breanos GmbH for Industrie 4.0
// Published in 2018 by Gerhard Eder gerhard.eder@breanos.com and Achim Bernhard achim.bernhard@breanos.com
// To the extent possible under law, the publishers have dedicated all copyright and related and neighboring rights to this software to the public domain worldwide. This software is distributed without any warranty.
// You should have received a copy of the CC0 Public Domain Dedication along with this software. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.
// <date>Monday, December 3, 2018 3:34:35 PM</date>
// </copyright>

//-----------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Documents;
using System.Linq;
using System.Collections.Generic;
using DiagramDesigner;
using System.Xml;
using System.Xml.Schema;
using System.IO;

namespace DesignerTool
{
    public partial class Window1 : Window
    {
        private Window1ViewModel window1ViewModel;

        public Window1()
        {
            InitializeComponent();
            window1ViewModel = new Window1ViewModel();
            this.DataContext = window1ViewModel;
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
