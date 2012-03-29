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
using de.ahzf.Blueprints.PropertyGraphs;
using de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable;
using de.ahzf.Blueprints;

#endregion

namespace de.ahzf.Bragi
{

    /// <summary>
    /// The TagExample
    /// </summary>
    public class TagExample : ITutorial
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
                return "TagExample";
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
                    Keyword.Tagging
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
                return "A small tagging example.";
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        /// <summary>
        /// The TagExample
        /// </summary>
        public TagExample()
        { }

        #endregion


        #region VertexTypes

        /// <summary>
        /// The vertex types
        /// </summary>
        public enum VertexType
        {
            Tag,
            Website
        }

        #endregion

        #region Vertex properties

        /// <summary>
        /// The type property
        /// </summary>
        public const String _Type = "Type";

        /// <summary>
        /// The name property
        /// </summary>
        public const String _Name = "Name";

        /// <summary>
        /// The Url property
        /// </summary>
        public const String _Url = "Url";

        #endregion

        #region TagLabel

        /// <summary>
        /// The tag label
        /// </summary>
        public const String _TagLabel = "tag";

        #endregion


        #region Run()

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public static void Start()
        {
            new TagExample().Run();
        }

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public void Run()
        {

            // Create a new simple property graph
            var _graph = GraphFactory.CreateGenericPropertyGraph(1);


            // Add tags
            var _good  = _graph.AddVertex(v => v.SetProperty(_Type, VertexType.Tag).
                                                 SetProperty(_Name, "good"));

            var _funny = _graph.AddVertex(v => v.SetProperty(_Type, VertexType.Tag).
                                                 SetProperty(_Name, "funny"));

            // Add websites
            var _cnn   = _graph.AddVertex(v => v.SetProperty(_Type, VertexType.Website).
                                                 SetProperty(_Name, "CNN").
                                                 SetProperty(_Url,  "http://cnn.com"));

            var _xkcd  = _graph.AddVertex(v => v.SetProperty(_Type, VertexType.Website).
                                                 SetProperty(_Name, "xkcd").
                                                 SetProperty(_Url,  "http://xkcd.com"));

            var _onion = _graph.AddVertex(v => v.SetProperty(_Type, VertexType.Website).
                                                 SetProperty(_Name, "onion").
                                                 SetProperty(_Url,  "http://theonion.com"));

            // Add edges
            var _edge1 = _graph.AddEdge(_cnn,   _good,  _TagLabel);
            var _edge2 = _graph.AddEdge(_xkcd,  _good,  _TagLabel);
            var _edge3 = _graph.AddEdge(_xkcd,  _funny, _TagLabel);
            var _edge4 = _graph.AddEdge(_onion, _funny, _TagLabel);



            // Find out which tags xkcd is tagged with
            foreach (var _TagName in _xkcd.OutEdges(_TagLabel).InV().Prop(_Name))
                Console.WriteLine(_TagName);



            // List tagged sites using LINQ
            var _WebList = from   Website
                           in     _graph.Vertices(v => v.Contains(_Type, VertexType.Website))
                           select new
                           {
                               Name  = Website.GetProperty(_Name),
                               Count = Website.OutDegree(_TagLabel)
                           };

            foreach (var _Site in _WebList)
                Console.WriteLine("{0}\t=> {1}", _Site.Name, _Site.Count);


            // List tagged sites 
            _graph.Vertices(v => v.Contains(_Type, VertexType.Website)).
                   ForEach (v => Console.WriteLine("{0}\t=> {1}",
                                                   v.GetProperty(_Name),
                                                   v.OutDegree  (_TagLabel)));

        }

        #endregion

    }

}
