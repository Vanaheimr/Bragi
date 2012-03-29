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
using System.Collections.Generic;

using de.ahzf.Illias;

#endregion

namespace de.ahzf.Bragi
{

    /// <summary>
    /// A quadstore demo
    /// </summary>
    public class QuadStoreDemo : ITutorial
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
                return "QuadStoreDemo";
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
                    Keyword.Quad,
                    Keyword.QuadStore,
                    Keyword.RDF
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
                return "Shows you how to setup and use a simple QuadStore.";
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        /// <summary>
        /// A quadstore demo
        /// </summary>
        public QuadStoreDemo()
        { }

        #endregion


        #region Run()

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public static void Start()
        {
            new QuadStoreDemo().Run();
        }

        /// <summary>
        /// Run the tutorial.
        /// </summary>
        public void Run()
        {

            IQuad<String, String, String, String> s1, s2, s3, s4, s5;

            var _QuadStore = new QuadStore<String, String, String, String>(
                                     SystemId:        "23/05",
                                     QuadIdConverter: (QuadId) => QuadId.ToString(),
                                     DefaultContext:  () => "0");

            // Note: Add repositories!

            using (var _Transaction = _QuadStore.BeginTransaction())
            {

                using (var _NestedTransaction = _Transaction.BeginNestedTransaction())
                {
                    s1 = _QuadStore.Add("Alice", "knows", "Bob");
                    _NestedTransaction.Commit();
                }

                s2 = _QuadStore.Add("Alice", "knows", "Dave");
                s3 = _QuadStore.Add("Bob",   "knows", "Carol");
                s4 = _QuadStore.Add("Eve",   "loves", "Alice");
                s5 = _QuadStore.Add("Carol", "loves", "Alice");

                _Transaction.Commit();

            }


            var q1 = _QuadStore.GetQuad(s2.QuadId);

        }

        #endregion

    }

}
