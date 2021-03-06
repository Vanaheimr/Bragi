﻿/*
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
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

using eu.Vanaheimr.Balder.InMemory;
using eu.Vanaheimr.Balder;

#endregion

namespace de.ahzf.Bragi
{

    /// <summary>
    /// A simple demo implementing a social graph.
    /// </summary>
    public class SocialGraphDemo
    {

        #region Data

        private const Int32  _NumberOfVertices = 100000;
        private const Int32  _NumberOfEdges    = 500000;
        private const String _FileName         = "SocialGraph_v100000-e500000.csv";

        #endregion

        #region Constructor(s)

        #region SocialGraphDemo(myArgs)

        /// <summary>
        /// Main routine!
        /// </summary>
        /// <param name="myArgs">The command line arguments.</param>
        public SocialGraphDemo(String[] myArgs)
        {

            //var _Graph = new SimplePropertyGraph();
            //var _Vertex1 = _Graph.AddVertex(5, v => v.SetProperty("key1", "value1").
            //                                          SetProperty("key2", 42));
            //var _Vertex2 = _Graph.AddVertex(23);

            //var b = _Vertex1.CompareTo(_Vertex2);

            //var x = _Vertex1.AsDynamic().key1;

            //_Vertex.GetProperty(

            //var aa = new List<Double>() { 1, 3 };
            //var bb = aa.Sum(_ => _ * _);


            //var _g      = TinkerGraphFactory.CreateTinkerGraph();
            //var _graph  = DemoGraphFactory.CreateDemoGraph();

            //var _index1 = _graph.CreateVerticesIndex("IdxNames",
            //                                         "DictionaryIndex",
            //                                         e => e["name"].ToString().ToLower() +
            //                                              e["age"].ToString(),
            //                                         e => Indexing.HasKeys(e, "name", "age"));

            //var _index2 = _graph.CreateVerticesIndex<Int32>("IdxAges",
            //                                                "DictionaryIndex",
            //                                                e => (Int32) e["age"],
            //                                                e => Indexing.HasKeys(e, "age"));

            //var _Idx = _graph.VerticesIndices().First();
            //_Idx.Insert(_graph.Vertices());
            //_index2.Insert(_graph.Vertices());

            ////var x = _Idx.As();
            //var y = _Idx.Equals("alice18").ToList();
            //var z = _Idx.Equals(18).ToList();


            ////_Idx.GetType().ContainsGenericParameters

            //var m = _index2.Equals(18).First();
            //var n = _index2.LargerThan(20).ToList();
            //foreach (var nn in n)
            //    if (((Int32) nn.GetProperty("age")) <= 20)
            //        throw new Exception("Mist!");

            //var n2 = _index1.LargerThan(20).ToList();

            // Create SocialGraph, if not existant!
            if (!File.Exists(_FileName))
                GenerateSocialGraph();


            // Create an in-memory graph using reflection
            //IPropertyGraph _SocialGraph = null;
            //var _AD = new AutoDiscovery<IPropertyGraph>(true);
            //if (!_AD.TryActivate("InMemoryPropertyGraph", out _SocialGraph))
            //{
            //    Console.WriteLine("Could not find the 'InMemoryGraph' implementation!");
            //    Environment.Exit(1);
            //}
            var _SocialGraph = GraphFactory.CreateGenericPropertyGraph(1);


            // Import vertices and edges
            ImportVertices(_SocialGraph);
            ImportEdges(_SocialGraph);


            var all1 = _SocialGraph.NumberOfVertices();
            var all2 = _SocialGraph.Vertices(v => v.Id > 10).Count();

            Console.ReadLine();

        }

        #endregion

        #endregion

        #region Generate a simple SocialGraph and write it into a CSV-file

        private void GenerateSocialGraph()
        {

            var _Stopwatch = new Stopwatch();

            Console.WriteLine("Generating {0} vertices having {1} edges...", _NumberOfVertices, _NumberOfEdges);
            _Stopwatch.Start();

            var _SocialGraph = SocialGraphGenerator.Generate(_NumberOfVertices,
                                                             _NumberOfEdges,
                                                             PreferentialAttachment: 3,
                                                             BatchNumber: 5000,
                                                             BatchAction: status =>
                                                                 {
                                                                     Console.SetCursorPosition(0, Console.CursorTop);
                                                                     Console.Write(status);
                                                                 });

            Console.WriteLine();
            Console.WriteLine("Time: {0}:{1:00} min", _Stopwatch.Elapsed.Minutes, _Stopwatch.Elapsed.Seconds);
            Console.WriteLine("Writing data to file...");

            var Histogram = SocialGraphGenerator.Histogram(_SocialGraph);

            _Stopwatch.Restart();
            //CSV.WriteToFile(_SocialGraph, _FileName);

            Console.WriteLine("Time: {0}:{1:00} min", _Stopwatch.Elapsed.Minutes, _Stopwatch.Elapsed.Seconds);

        }

        #endregion

        #region Import vertices

        private void ImportVertices(IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                          UInt64, Int64, String, String, Object,
                                                          UInt64, Int64, String, String, Object,
                                                          UInt64, Int64, String, String, Object> SocialGraph)
        {

            var _Stopwatch = new Stopwatch();
            _Stopwatch.Start();

            //CSV.ParseFile(_FileName, _CSVLine =>
            //                              {
            //                                  SocialGraph.AddVertex(UInt64.Parse(_CSVLine[0]));
            //                              }).Wait();

            _Stopwatch.Stop();

            Console.WriteLine("Vertex import: {0}:{1:00} min", _Stopwatch.Elapsed.Minutes, _Stopwatch.Elapsed.Seconds);


        }

        #endregion

        #region Import edges

        private void ImportEdges(IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                       UInt64, Int64, String, String, Object,
                                                       UInt64, Int64, String, String, Object,
                                                       UInt64, Int64, String, String, Object> SocialGraph)
        {

            var _Stopwatch = new Stopwatch();
            _Stopwatch.Start();

            //CSV.ParseFile(_FileName, _CSVLine =>
            //                              {

            //                                  var _VertexId0 = UInt64.Parse(_CSVLine[0]);

            //                                  for (var i = 1; i < _CSVLine.Count(); i++)
            //                                      SocialGraph.AddEdge(
            //                                          SocialGraph.VertexById(_VertexId0),
            //                                          SocialGraph.VertexById(UInt64.Parse(_CSVLine[i]))
            //                                      );

            //                              }).Wait();

            _Stopwatch.Stop();

            Console.WriteLine("Edge import: {0}:{1:00} min", _Stopwatch.Elapsed.Minutes, _Stopwatch.Elapsed.Seconds);

        }

        #endregion

    }

}
