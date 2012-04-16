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
using System.Windows.Media;

using de.ahzf.Vanaheimr.Aegir;

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

            var _HeatmapLayer = MapControl1.AddFeatureLayer<HeatmapLayer>("HeatmapLayer", 10);
            var _FeatureLayer = MapControl1.AddFeatureLayer("FeatureLayer", 20) as FeatureLayer;
            var _ShapeLayer   = MapControl1.AddFeatureLayer<ShapeLayer>("ShapeLayer", 30);

            var feature1a = MapControl1.AddFeature("FeatureLayer", "ahzf",     50.932253, 11.625075,   5,   5, Colors.Red);
            var feature1b = MapControl1.AddFeature("HeatmapLayer", "ahzf",     50.932253, 11.625075, 150, 150, Colors.Red);

            var feature2a = MapControl1.AddFeature("FeatureLayer", "Hannover", 52.373922,  9.743500,   5,   5, Colors.Red);
            var feature2b = MapControl1.AddFeature("HeatmapLayer", "Hannover", 52.373922,  9.743500, 100, 100, Colors.Blue);

            var feature3a = _FeatureLayer.AddFeature("c-base", 52.513191, 13.420057,   5,   5, Colors.Red);
            var feature3b = _HeatmapLayer.AddFeature("c-base", 52.513191, 13.420057, 150, 150, Colors.Yellow);

            var feature4a = _FeatureLayer.AddFeature("malmö", 55.618691, 12.999573,   5,   5, Colors.Red);
            var feature4b = _HeatmapLayer.AddFeature("malmö", 55.618691, 12.999573,  50,  50, Colors.Brown);

            //var f5 = _FeatureLayer.AddPathFeature ("aa", 52.013191, 13.020057, 106, 93, Colors.Orange);
        //    var f6 = _FeatureLayer.AddPathFeature(FeatureLayer.Thueringen, "aa", 55.058315, 5.866399, 106, 93, Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30));


            var feature5a = _ShapeLayer.AddShape(new Germany(Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));

            // Deutschland: 55.058315, 5.866399 => 47.270203, 15.041656
            // Width: 7.788112 Height: 9.175257


        }

    }

}
