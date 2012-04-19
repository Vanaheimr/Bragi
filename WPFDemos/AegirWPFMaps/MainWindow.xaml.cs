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
using System.Windows.Input;
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

            #region Subscribe to map events

            // Subscribe to mouse position change events and
            // show the current geo position below the map
            MapControl1.GeoPositionChanged   += (o, GeoPos) => GeoPositionTextBlock.Text = GeoPos.ToGeoString();

            MapControl1.DisplayOffsetChanged += (o, x, y)   => DisplayOffsetTextBlock.Text = "Offset: " + x + " / " + y;

            #endregion

            #region Add map layers

            var _TilesLayer   = MapControl1.AddLayer<TilesLayer>  ("TilesLayer",    0);
            var _ShapeLayerWG = MapControl1.AddLayer<ShapeLayer>  ("ShapeLayerWG", 10);
            var _ShapeLayerEG = MapControl1.AddLayer<ShapeLayer>  ("ShapeLayerEG", 11);
            var _HeatmapLayer = MapControl1.AddLayer<HeatmapLayer>("HeatmapLayer", 20);
            var _FeatureLayer = MapControl1.AddLayer<FeatureLayer>("FeatureLayer", 30);

            #endregion

            #region Add some features

            var feature1a = _FeatureLayer.AddFeature("ahzf",     50.932253, 11.625075,   5,   5, Colors.Red);
            var feature1b = _HeatmapLayer.AddFeature("ahzf",     50.932253, 11.625075, 150, 150, Colors.Red);

            var feature2a = _FeatureLayer.AddFeature("Hannover", 52.373922,  9.743500,   5,   5, Colors.Red);
            var feature2b = _HeatmapLayer.AddFeature("Hannover", 52.373922,  9.743500, 100, 100, Colors.Blue);

            var feature3a = _FeatureLayer.AddFeature("c-base",   52.513191, 13.420057,   5,   5, Colors.Red);
            var feature3b = _HeatmapLayer.AddFeature("c-base",   52.513191, 13.420057, 150, 150, Colors.Yellow);

            var feature4a = _FeatureLayer.AddFeature("malmö",    55.618691, 12.999573,   5,   5, Colors.Red);
            var feature4b = _HeatmapLayer.AddFeature("malmö",    55.618691, 12.999573,  50,  50, Colors.Brown);

            #endregion

            #region Add some shapes

            var feature5a = _ShapeLayerEG.AddShape(new Thueringen            (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            var feature5b = _ShapeLayerWG.AddShape(new Bayern                (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            var feature5c = _ShapeLayerWG.AddShape(new BadenWuerttemberg     (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            var feature5d = _ShapeLayerWG.AddShape(new Hessen                (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            var feature5e = _ShapeLayerWG.AddShape(new Saarland              (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            var feature5f = _ShapeLayerEG.AddShape(new Sachsen               (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            var feature5g = _ShapeLayerEG.AddShape(new SachsenAnhalt         (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            var feature5h = _ShapeLayerEG.AddShape(new Berlin                (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            var feature5i = _ShapeLayerWG.AddShape(new NordrheinWestfalen    (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            var feature5j = _ShapeLayerWG.AddShape(new RheinlandPfalz        (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            var feature5k = _ShapeLayerWG.AddShape(new Hamburg               (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            var feature5l = _ShapeLayerWG.AddShape(new Bremen                (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            var feature5m = _ShapeLayerEG.AddShape(new Brandenburg           (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            var feature5n = _ShapeLayerWG.AddShape(new SchleswigHolstein     (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            var feature5o = _ShapeLayerEG.AddShape(new MecklenburgVorpommern (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            var feature5p = _ShapeLayerWG.AddShape(new Niedersachsen         (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));

            #endregion

        }


        #region (private) MapSearchBox_ProcessInput(Sender, KeyEventArgs)

        /// <summary>
        /// Process any text entered into the MapSearchBox
        /// </summary>
        /// <param name="Sender">The sender of the event.</param>
        /// <param name="KeyEventArgs">The event parameters.</param>
        private void MapSearchBox_ProcessInput(Object Sender, KeyEventArgs KeyEventArgs)
        {

            if (KeyEventArgs.Key == Key.Enter)
            {

                #region Process a "latitude, longitude" input

                var TextArray = MapSearchBox.Text.Split(new String[3] { " ", ",", "." }, StringSplitOptions.RemoveEmptyEntries);

                Double latitude, longitude;
                UInt32 zoomlevel;

                if (TextArray.Length == 4)
                {
                    if (Double.TryParse(TextArray[0] + "," + TextArray[1], out latitude))
                        if (Double.TryParse(TextArray[2] + "," + TextArray[3], out longitude))
                        {
                            MapControl1.MoveTo(latitude, longitude);
                            return;
                        }
                }

                #endregion

                #region Process a "latitude, longitude, zoomlevel" input

                if (TextArray.Length == 5)
                    if (Double.TryParse(TextArray[0] + "," + TextArray[1], out latitude))
                        if (Double.TryParse(TextArray[2] + "," + TextArray[3], out longitude))
                            if (UInt32.TryParse(TextArray[4], out zoomlevel))
                            {
                                MapControl1.ZoomTo(latitude, longitude, zoomlevel);
                                return;
                            }

                #endregion

                // Search for a town name...

                KeyEventArgs.Handled = true;

            }

        }

        #endregion

    }

}
