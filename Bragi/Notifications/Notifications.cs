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
using System.Threading;
using System.Collections.Generic;

using de.ahzf.Illias.Commons;
using de.ahzf.Illias.Commons.Collections;
using de.ahzf.Vanaheimr.Blueprints.InMemory;
using de.ahzf.Vanaheimr.Hermod.Datastructures;
using de.ahzf.Vanaheimr.Hermod.Multicast;
using de.ahzf.Vanaheimr.Balder;
using de.ahzf.Vanaheimr.Walkyr.GraphML;
using de.ahzf.Vanaheimr.Styx;
using de.ahzf.Vanaheimr.Blueprints;
using System.Threading.Tasks;
using de.ahzf.Illias.Commons.Votes;


#endregion

namespace de.ahzf.Bragi
{

    public class NGraph
    {

        private INotificator<String>  NewDing;
        public  INotification<String> OnVertexAdded { get { return NewDing; } }

        public NGraph()
        {
            this.NewDing = new Notificator<String>();
        }

        public void Shoot(String Msg)
        {
            NewDing.SendNotification(Msg);
        }

    }


    public class StringLogger : ITarget<String>
    {

        public void ProcessNotification(String Message)
        {
            Console.WriteLine(Message);
        }

        public void ProcessError(dynamic Sender, Exception ExceptionMessage)
        {
            Console.WriteLine(ExceptionMessage);
        }

        public void ProcessCompleted(dynamic Sender, String Message)
        {
            Console.WriteLine(Message);
        }

    }



    public class Target<T> : ITarget<T>
    {

        public void ProcessNotification(T Message)
        {
            Console.WriteLine("Target<T>" + Message);
        }

        public void ProcessError(dynamic Sender, Exception ExceptionMessage)
        { }

        public void ProcessCompleted(dynamic Sender, String Message)
        { }

    }

    public class Target<T1, T2> : ITarget<T1, T2>
    {

        public void ProcessNotification(T1 Message1, T2 Message2)
        {
            Console.WriteLine("Target<T1, T2>" + Message1 + " / " + Message2);
        }

        public void ProcessError(dynamic Sender, Exception ExceptionMessage)
        { }

        public void ProcessCompleted(dynamic Sender, String Message)
        { }

    }

    public class VotingTarget<T1, T2, V> : IVotingTarget<T1, T2, V>
    {

        public V ProcessVoting(T1 Message1, T2 Message2, IVote<V> Vote)
        {
            Console.WriteLine("VotingTarget<T1, T2>" + Message1 + " / " + Message2 + " => true");
            return default(V);
        }

        public void ProcessNotification(T1 Message1, T2 Message2)
        {
            Console.WriteLine("Target<T1, T2>" + Message1 + " / " + Message2);
        }

        public void ProcessError(dynamic Sender, Exception ExceptionMessage)
        { }

        public void ProcessCompleted(dynamic Sender, String Message)
        { }

    }


    public class StringLogger2
    {

        private Target<String> T2;

        public ITarget<String> ImATarget { get { return T2; } }

        public StringLogger2()
        {
            this.T2 = new Target<String>();
        }

    }


    public class GraphLogger<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

        where TIdVertex        : IEquatable<TIdVertex>,       IComparable<TIdVertex>,       IComparable, TValueVertex
        where TIdEdge          : IEquatable<TIdEdge>,         IComparable<TIdEdge>,         IComparable, TValueEdge
        where TIdMultiEdge     : IEquatable<TIdMultiEdge>,    IComparable<TIdMultiEdge>,    IComparable, TValueMultiEdge
        where TIdHyperEdge     : IEquatable<TIdHyperEdge>,    IComparable<TIdHyperEdge>,    IComparable, TValueHyperEdge

        where TRevIdVertex     : IEquatable<TRevIdVertex>,    IComparable<TRevIdVertex>,    IComparable, TValueVertex
        where TRevIdEdge       : IEquatable<TRevIdEdge>,      IComparable<TRevIdEdge>,      IComparable, TValueEdge
        where TRevIdMultiEdge  : IEquatable<TRevIdMultiEdge>, IComparable<TRevIdMultiEdge>, IComparable, TValueMultiEdge
        where TRevIdHyperEdge  : IEquatable<TRevIdHyperEdge>, IComparable<TRevIdHyperEdge>, IComparable, TValueHyperEdge

