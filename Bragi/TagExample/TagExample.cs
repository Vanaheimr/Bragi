/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Balder <http://www.github.com/ahzf/Balder>
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

using de.ahzf.Balder;
using de.ahzf.Blueprints.PropertyGraphs;
using de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable;

#endregion

namespace de.ahzf.Tutorials
{

    /// <summary>
    /// The TagExample
    /// </summary>
    public class TagExample
    {

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


        #region TagExample()

        public TagExample()
        {

            // Create a new simple property graph
            var _graph = new PropertyGraph();


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



            // List tagged sites
            var _WebList = from   Website
                           in     _graph.V(v => { return VertexType.Website == v.GetPropertyCasted<String, Object, VertexType>(_Type); })
                           select new
                           {
                               Name  = Website.GetProperty(_Name),
                               Count = Website.OutEdges(_TagLabel).Count()
                           };

            foreach (var _Site in _WebList)
                Console.WriteLine("{0} => {1}", _Site.Name, _Site.Count);

        }

        #endregion

    }

}
