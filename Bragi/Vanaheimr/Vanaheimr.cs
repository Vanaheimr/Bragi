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

using de.ahzf.Vanaheimr.Styx;
using de.ahzf.Illias.Commons;
using System.Diagnostics;
using de.ahzf.Vanaheimr.Blueprints.InMemory;
using de.ahzf.Vanaheimr.Blueprints;

#endregion

namespace de.ahzf.Bragi
{

    /// <summary>
    /// Another demo.
    /// </summary>
    public class Vanaheimr : ITutorial
    {

        IGenericPropertyGraph<String, Int64, String, String, Object,
                              String, Int64, String, String, Object,
                              String, Int64, String, String, Object,
                              String, Int64, String, String, Object> graph;

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
        public Vanaheimr()
        { }

        #endregion


        #region Run()

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public static void Start()
        {
            new Vanaheimr().Run();
        }


        private IGenericPropertyVertex<String, Int64, String, String, Object,
                                       String, Int64, String, String, Object,
                                       String, Int64, String, String, Object,
                                       String, Int64, String, String, Object>

            AddProject(String Name, params IGenericPropertyVertex<String, Int64, String, String, Object,
                                                                  String, Int64, String, String, Object,
                                                                  String, Int64, String, String, Object,
                                                                  String, Int64, String, String, Object>[] Uses)

        {

            if (Uses == null)
                return graph.AddVertex(Name, "project");

            return graph.AddVertex(Name, "project", v => Uses.ForEach(v2 => v.AddEdge_chainable("uses", v2)));

        }


        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public void Run()
        {

            graph = GraphFactory.CreateGenericPropertyGraph2("Vanaheimr graph processing stack");

            var IlliasCommons  = AddProject("Illias Commons");
            var Eunomia        = AddProject("Eunomia");
            var Blueprints     = AddProject("Blueprints", IlliasCommons, Eunomia);

            while (true)
            {
                Thread.Sleep(100);
            }

        }

        #endregion

    }

}
