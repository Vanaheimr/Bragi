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

using de.ahzf.Styx;
using de.ahzf.Balder;
using de.ahzf.Blueprints;
using de.ahzf.Illias.Commons;
using de.ahzf.Blueprints.PropertyGraphs;
using System.Diagnostics;

#endregion

namespace de.ahzf.Bragi
{

    public static class LoopHelper
    {

        public static void Loop(this Int32 Loops, Action Do)
        {

            if (Do == null)
                throw new ArgumentNullException("Do", "The parameter 'Do' must not be null!");

            for (var i = 0; i < Loops; i++)
                Do();

        }

        public static void Loop(this Int32 Loops, Action<Int32, Int32> Do)
        {

            if (Do == null)
                throw new ArgumentNullException("Do", "The parameter 'Do' must not be null!");

            for (var i = 0; i < Loops; i++)
                Do(i, Loops);

        }



        public static void Loop(this Int32 Loops, Action<Int32> Do)
        {

            if (Do == null)
                throw new ArgumentNullException("Do", "The parameter 'Do' must not be null!");

            for (var i = 0; i < Loops; i++)
                Do(i);

        }

        public static void Loop(this UInt32 Loops, Action<UInt32> Do)
        {

            if (Do == null)
                throw new ArgumentNullException("Do", "The parameter 'Do' must not be null!");

            for (var i = 0U; i < Loops; i++)
                Do(i);

        }

        public static void Loop(this Int64 Loops, Action<Int64> Do)
        {

            if (Do == null)
                throw new ArgumentNullException("Do", "The parameter 'Do' must not be null!");

            for (var i = 0L; i < Loops; i++)
                Do(i);

        }

        public static void Loop(this UInt64 Loops, Action<UInt64> Do)
        {

            if (Do == null)
                throw new ArgumentNullException("Do", "The parameter 'Do' must not be null!");

            for (var i = 0UL; i < Loops; i++)
                Do(i);

        }

    }

    public static class Loop
    {

        public static void Do(Int32 Loops, Action<Int32> Do)
        {

            if (Do == null)
                throw new ArgumentNullException("Do", "The parameter 'Do' must not be null!");

            for (var i = 0; i < Loops; i++)
                Do(i);

        }

        public static void Do(UInt32 Loops, Action<UInt32> Do)
        {

            if (Do == null)
                throw new ArgumentNullException("Do", "The parameter 'Do' must not be null!");

            for (var i = 0U; i < Loops; i++)
                Do(i);

        }

        public static void Do(Int64 Loops, Action<Int64> Do)
        {

            if (Do == null)
                throw new ArgumentNullException("Do", "The parameter 'Do' must not be null!");

            for (var i = 0L; i < Loops; i++)
                Do(i);

        }

        public static void Do(UInt64 Loops, Action<UInt64> Do)
        {

            if (Do == null)
                throw new ArgumentNullException("Do", "The parameter 'Do' must not be null!");

            for (var i = 0UL; i < Loops; i++)
                Do(i);

        }


    }

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
            var _NumberOfVisits          = ((_NumberOfEdges / _NumberOfVertices) ^ 2) * _NumberOfVertices;
            var _NumberOfConcurrentTasks = 8;

            var j = Enumerable.Range(1, 10);
            3.Loop(() => { j.Take(3).ForEach(c => Console.WriteLine(c)); Console.WriteLine(); });


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
            
            var TimeStamp1 = _Stopwatch.Elapsed.TotalSeconds;

            _graph.Vertices().ForEach(x => x.OutDegree());

            var TimeStamp2 = _Stopwatch.Elapsed.TotalSeconds;

            Console.WriteLine(TimeStamp2 + "s : Traversed all vertices.knows.knows.");
            Console.WriteLine(Math.Round(_NumberOfVisits / (TimeStamp2-TimeStamp1)) + " traversals per second per core");

            while (true)
            {
                Thread.Sleep(100);
            }

        }

        #endregion

    }

}
