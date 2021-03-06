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
using System.Linq;
using System.Threading;
using System.Collections.Generic;

using eu.Vanaheimr.Illias.Commons.Collections;
using eu.Vanaheimr.Balder;
using eu.Vanaheimr.Hermod.Datastructures;
using eu.Vanaheimr.Bifrost.HTTP.Server;
using eu.Vanaheimr.Balder.UnitTests;
using eu.Vanaheimr.Balder.InMemory;
using eu.Vanaheimr.Illias.Commons.Votes;

#endregion

namespace de.ahzf.Bragi
{

    /// <summary>
    /// A small demo for distributed graph processing.
    /// </summary>
    public class NetworkingDemo : ITutorial
    {

        #region Name

        /// <summary>
        /// The name of the tutorial.
        /// </summary>
        public String Name
        {
            get
            {
                return "NetworkingDemo";
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

        #region NetworkingDemo()

        /// <summary>
        /// A small demo for distributed graph processing.
        /// </summary>
        public NetworkingDemo()
        { }

        #endregion

        #endregion


        #region Run()

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public static void Start()
        {
            new NetworkingDemo().Run();
        }

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public void Run()
        {

            var g1 = DemoGraphFactory.Create();

            var g2 = g1.AsReadOnly();


            using (var GraphServer = new BifrostHTTPServer(new IPPort(8080), (id, descr, init) => GraphFactory.CreateGenericPropertyGraph_WithStringIds(id, descr, () => new VetoVote(), init))
            {
                ServerName = "Vanaheimr Bifrost HTTP Server v0.1"
            } as IBifrostHTTPServer)
            {

                GraphServer.CreateNewGraph("123", "The first graph");

                var graph = GraphServer.CreateNewGraph("512", "demo graph", g => g.SetProperty(GraphDBOntology.Description.Suffix, "the second graph").SetProperty("hello", "world!").SetProperty("graphs", "are cool!"));
                var a1 = graph.ContainsKey("hello");
                var a2 = graph.ContainsKey("world!");
                var a3 = graph.ContainsKey("graphs");
                var a4 = graph.ContainsKey("are cool!");
                var a5 = graph.ContainsKey(GraphDBOntology.Description.Suffix);

                var c1 = graph.ContainsValue(123UL);

                var t = false;
                graph.UseProperty("Id", success => t = true);
                var ii = "i = " + t;

                var b1 = graph.Contains("Id", 123UL);
                var b2 = graph is IProperties<String, Object>;

                var aa = graph.GetProperties(null);

                var deleted3 = graph.Remove().ToList();
                var deleted1 = graph.Remove("hello");

                // ---------------------------------------------------------------

                var v1 = graph.AddVertex(v => v.SetProperty("Name", "Vertex1"));
                var v2 = graph.AddVertex(v => v.SetProperty("Name", "Vertex2"));
                var e1 = graph.AddEdge(v1, v2, "knows", e => e.SetProperty("Name", "Edge1"));

                var allV = graph.Vertices().ToList();
                var allE = graph.Edges().ToList();


                // ---------------------------------------------------------------


                //var HTTPClient1 = new HTTPClient(IPv4Address.Parse("127.0.0.1"), new IPPort(8080));
                //var _request1 = HTTPClient1.GET("/").//AccountId/RepositoryId/TransactionId/GraphId/VerticesById?Id=2&Id=3").
                //                              SetProtocolVersion(HTTPVersion.HTTP_1_1).
                //                              SetUserAgent("Hermod HTTP Client v0.1").
                //                              SetConnection("keep-alive").
                //                              AddAccept(HTTPContentType.JSON_UTF8, 1);

                //HTTPClient1.Execute(_request1, response => Console.WriteLine(response.Content.ToUTF8String()));

                //// ---------------------------------------------------------------

                //var HTTPClient2 = new HTTPClient(IPv4Address.Parse("127.0.0.1"), new IPPort(8080));
                //var _request2 = HTTPClient2.GET("/123/description").//AccountId/RepositoryId/TransactionId/GraphId/VerticesById?Id=2&Id=3").
                //                              SetProtocolVersion(HTTPVersion.HTTP_1_1).
                //                              SetUserAgent("Hermod HTTP Client v0.1").
                //                              SetConnection("keep-alive").
                //                              AddAccept(HTTPContentType.JSON_UTF8, 1);

                //HTTPClient2.Execute(_request2, response => Console.WriteLine(response.Content.ToUTF8String()));

                // ---------------------------------------------------------------
				
//#if !__MonoCS__				
				
//                var JavaScriptEngine = new Jurassic.ScriptEngine();
//                //Console.WriteLine(engine.Evaluate("5 * 10 + 2"));
//                //engine.SetGlobalValue("interop", 15);
//                //engine.ExecuteFile(@"c:\test.js");
//                //engine.Evaluate("interop = interop + 5");
//                //Console.WriteLine(engine.GetGlobalValue<int>("interop"));

//                JavaScriptEngine.Evaluate("function VertexFilter(vertex) { return vertex.Name == 'Vertex1' }");

//                foreach (var Vertex in graph.Vertices())
//                {
//                    //engine.SetGlobalValue("vertex", new JSPropertyVertex(_vv, engine));

//                    //engine.SetGlobalFunction("test", new Func<int, int, int>((a, b) => a + b));
//                    //Console.WriteLine(engine.Evaluate<int>("test(5, 6)"));

//                    //engine.Evaluate("var yesorno = vertex.Id > 1");
//                    //var Id      = engine.GetGlobalValue  ("vertex.Id");
//                    //var yesorno = engine.GetGlobalValue<Boolean>("yesorno");

//                    if (JavaScriptEngine.CallGlobalFunction<Boolean>("VertexFilter", new JSPropertyVertex(Vertex, JavaScriptEngine)))
//                        Console.WriteLine(Vertex.Id);

//                }

//                var aaa = from   V2
//                          in     graph.Vertices()
//                          where  JavaScriptEngine.CallGlobalFunction<Boolean>("VertexFilter", new JSPropertyVertex(V2, JavaScriptEngine))
//                          select V2;

//                var aaaa = aaa.ToList();


//                var GraphClient = new RemotePropertyGraph(IPv4Address.Parse("127.0.0.1"), new IPPort(8080)) { Id = 512 };
//                Console.WriteLine(GraphClient.Description);

//                //foreach (var V3 in GraphClient.Vertices())
//                //    Console.WriteLine(V3.Id);
//#endif
                while (true)
                    Thread.Sleep(100);

            }

        }

        #endregion


    }

}
