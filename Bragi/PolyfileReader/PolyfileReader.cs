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
using System.IO;
using System.Linq;
using System.Collections.Generic;

using de.ahzf.Illias;
using de.ahzf.Illias.Commons;
using de.ahzf.Styx;
using System.Text.RegularExpressions;
using de.ahzf.Vanaheimr.Aegir;
using System.Text;

#endregion

namespace de.ahzf.Bragi
{

    /// <summary>
    /// A quadstore demo
    /// </summary>
    public class PolyfileReader : ITutorial
    {

        #region ITutorial members

        #region Name

        /// <summary>
        /// The name of the tutorial.
        /// </summary>
        public String Name
        {
            get
            {
                return "QuadStoreDemo";
            }
        }

        #endregion

        #region Keywords

        /// <summary>
        /// The keywords of the tutorial.
        /// </summary>
        public IEnumerable<Keyword> Keywords
        {
            get
            {
                return new List<Keyword>() {
                    Keyword.Quad,
                    Keyword.QuadStore,
                    Keyword.RDF
                };
            }
        }

        #endregion

        #region Description

        /// <summary>
        /// A short description of the tutorial.
        /// </summary>
        public String Description
        {
            get
            {
                return "Shows you how to setup and use a simple QuadStore.";
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        /// <summary>
        /// A quadstore demo
        /// </summary>
        public PolyfileReader()
        { }

        #endregion


        #region Run()

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public static void Start()
        {
            new PolyfileReader().Run();
        }

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public void Run()
        {

            foreach (var Filename in new DirectoryInfo(Directory.GetCurrentDirectory()).
                                     EnumerateFiles("PolyfileReader/*.poly", SearchOption.TopDirectoryOnly))
            {

                var a = Polyfiles.Polyfile2ShapeInfo(new StreamReader("PolyfileReader/" + Filename).GetLines().Skip(1), 2, 23);

                var lat_start = a.Latitude_Start;
                var lng_start = a.Longitude_Start;
                var lat_end   = a.Longitude_Start;
                var lng_end   = a.Longitude_End;
                var zoom5     = a.PathAtZoomLevel[5];
            
            }



            //var _PolyMetaPipe = new PolyReaderMetaPipe(SearchPattern: "PolyfileReader/*.poly",
            //                                           SearchOption:  SearchOption.TopDirectoryOnly);

            //_PolyMetaPipe.SetSource(Directory.GetCurrentDirectory());

            //_PolyMetaPipe.ForEach(s => Console.WriteLine(s));

            foreach (var Filename in new DirectoryInfo(Directory.GetCurrentDirectory()).
                                     EnumerateFiles("PolyfileReader/*.poly",
                                                    SearchOption.TopDirectoryOnly))
            {

                Console.WriteLine("Processing... " + Filename);

                var Shapes = new Dictionary<UInt32, Tuple<List<GeoCoordinate>, Dictionary<UInt32, Tuple<List<Tuple<UInt64, UInt64>>, StringBuilder>>>>();

                UInt32 Integer = 1;
                UInt32 ShapeNumber = 1;
                GeoCoordinate GeoCoordinate = null;

                var min_lat = Double.MaxValue;
                var min_lng = Double.MaxValue;
                var max_lat = Double.MinValue;
                var max_lng = Double.MinValue;

                foreach (var line in new StreamReader("PolyfileReader/" + Filename).GetLines().Skip(1))
                {

                    if (UInt32.TryParse(line, out Integer))
                    {
                        ShapeNumber = Integer;
                        Shapes.Add(ShapeNumber, new Tuple<List<GeoCoordinate>, Dictionary<UInt32, Tuple<List<Tuple<UInt64, UInt64>>, StringBuilder>>>(
                                                    new List<GeoCoordinate>(),
                                                    new Dictionary<UInt32, Tuple<List<Tuple<UInt64, UInt64>>, StringBuilder>>()));
                    }

                    else if (line == "END")
                        continue;

                    else if (GeoCoordinate.TryParseString(line, out GeoCoordinate))
                    {

                        // Polyfiles store lng lat!!!
                        Shapes[ShapeNumber].Item1.Add(new GeoCoordinate(GeoCoordinate.Longitude, GeoCoordinate.Latitude));

                        if (min_lat > GeoCoordinate.Longitude)
                            min_lat = GeoCoordinate.Longitude;

                        if (min_lng > GeoCoordinate.Latitude)
                            min_lng = GeoCoordinate.Latitude;

                        if (max_lat < GeoCoordinate.Longitude)
                            max_lat = GeoCoordinate.Longitude;

                        if (max_lng < GeoCoordinate.Latitude)
                            max_lng = GeoCoordinate.Latitude;

                    }

                }


                var Output1 = new StreamWriter("PolyfileReader/" + Filename.Name.Replace(".poly", ".data"));
                var Array = new StringBuilder();

                var Output2 = new StreamWriter("PolyfileReader/" + Filename.Name.Replace(".poly", ".geo"));
                var Language = new StringBuilder();

                Shapes.ForEach((shape) =>
                {
                    Array.AppendLine(shape.Key.ToString());
                    shape.Value.Item1.ForEach(c =>
                    {
                        Array.AppendLine("\t\t\t{ " + c.Latitude.ToString("00.000000").Replace(",", ".") + ", " + c.Longitude.ToString("00.000000").Replace(",", ".") + " },");
                    });
                });

                Output1.WriteLine(Array.ToString());
                Output1.Flush();
                Output1.Close();


                var diff_lat = Math.Abs(min_lat - max_lat);
                var diff_lng = Math.Abs(min_lng - max_lng);

                var min_resolution = 2U;
                var max_resolution = 23U;

                Output2.WriteLine("From:       " + max_lat.ToString("00.000000").Replace(",", ".") + ", " + min_lng.ToString("00.000000").Replace(",", "."));
                Output2.WriteLine("To:         " + min_lat.ToString("00.000000").Replace(",", ".") + ", " + max_lng.ToString("00.000000").Replace(",", "."));
                Output2.WriteLine("Diff:       " + diff_lat.ToString("00.000000").Replace(",", ".") + ", " + diff_lng.ToString("00.000000").Replace(",", "."));
                Output2.WriteLine("Resolution: " + min_resolution + " -> " + max_resolution);

                Shapes.ForEach((shape) =>
                {

                    for (var resolution = min_resolution; resolution <= max_resolution; resolution++)
                    {

                        shape.Value.Item2.Add(resolution, new Tuple<List<Tuple<UInt64, UInt64>>, StringBuilder>(new List<Tuple<UInt64, UInt64>>(), new StringBuilder()));

                        shape.Value.Item1.ForEach(coor =>
                        {

                            var XY = GeoCalculations.WorldCoordinates_2_Screen(coor, resolution);

                            //if (XY.Item1 < min_x) min_x = XY.Item1;
                            //if (XY.Item2 < min_y) min_y = XY.Item2;

                            shape.Value.Item2[resolution].Item1.Add(XY);

                        });

                        //var Char = "M ";

                        //shape.Value.Item2[resolution].Item1.ForEach(XY =>
                        //{
                        //    shape.Value.Item2[resolution].Item2.Append(Char + (XY.Item1 - min_x) + " " + (XY.Item2 - min_y) + " ");
                        //    if (Char == "L ") Char = "";
                        //    if (Char == "M ") Char = "L ";
                        //});

                        //shape.Value.Item2[resolution].Item2.Append(" Z ");

                    }

                });

                var min_x = 0UL;
                var min_y = 0UL;

                for (var resolution = min_resolution; resolution <= max_resolution; resolution++)
                {

                    min_x = UInt64.MaxValue;
                    min_y = UInt64.MaxValue;

                    Shapes.ForEach((shape) =>
                    {
                        shape.Value.Item2[resolution].Item1.ForEach(XY =>
                        {
                            if (XY.Item1 < min_x) min_x = XY.Item1;
                            if (XY.Item2 < min_y) min_y = XY.Item2;
                        });
                    });

                    Shapes.ForEach((shape) =>
                    {

                        var Char = "M ";

                        shape.Value.Item2[resolution].Item1.ForEach(XY =>
                        {
                            shape.Value.Item2[resolution].Item2.Append(Char + (XY.Item1 - min_x) + " " + (XY.Item2 - min_y) + " ");
                            if (Char == "L ") Char = "";
                            if (Char == "M ") Char = "L ";
                        });

                        shape.Value.Item2[resolution].Item2.Append("Z ");

                    });

                }


                var ShapeLanguage = String.Empty;

                for (var resolution = min_resolution; resolution <= max_resolution; resolution++)
                {
                    ShapeLanguage = "\"";
                    Shapes.ForEach((shape) => ShapeLanguage += shape.Value.Item2[resolution].Item2.ToString().Trim() + " ");
                    Output2.WriteLine(ShapeLanguage.TrimEnd() + "\",");
                }


                //var Points = (from coor
                //              in Coordinates.Skip(1)
                //              select GeoCalculations.WorldCoordinates_2_Screen(coor, 7)).ToList();

                //var min_x = (from p in Points select p.Item1).Min();
                //var min_y = (from p in Points select p.Item2).Min();


                //var FirstPoint = new Tuple<uint, uint>((Points.First().Item1 - min_x), (Points.First().Item2 - min_y));
                //Language.Append("M " + FirstPoint.Item1 + " " + FirstPoint.Item2 + " L ");
                //Points.Skip(1).ForEach(p => { Language.Append((p.Item1 - min_x) + " " + (p.Item2 - min_y) + " "); });
                //Language.AppendLine("z");

                //Output2.WriteLine(Language.ToString());
                Output2.Flush();
                Output2.Close();

            }

        }

        #endregion

    }

}
