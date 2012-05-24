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
using System.Windows;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Vanaheimr.Bragi.LokiWPFGraphs
{

    public class GraphProperties
    {
        public String Key   { get; set; }       
        public Object Value { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// The main window
        /// </summary>
        public MainWindow()
        {

            InitializeComponent();

            #region Customize the vertex and edge captions

            // Vertices caption
            GraphCanvas.VertexCaption = v =>
            {
                Object Name;
                if (v.TryGetProperty("Name", out Name))
                    return Name as String;
                else
                    return v.Id.ToString();
            };

            #endregion

            var Graph = GraphCanvas.Graph;

            var Alice = Graph.AddVertex(v => v.SetProperty("Name", "Alice"));
            var Bob   = Graph.AddVertex(v => v.SetProperty("Name", "Bob"));
            var Carol = Graph.AddVertex(v => v.SetProperty("Name", "Carol"));
            var Dave  = Graph.AddVertex(v => v.SetProperty("Name", "Dave"));

            var e1    = Graph.AddEdge(Alice, "friends", Bob);
            var e2    = Graph.AddEdge(Bob,   "friends", Carol);
            var e3    = Graph.AddEdge(Alice, "friends", Carol);
            var e4    = Graph.AddEdge(Carol, "friends", Dave);

            #region Customize the vertex and edge tooltips

            // Vertices ToolTip
            GraphCanvas.VertexToolTip = v =>
            {
                Object Name;
                if (v.TryGetProperty("Name", out Name))
                    return Name as String;
                else
                    return v.Id.ToString();
            };

            // Edges ToolTip
            GraphCanvas.EdgeToolTip = e => e.Label;

            #endregion

            PropertiesDataGrid.ItemsSource = LoadCollectionData();

        }

        private List<GraphProperties> LoadCollectionData()
        {
            
            var _Properties = new List<GraphProperties>();

            _Properties.Add(new GraphProperties()
            {
                Key   = "Name",
                Value = "Alice",
            });

            _Properties.Add(new GraphProperties()
            {
                Key   = "Alter",
                Value = 23,
            });

            _Properties.Add(new GraphProperties()
            {
                Key   = "Lieblingsfarbe",
                Value = "Blau",
            });

            return _Properties;

        }

    }

}
