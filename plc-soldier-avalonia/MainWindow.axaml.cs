using Avalonia.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using plc_soldier_avalonia.Models;
using System.Diagnostics;
using Avalonia.Platform.Storage;
using System.Threading.Tasks;
using Avalonia.Input;
using SkiaSharp;

namespace plc_soldier_avalonia
{
    public partial class MainWindow : Window
    {
        // List of content for bottom space TabItems
        List<BottomTabItem> bottomItems = new List<BottomTabItem>()
        {
            new BottomTabItem(){Content = "�����-�� �����", Header = "������" },
            new BottomTabItem(){Content = "�����-�� �����", Header = "����� �����������" },
            new BottomTabItem(){Content = "�����-�� �����", Header = "��������" },
        };

        // List of content for left upper space TabItems
        List<LeftUpperTabItem> leftUpperItems = new List<LeftUpperTabItem>()
        {
            new LeftUpperTabItem(){Header = "���������� ����������", TreeViewContent = new ObservableCollection<Node> { new Node(@"C:\Users\T\source\repos\plc-soldier-wpf") } },
        };

        // List of content for left bottom space TabItems
        List<LeftBottomTabItem> leftBottomItems = new List<LeftBottomTabItem>()
        {
            new LeftBottomTabItem(){Header = "���������� ����������", Content = "�����-�� �����" },
        };

        // List of content for far right space TabItems
        List<FarRightTabItem> farRightItems = new List<FarRightTabItem>()
        {
            new FarRightTabItem(){Content = "�����-�� �����", Header = "��������" },
        };

        // List of content for central space TabItems
        List<CentralTabItem> centralItems = new List<CentralTabItem>()
        {
            new CentralTabItem(){Content = "�����-�� �����", Header = "������� �������" },
        };

        // A list containing bottom space Tabitems
        public ObservableCollection<BottomTabItem> BottomContent { get; set; }

        // A list containing left upper space Tabitems
        public ObservableCollection<LeftUpperTabItem> LeftUpperContent { get; set; }

        // A list containing left bottom space Tabitems
        public ObservableCollection<LeftBottomTabItem> LeftBottomContent { get; set; }

        // A list containing far right space Tabitems
        public ObservableCollection<FarRightTabItem> FarRightContent { get; set; }

        // A list containing central space Tabitems
        public ObservableCollection<CentralTabItem> CentralContent { get; set; }

        // Tooltips for the tool menu
        public ToolToolTip toolToolTips { get; set; }

        public MainWindow()
        {
            BottomContent = new ObservableCollection<BottomTabItem>();
            LeftUpperContent = new ObservableCollection<LeftUpperTabItem>();
            LeftBottomContent = new ObservableCollection<LeftBottomTabItem>();
            FarRightContent = new ObservableCollection<FarRightTabItem>();
            CentralContent = new ObservableCollection<CentralTabItem>();

            AddingTabItemsAtStartup(new List<LeftUpperTabItem>() { leftUpperItems[0] }, 
                                    new List<BottomTabItem>() { bottomItems[0], bottomItems[1], bottomItems[2] },
                                    new List<LeftBottomTabItem>() { leftBottomItems[0] },
                                    new List<FarRightTabItem>() { farRightItems[0] },
                                    new List<CentralTabItem>() { centralItems[0] });

            toolToolTips = new ToolToolTip()
            {
                TooltipA = "����������� ��������� 1",
                TooltipB = "����������� ��������� 2",
                TooltipC = "����������� ��������� 3",
                TooltipD = "����������� ��������� 4",
                TooltipE = "����������� ��������� 5",
                TooltipF = "����������� ��������� 6",
                TooltipG = "����������� ��������� 7",
                TooltipH = "����������� ��������� 8",
                TooltipI = "����������� ��������� 9"
            };

            InitializeComponent();

            BottomSpace.ItemsSource = BottomContent;
            LeftUpperSpace.ItemsSource = LeftUpperContent;
            LeftBottomSpace.ItemsSource = LeftBottomContent;
            FarRightSpace.ItemsSource = FarRightContent;
            CentralSpace.ItemsSource = CentralContent;

            ToolsMenu.DataContext = toolToolTips;

            // Adding Mouse click Event tracking
            this.AddHandler(PointerPressedEvent, MouseDownHandler, handledEventsToo: true);
        }

