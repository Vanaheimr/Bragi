/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Bragi <http://www.github.com/Vanaheimr/Bragi>
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region Usings

using System;
using System.Windows;

using de.ahzf.Vanaheimr.Aegir;
using System.Windows.Media;

#endregion

namespace MappingApplication
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// MainWindow
        /// </summary>
        public MainWindow()
        {

            InitializeComponent();

            // Subscribe to mouse position change events and
            // show the current geo position below the map
            MapControl1.GeoPositionChanged += (o, GeoPos) => GeoPositionTextBlock.Text = GeoPos.ToGeoString();

            var feature1 = MapControl1.AddFeature("ahzf",   50.932253, 11.625075, 15, 5, Colors.Red);
            var feature2 = MapControl1.AddFeature("c-base", 52.513191, 13.420057,  5, 5, Colors.Green);
            var feature3 = MapControl1.AddFeature("malmö",  55.618691, 12.999573,  5, 5, Colors.LightCoral);

        }

    }

}
