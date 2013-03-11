/*
 * Copyright (c) 2011-2013 Achim 'ahzf' Friedland <achim@ahzf.de>
 * This file is part of Loki <http://www.github.com/Vanaheimr/Loki>
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 * 
 * You may obtain a copy of the License at
 *     http://www.gnu.org/licenses/gpl.html
 *     
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * General Public License for more details.
 */

#region Usings

using System;
using System.Linq;

using eu.Vanaheimr.Hermod.HTTP;
using eu.Vanaheimr.Hermod.Datastructures;
using System.Threading.Tasks;
using System.Threading;
using de.ahzf.Bragi;
using System.Collections.Generic;
using de.ahzf.Loki.HTML5;

#endregion

namespace de.ahzf.Bragi
{

    /// <summary>
    /// A Loki HTML5 visualization demo using sigma.js.
    /// </summary>
    public class Sigma_js : ITutorial
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
                return "Sigma.js";
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
                    Keyword.Visualization,
                    Keyword.HTML5,
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
                return "A small visualization demo.";
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        /// <summary>
        /// A Loki HTML5 visualization demo using sigma.js.
        /// </summary>
        public Sigma_js()
        { }

        #endregion


        #region Run()

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public static void Start()
        {
            new Sigma_js().Run();
        }

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public void Run()
        {

            #region Start the HTTPServer

            var _HTTPServer = new HTTPServer<ILokiHTML5Service>(IPv4Address.Any, IPPort.HTTP, Autostart: true) {
                ServerName = "Vanaheimr Loki HTML5 Visualization Demo"
            };

            Console.WriteLine(_HTTPServer);

            #endregion

            var _Random                  = new Random();
            var _CancellationTokenSource = new CancellationTokenSource();
            var _EventSource             = _HTTPServer.URLMapping.EventSource("GraphEvents");

            var _SubmitTask = Task.Factory.StartNew(() =>
            {

                while (!_CancellationTokenSource.IsCancellationRequested)
                {
                    _EventSource.SubmitSubEvent("vertexadded", "{\"radius\": " + _Random.Next(5, 50) + ", \"x\": " + _Random.Next(50, 550) + ", \"y\": ",
                                                                                 _Random.Next(50, 350) + "}");
                    Thread.Sleep(1000);
                }

            },

            _CancellationTokenSource.Token,
            TaskCreationOptions.LongRunning,
            TaskScheduler.Default);


            Console.ReadLine();

            _CancellationTokenSource.Cancel();

            Console.WriteLine("done!");

        }

        #endregion

    }

}