        // Adding TabItems to TabControl at the startup
        private void AddingTabItemsAtStartup(List<LeftUpperTabItem> leftUpperItemsStartup, List<BottomTabItem> bottomItemsStartup, 
                                             List<LeftBottomTabItem> leftBottomTabItemsStartup, List<FarRightTabItem> farRightTabItemsStartup,
                                             List<CentralTabItem> centralTabItemsStartup)
        {
            foreach (var item in leftUpperItemsStartup)
            { 
                LeftUpperContent.Add(item);
            }

            foreach (var item in bottomItemsStartup)
            {
                BottomContent.Add(item);
            }

            foreach (var item in leftBottomTabItemsStartup)
            {
                LeftBottomContent.Add(item);
            }

            foreach (var item in farRightTabItemsStartup)
            {
                FarRightContent.Add(item);
            }

            foreach (var item in centralTabItemsStartup)
            {
                CentralContent.Add(item);
            }
        }

        // Removing TabItem
        private void Delete_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is Button b)
            {
                if (b.DataContext is BottomTabItem bottomExample) // Removing BottomTabItem
                {
                    BottomContent.Remove(bottomItems[0]);
                    BottomContent.Remove(bottomItems[1]);
                    BottomContent.Remove(bottomItems[2]);

                    if (BottomContent.Count == 0 ) 
                    {
                        /* 
                            Checking for an attempt to write values of 1 Star or 0 Pixel to TabStatus.
                            It is necessary to record only the initial pixel values.
                        */
                        if (CRB_Grid.RowDefinitions[2].Height != new GridLength(1, GridUnitType.Star) && CRB_Grid.RowDefinitions[2].Height != new GridLength(0, GridUnitType.Pixel))
                            TabStatus.gridLengths["Bottom_Height"] = CRB_Grid.RowDefinitions[2].Height;

                        if ((CentralContent.Count > 0)||(FarRightContent.Count > 0))  
                        {
                            CRB_Grid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                            CRB_Grid.RowDefinitions[2].Height = new GridLength(0, GridUnitType.Pixel);

                            CRB_Splitter.IsVisible = false;
                        }
                        else
                        {
                            /* 
                                Checking for an attempt to write values of 1 Star or 0 Pixel to TabStatus.
                                It is necessary to record only the initial pixel values.
                            */
                            if (LR_Grid.ColumnDefinitions[0].Width != new GridLength(1, GridUnitType.Star) && LR_Grid.ColumnDefinitions[0].Width != new GridLength(0, GridUnitType.Pixel))
                                TabStatus.gridLengths["LULB_Width"] = LR_Grid.ColumnDefinitions[0].Width;

                            LR_Grid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                            LR_Grid.ColumnDefinitions[2].Width = new GridLength(0, GridUnitType.Pixel);

                            LR_Splitter.IsVisible = false;
                        }
                    }
                } 
                else if (b.DataContext is LeftUpperTabItem leftExample) // Removing LeftUpperTabItem
                {
                    LeftUpperContent.Remove(leftExample);

                    if (LeftUpperContent.Count == 0)
                    {
                        /* 
                            Checking for an attempt to write values of 1 Star or 0 Pixel to TabStatus.
                            It is necessary to record only the initial pixel values.
                        */
                        if (LULB_Grid.RowDefinitions[2].Height != new GridLength(1, GridUnitType.Star) && LULB_Grid.RowDefinitions[2].Height != new GridLength(0, GridUnitType.Pixel))
                            TabStatus.gridLengths["LeftBottom_Height"] = LULB_Grid.RowDefinitions[2].Height;

                        if (LeftBottomContent.Count == 0)
                        {
                            /* 
                                Checking for an attempt to write values of 1 Star or 0 Pixel to TabStatus.
                                It is necessary to record only the initial pixel values.
                            */
                            if (LR_Grid.ColumnDefinitions[0].Width != new GridLength(1, GridUnitType.Star) && LR_Grid.ColumnDefinitions[0].Width != new GridLength(0, GridUnitType.Pixel))
                                TabStatus.gridLengths["LULB_Width"] = LR_Grid.ColumnDefinitions[0].Width;

                            LR_Grid.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Pixel);
                            LR_Grid.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);

                            LR_Splitter.IsVisible = false;
                        }
                        else
                        {
                            LULB_Grid.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Pixel);
                            LULB_Grid.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

                            LULB_Splitter.IsVisible = false;
                        }
                    }
                }
                else if (b.DataContext is LeftBottomTabItem leftBottomExample) // Removing LeftBottomTabItem
                {
                    LeftBottomContent.Remove(leftBottomExample);

                    if (LeftBottomContent.Count == 0)
                    {
                        /* 
                            Checking for an attempt to write values of 1 Star or 0 Pixel to TabStatus.
                            It is necessary to record only the initial pixel values.
                        */
                        if (LULB_Grid.RowDefinitions[2].Height != new GridLength(1, GridUnitType.Star) && LULB_Grid.RowDefinitions[2].Height != new GridLength(0, GridUnitType.Pixel))
                            TabStatus.gridLengths["LeftBottom_Height"] = LULB_Grid.RowDefinitions[2].Height;

                        if (LeftUpperContent.Count == 0)
                        {
                            /* 
                                Checking for an attempt to write values of 1 Star or 0 Pixel to TabStatus.
                                It is necessary to record only the initial pixel values.
                            */
                            if (LR_Grid.ColumnDefinitions[0].Width != new GridLength(1, GridUnitType.Star) && LR_Grid.ColumnDefinitions[0].Width != new GridLength(0, GridUnitType.Pixel))
                                TabStatus.gridLengths["LULB_Width"] = LR_Grid.ColumnDefinitions[0].Width;

                            LR_Grid.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Pixel);
                            LR_Grid.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);

                            LR_Splitter.IsVisible = false;
                        }
                        else
                        {
                            LULB_Grid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                            LULB_Grid.RowDefinitions[2].Height = new GridLength(0, GridUnitType.Pixel);

                            LULB_Splitter.IsVisible = false;
                        }
                    }  
                }
                else if (b.DataContext is FarRightTabItem farRightExample) // Removing FarRightTabItem
                {
                    FarRightContent.Remove(farRightExample);

                    if (FarRightContent.Count == 0)
                    {
                        /* 
                            Checking for an attempt to write values of 1 Star or 0 Pixel to TabStatus.
                            It is necessary to record only the initial pixel values.
                        */
                        if (CRR_Grid.ColumnDefinitions[2].Width != new GridLength(1, GridUnitType.Star) && CRR_Grid.ColumnDefinitions[2].Width != new GridLength(0, GridUnitType.Pixel))
                            TabStatus.gridLengths["FarRight_Width"] = CRR_Grid.ColumnDefinitions[2].Width;

                        if (BottomContent.Count == 0 && CentralContent.Count == 0)
                        {
                            /* 
                                Checking for an attempt to write values of 1 Star or 0 Pixel to TabStatus.
                                It is necessary to record only the initial pixel values.
                            */
                            if (LR_Grid.ColumnDefinitions[0].Width != new GridLength(1, GridUnitType.Star) && LR_Grid.ColumnDefinitions[0].Width != new GridLength(0, GridUnitType.Pixel))
                                TabStatus.gridLengths["LULB_Width"] = LR_Grid.ColumnDefinitions[0].Width;

                            LR_Grid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                            LR_Grid.ColumnDefinitions[2].Width = new GridLength(0, GridUnitType.Pixel);

                            LR_Splitter.IsVisible = false;
                        }
                        else if (CentralContent.Count == 0)
                        {
                            /* 
                                Checking for an attempt to write values of 1 Star or 0 Pixel to TabStatus.
                                It is necessary to record only the initial pixel values.
                            */
                            if (CRB_Grid.RowDefinitions[2].Height != new GridLength(1, GridUnitType.Star) && CRB_Grid.RowDefinitions[2].Height != new GridLength(0, GridUnitType.Pixel))
                                TabStatus.gridLengths["Bottom_Height"] = CRB_Grid.RowDefinitions[2].Height;

                            CRB_Grid.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Pixel);
                            CRB_Grid.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

                            CRB_Splitter.IsVisible = false;
                        }
                        else
                        {
                            CRR_Grid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                            CRR_Grid.ColumnDefinitions[2].Width = new GridLength(0, GridUnitType.Pixel);

                            CRR_Splitter.IsVisible = false;
                        }    
                    }
                }
                else if (b.DataContext is CentralTabItem centralExample) // Removing CentralTabItem
                {
                    CentralContent.Remove(centralExample);

                    if (CentralContent.Count == 0)
                    {
                        /* 
                            Checking for an attempt to write values of 1 Star or 0 Pixel to TabStatus.
                            It is necessary to record only the initial pixel values.
                        */
                        if (CRR_Grid.ColumnDefinitions[0].Width != new GridLength(1, GridUnitType.Star) && CRR_Grid.ColumnDefinitions[0].Width != new GridLength(0, GridUnitType.Pixel))
                            TabStatus.gridLengths["RightRight_Width"] = CRR_Grid.ColumnDefinitions[0].Width;

                        if (BottomContent.Count == 0 && FarRightContent.Count == 0)
                        {
                            /* 
                                Checking for an attempt to write values of 1 Star or 0 Pixel to TabStatus.
                                It is necessary to record only the initial pixel values.
                            */
                            if (LR_Grid.ColumnDefinitions[0].Width != new GridLength(1, GridUnitType.Star) && LR_Grid.ColumnDefinitions[0].Width != new GridLength(0, GridUnitType.Pixel))
                                TabStatus.gridLengths["LULB_Width"] = LR_Grid.ColumnDefinitions[0].Width;

                            LR_Grid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                            LR_Grid.ColumnDefinitions[2].Width = new GridLength(0, GridUnitType.Pixel);

                            LR_Splitter.IsVisible = false;
                        }
                        else if (FarRightContent.Count == 0)
                        {
                            /* 
                                Checking for an attempt to write values of 1 Star or 0 Pixel to TabStatus.
                                It is necessary to record only the initial pixel values.
                            */
                            if (CRB_Grid.RowDefinitions[2].Height != new GridLength(1, GridUnitType.Star) && CRB_Grid.RowDefinitions[2].Height != new GridLength(0, GridUnitType.Pixel))
                                TabStatus.gridLengths["Bottom_Height"] = CRB_Grid.RowDefinitions[2].Height;

                            CRB_Grid.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Pixel);
                            CRB_Grid.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

                            CRB_Splitter.IsVisible = false;
                        }
                        else
                        {
                            CRR_Grid.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Pixel);
                            CRR_Grid.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);

                            CRR_Splitter.IsVisible = false;
                        }
                    }
                }
            }
        }

        // Creating the Logical organizer TabItem
        private void LogicalOrganizer_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            LeftUpperExpansion();
            LULBExpansion();

            if (!LeftUpperContent.Contains(leftUpperItems[0])) LeftUpperContent.Add(leftUpperItems[0]);
        }

        // Creating the Control organizer TabItem
        private void HardwareOrganizer_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            LeftBottomExpansion();
            LULBExpansion();

            if (!LeftBottomContent.Contains(leftBottomItems[0])) LeftBottomContent.Add(leftBottomItems[0]);
        }

        // Creating the Errors TabItem
        private void Errors_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            BottomExpansion();
            CRBExpansion();

            if (!BottomContent.Contains(bottomItems[0])) BottomContent.Add(bottomItems[0]);
            if (!BottomContent.Contains(bottomItems[1])) BottomContent.Add(bottomItems[1]);
            if (!BottomContent.Contains(bottomItems[2])) BottomContent.Add(bottomItems[2]);
        }

        // Creating the Search results TabItem
        private void SearchResults_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            BottomExpansion();
            CRBExpansion();

            if (!BottomContent.Contains(bottomItems[0])) BottomContent.Add(bottomItems[0]);
            if (!BottomContent.Contains(bottomItems[1])) BottomContent.Add(bottomItems[1]);
            if (!BottomContent.Contains(bottomItems[2])) BottomContent.Add(bottomItems[2]);
        }

        // Creating the Watch TabItem
        private void Watch_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            BottomExpansion();
            CRBExpansion();

            if (!BottomContent.Contains(bottomItems[0])) BottomContent.Add(bottomItems[0]);
            if (!BottomContent.Contains(bottomItems[1])) BottomContent.Add(bottomItems[1]);
            if (!BottomContent.Contains(bottomItems[2])) BottomContent.Add(bottomItems[2]);               
        }

        // Creating the Workpace TabItem
        private void Work_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            CRBExpansion();
            CRRExpansion();
            CentralExpansion();

            if (!CentralContent.Contains(centralItems[0])) CentralContent.Add(centralItems[0]);
        }

        // Creating the Property TabItem
        private void Property_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            FarRightExpansion();
            CRRExpansion();
            CRBExpansion();

            if (!FarRightContent.Contains(farRightItems[0])) FarRightContent.Add(farRightItems[0]);
        }

        public void BottomExpansion()
        {
            if (BottomContent.Count == 0) 
            {
                if ((CentralContent.Count == 0) && (FarRightContent.Count == 0))
                {
                    CRB_Grid.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Pixel);
                    CRB_Grid.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                }
                else
                {
                    CRB_Grid.RowDefinitions[2].Height = TabStatus.gridLengths["Bottom_Height"];
                    CRB_Grid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);

                    CRB_Splitter.IsVisible = true;
                }
            }
        }

        public void CRBExpansion() 
        {
            if (CentralContent.Count == 0 && FarRightContent.Count == 0 && BottomContent.Count == 0) 
            { 
                if (LeftUpperContent.Count == 0 && LeftBottomContent.Count == 0)
                {
                    LR_Grid.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);
                    LR_Grid.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Pixel);
                }
                else
                {
                    LR_Grid.ColumnDefinitions[0].Width = TabStatus.gridLengths["LULB_Width"];
                    LR_Grid.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);

                    LR_Splitter.IsVisible = true;
                }
            }
        }

        public void CentralExpansion()
        {
            if (CentralContent.Count == 0)
            {
                if (FarRightContent.Count == 0)
                {
                    CRR_Grid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                    CRR_Grid.ColumnDefinitions[2].Width = new GridLength(0, GridUnitType.Pixel);
                }
                else
                {
                    CRR_Grid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                    CRR_Grid.ColumnDefinitions[2].Width = TabStatus.gridLengths["FarRight_Width"];

                    CRR_Splitter.IsVisible = true;
                }
            }
        }

        public void LeftUpperExpansion()
        {
            if (LeftUpperContent.Count == 0)
            {
                if (LeftBottomContent.Count == 0)
                {
                    LULB_Grid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                    LULB_Grid.RowDefinitions[2].Height = new GridLength(0, GridUnitType.Pixel);
                }
                else
                {
                    LULB_Grid.RowDefinitions[2].Height = TabStatus.gridLengths["LeftBottom_Height"];
                    LULB_Grid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);

                    LULB_Splitter.IsVisible = true;
                }
            }
        }

        public void LeftBottomExpansion()
        {
            if (LeftBottomContent.Count == 0)
            {
                if (LeftUpperContent.Count == 0)
                {
                    LULB_Grid.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                    LULB_Grid.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Pixel);
                }
                else
                {
                    LULB_Grid.RowDefinitions[2].Height = TabStatus.gridLengths["LeftBottom_Height"];
                    LULB_Grid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);

                    LULB_Splitter.IsVisible = true;
                }
            }
        }

        public void LULBExpansion()
        {
            if (LeftUpperContent.Count == 0 && LeftBottomContent.Count == 0)
            {
                if (BottomContent.Count == 0 && CentralContent.Count == 0 && FarRightContent.Count == 0) 
                {
                    LR_Grid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                    LR_Grid.ColumnDefinitions[2].Width = new GridLength(0, GridUnitType.Pixel);
                }
                else
                {
                    LR_Grid.ColumnDefinitions[0].Width = TabStatus.gridLengths["LULB_Width"];
                    LR_Grid.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);

                    LR_Splitter.IsVisible = true;
                }
            }
        }

        public void FarRightExpansion()
        {
            if (FarRightContent.Count == 0)
            {
                if (CentralContent.Count == 0)
                {
                    CRR_Grid.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);
                    CRR_Grid.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Pixel);
                }
                else
                {
                    CRR_Grid.ColumnDefinitions[2].Width = TabStatus.gridLengths["FarRight_Width"];
                    CRR_Grid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);

                    CRR_Splitter.IsVisible = true;
                }
            }
        }

        public void CRRExpansion()
        {
            if (CentralContent.Count == 0 && FarRightContent.Count == 0)
            {
                if (BottomContent.Count == 0)
                {
                    CRB_Grid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                    CRB_Grid.RowDefinitions[2].Height = new GridLength(0, GridUnitType.Pixel);
                }
                else
                {
                    CRB_Grid.RowDefinitions[2].Height = TabStatus.gridLengths["Bottom_Height"];
                    CRB_Grid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);

                    CRB_Splitter.IsVisible = true;
                }
            }
        }

        // Translation of the application into the language depending on the input parameter
        public void ApplicationTranslation(string language)
        {
            int languageIndex = ApplicationLocalozation.GetLanguageIndex(language);

            this.Title = ApplicationLocalozation.ApplicationTitle[languageIndex];

            File_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["File"][languageIndex];
            NewProject_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["New project"][languageIndex];
            OpenProject_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["Open project"][languageIndex];
            Settings_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["Settings"][languageIndex];
            Language_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["Language"][languageIndex];
            Russian_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["Russian"][languageIndex];
            English_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["English"][languageIndex];
            Exit_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["Exit"][languageIndex];
            Edit_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["Edit"][languageIndex];
            View_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["View"][languageIndex];
            LogicalOrganizer_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["Logical organizer"][languageIndex];
            HardwareOrganizer_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["Hardware organizer"][languageIndex];
            Errors_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["Errors"][languageIndex];
            SearchResults_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["Search results"][languageIndex];
            Watch_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["Watch"][languageIndex];
            WorkSpace_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["Workspace"][languageIndex];
            Property_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["Property"][languageIndex];
            Search_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["Search"][languageIndex];
            Logic_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["Logic"][languageIndex];
            Communications_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["Communications"][languageIndex];
            Tools_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["Tools"][languageIndex];
            Window_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["Window"][languageIndex];
            Help_MenuItem.Header = ApplicationLocalozation.TopMenuLanguages["Help"][languageIndex];

            bottomItems[0].Header = ApplicationLocalozation.BottomItemsLanguages[0][languageIndex].Header;
            bottomItems[0].Content = ApplicationLocalozation.BottomItemsLanguages[0][languageIndex].Content;
            bottomItems[1].Header = ApplicationLocalozation.BottomItemsLanguages[1][languageIndex].Header;
            bottomItems[1].Content = ApplicationLocalozation.BottomItemsLanguages[1][languageIndex].Content;
            bottomItems[2].Header = ApplicationLocalozation.BottomItemsLanguages[2][languageIndex].Header;
            bottomItems[2].Content = ApplicationLocalozation.BottomItemsLanguages[2][languageIndex].Content;

            leftUpperItems[0].Header = ApplicationLocalozation.LeftUpperItemsLanguages[0][languageIndex].Header;

            leftBottomItems[0].Header = ApplicationLocalozation.LeftBottomItemsLanguages[0][languageIndex].Header;
            leftBottomItems[0].Content = ApplicationLocalozation.LeftBottomItemsLanguages[0][languageIndex].Content;

            farRightItems[0].Header = ApplicationLocalozation.FarRightItemsLanguages[0][languageIndex].Header;
            farRightItems[0].Content = ApplicationLocalozation.FarRightItemsLanguages[0][languageIndex].Content;

            centralItems[0].Header = ApplicationLocalozation.CentralItemsLanguages[0][languageIndex].Header;
            centralItems[0].Content = ApplicationLocalozation.CentralItemsLanguages[0][languageIndex].Content;

            toolToolTips = ApplicationLocalozation.ToolToolTipLanguages[languageIndex];

            // Clearing and repopulating ItemsSource and DataContext. This is necessary to update the data.

            BottomSpace.ItemsSource = null;
            LeftUpperSpace.ItemsSource = null;
            LeftBottomSpace.ItemsSource = null;
            FarRightSpace.ItemsSource = null;
            CentralSpace.ItemsSource = null;

            BottomSpace.ItemsSource = BottomContent;
            LeftUpperSpace.ItemsSource = LeftUpperContent;
            LeftBottomSpace.ItemsSource = LeftBottomContent;
            FarRightSpace.ItemsSource = FarRightContent;
            CentralSpace.ItemsSource = CentralContent;

            ToolsMenu.DataContext = null;

            ToolsMenu.DataContext = toolToolTips;
        }

        // Translation of the application into Russian
        private void Russian_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ApplicationTranslation("russian");
        }

        // Translation of the application into English
        private void English_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ApplicationTranslation("english");
        }

        // Opening the project folder and displaying it in the logical organizer
        private async void OpenProject_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var dialog = new OpenFolderDialog();

            var result = await dialog.ShowAsync(this);

            if (result != null)
            {
                leftUpperItems[0].TreeViewContent = new ObservableCollection<Node> { new Node(result) };

                LeftUpperSpace.ItemsSource = null;

                LeftUpperSpace.ItemsSource = LeftUpperContent;
            }
        }

        // The event of clicking down the mouse button
        private void MouseDownHandler(object sender, PointerPressedEventArgs e)
        {
            var point = e.GetCurrentPoint(sender as Control);

            var xCursor = point.Position.X;
            var yCursor = point.Position.Y;

            var yTop_LeftUpperSpace = Main_Grid.RowDefinitions[0].Height.Value + Main_Grid.RowDefinitions[1].Height.Value + Main_Grid.RowDefinitions[2].Height.Value;
            var yBottom_LeftUpperSpace = this.Height - LULB_Grid.RowDefinitions[1].Height.Value - LULB_Grid.RowDefinitions[2].Height.Value;
            var xRight_LeftUpperSpace = LR_Grid.ColumnDefinitions[0].Width.Value;

            if (xCursor <= xRight_LeftUpperSpace && yCursor >= yTop_LeftUpperSpace && yCursor <= yBottom_LeftUpperSpace)
            {
                // functional
            }

            // var k = point.Pointer.Captured;

            // var i = LeftUpperSpace.DataContext as ObservableCollection<Node>;
        }

        private void Exit_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            BottomSpace.ItemsSource = null;
            LeftUpperSpace.ItemsSource = null;
            LeftBottomSpace.ItemsSource = null;
            FarRightSpace.ItemsSource = null;
            CentralSpace.ItemsSource = null;

            this.Close();
        }
    }
}