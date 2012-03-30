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

#endregion

namespace de.ahzf.Bragi
{

    /// <summary>
    /// The TagExample.
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
        /// The TagExample.
        /// </summary>
        public TagExample()
        { }

        #endregion


        #region Vertex labels

        /// <summary>
        /// The vertex labels.
        /// </summary>
        public enum VertexLabel
        {

            /// <summary>
            /// The "tag" vertex label.
            /// </summary>
            Tag,

            /// <summary>
            /// The "website" vertex label.
            /// </summary>
            Website

        }

        #endregion

        #region Vertex properties

        /// <summary>
        /// The type property
        /// </summary>
        public const String _type = "type";

        /// <summary>
        /// The name property
        /// </summary>
        public const String _name = "name";

        /// <summary>
        /// The url property
        /// </summary>
        public const String _url  = "url";

        #endregion

        #region Edge labels

        /// <summary>
        /// The "isTagged" edge label.
        /// </summary>
        public const String _isTaggedWith = "isTaggedWith";

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
            var _good  = _graph.AddVertex(v => v.SetProperty(_type, VertexLabel.Tag).
                                                 SetProperty(_name, "good"));

            var _funny = _graph.AddVertex(v => v.SetProperty(_type, VertexLabel.Tag).
                                                 SetProperty(_name, "funny"));

            // Add websites
            var _cnn   = _graph.AddVertex(v => v.SetProperty(_type, VertexLabel.Website).
                                                 SetProperty(_name, "CNN").
                                                 SetProperty(_url,  "http://cnn.com"));

            var _xkcd  = _graph.AddVertex(v => v.SetProperty(_type, VertexLabel.Website).
                                                 SetProperty(_name, "xkcd").
                                                 SetProperty(_url,  "http://xkcd.com"));

            var _onion = _graph.AddVertex(v => v.SetProperty(_type, VertexLabel.Website).
                                                 SetProperty(_name, "onion").
                                                 SetProperty(_url,  "http://theonion.com"));

            // Add edges using the semantic web style (s, p, o)
            var _edge1 = _graph.AddEdge(_cnn,   _isTaggedWith, _good);
            var _edge2 = _graph.AddEdge(_xkcd,  _isTaggedWith, _good);
            var _edge3 = _graph.AddEdge(_xkcd,  _isTaggedWith, _funny);
            var _edge4 = _graph.AddEdge(_onion, _isTaggedWith, _funny);



            // Find out which tags xkcd is tagged with
            _xkcd.                                      // The xkcd vertex
               OutEdges(_isTaggedWith).                 // Get all outedges with label "isTaggedWith"
               InV().                                   // Get the incoming vertices of the edges
               Prop(_name).                             // Get all properties with key "name"
               ForEach(Tag => Console.WriteLine(Tag));  // 



            // List tagged sites
            _graph.Vertices(v => v.Contains(_type, VertexLabel.Website)).
                   ForEach (v => Console.WriteLine("{0}\t=> {1}",
                                                   v.GetProperty(_name),
                                                   v.OutDegree  (_isTaggedWith)));


            // List tagged sites using LINQ
            var _WebList = from   Website
                           in     _graph.Vertices(v => v.Contains(_type, VertexLabel.Website))
                           select new
                           {
                               Name  = Website.GetProperty(_name),
                               Count = Website.OutDegree(_isTaggedWith)
                           };

            foreach (var _Site in _WebList)
                Console.WriteLine("{0}\t=> {1}", _Site.Name, _Site.Count);


        }

        #endregion

    }

}
