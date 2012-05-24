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
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using de.ahzf.Illias.Commons;
using de.ahzf.Vanaheimr.Styx;
using de.ahzf.Vanaheimr.Blueprints;
using de.ahzf.Vanaheimr.Blueprints.InMemory;
using de.ahzf.Vanaheimr.Blueprints.TraversalGraphs;
using de.ahzf.Vanaheimr.Balder;

#endregion

namespace de.ahzf.Bragi
{

    /// <summary>
    /// Another demo.
    /// </summary>
    public class TotallyFallen : ITutorial
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
                return "Totally Fallen";
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
                    Keyword.Concurrency,
                    Keyword.Parallel,
                    Keyword.MultiCores
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
                return "A small concurrency demo.";
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        /// <summary>
        /// A concurrency demo.
        /// </summary>
        public TotallyFallen()
        { }

        #endregion


        #region Run()

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public static void Start()
        {
            new TotallyFallen().Run();
        }

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public void Run()
        {

            var _Stopwatch               = new Stopwatch();
            var _Random                  = new Random();
            var _NumberOfVertices        = 10000;
            var _NumberOfEdges           = 600000;
            var _NumberOfConcurrentTasks = 10;


            // Create a new simple property graph
            var _graph = GraphFactory.CreateGenericPropertyGraph(1);
            
            _Stopwatch.Start();

            // Add vertices
            _NumberOfVertices.Loop(() => _graph.AddVertex());
            Console.WriteLine(_Stopwatch.Elapsed.TotalSeconds + "s : " + _NumberOfVertices + " vertices added.");


            // Add Edges
            _NumberOfEdges.Loop(() => _graph.AddEdge(_graph.VertexById((UInt64) _Random.Next(_NumberOfVertices)+1),
                                                     "knows",
                                                     _graph.VertexById((UInt64) _Random.Next(_NumberOfVertices)+1)));

            Console.WriteLine(_Stopwatch.Elapsed.TotalSeconds + "s : " + _NumberOfEdges + " vertices added.");

            //var Workload = _graph.Vertices().
            //               ToPartitions((UInt64) Math.Round((Double) _NumberOfVertices / (Double) _NumberOfConcurrentTasks));

            Double TimeStamp1, TimeStamp2;

            TimeStamp1 = _Stopwatch.Elapsed.TotalSeconds;
            var TraversalGraph = new VertexVertex_MatrixListGraph(_graph);
            TimeStamp2 = _Stopwatch.Elapsed.TotalSeconds;

            Console.WriteLine(Math.Round(TimeStamp2 - TimeStamp1, 6) + " secs to create the TraversalGraph.");

            #region Vertices.Out()

            //Console.WriteLine(Environment.NewLine + "Traversing all Vertices.Out()");

            TimeStamp1 = _Stopwatch.Elapsed.TotalSeconds;

            _graph.Vertices().Out().ForEach(x => { });

            TimeStamp2 = _Stopwatch.Elapsed.TotalSeconds;

            //Parallel.ForEach(_graph.Vertices(), x => x.Out().ForEach(y => { }));
            ////_graph.Vertices().AsParallel().Out().ForEach(x => { });

            //var TimeStamp3 = _Stopwatch.Elapsed.TotalSeconds;

            Console.WriteLine(Math.Round(TimeStamp2 - TimeStamp1, 6) + " secs (single threaded)");
            Console.WriteLine(Math.Round(_NumberOfEdges / (TimeStamp2 - TimeStamp1)) + " traversals per second (single threaded)");
            //Console.WriteLine(Math.Round(_NumberOfEdges / (TimeStamp3 - TimeStamp2)) + " traversals per second (multi threaded)");


            TimeStamp1 = _Stopwatch.Elapsed.TotalSeconds;

            _graph.Vertices().Out().ForEach(x => { });

            TimeStamp2 = _Stopwatch.Elapsed.TotalSeconds;

            Console.WriteLine(Math.Round(TimeStamp2 - TimeStamp1, 6) + " secs (single threaded)");
            Console.WriteLine(Math.Round(_NumberOfEdges / (TimeStamp2 - TimeStamp1)) + " traversals per second (single threaded)");

            
            #endregion

            #region Vertices.Out().Out()

            //Console.WriteLine(Environment.NewLine + "Traversing all Vertices.Out().Out()");

            //var TimeStamp4 = _Stopwatch.Elapsed.TotalSeconds;

            //_graph.Vertices().Out().Out().ForEach(x => { });

            //var TimeStamp5 = _Stopwatch.Elapsed.TotalSeconds;

            //Parallel.ForEach(_graph.Vertices(), x => x.Out().Out().ForEach(y => { }));

            ////_graph.Vertices().ForEach(x => x.OutDegree());

            ////var a = from v
            ////        in _graph.Vertices()//.AsParallel()
            ////        select v.OutDegree();


            //var TimeStamp6 = _Stopwatch.Elapsed.TotalSeconds;

            //Console.WriteLine(Math.Round((_NumberOfEdges^2) / (TimeStamp5 - TimeStamp4)) + " traversals per second (single threaded)");
            //Console.WriteLine(Math.Round((_NumberOfEdges^2) / (TimeStamp6 - TimeStamp5)) + " traversals per second (multi threaded)");

            #endregion

            while (true)
            {
                Thread.Sleep(100);
            }

        }

        #endregion

    }

}
