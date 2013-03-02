/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
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

#endregion

namespace de.ahzf.Bragi
{

    #region Keyword

    /// <summary>
    /// A single bragi keyword.
    /// </summary>
    public class Keyword
    {

        #region Data

        /// <summary>
        /// The internal keyword.
        /// </summary>
        public readonly String _Keyword;

        #endregion

        #region Constructor(s)

        #region Keyword(Keyword)

        /// <summary>
        /// Creates a new keyword.
        /// </summary>
        /// <param name="Keyword">The internal keyword.</param>
        public Keyword(String Keyword)
        {
            this._Keyword = Keyword;
        }

        #endregion

        #endregion

        #region Statics

        /// <summary>
        /// Quad
        /// </summary>
        public static readonly Keyword Quad                     = new Keyword("Quad");

        /// <summary>
        /// QuadStore
        /// </summary>
        public static readonly Keyword QuadStore                = new Keyword("QuadStore");

        /// <summary>
        /// RDF
        /// </summary>
        public static readonly Keyword RDF                      = new Keyword("RDF");
        
        /// <summary>
        /// Semantic Web
        /// </summary>
        public static readonly Keyword SemanticWeb              = new Keyword("Semantic Web");
        
        /// <summary>
        /// Social Networks
        /// </summary>
        public static readonly Keyword SocialNetworks           = new Keyword("Social Networks");

        /// <summary>
        /// Tagging
        /// </summary>
        public static readonly Keyword Tagging                  = new Keyword("Tagging");

        /// <summary>
        /// Dataflows
        /// </summary>
        public static readonly Keyword Dataflows                = new Keyword("Dataflows");

        /// <summary>
        /// Dataflow Processing
        /// </summary>
        public static readonly Keyword DataflowProcessing       = new Keyword("Dataflow Processing");

        /// <summary>
        /// Benchmarks
        /// </summary>
        public static readonly Keyword Benchmarks               = new Keyword("Benchmarks");

        /// <summary>
        /// Concurrency
        /// </summary>
        public static readonly Keyword Concurrency              = new Keyword("Concurrency");

        /// <summary>
        /// Parallel
        /// </summary>
        public static readonly Keyword Parallel                 = new Keyword("Parallel");

        /// <summary>
        /// Visualization
        /// </summary>
        public static readonly Keyword Visualization            = new Keyword("Visualization");

        /// <summary>
        /// HTML5
        /// </summary>
        public static readonly Keyword HTML5                    = new Keyword("HTML5");

        /// <summary>
        /// MultiCores
        /// </summary>
        public static readonly Keyword MultiCores               = new Keyword("MultiCores");

        /// <summary>
        /// Networking
        /// </summary>
        public static readonly Keyword Networking               = new Keyword("Networking");
        
        /// <summary>
        /// Multicast
        /// </summary>
        public static readonly Keyword Multicast                = new Keyword("Multicast");

        /// <summary>
        /// DistributedProcessing
        /// </summary>
        public static readonly Keyword DistributedProcessing    = new Keyword("Distributed Processing");

        /// <summary>
        /// Partitions
        /// </summary>
        public static readonly Keyword Partitions               = new Keyword("Partitions");

        #endregion

    }

    #endregion

}
