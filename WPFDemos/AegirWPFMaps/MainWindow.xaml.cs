/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
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
using de.ahzf.Vanaheimr.Aegir.Controls;

#endregion

namespace de.ahzf.Bragi.AegirWPFMaps
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Data

        /// <summary>
        /// The text of the map search box whenever we don't
        /// have something more useful to show.
        /// </summary>
        private const String MapSearchBoxText = "search: latitude longitude zoomlevel";

        #endregion

        #region MainWindow()

        /// <summary>
        /// MainWindow
        /// </summary>
        public MainWindow()
        {

            InitializeComponent();

            #region Subscribe to map events

            // Subscribe to mouse position change events and
            // show the current geo position below the map
            MapControl.GeoPositionChanged += (o, GeoPos)  => GeoPositionTextBlock.Text = GeoPos.ToGeoString();

            MapControl.MapViewChanged     += (o, x, y, m) => DisplayOffsetTextBlock.Text = "Offset: " + x + " / " + y;

            #endregion

            #region Add map layers

            var _TilesLayer         = MapControl.AddLayer<TilesLayer>       ("TilesLayer",        0);
            var _ShapeLayerWG       = MapControl.AddLayer<ShapeLayer>       ("ShapeLayerWG",     10);
            var _ShapeLayerEG       = MapControl.AddLayer<ShapeLayer>       ("ShapeLayerEG",     11);
            var _HeatmapLayer       = MapControl.AddLayer<HeatmapLayer>     ("HeatmapLayer",     20);
            var _EditFeatureLayer   = MapControl.AddLayer<EditFeatureLayer> ("EditFeatureLayer", 30, Visibility.Hidden);
            var _FeatureLayer       = MapControl.AddLayer<FeatureLayer>     ("FeatureLayer",     40);

            #endregion

            #region Add some features

            var feature1a = _FeatureLayer.AddFeature("ahzf",     new Latitude(50.932253), new Longitude(11.625075),   5,   5, Colors.Red);
            var feature1b = _HeatmapLayer.AddFeature("ahzf",     new Latitude(50.932253), new Longitude(11.625075), 150, 150, Colors.Red);

            var feature2a = _FeatureLayer.AddFeature("Hannover", new Latitude(52.373922), new Longitude( 9.743500),   5,   5, Colors.Red);
            var feature2b = _HeatmapLayer.AddFeature("Hannover", new Latitude(52.373922), new Longitude( 9.743500), 100, 100, Colors.Blue);

            var feature3a = _FeatureLayer.AddFeature("c-base",   new Latitude(52.513191), new Longitude(13.420057),   5,   5, Colors.Red);
            var feature3b = _HeatmapLayer.AddFeature("c-base",   new Latitude(52.513191), new Longitude(13.420057), 150, 150, Colors.Yellow);

            var feature4a = _FeatureLayer.AddFeature("malmö",    new Latitude(55.618691), new Longitude(12.999573),   5,   5, Colors.Red);
            var feature4b = _HeatmapLayer.AddFeature("malmö",    new Latitude(55.618691), new Longitude(12.999573),  50,  50, Colors.Brown);

            #endregion

            var efeature1a = _EditFeatureLayer.AddFeature("dfh", new Latitude(40.032253), new Longitude(7.025075), 5, 5, Colors.Green);

            #region Add some shapes

            //var feature5a = _ShapeLayerEG.AddShape(new Thueringen            (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            var feature5b = _ShapeLayerWG.AddShape(new Bayern                (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            //var feature5c = _ShapeLayerWG.AddShape(new BadenWuerttemberg     (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            //var feature5d = _ShapeLayerWG.AddShape(new Hessen                (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            //var feature5e = _ShapeLayerWG.AddShape(new Saarland              (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            //var feature5f = _ShapeLayerEG.AddShape(new Sachsen               (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            //var feature5g = _ShapeLayerEG.AddShape(new SachsenAnhalt         (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            //var feature5h = _ShapeLayerEG.AddShape(new Berlin                (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            //var feature5i = _ShapeLayerWG.AddShape(new NordrheinWestfalen    (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            //var feature5j = _ShapeLayerWG.AddShape(new RheinlandPfalz        (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            //var feature5k = _ShapeLayerWG.AddShape(new Hamburg               (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            //var feature5l = _ShapeLayerWG.AddShape(new Bremen                (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            //var feature5m = _ShapeLayerEG.AddShape(new Brandenburg           (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            //var feature5n = _ShapeLayerWG.AddShape(new SchleswigHolstein     (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            //var feature5o = _ShapeLayerEG.AddShape(new MecklenburgVorpommern (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));
            //var feature5p = _ShapeLayerWG.AddShape(new Niedersachsen         (Color.FromArgb(0xFF, 0xE0, 0xC0, 0x60), 1, Color.FromArgb(0x77, 0xE0, 0x60, 0x30)));

            #endregion

            // Set the deactivated style of the map search box
            MapSearchBox_LostFocus(this, null);

            // Set the initial focus on the map
            MapControl.Focus();

        }

        #endregion


        #region (private) MapSearchBox_ProcessInput(Sender, KeyEventArgs)

        /// <summary>
        /// Process any text entered into the MapSearchBox
        /// </summary>
        /// <param name="Sender">The sender of the event.</param>
        /// <param name="KeyEventArgs">The event parameters.</param>
        private void MapSearchBox_ProcessInput(Object Sender, KeyEventArgs KeyEventArgs)
        {

            switch (KeyEventArgs.Key)
            {

                #region Esc pressed => clear map search box

                case Key.Escape:
                    MapSearchBox.Clear();
                    MapSearchBorder.Background = new SolidColorBrush(Colors.White);
                    break;

                #endregion

                #region Enter pressed => zoom map

                case Key.Enter:

                    #region Process a "latitude, longitude" input

                    var TextArray = MapSearchBox.Text.Split(new String[3] { " ", ",", "." }, StringSplitOptions.RemoveEmptyEntries);

                    Double latitude, longitude;
                    UInt32 zoomlevel;

                    if (TextArray.Length == 4)
                    {

                        if (Double.TryParse(TextArray[0] + "," + TextArray[1], out latitude))
                            if (latitude >= -90 && latitude <= 90)

                                if (Double.TryParse(TextArray[2] + "," + TextArray[3], out longitude))
                                    if (longitude >= -90 && longitude <= 90)
                                    {
                                        MapControl.MoveTo(new Latitude(latitude), new Longitude(longitude));
                                        MapSearchBox_LostFocus(this, null);
                                        MapControl.Focus();
                                        return;
                                    }

                        MapSearchBorder.Background = new SolidColorBrush(Color.FromArgb(0x77, 0xFF, 0x00, 0x00));

                    }

                    #endregion

                    #region Process a "latitude, longitude, zoomlevel" input

                    if (TextArray.Length == 5)
                    {

                        if (Double.TryParse(TextArray[0] + "," + TextArray[1], out latitude))
                            if (latitude >= -90 && latitude <= 90)

                                if (Double.TryParse(TextArray[2] + "," + TextArray[3], out longitude))
                                    if (longitude >= -90 && longitude <= 90)

                                        if (UInt32.TryParse(TextArray[4], out zoomlevel))
                                            if (zoomlevel >= de.ahzf.Vanaheimr.Aegir.Controls.MapControl.MinZoomLevel &&
                                                zoomlevel <= de.ahzf.Vanaheimr.Aegir.Controls.MapControl.MaxZoomLevel)
                                            {
                                                MapControl.ZoomTo(new Latitude(latitude), new Longitude(longitude), zoomlevel);
                                                MapSearchBox_LostFocus(this, null);
                                                MapControl.Focus();
                                                return;
                                            }

                        MapSearchBorder.Background = new SolidColorBrush(Color.FromArgb(0x77, 0xFF, 0x00, 0x00));

                    }

                    #endregion

                    // Search for a town name...
                    MapSearchBorder.Background = new SolidColorBrush(Color.FromArgb(0x77, 0xFF, 0x00, 0x00));

                    KeyEventArgs.Handled = true;
                    break;

                #endregion

            }

        }

        #endregion

        #region (private) MapSearchBox_GotFocus(Sender, RoutedEventArgs)

        /// <summary>
        /// The map search box got the mouse focus.
        /// </summary>
        /// <param name="Sender">The sender of the event.</param>
        /// <param name="RoutedEventArgs">The event parameters.</param>
        private void MapSearchBox_GotFocus(Object Sender, RoutedEventArgs RoutedEventArgs)
        {

            MapSearchBorder.Background  = new SolidColorBrush(Colors.White);
            MapSearchBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x77, 0x77, 0x77));
            MapSearchBox.Foreground     = new SolidColorBrush(Colors.Black);

            if (MapSearchBox.Text == MapSearchBoxText)
                MapSearchBox.Clear();

        }

        #endregion

        #region (private) MapSearchBox_LostFocus(Sender, RoutedEventArgs)

        /// <summary>
        /// The map search box lost the mouse focus.
        /// </summary>
        /// <param name="Sender">The sender of the event.</param>
        /// <param name="RoutedEventArgs">The event parameters.</param>
        private void MapSearchBox_LostFocus(Object Sender, RoutedEventArgs RoutedEventArgs)
        {

            MapSearchBorder.Background  = new SolidColorBrush(Colors.Transparent);
            MapSearchBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xA0, 0xA0, 0xA0));
            MapSearchBox.Foreground     = new SolidColorBrush(Color.FromArgb(0xFF, 0xC0, 0xC0, 0xC0));

            if (MapSearchBox.Text == "")
                MapSearchBox.Text = MapSearchBoxText;

        }

        #endregion

    }

}
