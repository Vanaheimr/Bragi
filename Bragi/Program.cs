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
 * limitations under the License.E
 */

#region Usings

using System;

//using Mono;
//using Mono.CSharp;
//using RunCSharp;

#endregion

namespace de.ahzf.Bragi
{

    /// <summary>
    /// All Bragi tutorials.
    /// </summary>
    public class Bragi
    {

        #region Data

        //private static Runner _Compiler;

        #endregion

        //#region (static) StartMonoCSharpREPLShell(Args)

        ///// <summary>
        ///// The Mono C# REPL Shell
        ///// </summary>
        //public static void StartMonoCSharpREPLShell(params String[] Args)
        //{

        //    #region Feel free to step through...

        //    _Compiler = new Runner();

        //    var a = _Compiler.Execute("Math.Abs(-42);");
        //    var b = _Compiler.Execute("Math.Sin(Math.PI / 6);");
        //    var c = _Compiler.Execute("class Fact { public int Run(int n) { return n <= 0 ? 1 : n*Run(n-1); } }");
        //    var d = _Compiler.Execute("new Fact().Run(5);");
        //    var e = _Compiler.Execute("\"abcdefgh\".Substring(1, 2);");
        //    var f = _Compiler.Execute("class Echo { public Object Print(Object o) { return o; } }");
        //    var g = _Compiler.Execute("var test = 123;");
        //    var h = _Compiler.Execute("new Echo().Print(test);");

        //    #endregion

        //    #region Start the interactive (read-eval-print loop) shell...

        //    var _Report = new Report(new ConsoleReportPrinter());
        //    var _CLP    = new CommandLineParser(_Report);
        //    _CLP.UnknownOptionHandler += Mono.Driver.HandleExtraArguments;

        //    var _Settings = _CLP.ParseArguments(Args);
        //    if (_Settings == null || _Report.Errors > 0)
        //        Environment.Exit(1);

        //    var _Evaluator = new Evaluator(_Settings, _Report)
        //    {
        //        InteractiveBaseClass    = typeof(InteractiveBaseShell),
        //        DescribeTypeExpressions = true
        //    };

        //    //// Adding a assembly twice will lead to delayed errors...
        //    //_Evaluator.ReferenceAssembly(typeof(YourAssembly).Assembly);
        //    _Evaluator.ReferenceAssembly(typeof(Bragi).Assembly);
        //    _Evaluator.ReferenceAssembly(typeof(IPropertyGraph).Assembly);
        //    _Evaluator.ReferenceAssembly(typeof(GraphFactory).Assembly);

        //    var u1 = _Compiler.Execute("using de.ahzf.Bragi;");
        //    var u2 = _Compiler.Execute("using de.ahzf.Blueprints;");
        //    var u3 = _Compiler.Execute("using de.ahzf.Blueprints.PropertyGraphs;");
        //    var u4 = _Compiler.Execute("using de.ahzf.Blueprints.PropertyGraphs.InMemory");

        //    var _CSharpShell = new CSharpShell(_Evaluator, "BragiShell").Run();

        //    #endregion

        //}

        //#endregion


        /// <summary>
        /// Main...
        /// </summary>
        /// <param name="Args">Arguments...</param>
        public static void Main(String[] Args)
        {

            //NotificationsDemo.Start();
            //Vanaheimr.Start();
            //MulticastDemo.Start();
            PartitionGraphs.Start();
            //TraversalGraphs.Start();
            //NetworkingDemo.Start();
            //TagExample.Start();
            //PolyfileReader.Start();

            //StartMonoCSharpREPLShell();

            // Try to type:
            // "using de.ahzf.Bragi;"
            //
            // and then one of the following...
            //
            // "TagExample.Start();"
            // "ConcurrencyDemo.Start();"
            // "NetworkingDemo.Start();"
            // "SmallBenchmark.Start();"

        }

    }

}
