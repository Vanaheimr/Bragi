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

using de.ahzf.Balder;
using de.ahzf.Blueprints;
using de.ahzf.Blueprints.Indices;

#endregion

namespace de.ahzf.Bragi
{

    /// <summary>
    /// The Bragi tutorials
    /// </summary>
    public class Bragi
    {

        public static void Main(String[] Args)
        {

            //TagExample.Start();
            ConcurrencyDemo.Start();
            //var _Tutorial2 = new SmallBenchmark();

        }

    }

}
