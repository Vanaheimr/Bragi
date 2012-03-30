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
using System.Linq;
using System.Collections.Generic;

using de.ahzf.Illias.Commons;
using de.ahzf.Balder;
using de.ahzf.Blueprints;
using de.ahzf.Blueprints.PropertyGraphs;
using System.Threading.Tasks;
using System.Threading;

#endregion

namespace de.ahzf.Bragi
{

    /// <summary>
    /// A concurrency demo.
    /// </summary>
    public class ConcurrencyDemo : ITutorial
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
                return "Concurrency Demo";
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
        public ConcurrencyDemo()
        { }

        #endregion


        #region Run()

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public static void Start()
        {
            new ConcurrencyDemo().Run();
        }

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public void Run()
        {

            // Create a new simple property graph
            var _graph = GraphFactory.CreateGenericPropertyGraph(1);


            // Add tags
            var _good  = _graph.AddVertex(v => v.SetProperty("type", "tag").
                                                 SetProperty("name", "good"));


            var _NumberOfConcurrentTasks = 2;
            var _Task = new Task[_NumberOfConcurrentTasks];

            for (var i = 0; i < _NumberOfConcurrentTasks; i++)
            {

                _Task[i] = Task.Factory.StartNew(() => { for (var j = 0; j < 1000; j++) { _graph.AddVertex(v => v.SetProperty("Task", i).SetProperty("Number", j)); } })
                                       .ContinueWith(task => Console.WriteLine("Task: {0} Completed.", task.GetHashCode()));

            }

            Task.WaitAll(_Task);

            Console.WriteLine("All tasks completed.");

            Console.WriteLine("Number of vertices within the graph: " + _graph.NumberOfVertices());
            Console.WriteLine("Number of vertices within the graph: " + _graph.NumberOfVertices(v => v.Contains("Task", 1)));


            while (true)
            {
                Thread.Sleep(10);
            }

        }

        #endregion

    }

}
