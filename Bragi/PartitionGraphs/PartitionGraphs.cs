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

    /// <summary>
    /// A partition graph demo.
    /// </summary>
    public class PartitionGraphs : ITutorial
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
                return "Partition Graphs";
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
        public PartitionGraphs()
        { }

        #endregion


        #region Run()

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public static void Start()
        {
            new PartitionGraphs().Run();
        }

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public void Run()
        {

            var Graph1 = GraphFactory.CreateGenericPropertyGraph(1);
            var Graph2 = GraphFactory.CreateGenericPropertyGraph(2);
            var Graph3 = GraphFactory.CreateGenericPropertyGraph(3);
            var Graph4 = GraphFactory.CreateGenericPropertyGraph(4);
            var Graph5 = GraphFactory.CreateGenericPropertyGraph(5);

            var PartitionGraph1 = GraphFactory.CreatePartitionGraph(23UL, "Stack of graphs 1-5", Graph1, Graph2, Graph3, Graph4, Graph5);
            var PartitionGraph2 = Graph1.CreatePartitionGraph(42UL, "Stack of graphs 1-3", Graph2, Graph3);

            Graph2.OnVertexAdded           += (graph, vertex)       => Console.WriteLine("Vertex #" + vertex.Id + " had been added to graph #" + graph.Id);
            PartitionGraph1.OnVertexAdded  += (graph, vertex)       => Console.WriteLine("Vertex #" + vertex.Id + " had been added to the partition graph");
            PartitionGraph1.OnVertexAdding += (graph, vertex, vote) => { if (vertex.Id == 7) { Console.WriteLine("The vertex id '7' is not allowed!"); vote.Veto(); } };

            Graph1.AddVertex(1, "vertex", v => v.SetProperty("GraphId", 1));
            Graph2.AddVertex(2, "vertex", v => v.SetProperty("GraphId", 2));
            Graph3.AddVertex(3, "vertex", v => v.SetProperty("GraphId", 3));
            Graph4.AddVertex(4, "vertex", v => v.SetProperty("GraphId", 4));
            Graph5.AddVertex(5, "vertex", v => v.SetProperty("GraphId", 5));

            PartitionGraph1.AddVertex(6, "vertex", v => v.SetProperty("GraphId", 6));
            PartitionGraph1.AddVertex(7, "vertex", v => v.SetProperty("GraphId", 7));

            Console.WriteLine(PartitionGraph1.NumberOfVertices());
            Console.WriteLine(PartitionGraph1.NumberOfVertices(v => v.Id != 3));

            var v1a = PartitionGraph1.VertexById(1);
            var v2a = PartitionGraph1.VertexById(2);
            var v3a = PartitionGraph1.VertexById(3);
            var v4a = PartitionGraph1.VertexById(4);
            var v5a = PartitionGraph1.VertexById(5);
            var v6a = PartitionGraph1.VertexById(6);

            var v1b = PartitionGraph2.VertexById(1);
            var v2b = PartitionGraph2.VertexById(2);
            var v3b = PartitionGraph2.VertexById(3);
            var v4b = PartitionGraph2.VertexById(4);
            var v5b = PartitionGraph2.VertexById(5);
            var v6b = PartitionGraph2.VertexById(6);

        }

        #endregion

    }

}
