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
using System.Diagnostics;
using System.Collections.Generic;

using de.ahzf.Blueprints;
using de.ahzf.Blueprints.PropertyGraphs.InMemory;
using de.ahzf.Blueprints.PropertyGraphs;

#endregion

namespace de.ahzf.Bragi
{

    /// <summary>
    /// A SmallBenchmark
    /// Do not trust any benchmark you did not manipulate yourself ;)!
    /// </summary>
    public class SmallBenchmark : ITutorial
    {

        #region Name

        /// <summary>
        /// The name of the tutorial.
        /// </summary>
        public String Name
        {
            get
            {
                return "SmallBenchmark";
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
                    Keyword.Benchmarks
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
                return "A small graph processing benchmark.";
            }
        }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// A SmallBenchmark
        /// Do not trust any benchmark you did not manipulate yourself ;)!
        /// </summary>
        public SmallBenchmark()
        {
        }

        #endregion


        #region VertexTypes

        /// <summary>
        /// The vertex types
        /// </summary>
        public enum VertexType
        {
            Tag,
            Website
        }

        #endregion

        #region Vertex properties

        /// <summary>
        /// The type property
        /// </summary>
        public const String _Type = "Type";

        /// <summary>
        /// The name property
        /// </summary>
        public const String _Name = "Name";

        /// <summary>
        /// The Url property
        /// </summary>
        public const String _Url = "Url";

        #endregion

        #region TagLabel

        /// <summary>
        /// The tag label
        /// </summary>
        public const String _TagLabel = "tag";

        #endregion


        #region Run()

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public static void Start()
        {
            new SmallBenchmark().Run();
        }

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public void Run()
        {

            var Stopwatch = new Stopwatch();
            var PRNG      = new Random();

            // Create a new simple property graph
            var _Graph    = GraphFactory.CreateGenericPropertyGraph(1);

            var NumberOfUsers           = 20000UL;
            var NumberOfIterations      = 30;
            var MinNumberOfEdges        = 15;
            var MaxNumberOfEdges        = 25;

            var CountedNumberOfEdges    = 0UL;


            var d = new Dictionary<UInt64, UInt64>();
            for (var i = 0UL; i < NumberOfUsers; i++)
            {
                d.Add(i, i);
            }

            Stopwatch.Restart();
            foreach (var INT in d)
                CountedNumberOfEdges += INT.Value;
            Stopwatch.Stop();
            Console.WriteLine(Stopwatch.Elapsed.TotalMilliseconds + "ms");

            Stopwatch.Restart();
            var b = new UInt64[NumberOfUsers];
            foreach (var INT in b)
                CountedNumberOfEdges += INT;
            Stopwatch.Stop();
            Console.WriteLine(Stopwatch.Elapsed.TotalMilliseconds + "ms");


            IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                   UInt64, Int64, String, String, Object,
                                   UInt64, Int64, String, String, Object,
                                   UInt64, Int64, String, String, Object> ActualVertex = null;

            var Vertices = new IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object>[NumberOfUsers + 1];

            var Measurements = new Double[NumberOfIterations];
            
            Stopwatch.Start();

            Vertices[0] = _Graph.AddVertex(1-1, "default");

            for (var VertexId = 2UL - 1; VertexId < NumberOfUsers; VertexId++)
            {

                ActualVertex = _Graph.AddVertex(VertexId, "default", v => v.SetProperty("Name", "User" + VertexId)
                                                                           .SetProperty("Age",  PRNG.Next(0, 150)));
                Vertices[VertexId-1] = ActualVertex;
                _Graph.AddEdge(ActualVertex, Vertices[PRNG.Next(0, (Int32) VertexId-1)]);

                var NumberOfAdditionalEdges = (UInt64) PRNG.Next(MinNumberOfEdges, MaxNumberOfEdges);

                do
                {
                    _Graph.AddEdge(ActualVertex, Vertices[PRNG.Next(0, (Int32) VertexId - 1)]);
                } while (ActualVertex.OutDegree() < NumberOfAdditionalEdges);

                if (VertexId % 10000UL == 0)
                    Console.WriteLine(VertexId);

            }

            Stopwatch.Stop();
            Console.WriteLine("Creation Time: " + Stopwatch.Elapsed.TotalMilliseconds + "ms");
            Console.WriteLine();

            //var _ROGraph = new SimpleReadOnlyPropertyGraph(2, _Graph,
            //                                               SyncedVertexIds:  true,
            //                                               SyncedEdgeIds:    true
            //                                               );

            for (var Iteration = 0; Iteration < NumberOfIterations; Iteration++)
            {

                CountedNumberOfEdges = 0;

                Stopwatch.Restart();

                foreach (var _Vertex in _Graph.Vertices())
                {
                    CountedNumberOfEdges += (UInt64) _Vertex.OutDegree();
                }

                Stopwatch.Stop();
                Measurements[Iteration] = Stopwatch.Elapsed.TotalMilliseconds;
                Console.WriteLine(CountedNumberOfEdges + " edges in " + Stopwatch.Elapsed.TotalMilliseconds + "ms");

            }

            //var AverageAndStdDev = Measurements.AverageAndStdDev();
            //Console.WriteLine("Mean: " + AverageAndStdDev.Item1 + ", stddev: " + AverageAndStdDev.Item2);
            //Console.ReadLine();

        }

        #endregion

    }

}
