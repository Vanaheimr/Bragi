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
using de.ahzf.Illias.Commons.Collections;

using de.ahzf.Vanaheimr.Styx;
using de.ahzf.Vanaheimr.Balder;
using de.ahzf.Vanaheimr.Blueprints;
using de.ahzf.Vanaheimr.Blueprints.InMemory;
using de.ahzf.Vanaheimr.Blueprints.UnitTests;

#endregion

namespace de.ahzf.Bragi
{

    /// <summary>
    /// Another demo.
    /// </summary>
    public class GenericDemoGraph : ITutorial
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
                return "Vanaheimr";
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
        public GenericDemoGraph()
        { }

        #endregion


        #region Run()

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public static void Start()
        {
            new GenericDemoGraph().Run();
        }


        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public void Run()
        {

            var graph = GenericDemoGraphFactory.Create();

            var o1  = graph.VertexById(2).AsMutable().SetProperty("sex", "female");

            var a1  = graph.Vertices().P("name", "sex").ToArray();
            var a2  = graph.Vertices().P((k, v) => k == "sex").ToArray();

            var b1  = graph.Vertices().Ps("name", "sex").ToArray();
            var b2  = graph.Vertices().Ps((k, v) => k == "name" || k == "sex").ToArray();

            var c1  = graph.Vertices().PMap("name", "sex");
            var c2  = graph.Vertices().PMap((k, v) => k == "name" || k == "sex");

            var d1  = graph.Vertices().PTable("name", "sex");

            var e1  = graph.Vertices().Label();
            var e3  = graph.V(v => v.Id > 4).Where(v => v.Id < 6);
            var e4  = graph.Vertices().IdIn(3UL, 54UL, 6ul);

            var f1  = graph.Vertices().PFilter("name", n => "Alice".Equals(n)).ToArray();

            var Alice = graph.Vertices().PFilter("name", n => "Alice".Equals(n)).First();
            var Bob   = graph.Vertices().PFilter("name", n => "Bob".  Equals(n)).First();
            var Carol = graph.Vertices().PFilter("name", n => "Carol".Equals(n)).First();

            var he1   = graph.AddHyperEdge("friends", Alice, Bob);
            var he_1  = graph.HyperEdges().ToArray();
            var nhe   = graph.NumberOfHyperEdges();
            var hev1  = he1.Vertices().ToArray();
            var hel1  = graph.HyperEdgesByLabel("all", "friends").ToArray();
            var hel2  = graph.HyperEdgesByLabel().ToArray();

            var x2    = new UInt64[2] { 1, 2 }.ToEdges(graph).ToArray();

            var ae1   = Alice.OutEdges().ToArray();
            var ae2a  = Alice.Out().ToArray();
            var ae2b  = Alice.Out().Distinct().ToArray();
            var ae3   = Alice.Out("knows").ToArray();
            var ae4   = Alice.Out(e => e.Label == "knows").ToArray();
            var ae5a  = Alice.Out(e => e.GetDouble("weight") >  0.5).ToArray();
            var ae5b  = Alice.Out(e => e.GetDouble("weight") >= 0.5).ToArray();

            var l1a   = Alice.Both().ToArray();
            var l1b   = Alice.Both().Distinct().ToArray();

            while (true)
            {
                Thread.Sleep(100);
            }

        }

        #endregion

    }

}
