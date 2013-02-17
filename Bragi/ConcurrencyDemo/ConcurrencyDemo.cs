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
using System.Diagnostics;

using de.ahzf.Illias.Commons;
using de.ahzf.Vanaheimr.Styx;
using de.ahzf.Vanaheimr.Blueprints;
using de.ahzf.Vanaheimr.Blueprints.InMemory;
using de.ahzf.Vanaheimr.Balder;

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

            var _NumberOfVertices        = 100000;
            var _NumberOfConcurrentTasks = 8;


            // Create a new simple property graph
            var _graph = GraphFactory.CreateGenericPropertyGraph(1);

            var _Task = new Task[_NumberOfConcurrentTasks];
            
            var Stopwatch = new Stopwatch();
            Stopwatch.Restart();

            for (var i = 0; i < _NumberOfConcurrentTasks; i++)
            {

                _Task[i] = Task.Factory.StartNew((iCopy) => { for (var j = 0; j < _NumberOfVertices/_NumberOfConcurrentTasks; j++) { _graph.AddVertex(v => v.SetProperty("Task", iCopy).SetProperty("Number", j)); } }, i, new CancellationToken())
                                       .ContinueWith(task => Console.WriteLine("Task: {0} Completed.", task.GetHashCode()));

            }

            Task.WaitAll(_Task);

            Stopwatch.Stop();
            Console.WriteLine("All tasks completed.");
            Console.WriteLine("Added " + _NumberOfVertices + " in " + Stopwatch.Elapsed.TotalSeconds + "s using " + _NumberOfConcurrentTasks + " tasks (" + Math.Round(_NumberOfVertices / Stopwatch.Elapsed.TotalSeconds) + " vertices/s).");

            Console.WriteLine("Number of total vertices within the graph: " + _graph.NumberOfVertices());
            
            Console.WriteLine("TaskIds / NumberOfVertices: ");
            _graph.Vertices().
                   P("Task").
                   DuplicateFilter().
                   ForEach(TaskId => {
                       if (TaskId != null)
                       Console.WriteLine("Task '" + TaskId + "' added " +
                                         //_graph.NumberOfVertices(v => v.Contains("Task", TaskId)) +
                                         " vertices."
                                        );
                                     }
                          );
            Console.WriteLine("");


            while (true)
            {
                Thread.Sleep(10);
            }

        }

        #endregion

    }

}
