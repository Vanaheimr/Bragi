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
using de.ahzf.Vanaheimr.Balder;
using de.ahzf.Vanaheimr.Blueprints.TraversalGraphs;
using de.ahzf.Vanaheimr.Blueprints.InMemory;

#endregion

namespace de.ahzf.Bragi
{

    /// <summary>
    /// Another demo.
    /// </summary>
    public class Transactions : ITutorial
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
                return "Transactions";
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
        /// A transactions demo.
        /// </summary>
        public Transactions()
        { }

        #endregion


        #region Run()

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public static void Start()
        {
            new Transactions().Run();
        }

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public void Run()
        {

            // Create a new simple property graph
            var graph = GraphFactory.CreateGenericPropertyGraph(1);
            
            var v0 = graph.AddVertex("person", v => v.SetProperty("name", "Alice"));
            var v1 = graph.AddVertex("person", v => v.SetProperty("name", "Bob"));
            var v2 = graph.AddVertex("person", v => v.SetProperty("name", "Carol"));

            var s1 = graph.ToString();
            var s2 = v0.ToString();

            var e0 = graph.AddEdge(v0, v1);
            var e1 = graph.AddEdge(v1, v2);
            var e2 = graph.AddEdge(v2, v0);

            using (var transaction = graph.BeginTransaction())
            {

                //ToDo: Add delegates denying all modifications of the underlying graph!

                var txgraph = transaction.TXObject;

                var v0_ = txgraph.VertexById(0);
                var v1_ = txgraph.VertexById(1);
                var nv_ = txgraph.NumberOfVertices();

                var v3 = txgraph.AddVertex("person", v => v.SetProperty("name", "Dave"));
                var v4 = txgraph.AddVertex("person", v => v.SetProperty("name", "Eve"));
                var v5 = txgraph.AddVertex("thing",  v => v.SetProperty("type", "car"));

                var e3 = txgraph.AddEdge(v0, v3);
                var e4 = txgraph.AddEdge(v1, v4);

                transaction.Commit(); //ToDo: Add delegates denying all modifications of the tx graph!

            } // If the transaction was not committed -> Rollback()

            while (true)
            {
                Thread.Sleep(100);
            }

        }

        #endregion

    }

}