        where TVertexLabel     : IEquatable<TVertexLabel>,    IComparable<TVertexLabel>,    IComparable, TValueVertex
        where TEdgeLabel       : IEquatable<TEdgeLabel>,      IComparable<TEdgeLabel>,      IComparable, TValueEdge
        where TMultiEdgeLabel  : IEquatable<TMultiEdgeLabel>, IComparable<TMultiEdgeLabel>, IComparable, TValueMultiEdge
        where THyperEdgeLabel  : IEquatable<THyperEdgeLabel>, IComparable<THyperEdgeLabel>, IComparable, TValueHyperEdge

        where TKeyVertex       : IEquatable<TKeyVertex>,      IComparable<TKeyVertex>,      IComparable
        where TKeyEdge         : IEquatable<TKeyEdge>,        IComparable<TKeyEdge>,        IComparable
        where TKeyMultiEdge    : IEquatable<TKeyMultiEdge>,   IComparable<TKeyMultiEdge>,   IComparable
        where TKeyHyperEdge    : IEquatable<TKeyHyperEdge>,   IComparable<TKeyHyperEdge>,   IComparable

    {

        private VotingTarget<IReadOnlyGenericPropertyGraph <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,
                             IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,
                             Boolean> _NewVertexAdded;

        public  IVotingTarget<IReadOnlyGenericPropertyGraph <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,
                              IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,
                              Boolean> NewVertexAdded { get { return _NewVertexAdded; } }

        public GraphLogger()
        {

            this._NewVertexAdded = new VotingTarget<IReadOnlyGenericPropertyGraph <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,
                                                    IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,
                                                    Boolean>();

        }

    }




    /// <summary>
    /// A small demo for distributed graph processing using IP multicast.
    /// </summary>
    public class NotificationsDemo : ITutorial
    {

        #region Name

        /// <summary>
        /// The name of the tutorial.
        /// </summary>
        public String Name
        {
            get
            {
                return "NotificationsDemo";
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
                    Keyword.Multicast,
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
                return "A small demo for distributed graph processing using IP multicast.";
            }
        }

        #endregion

        #region Constructor(s)

        #region NotificationsDemo()

        /// <summary>
        /// A small demo for distributed graph processing.
        /// </summary>
        public NotificationsDemo()
        { }

        #endregion

        #endregion


        #region Run()

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public static void Start()
        {
            new NotificationsDemo().Run();
        }

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public void Run()
        {


            var NGraph = new NGraph();
            NGraph.OnVertexAdded.OnNotification +=          msg  => Console.WriteLine("direct: " + msg);
            NGraph.OnVertexAdded.OnError        += (sender, ex)  => Console.WriteLine("direct: " + ex);
            NGraph.OnVertexAdded.OnCompleted    += (sender, msg) => Console.WriteLine("direct: " + msg);

            var Logger1 = new StringLogger();
            NGraph.OnVertexAdded.
                   SendTo(Logger1);

            var Logger2 = new StringLogger2();
            NGraph.OnVertexAdded.
                   NFilter(msg => !msg.Contains("edge")).
                   SendTo(Logger2.ImATarget);

            Task.Factory.StartNew(() => NGraph.OnVertexAdded.ToIEnumerable().ForEach(str => Console.WriteLine("magic!!! " + str)));

            NGraph.Shoot("Hello vertex!");
            Console.WriteLine();
            NGraph.Shoot("Hello edge!");


            var graph       = GraphFactory.CreateGenericPropertyGraph_WithStringIds("graph01");
            var graphlogger = new GraphLogger<String, Int64, String, String, Object,
                                              String, Int64, String, String, Object,
                                              String, Int64, String, String, Object,
                                              String, Int64, String, String, Object>();

            graph.OnVertexAddition.OnVoting += (g, vertex, vote) => { if (vertex.Id == "0") vote.Deny(); };
            graph.OnVertexAddition.SendTo(graphlogger.NewVertexAdded);
            graph.OnVertexAddition.NewFuncArrow((g, v) => "vertex '" + v.Id + "' on graph '" + g.Id + "' added!").SendTo(Logger1);

            Task.Factory.StartNew(() => graph.OnVertexAddition.NewFuncArrow((g, v) => v).ToIEnumerable().ForEach(v => Console.WriteLine("It's magic a vertex... " + v.Id)));

            //graph.OnVertexAdded2.NewFuncArrow((g, v) => v).AsTask(v => v.ToIEnumerable().ForEach(v2 => Console.WriteLine("It's magic23 a vertex... " + v2.Id)));

            graph.AddVertex(v => v.SetProperty("hello", "world 1!"));
            graph.AddVertex(v => v.SetProperty("hello", "world 2!"));

            while (true)
            {
                Thread.Sleep(100);
            }

        }

        #endregion


    }

}
