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

using de.ahzf.Blueprints;
using de.ahzf.Blueprints.Schema;
using de.ahzf.Blueprints.PropertyGraphs;

using de.ahzf.Balder;

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
        /// A vertex is a tag.
        /// </summary>
        public const String isTag     = "Tag";

        /// <summary>
        /// A vertex is a user.
        /// </summary>
        public const String isUser    = "User";

        /// <summary>
        /// A vertex is a website.
        /// </summary>
        public const String isWebsite = "Website";

        #endregion

        #region Vertex properties

        /// <summary>
        /// The url property
        /// </summary>
        public const String Url  = "Url";

        #endregion

        #region Edge labels

        /// <summary>
        /// An edge to tag users and websites.
        /// </summary>
        public const String isTaggedWith = "isTaggedWith";

        /// <summary>
        /// An edge to like users and websites.
        /// </summary>
        public const String likes        = "likes";

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
            var graph       = GraphFactory.CreateGenericPropertyGraph2("TagExample");
            var graphSchema = graph.GetGraphSchema("TagExampleSchema", "The graph schema of the TagExample graph");


            // Add tags
            var _good  = graph.AddVertex("good",  isTag);
            var _funny = graph.AddVertex("funny", isTag);

            // Add Users
            var _Alice = graph.AddVertex("Alice", "User");
            var _Bob   = graph.AddVertex("Bob",   "User");

            // Add websites
            var _cnn   = graph.AddVertex("CNN",   isWebsite, v => v.SetProperty(Url, "http://cnn.com"));
            var _xkcd  = graph.AddVertex("xkcd",  isWebsite, v => v.SetProperty(Url, "http://xkcd.com"));
            var _onion = graph.AddVertex("onion", isWebsite, v => v.SetProperty(Url, "http://theonion.com"));

            // Add edges using the semantic web style (s, p, o)
            var _edge1 = graph.AddEdge(_cnn,   isTaggedWith, _good);
            var _edge2 = graph.AddEdge(_xkcd,  isTaggedWith, _good);
            var _edge3 = graph.AddEdge(_xkcd,  isTaggedWith, _funny);
            var _edge4 = graph.AddEdge(_onion, isTaggedWith, _funny);

            var _edge5 = graph.AddEdge(_Alice, likes, _xkcd);
            var _edge6 = graph.AddEdge(_Alice, likes, _Bob);


            // Find out which tags xkcd is tagged with
            _xkcd.                                      // The xkcd vertex
               OutEdges(isTaggedWith).                  // Get all outedges with label "isTaggedWith"
               InV().                                   // Get the incoming vertices of the edges
               Ids().                                   // Get all vertex Ids
               ForEach(Tag => Console.WriteLine(Tag));


            // List tagged sites
            graph.Vertices(v => v.Label == isWebsite).
                  ForEach (v => Console.WriteLine("{0}\t=> {1}",
                                                  v.Id,
                                                  v.OutDegree(isTaggedWith)));


            // List tagged sites using LINQ
            var _WebList = from   Website
                           in     graph.Vertices(v => v.Label == isWebsite)
                           select new
                           {
                               Name  = Website.Id,
                               Count = Website.OutDegree(isTaggedWith)
                           };

            foreach (var _Site in _WebList)
                Console.WriteLine("{0}\t=> {1}", _Site.Name, _Site.Count);


            Console.WriteLine();
            Console.WriteLine("Used vertex labels: ");
            Console.WriteLine("  " + graphSchema.Vertices().Ids().Cast<String>().Aggregate((a, b) => a + ", " + b));
            Console.WriteLine();

            Console.WriteLine("Used edge labels: ");
            graphSchema.Edges().ForEach(e => {
                Console.WriteLine("  " + e.Id + ": " + e.SetGet(GraphSchemaHandling.AlternativeUsage).Cast<String>().Aggregate((a, b) => a + ", " + b));
            });

        }

        #endregion

    }

}
