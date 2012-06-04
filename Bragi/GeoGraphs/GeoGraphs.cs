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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using de.ahzf.Illias.Commons;
using de.ahzf.Illias.Commons.Votes;
using de.ahzf.Vanaheimr.Styx;
using de.ahzf.Vanaheimr.Blueprints.InMemory;
using de.ahzf.Vanaheimr.Blueprints;
using System.Diagnostics;
using de.ahzf.Vanaheimr.Aegir;
using de.ahzf.Illias.Commons.Endianness;

#endregion

namespace de.ahzf.Bragi
{

    /// <summary>
    /// A partition graph demo.
    /// </summary>
    public class GeoGraphs : ITutorial
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
                return "GeoGraphs";
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
                    Keyword.Partitions
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
                return "A partition graph demo.";
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        /// <summary>
        /// A partition graph demo.
        /// </summary>
        public GeoGraphs()
        { }

        #endregion


        #region Run()

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public static void Start()
        {
            new GeoGraphs().Run();
        }

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public void Run()
        {

            var GeoCoordinate6 = GeoCoordinate.ParseString("-49.44903° N, -11.07488° E");


            var geo01       = new GeoCoordinate(new Latitude(57.64911), new Longitude(10.40744));
            var geo01hash   = geo01.ToGeoHash(11);
            var geo01hash32 = geo01.ToGeoHash32(16);
            var geo01hash64 = geo01.ToGeoHash64(32);



            //var seattle = new GeoCoordinate(47.621800, -122.350326);
            //var olympia = new GeoCoordinate(47.041917, -122.893766);

            //var distance = seattle.DistanceTo(olympia);
            //Console.WriteLine(distance + " km == 76,3866157995487 km? : " + (distance == 76.3866157995487));


            //var geo13 = new GeoCoordinate(35,  45);
            //var geo14 = new GeoCoordinate(35, 135);
            //var midpoint = geo13.MidPoint(geo14);


            //var LA  = new GeoCoordinate(34.122222,  118.4111111);
            //var NYC = new GeoCoordinate(40.66972222, 73.94388889);
            //var midpt = LA.MidPoint(NYC);// 39.54707861 97.201534

            var geohash64_1a = new GeoCoordinate(new Latitude(34.122222), new Longitude(-118.411111)).ToGeoHash64();
            var geohash64_1b = geohash64_1a.ToGeoCoordinate(6);

            Console.WriteLine(geohash64_1a.Value.ToBitString());
            Console.WriteLine(BitConverter.GetBytes(geohash64_1a.Value.SwapBytes()).ToBitString());


            Console.ReadLine();

        }

        #endregion

    }

}
