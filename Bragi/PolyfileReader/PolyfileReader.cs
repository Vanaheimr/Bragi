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

            var Filename = "rheinland-pfalz";

            var _StopwordFile = new StreamReader("PolyfileReader/" + Filename + ".poly");

            var Coordinates = (from   _GeoCoordinate
                               in     _StopwordFile.GetLines().Skip(2)
                               where  _GeoCoordinate != "END"
                               let tmp = GeoCoordinate.ParseString(_GeoCoordinate)
                               select new GeoCoordinate(tmp.Longitude, tmp.Latitude)).ToList();



            var Output1 = new StreamWriter("PolyfileReader/" + Filename + ".data");
            var Array = new StringBuilder();
            Coordinates.ForEach(c => Array.AppendLine("\t\t\t{ " + c.Latitude.ToString("00.000000").Replace(",", ".") + ", " + c.Longitude.ToString("00.000000").Replace(",", ".") + " },"));
            Output1.WriteLine(Array.ToString());
            Output1.Flush();
            Output1.Close();

            var min_lat = (from c in Coordinates select c.Latitude ).Min();
            var min_lng = (from c in Coordinates select c.Longitude).Min();
            var max_lat = (from c in Coordinates select c.Latitude ).Max();
            var max_lng = (from c in Coordinates select c.Longitude).Max();

            var diff_lat = Math.Abs(min_lat - max_lat);
            var diff_lng = Math.Abs(min_lng - max_lng);

            var Points = (from coor
                          in Coordinates.Skip(1)
                          select GeoCalculations.WorldCoordinates_2_Screen(coor, 7)).ToList();

            var min_x = (from p in Points select p.Item1).Min();
            var min_y = (from p in Points select p.Item2).Min();

            var Language   = new StringBuilder();
            var Output2    = new StreamWriter("PolyfileReader/" + Filename + ".geo");
            Language.AppendLine("From: " +  max_lat.ToString("00.000000").Replace(",", ".") + ", " +  min_lng.ToString("00.000000").Replace(",", "."));
            Language.AppendLine("To:   " +  min_lat.ToString("00.000000").Replace(",", ".") + ", " +  max_lng.ToString("00.000000").Replace(",", "."));
            Language.AppendLine("Diff: " + diff_lat.ToString("00.000000").Replace(",", ".") + ", " + diff_lng.ToString("00.000000").Replace(",", "."));
            var FirstPoint = new Tuple<uint, uint>((Points.First().Item1 - min_x), (Points.First().Item2 - min_y));
            Language.Append("M " + FirstPoint.Item1 + " " + FirstPoint.Item2 + " L ");
            Points.Skip(1).ForEach(p => { Language.Append((p.Item1 - min_x) + " " + (p.Item2 - min_y) + " "); });
            Language.AppendLine("z");
            Output2.WriteLine(Language.ToString());
            Output2.Flush();
            Output2.Close();

        }

        #endregion

    }

}
