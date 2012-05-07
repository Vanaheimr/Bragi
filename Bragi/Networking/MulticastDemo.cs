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
using System.Collections.Generic;

using de.ahzf.Blueprints;
using de.ahzf.Blueprints.UnitTests;
using de.ahzf.Bifrost.HTTP.Client;
using de.ahzf.Blueprints.PropertyGraphs;
using de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable;
using de.ahzf.Blueprints.JurassicGraph;

using de.ahzf.Hermod.Datastructures;
using de.ahzf.Styx;

#endregion

namespace de.ahzf.Bragi
{

    /// <summary>
    /// A small demo for distributed graph processing.
    /// </summary>
    public class MulticastDemo : ITutorial
    {

        #region Name

        /// <summary>
        /// The name of the tutorial.
        /// </summary>
        public String Name
        {
            get
            {
                return "MulticastDemo";
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
                    Keyword.Networking,
                    Keyword.DistributedProcessing
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
                return "A small demo for distributed graph processing.";
            }
        }

        #endregion

        #region Constructor(s)

        #region MulticastDemo()

        /// <summary>
        /// A small demo for distributed graph processing.
        /// </summary>
        public MulticastDemo()
        { }

        #endregion

        #endregion


        #region Run()

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public static void Start()
        {
            new MulticastDemo().Run();
        }

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public void Run()
        {

            var graph1 = GraphFactory.CreateGenericPropertyGraph(1);
            var graph2 = GraphFactory.CreateGenericPropertyGraph(2);

            var UDPMulticastSenderArrow   = new UDPMulticastSenderArrow<String>  ("224.100.0.1", 9001);
            var UDPMulticastReceiverArrow = new UDPMulticastReceiverArrow<String>("224.100.0.1", 9001);

            graph1.OnVertexAdded += (graph, vertex) =>
                UDPMulticastSenderArrow.ReceiveMessage("Vertex '" + vertex.Id.ToString() + "' was added!");

            UDPMulticastReceiverArrow.OnMessageAvailable += (sender, message) => { Console.WriteLine((sender as dynamic).Address + ": " + message); return true; };

            var v1 = graph1.AddVertex(v => v.SetProperty("graph", 1));
            var v2 = graph1.AddVertex(v => v.SetProperty("graph", 1));

            while (true)
            {
                Thread.Sleep(100);
            }

        }

        #endregion


    }

}
