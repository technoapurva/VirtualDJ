﻿#pragma checksum "..\..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "58639D82FC3FBADFC2B46677E26CA18C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WPFSoundVisualizationLib;


namespace Sample_NAudio {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 30 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem OpenFileMenuItem;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem CloseMenuItem;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem DefaultThemeMenuItem;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem ExpressionDarkMenuItem;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem ExpressionLightMenuItem;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WPFSoundVisualizationLib.DigitalClock clockDisplay;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WPFSoundVisualizationLib.AlbumArtDisplay albumArtPanel;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WPFSoundVisualizationLib.SpectrumAnalyzer spectrumAnalyzer;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WPFSoundVisualizationLib.WaveformTimeline waveformTimeline;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WPFSoundVisualizationLib.TimeEditor repeatStartTimeEdit;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WPFSoundVisualizationLib.TimeEditor repeatStopTimeEdit;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FileText;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BrowseButton;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PlayButton;
        
        #line default
        #line hidden
        
        
        #line 127 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PauseButton;
        
        #line default
        #line hidden
        
        
        #line 135 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button StopButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Sample_NAudio;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.OpenFileMenuItem = ((System.Windows.Controls.MenuItem)(target));
            
            #line 31 "..\..\..\MainWindow.xaml"
            this.OpenFileMenuItem.Click += new System.Windows.RoutedEventHandler(this.OpenFileMenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CloseMenuItem = ((System.Windows.Controls.MenuItem)(target));
            
            #line 35 "..\..\..\MainWindow.xaml"
            this.CloseMenuItem.Click += new System.Windows.RoutedEventHandler(this.CloseMenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.DefaultThemeMenuItem = ((System.Windows.Controls.MenuItem)(target));
            
            #line 41 "..\..\..\MainWindow.xaml"
            this.DefaultThemeMenuItem.Checked += new System.Windows.RoutedEventHandler(this.DefaultThemeMenuItem_Checked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ExpressionDarkMenuItem = ((System.Windows.Controls.MenuItem)(target));
            
            #line 45 "..\..\..\MainWindow.xaml"
            this.ExpressionDarkMenuItem.Checked += new System.Windows.RoutedEventHandler(this.ExpressionDarkMenuItem_Checked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ExpressionLightMenuItem = ((System.Windows.Controls.MenuItem)(target));
            
            #line 49 "..\..\..\MainWindow.xaml"
            this.ExpressionLightMenuItem.Checked += new System.Windows.RoutedEventHandler(this.ExpressionLightMenuItem_Checked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.clockDisplay = ((WPFSoundVisualizationLib.DigitalClock)(target));
            return;
            case 7:
            this.albumArtPanel = ((WPFSoundVisualizationLib.AlbumArtDisplay)(target));
            return;
            case 8:
            this.spectrumAnalyzer = ((WPFSoundVisualizationLib.SpectrumAnalyzer)(target));
            return;
            case 9:
            this.waveformTimeline = ((WPFSoundVisualizationLib.WaveformTimeline)(target));
            return;
            case 10:
            this.repeatStartTimeEdit = ((WPFSoundVisualizationLib.TimeEditor)(target));
            return;
            case 11:
            this.repeatStopTimeEdit = ((WPFSoundVisualizationLib.TimeEditor)(target));
            return;
            case 12:
            this.FileText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 13:
            this.BrowseButton = ((System.Windows.Controls.Button)(target));
            
            #line 118 "..\..\..\MainWindow.xaml"
            this.BrowseButton.Click += new System.Windows.RoutedEventHandler(this.BrowseButton_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.PlayButton = ((System.Windows.Controls.Button)(target));
            
            #line 124 "..\..\..\MainWindow.xaml"
            this.PlayButton.Click += new System.Windows.RoutedEventHandler(this.PlayButton_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.PauseButton = ((System.Windows.Controls.Button)(target));
            
            #line 132 "..\..\..\MainWindow.xaml"
            this.PauseButton.Click += new System.Windows.RoutedEventHandler(this.PauseButton_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            this.StopButton = ((System.Windows.Controls.Button)(target));
            
            #line 140 "..\..\..\MainWindow.xaml"
            this.StopButton.Click += new System.Windows.RoutedEventHandler(this.StopButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

